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
	public partial class GpsGateForm : Form
	{
		private static IList<Point> CurrentPointsList = new List<Point>();
		public Dictionary<Point, Point> PointDictionary = new Dictionary<Point, Point>();

		public GpsGateForm()
		{
			InitializeComponent();
		}

		protected void OnMouseClick(object sender, MouseEventArgs e)
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
				CurrentPointsList = CurrentPointsList.OrderBy(r => r.X).ToList();
				DrawCurrentLine();
			}
		}

		private void DrawCurrentLine()
		{
			bool isIntersects = IsCurrentlineIntersects();
			if (!isIntersects)
			{
				PointDictionary.Add(CurrentPointsList[0],CurrentPointsList[1]);
				Refresh();
			}
			else
			{
				CurrentPointsList.Clear();
			}
		}


		public Point Start { get; private set; }
		public Point End { get; private set; }


		private bool IsCurrentlineIntersects()
		{
			//var constants = GetLineEquationConstants(CurrentPointsList[0], CurrentPointsList[1]);
			Start = CurrentPointsList[0];
			End = CurrentPointsList[1];

			foreach (var line in PointDictionary.OrderBy(r=>r.Key.X))
			{
				var lineEquation = new LineEquation(Start, End,line.Key, line.Value);
				if (lineEquation.GetIntersectionWithLine())
				{
					return true;
				}
				
			}
			return false;
		}

		
		

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			foreach (var p in PointDictionary)
			{
				e.Graphics.DrawLine(SystemPens.ControlDarkDark, p.Key, p.Value);
			}
			CurrentPointsList.Clear();
			base.OnPaint(e);
		}
		
	}
}
