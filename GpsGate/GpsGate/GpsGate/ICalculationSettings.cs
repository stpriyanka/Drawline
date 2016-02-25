using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpsGate
{
	//Line equation:  Ax+By=C
	//where: A = y2-y1, B = x1-x2, C = A*x1+B*y1
	interface ICalculationSettings
	{
		Point ClickedPoint1 { get; set; }
		Point ClickedPoint2 { get; set; }
		Point ExistingLinePoint1 { get; set; }
		Point ExistingLinePoint2 { get; set; }
		double A1 { get; set; }
		double B1 { get; set; }
		double C1 { get; set; }
		double A2 { get; set; }
		double B2 { get; set; }
		double C2 { get; set; }
		bool IsIntersects();

	}
}
