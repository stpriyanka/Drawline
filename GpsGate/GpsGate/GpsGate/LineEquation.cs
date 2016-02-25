using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
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
		public double mClickedline { get; set; }
		public double cClickedline { get; set; }
		public double mPreviousline { get; set; }
		public double cPreviousline { get; set; }
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
			mClickedline = GetLineEquationConstants(Start, End)[0];
			cClickedline = GetLineEquationConstants(Start, End)[1];
			mPreviousline = GetLineEquationConstants(OtherLIneStart, OtherLIneEnd)[0];
			cPreviousline = GetLineEquationConstants(OtherLIneStart, OtherLIneEnd)[1];


			if (LengthOfClickedLine > LengthOfPreviousLine)
			{
				if (Start.Y > End.Y)
				{
					Sorting(End.X, Start.X, Start.Y, End.Y);
				}
				else
				{
					Sorting(End.X, Start.X, End.Y, Start.Y);

				}
				for (int i = Start.X; i < End.X; i++)
				{
					if (Start.Y < End.Y)
					{
						for (int j = Start.Y; j < End.Y; j++)
						{
							if (Math.Abs(((j - mClickedline * i - cClickedline) - (j - (mPreviousline * i) - cPreviousline))) < 0)
							{
								return true;
							}
						}
					}
					else
					{
						for (int j = End.Y; j < Start.Y; j++)
						{
							if (Math.Abs(((j - mClickedline * i - cClickedline) - (j - (mPreviousline * i) - cPreviousline))) < 0)
							{
								return true;
							}
						}
					}
				}
			}
			else
			{
				for (int i = OtherLIneStart.X; i < OtherLIneEnd.X; i++)
				{
					if (OtherLIneStart.Y < OtherLIneEnd.Y)
					{
						for (int j = OtherLIneStart.Y; j < OtherLIneEnd.Y; j++)
						{
							if (Math.Abs(((j - mClickedline * i - cClickedline) - (j - (mPreviousline * i) - cPreviousline))) < 0)
							{
								return true;
							}
						}
					}
					else
					{
						for (int j = OtherLIneEnd.Y; j < OtherLIneStart.Y; j++)
						{
							if (Math.Abs(((j - mClickedline * i - cClickedline) - (j - (mPreviousline * i) - cPreviousline))) < 0)
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		private double[] GetLineEquationConstants(Point clickedPoint1, Point clickedPoint2)
		{
			var constantArray = new Double[2];

			// following y=mx+c equation where m=(change in y)/(change in x) and c=y-mx for any point on the line
			var m = ((double)(clickedPoint1.Y - clickedPoint2.Y) / (double)(clickedPoint1.X - clickedPoint2.X));
			constantArray[0] = m;
			var c = clickedPoint1.Y - (double)((double)m * (clickedPoint1.X));
			constantArray[1] = c;
			return constantArray;
		}


		private bool Sorting(int upperX, int lowerX, int upperY, int lowerY)
		{
			for (int i = lowerX; i <= upperX; i++)
			{
				for (int j = lowerY; j <= upperY; j++)
				{
					if (((int)(j - (mClickedline * i) - cClickedline) - (int)(j - (mPreviousline * i) - cPreviousline)) == 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		private static double Convertdouble(double x)
		{
			var floatstring = x.ToString(CultureInfo.InvariantCulture);
			var firstpart = floatstring.Split('.').FirstOrDefault();
			var lastpart = floatstring.Split('.').Last();
			if (string.IsNullOrEmpty(lastpart))
			{
				lastpart = "00";
			}
			else
			{
				lastpart = lastpart.Substring(0, 1);
			}
			var newstring = string.Format("{0}.{1}", firstpart, lastpart);
			return double.Parse(newstring, CultureInfo.InvariantCulture);

		}



	}

}

