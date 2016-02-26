using System.Collections.Generic;
using System.Drawing;

namespace GpsGate
{
	public class PointDictionaries
	{
		public Dictionary<Point, Point> CurrentPointsDictionary { get; set; }
		public Dictionary<Point, Point> OldPointsDictionary { get; set; }

		public PointDictionaries()
		{
			CurrentPointsDictionary=new Dictionary<Point, Point>();
			OldPointsDictionary=new Dictionary<Point, Point>();

		}
	}
}
