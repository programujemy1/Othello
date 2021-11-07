using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Game.Control.Keyboard.Hotkeys
{
	/// <summary>
	/// Description of Detector.
	/// </summary>
	public class Detector
	{
		bool _canDetect;
		public List<Keys> _keys;
		List<Keys> _dkeys;
		
		public event OnDetectedDelegate OnDetected;
		public delegate void OnDetectedDelegate();
		
		public Detector(List<Keys> keys = null)
		{
			_canDetect = keys != null ? true : false;
			_keys = new List<Keys>();
			_dkeys = new List<Keys>();
			if (keys != null) {
				foreach(var key in keys){
					_keys.Add(key);
				}
			}
			InterceptKeys.OnUp += OnUp;
			InterceptKeys.OnDown += OnDown;
		}
		
		public void Clear()
		{
			_canDetect = false;
			_keys.Clear();
			_dkeys.Clear();
		}
		
		public void New(List<Keys> keys)
		{
			_keys.Clear();
			_dkeys.Clear();
			foreach(var key in keys){
				_keys.Add(key);
			}
			_canDetect = true;
		}
		
		void OnUp(Keys key)
		{
			if (!_canDetect) return;
			if (_dkeys.Contains(key)){
				if (OnDetected != null && _keys.Contains(key)
				    && _keys.Count == _dkeys.Count){
					bool success = true;
					for(int i = 0; i < _keys.Count; i++){
						if (_keys[i] != _dkeys[i]){
							success = false;
							break;
						}
					}
					if (success){
						try{
							OnDetected();
						} catch {}
					}
				}
				_dkeys.Remove(key);
			}
		}
		
		void OnDown(Keys key)
		{
			if (!_canDetect) return;
			if (!_dkeys.Contains(key)){
				_dkeys.Add(key);
			}
		}
	}
}
