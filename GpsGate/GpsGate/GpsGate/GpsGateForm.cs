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
		private PointDictionaries _pointListsDepo;
		public static Point firstClickedPoint { get; set; }
		
		public GpsGateForm()
		{
			InitializeComponent();
			_pointListsDepo = new PointDictionaries();
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
			if (firstClickedPoint.X == 0 && firstClickedPoint.Y== 0)
			{
				firstClickedPoint = pointClicked;
			}
			else
			{
				if (_pointListsDepo != null)
				{
					_pointListsDepo.CurrentPointsDictionary.Add(firstClickedPoint,pointClicked);
					_pointListsDepo.CurrentPointsDictionary.OrderByDescending(r => r.Key.X).ToList();
				}
				DrawCurrentLine();
			}
		}
		private void DrawCurrentLine()
		{
			if (!IsCurrentlineIntersects())
			{
				if (_pointListsDepo.CurrentPointsDictionary.Count == 1)
				{
					_pointListsDepo.OldPointsDictionary.Add(_pointListsDepo.CurrentPointsDictionary.Keys.FirstOrDefault(),
						_pointListsDepo.CurrentPointsDictionary.Values.FirstOrDefault());
				}
				
				_pointListsDepo.CurrentPointsDictionary = new Dictionary<Point, Point>();
				firstClickedPoint = new Point();
				Refresh();
			}
			else
			{
				_pointListsDepo.CurrentPointsDictionary=new Dictionary<Point, Point>();
			}
		}
		private bool IsCurrentlineIntersects()
		{
			foreach (var p in _pointListsDepo.OldPointsDictionary.OrderByDescending(r => r.Key.X))
			{
				var calculation = new Calculation(_pointListsDepo.CurrentPointsDictionary.Keys.FirstOrDefault(), _pointListsDepo.CurrentPointsDictionary.Values.FirstOrDefault(), p.Key, p.Value);
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
			if (_pointListsDepo.OldPointsDictionary != null)
			{
				foreach (var p in _pointListsDepo.OldPointsDictionary.OrderByDescending(r => r.Key.X))
				{
					e.Graphics.DrawLine(SystemPens.ControlDarkDark, p.Key, p.Value);
				}
				_pointListsDepo.CurrentPointsDictionary=new Dictionary<Point, Point>();
			}
			base.OnPaint(e);
		}
	}
}
