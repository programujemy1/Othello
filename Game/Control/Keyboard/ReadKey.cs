using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Game.Control.Keyboard
{
	/// <summary>
	/// Description of ReadKey.
	/// </summary>
	public class ReadKey
	{
		public Keys _key;
		List<Keys> _dkeys;
		
		public event OnReadDelegate OnRead;
		public delegate bool OnReadDelegate();
		
		public event OnSuccessDelegate OnSuccess;
		public delegate void OnSuccessDelegate();
		
		public ReadKey()
		{
			_dkeys = new List<Keys>();
		}
		
		public void Subscribe()
		{
			_dkeys.Clear();
			InterceptKeys.OnUp += OnUp;
			InterceptKeys.OnDown += OnDown;
		}
		
		public void Unsubscribe()
		{
			InterceptKeys.OnUp -= OnUp;
			InterceptKeys.OnDown -= OnDown;
		}
		
		public string GetText()
		{
			return _key.ToString();
		}
		
		public int GetNumber()
		{
			int num = -1;
			if (_key == Keys.D0 || _key == Keys.NumPad0) num = 0;
			else if (_key == Keys.D1 || _key == Keys.NumPad1) num = 1;
			else if (_key == Keys.D2 || _key == Keys.NumPad2) num = 2;
			else if (_key == Keys.D3 || _key == Keys.NumPad3) num = 3;
			else if (_key == Keys.D4 || _key == Keys.NumPad4) num = 4;
			else if (_key == Keys.D5 || _key == Keys.NumPad5) num = 5;
			else if (_key == Keys.D6 || _key == Keys.NumPad6) num = 6;
			else if (_key == Keys.D7 || _key == Keys.NumPad7) num = 7;
			else if (_key == Keys.D8 || _key == Keys.NumPad8) num = 8;
			else if (_key == Keys.D9 || _key == Keys.NumPad9) num = 9;
			return num;
		}
		
		void OnUp(Keys key)
		{
			if (_dkeys.Contains(key)){
				_dkeys.Remove(key);
				if (OnRead != null){
					_key = key;
					foreach(Delegate d in OnRead.GetInvocationList())
					{
						OnReadDelegate action = (OnReadDelegate)d;
						if (action()){
							if (OnSuccess != null){
								OnSuccess();
							}
							OnRead -= action;
						}
					}
				}
			}
		}
		
		void OnDown(Keys key)
		{
			if (!_dkeys.Contains(key)){
				_dkeys.Add(key);
			}
		}
	}
}
