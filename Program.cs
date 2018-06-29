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
        private static int startingx = 37;
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
            else if (Buffer.gameMap[y, x] == 'L')
            {
                Environment.Exit(0);
            }
            else if (Buffer.gameMap[y, x] == 'R')
            {
                Environment.Exit(0);
            }
            else if (Buffer.gameMap[y, x] == 'C')
            {
                x = previousx;
                y = previousy;
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
        public void Move()
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

        public void EnemyCollision()
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

    //Class for the left right enemy
    class LeftRightEnemy : Enemy
    {
        //Boolean to check if the left right enemy is going left or right
        public bool goingLeft = true;

        //Method to make the left right enemy go left or right
        public void Move()
        {
            if (goingLeft == true)
            {
                previousx = x;
                previousy = y;
                x--;
            }
            else
            {
                previousx = x;
                previousy = y;
                x++;
            }
        }

        public void EnemyCollision()
        {
            if (Buffer.gameMap[y, x] == '#')
            {
                x = previousx;
                y = previousy;
                goingLeft = !goingLeft;
            }
            else if (Buffer.gameMap[y, x] == '$')
            {
                Environment.Exit(0);
            }
        }

        public LeftRightEnemy(int startingx1, int startingy1, bool leftOrRight)
        {
            display = 'L';
            x = startingx1;
            y = startingy1;
            previousx = startingx1;
            previousy = startingy1;
            goingLeft = leftOrRight;
        }
    }


    //Class for the random enemy
    class RandomEnemy : Enemy
    {
        static Random randNum = new Random();
        
        //Method to make the random enemy move
        public void Move()
        {
            switch (randNum.Next(1, 5))
            { 
                case 1:
                    previousx = x;
                    previousy = y;
                    x--;
                    break;
                case 2:
                    previousx = x;
                    previousy = y;
                    x++;
                    break;
                case 3:
                    previousx = x;
                    previousy = y;
                    y--;
                    break;
                case 4:
                    previousx = x;
                    previousy = y;
                    y++;
                    break;
                default:
                    break;
            }
        }

        public void EnemyCollision()
        {
            if (Buffer.gameMap[y, x] == '#')
            {
                x = previousx;
                y = previousy;
                
            }
            else if (Buffer.gameMap[y, x] == '$')
            {
                Environment.Exit(0);
            }
            else if (Buffer.gameMap[y, x] == 'R')
            { 
                x = previousx;
                y = previousy;
            }
            else if (Buffer.gameMap[y, x] == 'L')
            {
                x = previousx;
                y = previousy;
            }
            else if (Buffer.gameMap[y, x] == 'U')
            {
                x = previousx;
                y = previousy;
            }
        }

        public RandomEnemy(int startingx1, int startingy1)
        {
            display = 'R';
            x = startingx1;
            y = startingy1;
            previousx = startingx1;
            previousy = startingy1;
        }
    }

    class Bullet : Enemy
    {
        bool bulletAlive = true;
        //Method to make the up down enemy go up or down
        public void Move()
        {
            if (bulletAlive == true)
            {
                previousx = x;
                previousy = y;
                y++;
            }
        }

        public void EnemyCollision()
        {
            if (Buffer.gameMap[y, x] == '#')
            {
                Buffer.gameMap[previousy, previousx] = ' ';
                x = 0;
                y = 0;
                previousx = 0;
                previousy = 0;
                display = '#';
            }
            else if (Buffer.gameMap[y, x] == '$')
            {
                Console.WriteLine("YOU WERE HIT");
            }
        }

        public Bullet(int startingx1, int startingy1)
        {
            display = '•';
            x = startingx1;
            y = startingy1;
            previousx = startingx1;
            previousy = startingy1;
        }
    }

    //Class for the random enemy
    class CannonEnemy : Enemy
    {
        public List<Bullet> Bullets = new List<Bullet>();
        public int bulletTimer = 0;
        public int bulletTimerMax = 3;
        public int bulletCount = 0;


        //Method to make the cannon shoot
        public void Shoot()
        {
            Bullets.Add(new Bullet(x, y));
            bulletCount++;
        }

        public CannonEnemy(int startingx1, int startingy1)
        {
            display = 'C';
            x = startingx1;
            y = startingy1;
            previousx = startingx1;
            previousy = startingy1;
        }
    }
    
    //Main class
    class Program
    {
        static object Baton = new object();

        static void Instructions()
        {
            string space = "                                           ";
            string instructions = "Welcome to the Maze Game\n" + space +
                "Use the arrow keys to control your player\n" + space +
                "Reach the goal post to win\n" + space +
                "If you get hit by the enemies you will die\n";
            Console.SetCursorPosition(43, 0);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(instructions);
            Console.ResetColor();
        }

        static void SetUpConsoleWindow()
        {
            //Sets up the basics of the console window
            Console.Title = "Maze Game";
            Console.CursorVisible = false;
            Console.SetWindowSize(50, 22);
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }

        static void EnemiesMovement()
        {
            List<UpDownEnemy> UpDownEnemies = new List<UpDownEnemy>();
            List<LeftRightEnemy> LeftRightEnemies = new List<LeftRightEnemy>();
            List<RandomEnemy> RandomEnemies = new List<RandomEnemy>();
            List<CannonEnemy> CannonEnemies = new List<CannonEnemy>();

            UpDownEnemies.Add(new UpDownEnemy(7, 15, true));
            UpDownEnemies.Add(new UpDownEnemy(17, 2, true));
            UpDownEnemies.Add(new UpDownEnemy(3, 9, true));
            UpDownEnemies.Add(new UpDownEnemy(8, 5, true));
            UpDownEnemies.Add(new UpDownEnemy(14, 7, true));
            LeftRightEnemies.Add(new LeftRightEnemy(10, 5, true));
            LeftRightEnemies.Add(new LeftRightEnemy(12, 15, true));
            LeftRightEnemies.Add(new LeftRightEnemy(13, 12, true));
            LeftRightEnemies.Add(new LeftRightEnemy(14, 13, true));
            LeftRightEnemies.Add(new LeftRightEnemy(18, 17, true));
            RandomEnemies.Add(new RandomEnemy(4, 4));
            RandomEnemies.Add(new RandomEnemy(5, 5));
            RandomEnemies.Add(new RandomEnemy(4, 7));
            RandomEnemies.Add(new RandomEnemy(4, 9));
            RandomEnemies.Add(new RandomEnemy(4, 13));
            CannonEnemies.Add(new CannonEnemy(40, 1));
            CannonEnemies.Add(new CannonEnemy(41, 2));
            CannonEnemies.Add(new CannonEnemy(39, 3));
            CannonEnemies.Add(new CannonEnemy(38, 4));
            CannonEnemies.Add(new CannonEnemy(37, 5));

            CannonEnemies[0].DrawToGameMap();
            CannonEnemies[1].DrawToGameMap();
            CannonEnemies[2].DrawToGameMap();
            CannonEnemies[3].DrawToGameMap();
            CannonEnemies[4].DrawToGameMap();
            
            while (true)
            {
                for (int i = 0; i < UpDownEnemies.Count; i++)
                {
                    UpDownEnemies[i].Move();
                    UpDownEnemies[i].EnemyCollision();
                    LeftRightEnemies[i].Move();
                    LeftRightEnemies[i].EnemyCollision();
                    RandomEnemies[i].Move();
                    RandomEnemies[i].EnemyCollision();
                    
                    if (CannonEnemies[i].bulletTimer == CannonEnemies[i].bulletTimerMax)
                    { 
                        CannonEnemies[i].Shoot();
                        CannonEnemies[i].bulletTimer = 0;
                    }
                    else
                    {
                        CannonEnemies[i].bulletTimer++;
                    }

                    for (int j = 0; j < CannonEnemies[i].bulletCount; j++)
                    {
                        CannonEnemies[i].Bullets[j].Move();
                    }

                    for (int j = 0; j < CannonEnemies[i].bulletCount; j++)
                    {
                        CannonEnemies[i].Bullets[j].EnemyCollision();
                    }


                    UpDownEnemies[i].Delete();
                    UpDownEnemies[i].DrawToGameMap();
                    LeftRightEnemies[i].Delete();
                    LeftRightEnemies[i].DrawToGameMap();
                    RandomEnemies[i].Delete();
                    RandomEnemies[i].DrawToGameMap();

                    for (int j = 0; j < CannonEnemies[i].bulletCount; j++)
                    {
                        CannonEnemies[i].Bullets[j].Delete();
                    }

                    for (int j = 0; j < CannonEnemies[i].bulletCount; j++)
                    {
                        CannonEnemies[i].Bullets[j].DrawToGameMap();
                    }

                    CannonEnemies[i].DrawToGameMap();
                    
                }
                lock (Baton)
                {
                    Buffer.GameMapToBuffer();
                    Buffer.DrawGameMap();
                }
                Thread.Sleep(200);
            }
        }

        //Main program
        static void Main(string[] args)
        {
            SetUpConsoleWindow();
            Instructions();

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