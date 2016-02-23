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

		//public Form1()
		//{
		//	PointDictionary.Add(new Point { X = 10, Y = 10 }, new Point { X = 85, Y = 85 });

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
			if (!CurrentPointsList.Any())
			{
				CurrentPointsList.Add(pointClicked);
			}
			else
			{
				var existingpoint = CurrentPointsList.FirstOrDefault();
				if (existingpoint.X > pointClicked.X)
				{
					CurrentPointsList.Clear();
					CurrentPointsList.Add(pointClicked);
					CurrentPointsList.Add(existingpoint);
				}
				else
				{
					CurrentPointsList.Add(pointClicked);
				}
				}
			//CurrentPointsList.Add(PointClicked);
		}



		public bool IsIntersects(Point a, Point b)
		{
			//List<Point> allPointsList = GetAllPointsForPreviousDrawnLines();
			int x1, y1, x2, y2;
			x1 = 10;
			y1 = 10;
			x2 = 85;
			y2 = 85;


			foreach (var line in PointDictionary)
			{

			
			var lineConstantValues = GetLineEquationConstants(line.Key,line.Value);
			var m = lineConstantValues[0];
			var c = lineConstantValues[1];
			//equation of existing line: y-mx-c=0 for all points on the line
			var pointListsForDrawnLine = GetAllPointsForDrawnLines(a, b);
			var lineConstantValues1 = GetLineEquationConstants(a, b);
			var m1 = lineConstantValues1[0];
			var c1 = lineConstantValues1[1];


			for (int i = a.X; i < b.X; i++)
			{
				if (((Convert.ToDouble(i) * m1 + c1) == (Convert.ToDouble(i) * m) + c))
				{
					return true;
				}
			}
			}

			//foreach (var p in pointListsForDrawnLine)
			//{
			//	var result = p.Y - (m*p.X) - c;
			//	if (result== 0)
			//	{
			//		return true;
			//	}
			//}

			//foreach (var p in pointListsForDrawnLine)
			//{
			//	var result = p.X - (m * p.Y) - c;
			//	if (result<=0)
			//	{
			//		return true;
			//	}
			//}
			return false;
		}

		private double[] GetLineEquationConstants(Point a, Point b)
		{
			var constantArray = new double[2];

			// following y=mx+c equation where m=(change in y)/(change in x) and c=y-mx for any point on the line

			var m =(a.Y - b.Y)/(a.X - b.X);
			constantArray[0] = m;
			var c = a.Y - (m*a.X);
			constantArray[1] = c;
			return constantArray;
		}

		
		private List<Point> GetAllPointsForDrawnLines(Point p,Point q)
		{
			var pointlist = new List<Point>();
			
			var lineConstantValues = GetLineEquationConstants(new Point{X =p.X,Y=p.Y},new Point{X=q.X,Y=q.Y});
			var m = lineConstantValues[0];
			var c = lineConstantValues[1];

			for (int i = p.X; i < q.X; i++)
			{
				var y = m*i + c;
				pointlist.Add(new Point
				{
					X = i,
					Y = Convert.ToInt32(y)
				});
			}

			return pointlist;
		}

	}

	
}
