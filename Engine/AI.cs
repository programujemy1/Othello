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
		
		public AI() {
			Minimax.init();
		}
		
		public Tuple<int, Point> play(int[,] board) {
			if(!BoardHelper.isGameFinished(board)) {
				if(BoardHelper.hasAnyMoves(board,mark)) {
					Console.WriteLine("thinking...");
					Point aiPlayPoint;
					/* 
 						* solveUsingParallel > solveUsingThreads > solveUsingTasks > solveNoMultiThreading
 					*/
 					aiPlayPoint = Minimax.solveUsingParallel(board, mark, depth); // FASTEST
					//aiPlayPoint = Minimax.solveUsingThreads(board, mark, depth); //
					//aiPlayPoint = Minimax.solveUsingTasks(board, mark, depth); //
					//aiPlayPoint = Minimax.solveNoMultiThreading(board, mark, depth); // SLOWEST
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
