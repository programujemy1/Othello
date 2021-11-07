/*
 * Created by SharpDevelop.
 * User: Marcin
 * Date: 2021-11-05
 * Time: 20:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Engine
{
	/// <summary>
	/// Description of Evaluator.
	/// </summary>
	public class Evaluator
	{
		//Evaluation Function Changes during Early-Game / Mid-Game / Late-Game
		enum GamePhase {
			EARLY_GAME,
			MID_GAME,
			LATE_GAME
		}
		
		GamePhase getGamePhase(int[,] board){
			int sc = BoardHelper.getTotalStoneCount(board);
			if(sc<20) return GamePhase.EARLY_GAME;
			else if(sc<=58) return GamePhase.MID_GAME;
			else return GamePhase.LATE_GAME;
		}
		
		int evalDiscDiff(int[,] board , int player){
			int oplayer = (player==1) ? 2 : 1;
			
			int mySC = BoardHelper.getPlayerStoneCount(board,player);
			int opSC = BoardHelper.getPlayerStoneCount(board,oplayer);
			
			return 100 * (mySC - opSC) / (mySC + opSC);
		}
		
		int evalMobility(int[,] board , int player){
			int oplayer = (player==1) ? 2 : 1;
			
			int myMoveCount = BoardHelper.getAllPossibleMoves(board,player).Count;
			int opMoveCount = BoardHelper.getAllPossibleMoves(board,oplayer).Count;
			
			return 100 * (myMoveCount - opMoveCount) / (myMoveCount + opMoveCount + 1);
		}
		
		int evalCorner(int[,] board , int player){
			int oplayer = (player==1) ? 2 : 1;
			
			int myCorners = 0;
			int opCorners = 0;
			
			if(board[0,0]==player) myCorners++;
			if(board[7,0]==player) myCorners++;
			if(board[0,7]==player) myCorners++;
			if(board[7,7]==player) myCorners++;
			
			if(board[0,0]==oplayer) opCorners++;
			if(board[7,0]==oplayer) opCorners++;
			if(board[0,7]==oplayer) opCorners++;
			if(board[7,7]==oplayer) opCorners++;
			
			return 100 * (myCorners - opCorners) / (myCorners + opCorners + 1);
		}
		
		int evalBoardMap(int[,] board , int player){
			int oplayer = (player==1) ? 2 : 1;
			int[,] W = {
				{200 , -100, 100,  50,  50, 100, -100,  200},
				{-100, -200, -50, -50, -50, -50, -200, -100},
				{100 ,  -50, 100,   0,   0, 100,  -50,  100},
				{50  ,  -50,   0,   0,   0,   0,  -50,   50},
				{50  ,  -50,   0,   0,   0,   0,  -50,   50},
				{100 ,  -50, 100,   0,   0, 100,  -50,  100},
				{-100, -200, -50, -50, -50, -50, -200, -100},
				{200 , -100, 100,  50,  50, 100, -100,  200}};
			
			//if corners are taken W for that 1/4 loses effect
			if(board[0,0] != 0){
				for (int i = 0; i < 3; i++) {
					for (int j = 0; j <= 3; j++) {
						W[i,j] = 0;
					}
				}
			}
			
			if(board[0,7] != 0){
				for (int i = 0; i < 3; i++) {
					for (int j = 4; j <= 7; j++) {
						W[i,j] = 0;
					}
				}
			}
			
			if(board[7,0] != 0){
				for (int i = 5; i < 8; i++) {
					for (int j = 0; j <= 3; j++) {
						W[i,j] = 0;
					}
				}
			}
			
			if(board[7,7] != 0){
				for (int i = 5; i < 8; i++) {
					for (int j = 4; j <= 7; j++) {
						W[i,j] = 0;
					}
				}
			}
			
			
			int myW = 0;
			int opW = 0;
			
			for (int i = 0; i < 8; i++) {
				for (int j = 0; j < 8; j++) {
					if(board[i,j]==player) myW += W[i,j];
					if(board[i,j]==oplayer) opW += W[i,j];
				}
			}
			
			return (myW - opW) / (myW + opW + 1);
		}
		
		int evalParity(int[,] board){
			int remDiscs = 64 - BoardHelper.getTotalStoneCount(board);
			return remDiscs % 2 == 0 ? -1 : 1;
		}
		
		public int eval(int[,] board , int player){
			
			//terminal
			if(BoardHelper.isGameFinished(board)){
				return 1000*evalDiscDiff(board, player);
			}
			
			//semi-terminal
			switch (getGamePhase(board)){
				case GamePhase.EARLY_GAME:
					return 1000*evalCorner(board,player) + 50*evalMobility(board,player);
				case GamePhase.MID_GAME:
					return 1000*evalCorner(board,player) + 20*evalMobility(board,player) + 10*evalDiscDiff(board, player) + 100*evalParity(board);
				case GamePhase.LATE_GAME:
				default:
					return 1000*evalCorner(board,player) + 100*evalMobility(board,player) + 500*evalDiscDiff(board, player) + 500*evalParity(board);
			}
		}
	}
}
