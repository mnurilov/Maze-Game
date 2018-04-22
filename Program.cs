using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Game
{
    class Wall
    { 
        public char display = '#';
        public int length = 18;
        public int width = 48;
        
        public void Draw(string s)
        {
            for (int row = 0; row < length; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (col == 0 || col == width - 1 || row == 0 || row == length - 1)
                    { 
                        Console.Write( display);
                    }
                    else 
                    { 
                        Console.Write(" ");    
                    }
                }
                Console.WriteLine();
            }
        }
    }

    class Program
    {
        public static string Buffer = "";

        static void DisplayMap() 
        {
            Console.Write(Buffer);  
        }
        static void Main(string[] args)
        {
            Wall Wall = new Wall();
            Wall.Draw(Buffer);
            DisplayMap();
            Console.ReadLine();
        }
    }
}
