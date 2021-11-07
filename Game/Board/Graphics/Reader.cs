using System;
using System.Drawing;
using System.Windows.Forms;
using SystemImaging = System.Drawing.Imaging;
using SystemGraphics = System.Drawing.Graphics;
using GamePuzzle = Game.Board.Puzzle;

namespace Game.Board.Graphics
{
	/// <summary>
	/// Description of Reader.
	/// </summary>
	public class Reader
	{
		bool valid;
		int bx, by, blen;
		int bgx, bgy, bglen;
		int w, h;
		Bitmap bitmap;
		
		void Clear()
		{
			this.valid = false;
			this.bx = 0;
			this.by = 0;
			this.blen = 0;
			this.bgx = 0;
			this.bgy = 0;
			this.bglen = 0;
			if (this.bitmap != null){
				this.bitmap.Dispose();
				this.bitmap = null;
			}
			this.w = Screen.PrimaryScreen.Bounds.Width;
			this.h = Screen.PrimaryScreen.Bounds.Height;
			this.bitmap = new Bitmap(this.w, this.h);
			SystemGraphics gr = SystemGraphics.FromImage(this.bitmap);
			gr.CopyFromScreen(0, 0, 0, 0, this.bitmap.Size);
			this.bitmap.Save("bitmap.png", SystemImaging.ImageFormat.Png);
		}
		
		public bool Update()
		{
			this.Clear();
			GetBackGround();
			if (this.bglen > 100){
				GetBoard();
				if (this.blen > 100){
					this.GetItems();
					this.valid = true;
				}
			}
			return this.valid;
		}
		
		public void GetBackGround(){
			long start_time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
			
			bool bgfound = false;
			int bgx = 0;
			int bgy = 0;
			int bglen = 0;
			int width = this.w;
			int height = this.h;
			
			for (int x = 0; x < this.w; x+= 50){
				for (int y = 0; y < this.h; y+= 50){
					var col = bitmap.GetPixel(x,y);
					Color color = new Color(col.R, col.G, col.B);
					if (Colors.IsBackGround(color)){
						bgfound = true;
						bgx = x;
						bgy = y;
						break;
					}
				}

				if (bgfound)
				{
					break;
				}
			}

			if (!bgfound)
				return;
			
			int xbgstep = 0;
			int ybgstep = 0;
			for (int x = 1; x < 55; x++){
				var col = bitmap.GetPixel(bgx - x, bgy);
				Color color = new Color(col.R, col.G, col.B);
				if (Colors.IsBackGround(color)){
					xbgstep = x;
				}
			}
			this.bgx = bgx - xbgstep;
			
			for (int y = 1; y < 55; y++){
				var col = bitmap.GetPixel(bgx, bgy - y);
				Color color = new Color(col.R, col.G, col.B);
				if (Colors.IsBackGround(color)){
					ybgstep = y;
				}
			}
			this.bgy = bgy - ybgstep;
			
			for (int y = this.bgy; y < height; y++){
				var col = bitmap.GetPixel(this.bgx, y);
				Color color = new Color(col.R, col.G, col.B);
				if (Colors.IsBackGround(color)){
					bglen++;
				}
			}
			this.bglen = bglen;
			long time = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - start_time;
			Console.WriteLine("BACKGROUND " + "Time = " + time.ToString() + "ms");
			Console.WriteLine("X = " + this.bgx.ToString() + "\nY = " + this.bgy.ToString() + "\nLenght = " + this.bglen.ToString() + "\n");
		}
		
		public void GetBoard(){
			long start_time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
			
			bool bfound = false;
			int bx = 0;
			int by = 0;
			int blen = 0;
			int width = this.w;
			int height = this.h;
			
			int ystep = Convert.ToInt32(this.bglen / 3);
			for (int x = this.bgx; x < this.bglen + this.bgx; x++){
				for (int y = this.bgy; y < this.bglen + this.bgy; y+=ystep){
					var col = bitmap.GetPixel(x, y);
					Color color = new Color(col.R, col.G, col.B);
					if (!Colors.IsBackGround(color) && Colors.IsBorder(color)){
						bfound = true;
						bx = x;
						by = y;
						break;
					}
				}
				if (bfound){
					break;
				}
			}
			if (!bfound){
				return;
			}
			this.bx = bx;
			
			int miny = by;
			for (int x = 0; x < 3; x++){
				int currentby = by;
				for (int y = by-ystep-1; y < by + 1; y++){
					var col = bitmap.GetPixel(bx + x, y);
					Color color = new Color(col.R, col.G, col.B);
					if (!Colors.IsBackGround(color) && Colors.IsBorder(color)){
						currentby = y;
						break;
					}
				}
				if (currentby < miny){
					miny = currentby;
				}
			}
			this.by = miny;
			
			int maxy = this.by;
			for (int x = 0; x < 3; x++){
				int currentby = by;
				for (int y = this.bglen + this.bgy - 1; y > this.by; y--){
					var col = bitmap.GetPixel(bx + x, y);
					Color color = new Color(col.R, col.G, col.B);
					if (!Colors.IsBackGround(color) && Colors.IsBorder(color)){
						if (y > maxy){
							maxy = y;
							blen = y - this.by;
						}
						break;
					}
				}
			}
			this.blen = blen + 1;
			
			long time = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - start_time;
			Console.WriteLine("PUZZLE BOARD " + "Time = " + time.ToString() + "ms");
			Console.WriteLine("X = " + this.bx.ToString() + "\nY = " + this.by.ToString() + "\nLenght = " + this.blen.ToString() + "\n");
		}
		
		public void GetItems(){
			long start_time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
			
			int[] isize = this.GetItemSize();
			int ilen = isize[0];
			int imargin = isize[1];
			int ibx = this.bx + imargin;
			int iby = this.by + imargin;
			int itotal = ilen + imargin;
			int icenter = Convert.ToInt32(ilen / 2) - 5;
			for (int x = 0; x < 8; x++){
				bx = ibx + icenter + x * itotal;
				for (int y = 0; y < 8; y++){
					Item item = GamePuzzle.items[x,y];
					item.bx = bx;
					item.by = iby + icenter + y * itotal;
					item.mark = this.GetItemType(bx, item.by - icenter + 5, ilen - 10);
					GamePuzzle._items[x,y] = item.mark;
				}
			}
			
			long time = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - start_time;
			Console.WriteLine("PUZZLE ITEM " + "Time = " + time.ToString() + "ms");
			Console.WriteLine("Lenght=" + ilen.ToString() + "\nMargin=" + imargin.ToString() + "\n");
		}
		
		public int[] GetItemSize(){
			int ilen = 0;
			int xstart = 0;
			int imargin = 1;
			for (int i = 1; i < 10; i++){
				var col = bitmap.GetPixel(this.bx + i, this.by + i);
				Color color = new Color(col.R, col.G, col.B);
				if (!Colors.IsBackGround(color) && Colors.IsBorder(color)){
					imargin++;
					continue;
				}
				if (Colors.IsBackGround(color)){
					xstart = this.bx + i;
					break;
				}
			}
			for (int i = imargin; i < this.blen; i++){
				var col = bitmap.GetPixel(xstart, this.by + i);
				Color color = new Color(col.R, col.G, col.B);
				if (!Colors.IsBackGround(color) && Colors.IsBorder(color)){
					break;
				}
				ilen++;
			}
			return new int[2] {ilen, imargin};
		}
		
		public int GetItemType(int bx, int by, int maxy)
		{
			for (int y = 0; y < maxy; y++){
				var col = bitmap.GetPixel(bx, by + y);
				Color color = new Color(col.R, col.G, col.B);
				if (Colors.IsWhite(color)){
					return 1;
				}
				if (Colors.IsBlack(color)){
					return 2;
				}
			}
			return 0;
		}
	}
}