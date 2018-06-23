using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Maze_Game
{
    //A class used to make the buffer and game map global for all other classes to use
    static class Buffer
    {
        //Length and width of the game map
        public static int length = 20;
        public static int width = 43;

        //Storages of the game map the char array which holds the logic game map and the string which is for visuals
        public static string buffer = "";
        public static char[,] gameMap = new char[length, width];

        public static void GameMapToBuffer()
        {
            //Reset buffer string to empty
            buffer = "";

            //Nested for loops to store the game map array into the buffer string
            for (int row = 0; row < length; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    buffer += gameMap[row, col];
                }
                buffer += '\n';
            }
        }

        //Draws the game map onto the console window
        public static void DrawGameMap()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(buffer);
        }
    }

    //The player class which holds the logic for the player
    class Player
    {
        //Variables that hold the player's display
        public char display = '$';

        //Variables to control where the player starts
        private static int startingx = 1;
        private static int startingy = 18;

        //Variables that hold the player's current position
        public int x = startingx;
        public int y = startingy;

        //Variables that hold the player's previous position
        public int previousx = startingx;
        public int previousy = startingy;

        //directional movements
        public void MoveUp()
        {
            previousy = y;
            previousx = x;
            y--;
        }
        public void MoveDown()
        {
            previousy = y;
            previousx = x;
            y++;
        }
        public void MoveLeft()
        {
            previousx = x;
            previousy = y;
            x--;
        }
        public void MoveRight()
        {
            previousx = x;
            previousy = y;
            x++;
        }

        //chooses the direction that the player goes
        public void Direction(ConsoleKey direction)
        {
            switch (direction)
            {
                case ConsoleKey.UpArrow:
                    MoveUp();
                    break;
                case ConsoleKey.DownArrow:
                    MoveDown();
                    break;
                case ConsoleKey.LeftArrow:
                    MoveLeft();
                    break;
                case ConsoleKey.RightArrow:
                    MoveRight();
                    break;
                default:
                    break;
            }
        }

        //Draws the player on the game map at its current position
        public void DrawToGameMap()
        {
            //y and x are swapped so that it plots correctly on the game map just pretend its like normal coords
            Buffer.gameMap[y, x] = display;
        }

        //Deletes the player on the game map at its previous position
        public void Delete()
        {
            //y and x are swapped so that it plots correctly on the game map just pretend its like normal coords
            Buffer.gameMap[previousy, previousx] = ' ';
        }

        //Collision detection for the player
        public void PlayerCollision()
        {
            if (Buffer.gameMap[y, x] == '#')
            {
                x = previousx;
                y = previousy;
            }
            else if (Buffer.gameMap[y, x] == 'U')
            {
                Environment.Exit(0);
            }
        }
    }

    //Wall class to hold the game logic for the wall
    static class Wall
    {
        //Variables that hold the wall's display 
        public static char display = '#';

        //A function that draws the walls onto the game map
        public static void DrawToGameMap()
        {
            for (int row = 0; row < Buffer.length; row++)
            {
                for (int col = 0; col < Buffer.width; col++)
                {
                    if (col == 0 || col == Buffer.width - 1 || row == 0 || row == Buffer.length - 1)
                    {
                        Buffer.gameMap[row, col] = '#';
                    }
                    else
                    {
                        Buffer.gameMap[row, col] = ' ';
                    }
                }
            }
            for (int i = 16; i < 19; i++)
            {
                Buffer.gameMap[i, 5] = '#';
            }

            for (int i = 0; i < 37; i++)
            {
                Buffer.gameMap[14, i] = '#';
            }
        }
    }

    class Enemy 
    {
        //Display character for the enemy
        public char display;

        //Current coordinates for the enemy
        public int x;
        public int y;

        //Previous coordinates for the enemy
        public int previousx;
        public int previousy;

        public void Move()
        {
        }

        public void EnemyCollision()
        {
        }

        public void DrawToGameMap()
        {
            Buffer.gameMap[y, x] = display;
        }

        public void Delete()
        {
            Buffer.gameMap[previousy, previousx] = ' ';
        }
    }

    //Class for the up down enemy
    class UpDownEnemy : Enemy
    {
        //Boolean to check if the up down enemy is going up or down
        public bool goingUp = true;

        //Method to make the up down enemy go up or down
        public new void Move()
        {
            if (goingUp == true)
            {
                previousx = x;
                previousy = y;
                y--;
            }
            else
            {
                previousx = x;
                previousy = y;
                y++;
            }
        }

        public new void EnemyCollision()
        {
            if (Buffer.gameMap[y, x] == '#')
            {
                x = previousx;
                y = previousy;
                goingUp = !goingUp;
            }
            else if (Buffer.gameMap[y, x] == '$')
            {
                Environment.Exit(0);
            }
        }

        public UpDownEnemy(int startingx1, int startingy1, bool upOrDown)
        {
            display = 'U';
            x = startingx1;
            y = startingy1;
            previousx = startingx1;
            previousy = startingy1;
            goingUp = upOrDown;
        }
    }

    //Main class
    class Program
    {
        static object Baton = new object();

        static void SetUpConsoleWindow()
        {
            //Sets up the basics of the console window
            Console.Title = "Maze Game";
            Console.CursorVisible = false;
        }

        static void EnemiesMovement()
        {
            List<UpDownEnemy> Enemies = new List<UpDownEnemy>();

           
            Enemies.Add(new UpDownEnemy(7, 15, true));
            Enemies.Add(new UpDownEnemy(17, 2, true));
            Enemies.Add(new UpDownEnemy(3, 9, true));
            Enemies.Add(new UpDownEnemy(8, 5, true));
            Enemies.Add(new UpDownEnemy(14, 7, true));



            //UpDownEnemy upDownEnemy1 = new UpDownEnemy(5, 5, true);


            while (true)
            {
                for (int i = 0; i < Enemies.Count; i++) 
                {
                    Enemies[i].Move();
                    Enemies[i].EnemyCollision();

                    Enemies[i].Delete();
                    Enemies[i].DrawToGameMap();
                }
                lock (Baton)
                {
                    Buffer.GameMapToBuffer();
                    Buffer.DrawGameMap();
                }
                Thread.Sleep(250);
            }
        }
        //Main program
        static void Main(string[] args)
        {
            SetUpConsoleWindow();

            ConsoleKey input;
            Player Player = new Player();

            Wall.DrawToGameMap();
            Player.DrawToGameMap();
            Buffer.GameMapToBuffer();
            Buffer.DrawGameMap();

            Thread EnemyThread = new Thread(EnemiesMovement);
            EnemyThread.Start();



            //Game Loop
            while (true)
            {
                while (!Console.KeyAvailable)
                {
                }
                input = Console.ReadKey().Key;

                Player.Direction(input);
                Player.PlayerCollision();
                Player.Delete();
                Player.DrawToGameMap();
                lock (Baton)
                {
                    Buffer.GameMapToBuffer();
                    Buffer.DrawGameMap();
                }

            }
        }
    }
}