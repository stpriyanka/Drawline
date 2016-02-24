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
		public Dictionary<Point, Point> PointDictionary = new Dictionary<Point, Point>();


		//public Form1()
		//{
		//	PointDictionary.Add(new Point { X = 10, Y = 10 }, new Point { X = 35, Y = 35 });
		//	PointDictionary.Add(new Point { X = 20, Y = 25 }, new Point { X = 25, Y = 30 });
		//}
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
		private void AddPointToCurrentList(Point pointClicked)
		{

			CurrentPointsList.Add(pointClicked);
		}
		public bool IsIntersects(Point a, Point b)
		{
			//List<Point> allPointsList = GetAllPointsForPreviousDrawnLines();
			List<Point> pointListsForDrawnLine = new List<Point>();

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
					var constants = GetLineEquationConstants(line.Key,line.Value);
					m = constants[0];
					c = constants[1];

					pointListsForDrawnLine = GetAllPointsForDrawnLines(a, b);

					
				}
				else
				{
					x1 = a.X;
					y1 = a.Y;
					x2 = b.X;
					y2 = b.Y;
					var constants = GetLineEquationConstants(a,b);
					m = constants[0];
					c = constants[1];
					//pointListsForDrawnLine = GetAllPointsForDrawnLines(new Point(line.Key.X,line.Key.Y),new Point(line.Value.X,line.Value.Y));
					
					pointListsForDrawnLine = GetAllPointsForDrawnLines(line.Key, line.Value);
					
				}

				foreach (var p in pointListsForDrawnLine)
				{
					//
					var x = p.Y - p.X*m - c;
					if (Equals(x, 0.0))
					{
						return true;
					}
				}
				if (x1 < x2 && y1<y2)
				{
					for (int i = x1; i <= x2; i++)
					{
						for (int j = y1; j <= y2; j++)
						{
							if (pointListsForDrawnLine.Contains(new Point(i, j)))
							{
								return true;
							}
						}
					}

				}
				else if (x1 > x2 && y1 < y2)
				{
					for (int i = x2; i <= x1; i++)
					{
						for (int j = y1; j <= y2; j++)
						{
							if (pointListsForDrawnLine.Contains(new Point(i, j)))
							{
								return true;
							}
						}
					}
				}
				else if (x1 < x2 && y2 < y1)
				{
					for (int i = x1; i <= x2; i++)
					{
						for (int j = y2; j <= y1; j++)
						{
							if (pointListsForDrawnLine.Contains(new Point(i, j)))
							{
								return true;
							}
						}
					}
				}
				else if (x1 > x2 && y2 < y1)
				{
					for (int i = x2; i <= x1; i++)
					{
						for (int j = y2; j <= y1; j++)
						{
							if (pointListsForDrawnLine.Contains(new Point(i, j)))
							{
								return true;
							}
						}
					}
				}

				//var lineConstantValues = GetLineEquationConstants(new Point(x1,y1),new Point(x2,y2));
				//var m = lineConstantValues[0];
				//var c = lineConstantValues[1];
				//equation of existing line: y-mx-c=0 for all points on the line
				//var lineConstantValues1 = GetLineEquationConstants(a, b);
				//var m1 = lineConstantValues1[0];
				//var c1 = lineConstantValues1[1];

				
			}

			return false;
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
					pointlist.Add(new Point
					{
						X = i,
						Y = Convert.ToInt32(y)
					});
				}

			}
			else
			{
				for (int i = q.X; i <= p.X; i++)
				{
					var y = m * i + c;
					pointlist.Add(new Point
					{
						X = i,
						Y = Convert.ToInt32(y)
					});
				}
			}
			return pointlist;
		}

	}


}
