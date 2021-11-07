using System;

namespace Game.Board
{
	/// <summary>
	/// Description of Item.
	/// </summary>
	public class Item
	{
		public int bx = 0;
		public int by = 0;
		public int mark = 0;
		
		public void Set(int x, int y, int m)
		{
			bx = x;
			by = y;
			mark = m;
		}
		
		public override string ToString()
		{
			return mark.ToString();
		}
	}
}
