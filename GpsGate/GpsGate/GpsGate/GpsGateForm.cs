using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GpsGate
{
	public partial class GpsGateForm : Form, IPointLists
	{

		public IList<Point> CurrentPointsList { get; set; }
		public Dictionary<Point, Point> PointDictionary { get; set; }
		public Point Start { get; set; }
		public Point End { get; set; }
		public GpsGateForm()
		{
			InitializeComponent();
			CurrentPointsList = new List<Point>();
			PointDictionary = new Dictionary<Point, Point>();
		}

		private void OnMouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				AddPointToCurrentList(e.Location);
			}
			//base.OnMouseClick(e);
		}
		private void AddPointToCurrentList(Point pointClicked)
		{
			if (!CurrentPointsList.Any())
			{
				CurrentPointsList.Add(pointClicked);
			}
			else
			{
				CurrentPointsList.Add(pointClicked);
				CurrentPointsList = CurrentPointsList.OrderByDescending(r => r.X).ToList();
				DrawCurrentLine();
			}
		}
		private void DrawCurrentLine()
		{

			if (!IsCurrentlineIntersects())
			{
				if (CurrentPointsList.Count == 2)
				{
					PointDictionary.Add(CurrentPointsList[0], CurrentPointsList[1]);
				}
				Refresh();
			}
			else
			{
				CurrentPointsList.Clear();
			}
		}
		private bool IsCurrentlineIntersects()
		{
			Start = CurrentPointsList[0];
			End = CurrentPointsList[1];
			foreach (var line in PointDictionary.OrderByDescending(r => r.Key.X))
			{
				var calculation = new Calculation(Start, End, line.Key, line.Value);
				if (calculation.IsIntersects())
				{
					return true;
				}

			}
			return false;
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			foreach (var p in PointDictionary.OrderByDescending(r => r.Key.X))
			{
				e.Graphics.DrawLine(SystemPens.ControlDarkDark, p.Key, p.Value);
			}
			CurrentPointsList.Clear();
			base.OnPaint(e);
		}

	}
}
