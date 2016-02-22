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

		//private Point _setPointA, _setPointB;
		private static IList<Point> CurrentPointsList = new List<Point>();
		private Dictionary<Point, Point> PointDictionary = new Dictionary<Point, Point>();

		public Form1()
		{
			PointDictionary.Add(new Point { X = 10, Y = 10 }, new Point { X = 85, Y = 85 });

		}
		protected override void OnMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (!CurrentPointsList.Any())
				{
					//add selected point to current points list
					AddPointsToCurrentList(e.Location);
				}
				else
				{
					//add selected point to current points list
					AddPointsToCurrentList(e.Location);
					//check if not intersects
					if (!IsIntersects(CurrentPointsList[0], CurrentPointsList[1]))
					{
						PointDictionary.Add(CurrentPointsList[0], CurrentPointsList[1]);
						Refresh();
					}
					else
					{
						CurrentPointsList.Clear();
					}
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
			CurrentPointsList.Clear();
		}

		private void AddPointsToCurrentList(Point PointClicked)
		{
			CurrentPointsList.Add(PointClicked);
		}



		public bool IsIntersects(Point a, Point b)
		{

			List<Point> allPointsList = GetAllPointsForPreviousDrawnLines();


			//checking if any point of the list is on the given line

			var m1 = GetSlopOfTheLine(a.X, a.Y, b.X, b.Y);
			int uppervalue, lowervalue;
			//if (a.X>=b.X)
			//{
			//	u
			//}
			
			for (int j = a.X; j < b.X; j++)
			{
				var point = new Point() { X = j, Y = Convert.ToInt32(m1 * j) };
				if (allPointsList.Contains(point))
				{
					return true;
				}

			}

			for (int j = a.Y; j < b.Y; j++)
			{
				var point = new Point() { X = j, Y = Convert.ToInt32(m1 * j) };
				if (allPointsList.Contains(point))
				{
					return true;
				}

			}
			return false;
		}

		private double GetSlopOfTheLine(int x1, int y1, int x2, int y2)
		{
			if ((x2 - x1) == 0) return 0;
			return (double)(y2 - y1)/(x2 - x1);
		}

		private List<Point> GetAllPointsForPreviousDrawnLines()
		{
			var pointlist = new List<Point>();
			int x1, y1, x2, y2;
			x1 = 10;
			y1 = 10;
			x2 = 85;
			y2 = 85;

			var m = GetSlopOfTheLine(x1, y1, x2, y2);
			for (int i = x1; i < x2; i++)
			{

				pointlist.Add(new Point
				{
					X = i,
					Y = Convert.ToInt32(m * i)
				});
			}

			return pointlist;
		}
	}

}
