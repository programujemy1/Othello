/*
 * Created by SharpDevelop.
 * User: Marcin
 * Date: 2021-11-05
 * Time: 20:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Collections.Generic;

namespace Engine
{
	/// <summary>
	/// Description of Minimax.
	/// </summary>
	public class Minimax
	{
		static MinMaxTask[,] threads = new MinMaxTask[8,8];
		static MultiThreading multiThreading = new MultiThreading();
		
		public static void init(){
			for (int x = 0; x < 8; x++){
				for (int y = 0; y < 8; y++){
					threads[x,y] = new MinMaxTask(x.ToString() + y.ToString());
				}
			}
		}
		
		//returns max score move
		public static Point solve(int[,] board, int player,int depth){
			List<Point> APM = BoardHelper.getAllPossibleMoves(board,player);
			
			multiThreading.New();
			foreach(Point move in APM){
				int[,] node = BoardHelper.getNewBoardAfterMove(board,move,player);
				threads[move.X, move.Y].New(node,player,depth-1,false,int.MinValue,int.MaxValue);
				multiThreading.Add(threads[move.X, move.Y]);
			}
			multiThreading.OnDone += () => {
				Console.WriteLine("multithreading done");
			};
			multiThreading.Start();
			while (!multiThreading.IsDone()){
			}
			int bestScore = int.MinValue;
			Point bestMove = new Point();
			foreach(Point move in APM){
				int childScore = threads[move.X, move.Y]._score;
				if(childScore > bestScore) {
					bestScore = childScore;
					bestMove = move;
				}
			}
			return bestMove;
		}
	}
}
