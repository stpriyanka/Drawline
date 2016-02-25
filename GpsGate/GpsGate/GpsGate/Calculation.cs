using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpsGate
{
	public class Calculation : ICalculationSettings
	{
		public Point ClickedPoint1 { get; set; }
		public Point ClickedPoint2 { get; set; }
		public Point ExistingLinePoint1 { get; set; }
		public Point ExistingLinePoint2 { get; set; }
		public double A1 { get; set; }
		public double B1 { get; set; }
		public double C1 { get; set; }
		public double A2 { get; set; }
		public double B2 { get; set; }
		public double C2 { get; set; }

		public Calculation(Point clicked1, Point clicked2, Point existing1, Point existing2)
		{
			ClickedPoint1 = clicked1;
			ClickedPoint2 = clicked2;
			ExistingLinePoint1 = existing1;
			ExistingLinePoint2 = existing2;
			B1 = clicked1.X - clicked2.X;
			B2 = existing1.X - existing2.X;
			if (clicked1.Y < clicked2.Y)
			{
				A1 = clicked2.Y - clicked1.Y;
			}
			else
			{
				A1 = clicked1.Y - clicked2.Y;
			}
			C1 = A1 * clicked1.X + B1 * clicked1.Y;

			if (existing1.Y < existing2.Y)
			{
				A2 = existing2.Y - existing1.Y;

			}
			else
			{
				A2 = existing1.Y - existing2.Y;
			}
			C2 = A2 * existing1.X + B2 * existing1.Y;
		}

		public bool IsIntersects()
		{
			double det = A1 * B2 - A2 * B1;
			if (det.Equals(0))
			{
				return false;
			}
			else
			{
				var x = (B2 * C1 - B1 * C2) / det;
				var y = (A1 * C2 - A2 * C1) / det;
				for (double i = 0; i <= 1; i = i + .1)
				{
					var k = x + i;
					var linequation = C1 - (A1 * k) - (B1 * (y));
					if (linequation.Equals(0))
					{
						return true;
					}
				}
			}

			return false;
		}

	}
}
