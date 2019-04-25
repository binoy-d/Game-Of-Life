using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public partial class Form1 : Form
    {
        private int scale = 20;
        private bool running = true;
        private int speed = 10;
        private Cell[,] grid = new Cell[20,20];
        public Form1()
        {
            
            InitializeComponent();
            gameTimer.Interval = 1000 / speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();
            canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            canvas.MouseClick+= new MouseEventHandler(Canvas_Click);
            StartGame();
        }

        private void StartGame()
        {
            Random gen = new Random();
            for(int y = 0;y<grid.GetLength(0); y++)
            {
                for(int x = 0;x<grid.GetLength(1); x++)
                {
                    grid[y, x] = new Cell(x, y);
                }
            }
        }
        private void UpdateScreen(object sender, EventArgs e)
        {
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    grid[y, x].Tick(grid);
                }
            }
            canvas.Invalidate();
        }
        private void Canvas_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;
            Console.WriteLine("X: " + coordinates.X / scale + ", Y: " + coordinates.Y / scale);
            if (coordinates.X < grid.GetLength(0) * scale && coordinates.Y < grid.GetLength(1) * scale) { 
                Cell currentCell = grid[coordinates.X / scale, coordinates.Y / scale];
                currentCell.Active = true;
                Console.WriteLine($"scanning from ({currentCell.neighborY},{currentCell.neighborX}) to {currentCell.maxNeighborY},{currentCell.maxNeighborX})");
                Console.WriteLine(currentCell.numActiveNeighbors);
            }
        }
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (running)
            {

                Brush inactiveColor = Brushes.Black;
                Brush activeColor = Brushes.White;


                for (int y = 0; y < grid.GetLength(0); y++)
                {
                    for (int x = 0; x < grid.GetLength(1); x++)
                    {

                        if (grid[y, x].Active == true)
                        {
                            g.FillRectangle(activeColor, y*scale, x*scale, scale, scale);
                        }
                        else
                        {
                            g.FillRectangle(inactiveColor, y * scale, x * scale, scale, scale);
                        }
                    }
                }
            }
        }
    }
}
