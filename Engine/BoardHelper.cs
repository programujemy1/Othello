/*
 * Created by SharpDevelop.
 * User: Marcin
 * Date: 2021-11-05
 * Time: 20:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Collections.Generic;

namespace Engine
{
	/// <summary>
	/// Description of BoardHelper.
	/// </summary>
	public class BoardHelper {
		
		public static bool isGameFinished(int[,] board){
			return !(hasAnyMoves(board,1) || hasAnyMoves(board,2));
		}
		
		
		public static int[,] getStartBoard(){
			int[, ] b = new int[8, 8];
			for (var i = 0; i < 8; i++)
			{
				for (var j = 0; j < 8; j++)
				{
					b[i, j] = 0;
				}
			}
			b[3,3] = 2;
			b[3,4] = 1;
			b[4,3] = 1;
			b[4,4] = 2;
			return b;
		}
		
		public static Point getMove(int[,] before , int[,] after){
			for (int i = 0; i < 8; i++) {
				for (int j = 0; j < 8; j++) {
					if(before[i,j]==0 && after[i,j]!=0){
						return new Point(i,j);
					}
				}
			}
			return new Point();
		}
		
		public static int getWinner(int[,] board){
			if(!isGameFinished(board))
				//game not finished
				return -1;
			else{
				//count stones
				int p1s = getPlayerStoneCount(board,1);
				int p2s = getPlayerStoneCount(board,2);
				
				if(p1s == p2s){
					//tie
					return 0;
				}else if(p1s > p2s){
					//p1 wins
					return 1;
				}else{
					//p2 wins
					return 2;
				}
			}
		}
		
		public static int getTotalStoneCount(int[,] board){
			int c = 0;
			for (int i = 0; i < 8; i++) {
				for (int j = 0; j < 8; j++) {
					if(board[i,j] != 0) c++;
				}
			}
			return c;
		}
		
		public static int getPlayerStoneCount(int[,] board, int player){
			int score = 0;
			for (int i = 0; i < 8; i++) {
				for (int j = 0; j < 8; j++) {
					if(board[i,j] == player) score++;
				}
			}
			return score;
		}
		
		
		public static bool hasAnyMoves(int[,] board, int player){
			return getAllPossibleMoves(board,player).Count > 0;
		}
		
		public static List<Point> getAllPossibleMoves(int[,] board, int player){
			List<Point> result = new List<Point>();
			for (int i = 0; i < 8; i++) {
				for (int j = 0; j < 8; j++) {
					if(canPlay(board,player,i,j)){
						result.Add(new Point(i,j));
					}
				}
			}
			return result;
		}
		
		public static List<Point> getReversePoints(int[,] board,int player,int i,int j){
			
			List<Point> allReversePoints = new List<Point>();
			
			int mi , mj , c;
			int oplayer = ((player == 1) ? 2 : 1);
			
			//move up
			List<Point> mupts = new List<Point>();
			mi = i - 1;
			mj = j;
			while(mi>0 && board[mi,mj] == oplayer){
				mupts.Add(new Point(mi,mj));
				mi--;
			}
			if(mi>=0 && board[mi,mj] == player && mupts.Count>0){
				allReversePoints.AddRange(mupts);
			}
			
			
			//move down
			List<Point> mdpts = new List<Point>();
			mi = i + 1;
			mj = j;
			while(mi<7 && board[mi,mj] == oplayer){
				mdpts.Add(new Point(mi,mj));
				mi++;
			}
			if(mi<=7 && board[mi,mj] == player && mdpts.Count>0){
				allReversePoints.AddRange(mdpts);
			}
			
			//move left
			List<Point> mlpts = new List<Point>();
			mi = i;
			mj = j - 1;
			while(mj>0 && board[mi,mj] == oplayer){
				mlpts.Add(new Point(mi,mj));
				mj--;
			}
			if(mj>=0 && board[mi,mj] == player && mlpts.Count>0){
				allReversePoints.AddRange(mlpts);
			}
			
			//move right
			List<Point> mrpts = new List<Point>();
			mi = i;
			mj = j + 1;
			while(mj<7 && board[mi,mj] == oplayer){
				mrpts.Add(new Point(mi,mj));
				mj++;
			}
			if(mj<=7 && board[mi,mj] == player && mrpts.Count>0){
				allReversePoints.AddRange(mrpts);
			}
			
			//move up left
			List<Point> mulpts = new List<Point>();
			mi = i - 1;
			mj = j - 1;
			while(mi>0 && mj>0 && board[mi,mj] == oplayer){
				mulpts.Add(new Point(mi,mj));
				mi--;
				mj--;
			}
			if(mi>=0 && mj>=0 && board[mi,mj] == player && mulpts.Count>0){
				allReversePoints.AddRange(mulpts);
			}
			
			//move up right
			List<Point> murpts = new List<Point>();
			mi = i - 1;
			mj = j + 1;
			while(mi>0 && mj<7 && board[mi,mj] == oplayer){
				murpts.Add(new Point(mi,mj));
				mi--;
				mj++;
			}
			if(mi>=0 && mj<=7 && board[mi,mj] == player && murpts.Count>0){
				allReversePoints.AddRange(murpts);
			}
			
			//move down left
			List<Point> mdlpts = new List<Point>();
			mi = i + 1;
			mj = j - 1;
			while(mi<7 && mj>0 && board[mi,mj] == oplayer){
				mdlpts.Add(new Point(mi,mj));
				mi++;
				mj--;
			}
			if(mi<=7 && mj>=0 && board[mi,mj] == player && mdlpts.Count>0){
				allReversePoints.AddRange(mdlpts);
			}
			
			//move down right
			List<Point> mdrpts = new List<Point>();
			mi = i + 1;
			mj = j + 1;
			while(mi<7 && mj<7 && board[mi,mj] == oplayer){
				mdrpts.Add(new Point(mi,mj));
				mi++;
				mj++;
			}
			if(mi<=7 && mj<=7 && board[mi,mj] == player && mdrpts.Count>0){
				allReversePoints.AddRange(mdrpts);
			}
			
			return allReversePoints;
		}
		
		public static bool canPlay(int[,] board,int player,int i,int j){
			
			if(board[i,j] != 0) return false;
			
			int mi , mj , c;
			int oplayer = ((player == 1) ? 2 : 1);
			
			//move up
			mi = i - 1;
			mj = j;
			c = 0;
			while(mi>0 && board[mi,mj] == oplayer){
				mi--;
				c++;
			}
			if(mi>=0 && board[mi,mj] == player && c>0) return true;
			
			
			//move down
			mi = i + 1;
			mj = j;
			c = 0;
			while(mi<7 && board[mi,mj] == oplayer){
				mi++;
				c++;
			}
			if(mi<=7 && board[mi,mj] == player && c>0) return true;
			
			//move left
			mi = i;
			mj = j - 1;
			c = 0;
			while(mj>0 && board[mi,mj] == oplayer){
				mj--;
				c++;
			}
			if(mj>=0 && board[mi,mj] == player && c>0) return true;
			
			//move right
			mi = i;
			mj = j + 1;
			c = 0;
			while(mj<7 && board[mi,mj] == oplayer){
				mj++;
				c++;
			}
			if(mj<=7 && board[mi,mj] == player && c>0) return true;
			
			//move up left
			mi = i - 1;
			mj = j - 1;
			c = 0;
			while(mi>0 && mj>0 && board[mi,mj] == oplayer){
				mi--;
				mj--;
				c++;
			}
			if(mi>=0 && mj>=0 && board[mi,mj] == player && c>0) return true;
			
			//move up right
			mi = i - 1;
			mj = j + 1;
			c = 0;
			while(mi>0 && mj<7 && board[mi,mj] == oplayer){
				mi--;
				mj++;
				c++;
			}
			if(mi>=0 && mj<=7 && board[mi,mj] == player && c>0) return true;
			
			//move down left
			mi = i + 1;
			mj = j - 1;
			c = 0;
			while(mi<7 && mj>0 && board[mi,mj] == oplayer){
				mi++;
				mj--;
				c++;
			}
			if(mi<=7 && mj>=0 && board[mi,mj] == player && c>0) return true;
			
			//move down right
			mi = i + 1;
			mj = j + 1;
			c = 0;
			while(mi<7 && mj<7 && board[mi,mj] == oplayer){
				mi++;
				mj++;
				c++;
			}
			if(mi<=7 && mj<=7 && board[mi,mj] == player && c>0) return true;
			
			//when all hopes fade away
			return false;
		}
		
		public static int[,] getNewBoardAfterMove(int[,] board, Point move , int player){
			//get clone of old board
			int[,] newboard = new int[8,8];
			for (int k = 0; k < 8; k++) {
				for (int l = 0; l < 8; l++) {
					newboard[k,l] = board[k,l];
				}
			}
			
			//place piece
			newboard[move.X,move.Y] = player;
			//reverse pieces
			List<Point> rev = BoardHelper.getReversePoints(newboard,player,move.X,move.Y);
			foreach(Point pt in rev){
				newboard[pt.X,pt.Y] = player;
			}
			
			return newboard;
		}
		
		public static List<Point> getStableDisks(int[,] board,int player,int i,int j){
			
			List<Point> stableDiscs = new List<Point>();
			
			int mi , mj;
			int oplayer = ((player == 1) ? 2 : 1);
			
			//move up
			List<Point> mupts = new List<Point>();
			mi = i - 1;
			mj = j;
			while(mi>0 && board[mi,mj] == player){
				mupts.Add(new Point(mi,mj));
				mi--;
			}
			foreach(Point sd in mupts){
				bool redundant = false;
				foreach(Point stableDisc in stableDiscs){
					if(sd.Equals(stableDisc)){
						redundant = true;
						break;
					}
				}
				if(!redundant) stableDiscs.Add(sd);
			}
			
			//move down
			List<Point> mdpts = new List<Point>();
			mi = i + 1;
			mj = j;
			while(mi<7 && board[mi,mj] == oplayer){
				mdpts.Add(new Point(mi,mj));
				mi++;
			}
			foreach(Point sd in mdpts){
				bool redundant = false;
				foreach(Point stableDisc in stableDiscs){
					if(sd.Equals(stableDisc)){
						redundant = true;
						break;
					}
				}
				if(!redundant) stableDiscs.Add(sd);
			}
			
			//move left
			List<Point> mlpts = new List<Point>();
			mi = i;
			mj = j - 1;
			while(mj>0 && board[mi,mj] == oplayer){
				mlpts.Add(new Point(mi,mj));
				mj--;
			}
			foreach(Point sd in mlpts){
				bool redundant = false;
				foreach(Point stableDisc in stableDiscs){
					if(sd.Equals(stableDisc)){
						redundant = true;
						break;
					}
				}
				if(!redundant) stableDiscs.Add(sd);
			}
			
			//move right
			List<Point> mrpts = new List<Point>();
			mi = i;
			mj = j + 1;
			while(mj<7 && board[mi,mj] == oplayer){
				mrpts.Add(new Point(mi,mj));
				mj++;
			}
			foreach(Point sd in mrpts){
				bool redundant = false;
				foreach(Point stableDisc in stableDiscs){
					if(sd.Equals(stableDisc)){
						redundant = true;
						break;
					}
				}
				if(!redundant) stableDiscs.Add(sd);
			}
			
			//move up left
			List<Point> mulpts = new List<Point>();
			mi = i - 1;
			mj = j - 1;
			while(mi>0 && mj>0 && board[mi,mj] == oplayer){
				mulpts.Add(new Point(mi,mj));
				mi--;
				mj--;
			}
			foreach(Point sd in mulpts){
				bool redundant = false;
				foreach(Point stableDisc in stableDiscs){
					if(sd.Equals(stableDisc)){
						redundant = true;
						break;
					}
				}
				if(!redundant) stableDiscs.Add(sd);
			}
			
			//move up right
			List<Point> murpts = new List<Point>();
			mi = i - 1;
			mj = j + 1;
			while(mi>0 && mj<7 && board[mi,mj] == oplayer){
				murpts.Add(new Point(mi,mj));
				mi--;
				mj++;
			}
			foreach(Point sd in murpts){
				bool redundant = false;
				foreach(Point stableDisc in stableDiscs){
					if(sd.Equals(stableDisc)){
						redundant = true;
						break;
					}
				}
				if(!redundant) stableDiscs.Add(sd);
			}
			
			//move down left
			List<Point> mdlpts = new List<Point>();
			mi = i + 1;
			mj = j - 1;
			while(mi<7 && mj>0 && board[mi,mj] == oplayer){
				mdlpts.Add(new Point(mi,mj));
				mi++;
				mj--;
			}
			foreach(Point sd in mdlpts){
				bool redundant = false;
				foreach(Point stableDisc in stableDiscs){
					if(sd.Equals(stableDisc)){
						redundant = true;
						break;
					}
				}
				if(!redundant) stableDiscs.Add(sd);
			}
			
			//move down right
			List<Point> mdrpts = new List<Point>();
			mi = i + 1;
			mj = j + 1;
			while(mi<7 && mj<7 && board[mi,mj] == oplayer){
				mdrpts.Add(new Point(mi,mj));
				mi++;
				mj++;
			}
			foreach(Point sd in mdrpts){
				bool redundant = false;
				foreach(Point stableDisc in stableDiscs){
					if(sd.Equals(stableDisc)){
						redundant = true;
						break;
					}
				}
				if(!redundant) stableDiscs.Add(sd);
			}
			
			return stableDiscs;
		}
		
		
		public static List<Point> getFrontierSquares(int[,] board,int player){
			
			List<Point> frontiers = new List<Point>();
			
			int oplayer = ((player == 1) ? 2 : 1);
			
			for (int i = 0; i < 8; i++) {
				for (int j = 0; j < 8; j++) {
					if(board[i,j]==oplayer){
						
						List<Point> possiblefrontiers = new List<Point>();
						
						//check 8 directions
						
						//up
						if(i>0 && board[i-1,j]==0) possiblefrontiers.Add(new Point(i-1,j));
						//down
						if(i<7 && board[i+1,j]==0) possiblefrontiers.Add(new Point(i+1,j));
						//right
						if(j<7 && board[i,j+1]==0) possiblefrontiers.Add(new Point(i,j+1));
						//left
						if(j>0 && board[i,j-1]==0) possiblefrontiers.Add(new Point(i,j-1));
						//up-left
						if(i>0 && j>0 && board[i-1,j-1]==0) possiblefrontiers.Add(new Point(i-1,j-1));
						//up-right
						if(i>0 && j<7 && board[i-1,j+1]==0) possiblefrontiers.Add(new Point(i-1,j+1));
						//down-left
						if(i<7 && j>0 && board[i+1,j-1]==0) possiblefrontiers.Add(new Point(i+1,j-1));
						//down-right
						if(i<7 && j<7 && board[i+1,j+1]==0) possiblefrontiers.Add(new Point(i+1,j+1));
						
						//remove duplicates
						foreach(Point pf in possiblefrontiers){
							bool redundant = false;
							foreach(Point f in frontiers){
								if(f.Equals(pf)){
									redundant = true;
									break;
								}
							}
							if(!redundant) frontiers.Add(pf);
						}
					}
				}
			}
			
			return frontiers;
		}
		
	}
}
