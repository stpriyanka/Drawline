using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DrawLineOnmouse
{
	//public partial class Form1 : Form
	//{

	public partial class Form1 : Form
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}

		private Point _setPointA, _setPointB;
		private static List<Point> PointList = new List<Point>();
		private Dictionary<Point, Point> PointDictionary = new Dictionary<Point, Point>();

		public Form1()
		{
			PointDictionary.Add(new Point { X = 10, Y = 10 }, new Point { X = 85, Y = 85 });

		}
		protected override void OnMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (!PointList.Any())
				{
					_setPointA = e.Location;
					PointList.Add(_setPointA);
				}
				else
				{
					_setPointB = e.Location;
					PointList.Add(_setPointB);

					//check if not intersects

					if (_setPointA.X <= _setPointB.X)
					{
						PointDictionary.Add(_setPointA, _setPointB);
					}
					else
					{
						PointDictionary.Add(_setPointB, _setPointA);
					}
					Refresh();
				}
			}
			base.OnMouseClick(e);
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			foreach (var v in PointDictionary)
			{
				e.Graphics.DrawLine(SystemPens.ControlDarkDark, v.Key, v.Value);
			}

			base.OnPaint(e);
			PointList.Clear();
		}


		//public bool IsIntersects(Point a, Point b)
		//{
		//	int x1, y1, x2, y2;
		//	x1 = 10;
		//	y1 = 10;
		//	x2 = 85;
		//	y2 = 85;
		//}
	}

}
