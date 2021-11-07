using System;

namespace Game.Board.Graphics
{
	/// <summary>
	/// Description of Color.
	/// </summary>
	public class Color
	{
		public int r;
		public int g;
		public int b;
		public Color(int r, int g, int b)
		{
			this.r = r;
			this.g = g;
			this.b = b;
		}
		
		public static bool operator ==(Color a, Color b)
		{
			return Math.Abs(a.r - b.r) < 2 && Math.Abs(a.g - b.g) < 2 && Math.Abs(a.b - b.b) < 2;
		}
		
		public static bool operator !=(Color a, Color b)
		{
			return Math.Abs(a.r - b.r) >= 2 || Math.Abs(a.g - b.g) >= 2 || Math.Abs(a.b - b.b) >= 2;
		}
		
		public static bool operator <(Color a, Color b)
		{
			return a.r < b.r && a.g < b.g && a.b < b.b;
		}
		
		public static bool operator >(Color a, Color b)
		{
			return a.r > b.r && a.g > b.g && a.b > b.b;
		}
		
		public override string ToString()
		{
			return r.ToString() + " " + g.ToString() + " " + b.ToString();
		}
	}
}
