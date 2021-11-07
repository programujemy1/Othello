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

namespace Engine
{
	/// <summary>
	/// Description of Minimax.
	/// </summary>
	public class Minimax
	{
		static int nodesExplored = 0;
		
		//returns max score move
		public static Point solve(int[,] board, int player,int depth,Evaluator e){
			nodesExplored = 0;
			int bestScore = int.MinValue;
			Point bestMove = new Point();
			foreach(Point move in BoardHelper.getAllPossibleMoves(board,player)){
				//create new node
				int[,] newNode = BoardHelper.getNewBoardAfterMove(board,move,player);
				//recursive call
				int childScore = MMAB(newNode,player,depth-1,false,int.MinValue,int.MaxValue,e);
				if(childScore > bestScore) {
					bestScore = childScore;
					bestMove = move;
				}
			}
			Console.WriteLine("Nodes Explored : " + nodesExplored);
			return bestMove;
		}
		
		//returns minimax value for a given node with A/B pruning
		private static int MMAB(int[,] node,int player,int depth,bool max,int alpha,int beta,Evaluator e){
			nodesExplored++;
			//if terminal reached or depth limit reached evaluate
			if(depth == 0 || BoardHelper.isGameFinished(node)){
				//BoardPrinter bpe = new BoardPrinter(node,"Depth : " + depth);
				return e.eval(node,player);
			}
			int oplayer = (player==1) ? 2 : 1;
			//if no moves available then forfeit turn
			if((max && !BoardHelper.hasAnyMoves(node,player)) || (!max && !BoardHelper.hasAnyMoves(node,oplayer))){
				//System.out.println("Forfeit State Reached !");
				return MMAB(node,player,depth-1,!max,alpha,beta,e);
			}
			int score;
			if(max){
				//maximizing
				score = int.MinValue;
				foreach(Point move in BoardHelper.getAllPossibleMoves(node,player)){ //my turn
					//create new node
					int[,] newNode = BoardHelper.getNewBoardAfterMove(node,move,player);
					//recursive call
					int childScore = MMAB(newNode,player,depth-1,false,alpha,beta,e);
					if(childScore > score) score = childScore;
					//update alpha
					if(score > alpha) alpha = score;
					if(beta <= alpha) break; //Beta Cutoff
				}
			}else{
				//minimizing
				score = int.MaxValue;
				foreach(Point move in BoardHelper.getAllPossibleMoves(node,oplayer)){ //opponent turn
					//create new node
					int[,] newNode = BoardHelper.getNewBoardAfterMove(node,move,oplayer);
					//recursive call
					int childScore = MMAB(newNode,player,depth-1,true,alpha,beta,e);
					if(childScore < score) score = childScore;
					//update beta
					if(score < beta) beta = score;
					if(beta <= alpha) break; //Alpha Cutoff
				}
			}
			return score;
		}
	}
}
