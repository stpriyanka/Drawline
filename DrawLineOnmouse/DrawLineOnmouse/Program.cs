using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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

		private static IList<Point> CurrentPointsList = new List<Point>();
		public Dictionary<Point, Point> PointDictionary = new Dictionary<Point, Point>();

		protected override void OnMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (!CurrentPointsList.Any())
				{
					//add selected point to current points list
					AddPointToCurrentList(e.Location);
				}
				else
				{
					//add selected point to current points list and try to draw the line
					AddPointToCurrentList(e.Location);
					var x = CurrentPointsList.OrderBy(r => r.X).ToArray();
					//check if not intersects
					if (!IsIntersects(x[0], x[1]))
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
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

			foreach (var v in PointDictionary)
			{
				e.Graphics.DrawLine(SystemPens.ControlDarkDark, v.Key, v.Value);
			}

			base.OnPaint(e);
			CurrentPointsList.Clear();
		}
		private void AddPointToCurrentList(Point pointClicked)
		{
			CurrentPointsList.Add(pointClicked);
		}
		public bool IsIntersects(Point a, Point b)
		{
			List<Point> pointListsForDrawnLine = new List<Point>();
			
			bool isintersects = false;
			foreach (var line in PointDictionary)
			{
				int x1, y1, x2, y2;
				double m, c;
				if (Math.Abs(line.Key.X - line.Value.X) >= Math.Abs(b.X - a.X))
				{
					x1 = line.Key.X;
					y1 = line.Key.Y;
					x2 = line.Value.X;
					y2 = line.Value.Y;

					if (IsLinesParallel(line.Key, line.Value, a, b))
					{
						return false;
					}
					pointListsForDrawnLine = GetAllPointsForDrawnLines(a, b);
				}
				else
				{
					x1 = a.X;
					y1 = a.Y;
					x2 = b.X;
					y2 = b.Y;
					if (IsLinesParallel(line.Key, line.Value, a, b))
					{
						return false;
					}
					pointListsForDrawnLine = GetAllPointsForDrawnLines(line.Key, line.Value);
				}

				if (x1 < x2 && y1 < y2)
				{
					isintersects = Sorting(x2, x1, y2, y1, pointListsForDrawnLine);
				}
				else if (x1 > x2 && y1 < y2)
				{
					isintersects = Sorting(x1, x2, y2, y1, pointListsForDrawnLine);
				}
				else if (x1 < x2 && y2 < y1)
				{
					isintersects = Sorting(x2, x1, y1, y2, pointListsForDrawnLine);

				}
				else if (x1 > x2 && y2 < y1)
				{
					isintersects = Sorting(x1, x2, y1, y2, pointListsForDrawnLine);

				}
				}

			return isintersects;
		}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
		public double[] GetLineEquationConstants(Point a, Point b)
		{
			var constantArray = new double[2];

			// following y=mx+c equation where m=(change in y)/(change in x) and c=y-mx for any point on the line
			double m = (double)(a.Y - b.Y) / (double)(a.X - b.X);
			constantArray[0] = m;
			double c = Convert.ToDouble(a.Y - (m * Convert.ToDouble(a.X)));
			constantArray[1] = c;
			return constantArray;
		}
		public List<Point> GetAllPointsForDrawnLines(Point p, Point q)
		{
			var pointlist = new List<Point>();
			var lineConstantValues = GetLineEquationConstants(new Point { X = p.X, Y = p.Y }, new Point { X = q.X, Y = q.Y });
			var m = lineConstantValues[0];
			var c = lineConstantValues[1];
			if (p.X < q.X)
			{
				for (int i = p.X; i <= q.X; i++)
				{
					var y = m * i + c;
					int y1;
					try
					{
						y1 = Convert.ToInt32(y);
					}
					catch (Exception e)
					{
						y1 = 0;
					}
					pointlist.Add(new Point
					{
						X = i,
						Y = y1
					});
				}

			}
			else
			{
				for (int i = q.X; i <= p.X; i++)
				{
					var y = m * i + c;
					int y1;
					try
					{
						y1 = Convert.ToInt32(y);
					}
					catch (Exception e)
					{
						y1 = 0;
					}
					pointlist.Add(new Point
					{
						X = i,
						Y = y1
					});
				}
			}
			return pointlist.OrderByDescending(r=>r.X).ToList();
		}
		private bool Sorting(int upperX, int lowerX, int upperY, int lowerY, List<Point> pointListsForDrawnLine)
		{
			for (int i = lowerX; i <= upperX; i++)
			{
				for (int j = lowerY; j <= upperY; j++)
				{
					if (pointListsForDrawnLine.Contains(new Point(i, j)))
					{
						return true;
					}
				}
			}
			return false;
		}
		private bool IsLinesParallel(Point a,Point b,Point p,Point q)
		{
			var constants1 = GetLineEquationConstants(a, b);
			var constants2 = GetLineEquationConstants(p, q);

			if (((double)constants1[0] - (double)constants2[0])==0)
			{
				return true;
			}
			return false;
		}
	}


}
