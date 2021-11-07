using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace Game.Board
{
	/// <summary>
	/// Description of Puzzle.
	/// </summary>
	public class Puzzle
	{
		public static int[,] _items = new int[8,8];
		public static Item[,] items = new Item[8,8];
		
		public static void TestPrint()
		{
			string text = "";
			for (int y = 0; y < 8; y++){
				for (int x = 0; x < 8; x++){
					text += _items[x,y].ToString() + " ";
				}
				text += "\n";
			}
			Console.WriteLine(text);
		}
		
		public static void TestCursor()
		{
			for (int y = 0; y < 8; y++){
				for (int x = 0; x < 8; x++){
					Item item = items[x,y];
					Control.Mouse.SetCursorPos(item.bx, item.by);
					Thread.Sleep(100);
				}
			}
		}
	}
}