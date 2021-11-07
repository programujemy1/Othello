using System;
using System.Drawing;

namespace Engine
{
	/// <summary>
	/// Description of AI.
	/// </summary>
	public class AI
	{
		public int mark;
		public int depth;
		Evaluator evaluator;
		
		public AI() {
			evaluator = new Evaluator();
		}
		
		public Tuple<int, Point> play(int[,] board) {
			if(!BoardHelper.isGameFinished(board)) {
				if(BoardHelper.hasAnyMoves(board,mark)) {
					Console.WriteLine("thinking...");
					Point aiPlayPoint = Minimax.solve(board, mark, depth, evaluator);
					int i = aiPlayPoint.X;
					int j = aiPlayPoint.Y;
					if(BoardHelper.canPlay(board,mark,i,j)) {
						return new Tuple<int, Point>(1, aiPlayPoint);
					}
					Console.WriteLine("FATAL : AI Invalid Move !");
				}
			}
			return new Tuple<int, Point>(0, new Point());
		}
	}
}
