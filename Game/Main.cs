using System;
using System.Collections.Generic;
using ReadKey = Game.Control.Keyboard.ReadKey;
using HotkeysCreator = Game.Control.Keyboard.Hotkeys.Creator;
using HotkeysDetector = Game.Control.Keyboard.Hotkeys.Detector;
using SettingsManager = Game.Settings.Manager;
using GameItem = Game.Board.Item;
using GamePuzzle = Game.Board.Puzzle;
using GameGraphicsReader = Game.Board.Graphics.Reader;
using EngineAI  = Engine.AI;

namespace Game
{
	/// <summary>
	/// Description of Main.
	/// </summary>
	public class Main
	{
		static bool _started = false;
		public static SettingsManager _settings = new SettingsManager(new Dictionary<string, string>()
		                                                              {
		                                                              	{"Depth","6"},
		                                                              	{"Color","W"},
		                                                              	{"Play","S"},
		                                                              	{"Stop","Q"}
		                                                              });
		public static HotkeysCreator _playHK = new HotkeysCreator(_settings.GetSettings("Play"));
		public static HotkeysCreator _stopHK = new HotkeysCreator(_settings.GetSettings("Stop"));
		public static ReadKey _depthRK = new ReadKey();
		public static ReadKey _colorRK = new ReadKey();
		public static GameGraphicsReader _ggReader = new GameGraphicsReader();
		public static EngineAI _ai = new EngineAI();
		public static void init()
		{
			Console.SetWindowSize(Math.Min(150, Console.LargestWindowWidth),Math.Min(60, Console.LargestWindowHeight));
			for (int x = 0; x < 8; x++){
				for (int y = 0; y < 8; y++){
					GamePuzzle.items[x,y] = new GameItem();
					GamePuzzle._items[x,y] = 0;
				}
			}
			_ai.mark = _settings.GetSettings("Color") == "W" ? 1 : 2;
			_ai.depth = Convert.ToInt32(_settings.GetSettings("Depth"));
			_playHK.OnDetected += start;
			_stopHK.OnDetected += stop;
			_depthRK.OnSuccess += () => _ai.depth = _depthRK.GetNumber();
			_colorRK.OnSuccess += () => _ai.mark = _colorRK.GetText() == "W" ? 1 : 2;
		}
		static void start()
		{
			Console.Clear();
			Console.WriteLine("Started\n");
			_started = true;
			asyncPlay();
		}
		static void stop()
		{
			Console.Clear();
			Console.WriteLine("Stopped\n");
			_started = false;
		}
		static void asyncPlay(){
			if (!_started){
				//stop async
			}
			if (_ggReader.Update()){
				GamePuzzle.TestPrint();
				//GamePuzzle.TestCursor();
				//_ai.mark = 2;
				var play = _ai.play(GamePuzzle._items);
				var state = play.Item1;
				if (state == 0){
					_started = false;
					//stop async
					Console.WriteLine("No Move");
					return;
				}
				if (state == 1){
					var move = play.Item2;
					GameItem item = GamePuzzle.items[move.X,move.Y];
					Control.Mouse.LeftClick(item.bx, item.by);
					Console.WriteLine("Move " + move.X.ToString() + " " + move.Y.ToString());
				}
			}
		}
	}
}
