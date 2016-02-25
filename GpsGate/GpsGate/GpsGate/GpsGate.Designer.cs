namespace GpsGate
{
	partial class GpsGate
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// GpsGate
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(282, 253);
			this.Name = "GpsGate";
			this.Text = "GpsGate";
			this.Load += new System.EventHandler(this.GpsGate_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.GpsGate_Paint);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GpsGate_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GpsGate_MouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GpsGate_MouseUp);
			this.ResumeLayout(false);

		}

		#endregion
	}
}

