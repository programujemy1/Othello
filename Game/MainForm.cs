using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Keyboard = Game.Control.Keyboard;

namespace Game
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
            ThreadPool.SetMinThreads(400, 4);
			this.StartPosition = FormStartPosition.Manual;
			this.Location = Cursor.Position;
			Keyboard.InterceptKeys.OnProcessStart();
			ColorBttn.Text = Main._settings.GetSettings("Color");
			DepthBttn.Text = Main._settings.GetSettings("Depth");
			PlayHotkeyChangeBttn.Text = Main._settings.GetSettings("Play");
			StopHotkeyChangeBttn.Text = Main._settings.GetSettings("Stop");
			Main.init();
		}
		
		void MainFormFormClosed(object sender, FormClosedEventArgs e)
		{
			Keyboard.InterceptKeys.OnProcessClose();
		}
		
		void PlayHotkeyChangeBttnClick(object sender, EventArgs e)
		{
			using(HotkeysForm HF = new HotkeysForm())
			{
				Main._playHK.OnCreated += () => {
					HF.Close();
					PlayHotkeyChangeBttn.Text = String.Join(" + ", Main._playHK._keys.ToArray());
					Main._settings.SaveSettings("Play", PlayHotkeyChangeBttn.Text);
					return true;
				};
				Main._playHK.Restart();
				HF.StartPosition = FormStartPosition.Manual;
				Point MFL = MainForm.ActiveForm.Location;
				Point PFL = PlayHotkeyChangeBttn.Location;
				HF.Location = new Point(MFL.X + PFL.X + 25, MFL.Y + PFL.Y + 30);
				HF.ShowDialog();
			}
		}
		
		void StopHotkeyChangeBttnClick(object sender, EventArgs e)
		{
			using(HotkeysForm HF = new HotkeysForm())
			{
				Main._stopHK.OnCreated += () => {
					HF.Close();
					StopHotkeyChangeBttn.Text = String.Join(" + ", Main._stopHK._keys.ToArray());
					Main._settings.SaveSettings("Stop", StopHotkeyChangeBttn.Text);
					return true;
				};
				Main._stopHK.Restart();
				HF.StartPosition = FormStartPosition.Manual;
				Point MFL = MainForm.ActiveForm.Location;
				Point SFL = StopHotkeyChangeBttn.Location;
				HF.Location = new Point(MFL.X + SFL.X + 25, MFL.Y + SFL.Y + 30);
				HF.ShowDialog();
			}
		}
		
		void ColorBttnClick(object sender, EventArgs e)
		{
			using(KeyForm KF = new KeyForm())
			{
				KF.TitleLabel.Text += "W or B";
				Main._colorRK.OnRead += () => {
					Keys key = Main._colorRK._key;
					string keyText = key.ToString();
					if (keyText.ToLower() == "w" || keyText.ToLower() == "b")
					{
						KF.Close();
						Main._colorRK.Unsubscribe();
						ColorBttn.Text = keyText;
						Main._settings.SaveSettings("Color", ColorBttn.Text);
						return true;
					}
					return false;
				};
				Main._colorRK.Subscribe();
				KF.StartPosition = FormStartPosition.Manual;
				Point MFL = MainForm.ActiveForm.Location;
				Point SFL = ColorBttn.Location;
				KF.Location = new Point(MFL.X + SFL.X + 25, MFL.Y + SFL.Y + 30);
				KF.ShowDialog();
			}
		}
		
		void DepthBttnClick(object sender, EventArgs e)
		{
			using(KeyForm KF = new KeyForm())
			{
				KF.TitleLabel.Text += "1 - 9";
				Main._depthRK.OnRead += () => {
					int num = Main._depthRK.GetNumber();
					if (num > 0)
					{
						KF.Close();
						Main._depthRK.Unsubscribe();
						DepthBttn.Text = num.ToString();
						Main._settings.SaveSettings("Depth", DepthBttn.Text);
						return true;
					}
					return false;
				};
				Main._depthRK.Subscribe();
				KF.StartPosition = FormStartPosition.Manual;
				Point MFL = MainForm.ActiveForm.Location;
				Point SFL = DepthBttn.Location;
				KF.Location = new Point(MFL.X + SFL.X + 25, MFL.Y + SFL.Y + 30);
				KF.ShowDialog();
			}
		}
	}
}
