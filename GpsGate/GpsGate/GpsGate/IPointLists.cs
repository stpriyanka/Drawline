using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpsGate
{
	public interface IPointLists
	{
		IList<Point> CurrentPointsList { get; set; }
		Dictionary<Point, Point> PointDictionary { get; set; }
		Point Start { get; set; }
		Point End { get; set; }
	}
}
