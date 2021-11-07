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
using System.Diagnostics;
using System.Threading.Tasks;

namespace Engine
{
	/// <summary>
	/// Description of Minimax.
	/// </summary>
	public class Minimax
	{
		static Stopwatch SW = new Stopwatch();
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
		public static Point solveUsingTasks(int[,] board, int player,int depth){
			SW.Restart();
			
			List<Point> APM = BoardHelper.getAllPossibleMoves(board,player);
			
			foreach(Point move in APM){
				int[,] node = BoardHelper.getNewBoardAfterMove(board,move,player);
				threads[move.X, move.Y].New(node,player,depth-1,false,int.MinValue,int.MaxValue);
				threads[move.X, move.Y].StartTask();
			}
			while (true){
				bool done = true;
				foreach(Point move in APM){
					if (!threads[move.X, move.Y]._ready){
						done = false;
						break;
					}
				}
				if (done){
					Console.WriteLine("multithreading done");
					break;
				}
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
			
			SW.Stop();
			Console.WriteLine(SW.ElapsedTicks);
			
			return bestMove;
		}
		
		public static Point solveUsingThreads(int[,] board, int player,int depth){
			SW.Restart();
			
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
			
			SW.Stop();
			Console.WriteLine(SW.ElapsedTicks);
			return bestMove;
		}
		
		public static Point solveNoMultiThreading(int[,] board, int player,int depth){
			SW.Restart();
			
			List<Point> APM = BoardHelper.getAllPossibleMoves(board,player);
			
			foreach(Point move in APM){
				int[,] node = BoardHelper.getNewBoardAfterMove(board,move,player);
				threads[move.X, move.Y].New(node,player,depth-1,false,int.MinValue,int.MaxValue);
				threads[move.X, move.Y].StartThread();
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
			
			SW.Stop();
			Console.WriteLine(SW.ElapsedTicks);
			return bestMove;
		}
		
		public static Point solveUsingParallel(int[,] board, int player,int depth){
			SW.Restart();
			
			List<Point> APM = BoardHelper.getAllPossibleMoves(board,player);
			
			Parallel.ForEach(
				APM,
				new ParallelOptions { MaxDegreeOfParallelism = 8 },
				move => {
					int[,] node = BoardHelper.getNewBoardAfterMove(board,move,player);
					threads[move.X, move.Y].New(node,player,depth-1,false,int.MinValue,int.MaxValue);
					threads[move.X, move.Y].StartThread();
				}
			);
			
			int bestScore = int.MinValue;
			Point bestMove = new Point();
			foreach(Point move in APM){
				int childScore = threads[move.X, move.Y]._score;
				if(childScore > bestScore) {
					bestScore = childScore;
					bestMove = move;
				}
			}
			
			SW.Stop();
			Console.WriteLine(SW.ElapsedTicks);
			return bestMove;
		}
	}
}
