using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Engine
{
	/// <summary>
	/// Description of MultiThreading.
	/// </summary>
	public class MultiThreading
	{
		private List<Thread> _threads = new List<Thread>();
		
		public event OnDoneDelegate OnDone;
		public delegate void OnDoneDelegate();
		
		private bool _done = false;
		private readonly object _done_locker = new object();
		public bool IsDone(bool done = false){
			lock (_done_locker)
			{
				if (done){
					_done = true;
					if (OnDone != null){
						foreach(Delegate d in OnDone.GetInvocationList())
						{
							OnDoneDelegate action = (OnDoneDelegate)d;
							action();
							OnDone -= action;
						}
					}
				}
				return _done;
			}
		}
		
		public void New(){
			_done = false;
			_threads.Clear();
		}
		
		public void Add(MinMaxTask t){
			var tcopy = t;
			_threads.Add(new Thread(tcopy.StartThread));
		}
		
		public void Start()
		{
			foreach (var thread in _threads){thread.Start();}
			var waitall = new Thread(() => {foreach (Thread thread in _threads) {thread.Join();}IsDone(true);});
			waitall.Start();
		}
	}
}
