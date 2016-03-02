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
		public PointDictionaries _pointListsDepo { get; set; }
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
			if (firstClickedPoint == default(Point))
			{
				firstClickedPoint = pointClicked;
			}
			else
			{
				if (_pointListsDepo != null)
				{
					_pointListsDepo.CurrentPointsDictionary.Add(firstClickedPoint, pointClicked);
					_pointListsDepo.CurrentPointsDictionary.OrderByDescending(r => r.Key.X).ToList();
				}
				DrawCurrentLine();
			}
		}
		public void DrawCurrentLine()
		{
			if (!IsCurrentlineIntersects())
			{
				foreach (var p in _pointListsDepo.CurrentPointsDictionary)
				{
					_pointListsDepo.OldPointsDictionary.Add(p.Key, p.Value);
				}
				firstClickedPoint = new Point();
				Refresh();
			}
			else
			{
				foreach (var p in _pointListsDepo.CurrentPointsDictionary)
				{
					foreach (var x in _pointListsDepo.CurrentPointsDictionary)
					{
						BreakLineIntoParts(x.Key, x.Value, p.Key, p.Value);
					}
				}
				firstClickedPoint = new Point();
				_pointListsDepo.CurrentPointsDictionary.Clear();
			}
		}
		private bool IsCurrentlineIntersects()
		{
			bool isintersect = false;
			foreach (var p in _pointListsDepo.OldPointsDictionary.OrderByDescending(r => r.Key.X))
			{
				var p1 = p;
				Task.Run(() =>
				{
					foreach (var currentpoint in _pointListsDepo.CurrentPointsDictionary)
					{
						var calculation = new Calculation(currentpoint.Key, currentpoint.Value, p1.Key, p1.Value);
						calculation.IsIntersects();
					}
				}).Wait();
			}
			return isintersect;
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
				_pointListsDepo.CurrentPointsDictionary.Clear();
			}
			base.OnPaint(e);
		}

		public void BreakLineIntoParts(Point a, Point b, Point p, Point q)
		{
			_pointListsDepo.CurrentPointsDictionary.Add(a, new Point(b.X + 1, b.Y));
			_pointListsDepo.CurrentPointsDictionary.Add(new Point(b.X + 1, b.Y), b);
			DrawCurrentLine();
		}
	}
}
