﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GpsGate
{
	//Originally: http://www.artiom.pro/2013/06/c-find-intersections-of-two-line-by.html

	public class LineEquation
	{
		public Point Start { get; set; }
		public Point End { get; set; }
		public Point OtherLIneStart { get; set; }
		public Point OtherLIneEnd { get; set; }

		public int LengthOfClickedLine { get; set; }
		public int LengthOfPreviousLine { get; set; }

		//public double B1{ get; set; }
		//public double C1 { get; set; }
		//public double A2 { get; set; }
		//public double B2 { get; set; }
		//public double C2 { get; set; }
		public LineEquation(Point x1, Point y1, Point x2, Point y2)
		{
			Start = x1;
			End = y1;
			OtherLIneStart = x2;
			OtherLIneEnd = y2;
			LengthOfClickedLine = End.X - Start.X;
			LengthOfPreviousLine = OtherLIneEnd.X - OtherLIneStart.X;
		}


		public bool GetIntersectionWithLine()
		{
			var mClickedline = GetLineEquationConstants(Start, End)[0];
			var cClickedline = GetLineEquationConstants(Start, End)[1];
			var mPreviousline = GetLineEquationConstants(OtherLIneStart, OtherLIneEnd)[0];
			var cPreviousline = GetLineEquationConstants(OtherLIneStart, OtherLIneEnd)[1];
			var allPoints = new List<Point>();
			if (LengthOfClickedLine > LengthOfPreviousLine)
			{
				allPoints = GetAllPointsForDrawnLines(Start, End);

				if (allPoints.Select(p => p.Y - (mPreviousline * p.X) + cPreviousline).Any(result => (double)(result) == 0.0))
				{
					return true;
				}
				return allPoints.Select(p => p.X - (mPreviousline*p.Y) - cPreviousline).Any(result => Convert.ToInt32(result) == 0);
			}
			else
			{
				allPoints = GetAllPointsForDrawnLines(OtherLIneStart, OtherLIneEnd);
				if (allPoints.Select(p => p.Y - (mClickedline * p.X) + cClickedline).Any(result => Convert.ToInt32(result) == 0))
				{
					return true;
				}
				return allPoints.Select(p => p.X - (mClickedline*p.Y) - cClickedline).Any(result => Convert.ToInt32(result) == 0);
			}

		}

		private float[] GetLineEquationConstants(Point clickedPoint1, Point clickedPoint2)
		{
			var constantArray = new float[2];

			// following y=mx+c equation where m=(change in y)/(change in x) and c=y-mx for any point on the line
			var m = (float)(clickedPoint1.Y - clickedPoint2.Y) / (float)(clickedPoint1.X - clickedPoint2.X);
			constantArray[0] = m;
			var c = (float)(clickedPoint1.Y - (m * (float)(clickedPoint1.X)));
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
						y1 = 1;
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
						y1 = 1;
					}
					pointlist.Add(new Point
					{
						X = i,
						Y = y1
					});
				}
			}
			return pointlist.OrderByDescending(r => r.X).ToList();
		}

	}

}
