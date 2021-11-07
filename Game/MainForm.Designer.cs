/*
 * Created by SharpDevelop.
 * User: Marcin
 * Date: 2021-11-07
 * Time: 14:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Game
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.PlayHotkeyChangeBttn = new System.Windows.Forms.Button();
			this.StopHotkeyChangeBttn = new System.Windows.Forms.Button();
			this.ColorBttn = new System.Windows.Forms.Button();
			this.DepthBttn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10, 45);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 26);
			this.label1.TabIndex = 2;
			this.label1.Text = "Depth\r\n";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(10, 10);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(39, 27);
			this.label2.TabIndex = 3;
			this.label2.Text = "Color\r\n";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(120, 10);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(33, 23);
			this.label3.TabIndex = 5;
			this.label3.Text = "Play";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(120, 45);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(33, 23);
			this.label4.TabIndex = 8;
			this.label4.Text = "Stop";
			// 
			// PlayHotkeyChangeBttn
			// 
			this.PlayHotkeyChangeBttn.BackColor = System.Drawing.SystemColors.Control;
			this.PlayHotkeyChangeBttn.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PlayHotkeyChangeBttn.Location = new System.Drawing.Point(160, 6);
			this.PlayHotkeyChangeBttn.Name = "PlayHotkeyChangeBttn";
			this.PlayHotkeyChangeBttn.Size = new System.Drawing.Size(134, 23);
			this.PlayHotkeyChangeBttn.TabIndex = 10;
			this.PlayHotkeyChangeBttn.Text = "S";
			this.PlayHotkeyChangeBttn.UseVisualStyleBackColor = false;
			this.PlayHotkeyChangeBttn.Click += new System.EventHandler(this.PlayHotkeyChangeBttnClick);
			// 
			// StopHotkeyChangeBttn
			// 
			this.StopHotkeyChangeBttn.BackColor = System.Drawing.SystemColors.Control;
			this.StopHotkeyChangeBttn.Cursor = System.Windows.Forms.Cursors.Hand;
			this.StopHotkeyChangeBttn.Location = new System.Drawing.Point(160, 39);
			this.StopHotkeyChangeBttn.Name = "StopHotkeyChangeBttn";
			this.StopHotkeyChangeBttn.Size = new System.Drawing.Size(134, 23);
			this.StopHotkeyChangeBttn.TabIndex = 11;
			this.StopHotkeyChangeBttn.Text = "Q";
			this.StopHotkeyChangeBttn.UseVisualStyleBackColor = false;
			this.StopHotkeyChangeBttn.Click += new System.EventHandler(this.StopHotkeyChangeBttnClick);
			// 
			// ColorBttn
			// 
			this.ColorBttn.BackColor = System.Drawing.SystemColors.Control;
			this.ColorBttn.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.ColorBttn.Location = new System.Drawing.Point(50, 6);
			this.ColorBttn.Name = "ColorBttn";
			this.ColorBttn.Size = new System.Drawing.Size(53, 23);
			this.ColorBttn.TabIndex = 12;
			this.ColorBttn.Text = "W";
			this.ColorBttn.UseVisualStyleBackColor = false;
			this.ColorBttn.Click += new System.EventHandler(this.ColorBttnClick);
			// 
			// DepthBttn
			// 
			this.DepthBttn.BackColor = System.Drawing.SystemColors.Control;
			this.DepthBttn.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.DepthBttn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.DepthBttn.Location = new System.Drawing.Point(50, 39);
			this.DepthBttn.Name = "DepthBttn";
			this.DepthBttn.Size = new System.Drawing.Size(53, 23);
			this.DepthBttn.TabIndex = 13;
			this.DepthBttn.Text = "6";
			this.DepthBttn.UseVisualStyleBackColor = false;
			this.DepthBttn.Click += new System.EventHandler(this.DepthBttnClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(314, 75);
			this.Controls.Add(this.DepthBttn);
			this.Controls.Add(this.ColorBttn);
			this.Controls.Add(this.StopHotkeyChangeBttn);
			this.Controls.Add(this.PlayHotkeyChangeBttn);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MainForm";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFormFormClosed);
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button DepthBttn;
		private System.Windows.Forms.Button ColorBttn;
		private System.Windows.Forms.Button StopHotkeyChangeBttn;
		private System.Windows.Forms.Button PlayHotkeyChangeBttn;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}
