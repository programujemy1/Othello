using System;
using System.Linq;
using System.Collections.Generic;

namespace Game.Board.Graphics
{
	/// <summary>
	/// Description of Colors.
	/// </summary>
	public class Colors
	{
		public static List<Color> black = new List<Color>{
			new Color(-1,-1,-1),
			new Color(50, 50, 50)
		};
		public static List<Color> white = new List<Color>{
			new Color(250, 250, 250),
			new Color(256, 256, 256)
		};
		public static List<Color> border = new List<Color>{
			new Color(40, 100, 60),
			new Color(65,140,90)
		};
		public static List<Color> background = new List<Color>{
			new Color(65, 140, 90),
			new Color(70,147,94)
		};
		public static bool IsBlack(Color color){
			return color > black[0] && color < black[1];
		}
		public static bool IsWhite(Color color){
			return color > white[0] && color < white[1];
		}
		public static bool IsBackGround(Color color){
			return color > background[0] && color < background[1];
		}
		public static bool IsBorder(Color color){
			return color > border[0] && color < border[1];
		}
	}
}