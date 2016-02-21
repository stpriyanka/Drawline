using System;
using System.Drawing;
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

		private Point _setPointA, _setPointB;

		protected override void OnMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				_setPointB = new Point(e.X, e.Y);
			else
				_setPointA = new Point(e.X, e.Y);
			Refresh();
			base.OnMouseClick(e);
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.DrawLine(SystemPens.ControlDarkDark, _setPointA, _setPointB);
			base.OnPaint(e);
		}
	}

}
