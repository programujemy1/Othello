using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Game.Control.Keyboard.Hotkeys
{
	/// <summary>
	/// Description of Creator.
	/// </summary>
	public class Creator
	{
		Detector _detector;
		
		public bool _created;
		public List<Keys> _keys;
		
		public event OnCreatedDelegate OnCreated;
		public delegate bool OnCreatedDelegate();
		
		public event OnDetectedDelegate OnDetected;
		public delegate void OnDetectedDelegate();
		
		public Creator(string hotkeysTxt)
		{
			_keys = new List<Keys>();
			string[] t = hotkeysTxt.Split(new string[] { " + " }, StringSplitOptions.None);
			bool success = true;
			foreach(var txt in t){
				Keys key;
				if (Keys.TryParse(txt, out key)){
					_keys.Add(key);
				}
				else{
					success = false;
				}
			}
			_detector = new Detector(success ? _keys : null);
			if (!success){
				_keys.Clear();
			}
			_detector.OnDetected += () => {
				if (OnDetected != null){
					OnDetected();
				}
			};
		}
		
		public void Restart()
		{
			_detector.Clear();
			_keys.Clear();
			_created = false;
			OnCreated += () => {
				InterceptKeys.OnUp -= OnUp;
				InterceptKeys.OnDown -= OnDown;
				return true;
			};
			InterceptKeys.OnUp += OnUp;
			InterceptKeys.OnDown += OnDown;
		}
		
		void OnUp(Keys key)
		{
			if (!_created && _keys.Contains(key)){
				_detector.New(_keys);
				foreach(Delegate d in OnCreated.GetInvocationList())
				{
					OnCreatedDelegate action = (OnCreatedDelegate)d;
					if (action()){
						OnCreated -= action;
					}
				}
				_created = true;
			}
		}
		
		void OnDown(Keys key)
		{
			if (!_created && !_keys.Contains(key))
			{
				_keys.Add(key);
			}
		}
	}
}
