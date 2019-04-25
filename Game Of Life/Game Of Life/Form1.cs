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
        private int scale = 10;
        private bool running = false;
        private int speed = 10;
        private Cell[,] grid = new Cell[80,60];
        public Form1()
        {
            
            InitializeComponent();
            gameTimer.Interval = 1000 / speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();
            canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            canvas.MouseClick+= new MouseEventHandler(Canvas_Click);
            this.KeyDown += new KeyEventHandler(Canvas_KeyDown);
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
            if (running)
            {
                for (int y = 0; y < grid.GetLength(0); y++)
                {
                    for (int x = 0; x < grid.GetLength(1); x++)
                    {
                        grid[y, x].countNeighbors(grid);
                    }
                }
                for (int y = 0; y < grid.GetLength(0); y++)
                {
                    for (int x = 0; x < grid.GetLength(1); x++)
                    {
                        grid[y, x].Tick();
                    }
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

                currentCell.Active = !currentCell.Active;
                Console.WriteLine(currentCell.numActiveNeighbors);
                canvas.Invalidate() ;
            }
        }
        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
            {
                running = !running;
                Console.WriteLine(running);
            }
            if (e.KeyCode == Keys.R)
            {
                running = false;
                Random gen = new Random();

                for (int y = 0; y < grid.GetLength(0); y++)
                {
                    for (int x = 0; x < grid.GetLength(1); x++)
                    {
                        
                        if (gen.Next(100) < 20)
                        {
                            grid[y, x].Active = !grid[y,x].Active;
                        }
                    }
                }
                
                Console.WriteLine("randomed");
            }
        }
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

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

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.Location = new System.Drawing.Point(4, 1);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(818, 575);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label1.Location = new System.Drawing.Point(828, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Space: Play/Pause";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label2.Location = new System.Drawing.Point(828, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "R: Generate Random";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label3.Location = new System.Drawing.Point(828, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Click: Toggle Cell";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(944, 578);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.canvas);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
