using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GpsGate
{
	public partial class GpsGate : Form
	{
		bool isDrawing;
		// our collection of strokes for drawing
		readonly List<List<Point>> _strokes = new List<List<Point>>();
		readonly List<List<Point>> _oldstrokes = new List<List<Point>>();

		// the current stroke being drawn
		List<Point> _currStroke;
		// our pen
		Pen _pen = new Pen(Color.Red, 2);

		public GpsGate()
		{
			InitializeComponent();
		}

		public void GpsGate_Load(object sender, MouseEventArgs e)
		{
		}

		private void GpsGate_MouseDown(object sender, MouseEventArgs e)
		{
			isDrawing = true;
			// mouse is down, starting new stroke
			_currStroke = new List<Point>();
			// add the initial point to the new stroke
			_currStroke.Add(e.Location);
			// add the new stroke collection to our strokes collection
			_strokes.Add(_currStroke);
		}

		private void GpsGate_MouseMove(object sender, MouseEventArgs e)
		{
			if (isDrawing)
			{
				// record stroke point if we're in drawing mode
				_currStroke.Add(e.Location);
				Refresh(); // refresh the drawing to see the latest section
			}
		}

		private void GpsGate_MouseUp(object sender, MouseEventArgs e)
		{
			isDrawing = false;
			//add data to old list
		}

		private void GpsGate_Paint(object sender, PaintEventArgs e)
		{
			// now handle and redraw our strokes on the paint event
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			foreach (List<Point> stroke in _strokes.Where(x => x.Count > 1))
				//check old list here
				if (IsStrokesPresent(stroke))
				{
					{
						//_oldstrokes.Add(stroke);
						e.Graphics.DrawLines(_pen, stroke.ToArray());
					}
				}
		}

		public bool IsStrokesPresent(List<Point> strokesToCheck)
		{
			foreach (var p in _oldstrokes)
			{
				if (p==strokesToCheck)
				{
					return false;
				}
			}
			return true;
		}

		private void GpsGate_Load(object sender, EventArgs e)
		{

		}

	}
}
