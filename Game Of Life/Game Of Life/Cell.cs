using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Of_Life
{
    class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int neighborX { get; set; }
        public int neighborY { get; set; }
        public int maxNeighborX { get; set; }
        public int maxNeighborY { get; set; }
        public int numActiveNeighbors { get; set; }
        public bool Active { get; set; }

        public Cell(int xpos,int ypos)
        {
            X = xpos;
            Y = ypos;
            numActiveNeighbors = 0;
            Active = false;
        }
        public void Tick(Cell[,] grid)
        {
            numActiveNeighbors = 0;



            if (X!= 0 && Y!=0 && grid[X - 1, Y - 1].Active)
            {
                numActiveNeighbors++;
            }
            if (Y != 0 && grid[X , Y - 1].Active)
            {
                numActiveNeighbors++;
            }
            if (X < grid.GetLength(1)-1 && Y!=0 && grid[X + 1, Y - 1].Active)
            {
                numActiveNeighbors++;
            }
            if (X != 0 &&grid[X - 1, Y ].Active)
            {
                numActiveNeighbors++;
            }
            if (X!=0 && Y < grid.GetLength(0)-1 && grid[X - 1, Y + 1].Active)
            {
                numActiveNeighbors++;
            }
            if (X < grid.GetLength(1)-1 && Y < grid.GetLength(0)-1 && grid[X + 1, Y + 1].Active)
            {
                numActiveNeighbors++;
            }
            if ( Y < grid.GetLength(0)-1 && grid[X, Y + 1].Active)
            {
                numActiveNeighbors++;
            }
            if (X < grid.GetLength(1)-1 && grid[X+1, Y].Active)
            {
                numActiveNeighbors++;
            }


        }

    }
}
