using System;
using System.Drawing;

namespace Engine
{
	/// <summary>
	/// Description of MinMaxTask.
	/// </summary>
	public class MinMaxTask
	{
		public int _score;
		public string _id;
		
		int[,] _node;
		int _player;
		int _depth;
		bool _max;
		int _alpha;
		int _beta;
		
		public MinMaxTask(string id){
			this._id = id;
		}
		
		public void New(int[,] node, int player, int depth, bool max, int alpha, int beta){
			this._score = int.MinValue;
			this._node = node;
			this._player = player;
			this._depth = depth;
			this._max = max;
			this._alpha = alpha;
			this._beta = beta;
		}
		
		public void Start(){
			Console.WriteLine("thread id " + this._id);
			this._score = MMAB(this._node, this._player, this._depth, this._max, this._alpha, this._beta);
		}
		
		//returns minimax value for a given node with A/B pruning
		int MMAB(int[,] node,int player,int depth,bool max,int alpha,int beta){
			//if terminal reached or depth limit reached evaluate
			if(depth == 0 || BoardHelper.isGameFinished(node)){
				//BoardPrinter bpe = new BoardPrinter(node,"Depth : " + depth);
				return eval(node,player);
			}
			int oplayer = (player==1) ? 2 : 1;
			//if no moves available then forfeit turn
			if((max && !BoardHelper.hasAnyMoves(node,player)) || (!max && !BoardHelper.hasAnyMoves(node,oplayer))){
				//System.out.println("Forfeit State Reached !");
				return MMAB(node,player,depth-1,!max,alpha,beta);
			}
			int score;
			if(max){
				//maximizing
				score = int.MinValue;
				foreach(Point move in BoardHelper.getAllPossibleMoves(node,player)){ //my turn
					//create new node
					int[,] newNode = BoardHelper.getNewBoardAfterMove(node,move,player);
					//recursive call
					int childScore = MMAB(newNode,player,depth-1,false,alpha,beta);
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
					int childScore = MMAB(newNode,player,depth-1,true,alpha,beta);
					if(childScore < score) score = childScore;
					//update beta
					if(score < beta) beta = score;
					if(beta <= alpha) break; //Alpha Cutoff
				}
			}
			return score;
		}
		
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
		
		int eval(int[,] board , int player){
			
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
