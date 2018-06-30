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
    static class Player
    {
        public enum Movement { Up, Right, Down, Left };
        public static Movement movement = Movement.Up;
      
        //Variables that hold the player's display
        public static char display = '$';

        //Variables to control where the player starts
        private static int startingx = 1;
        private static int startingy = 18;

        //Variables that hold the player's current position
        public static int x = startingx;
        public static int y = startingy;

        //Variables that hold the player's previous position
        public static int previousx = startingx;
        public static int previousy = startingy;

        //directional movements
        public static void MoveUp()
        {
            previousy = y;
            previousx = x;
            y--;
            movement = Movement.Up;
        }
        public static void MoveDown()
        {
            previousy = y;
            previousx = x;
            y++;
            movement = Movement.Down;
        }
        public static void MoveLeft()
        {
            previousx = x;
            previousy = y;
            x--;
            movement = Movement.Left;
        }
        public static void MoveRight()
        {
            previousx = x;
            previousy = y;
            x++; 
            movement = Movement.Right;
        }

        //chooses the direction that the player goes
        public static void Direction(ConsoleKey direction)
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
        public static void DrawToGameMap()
        {
            //y and x are swapped so that it plots correctly on the game map just pretend its like normal coords
            Buffer.gameMap[y, x] = display;
        }

        //Deletes the player on the game map at its previous position
        public static void Delete()
        {
            //y and x are swapped so that it plots correctly on the game map just pretend its like normal coords
            Buffer.gameMap[previousy, previousx] = ' ';
        }

        //Collision detection for the player
        public static void PlayerCollision()
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
            else if (Buffer.gameMap[y, x] == '•')
            {
                Environment.Exit(0);
            }
            else if (Buffer.gameMap[y, x] == 'G')
            {
                Environment.Exit(0);
            }
            else if (Buffer.gameMap[y, x] == '☐')
            {
                for (int i = 0; i < Program.Boxes.Count; i++)
                {
                    if (Program.Boxes[i].x == x && Program.Boxes[i].y == y)
                    {
                        switch (movement)
                        {
                            case Movement.Up:
                                if (Buffer.gameMap[Program.Boxes[i].y - 1, Program.Boxes[i].x] == '#')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y - 1, Program.Boxes[i].x] == ' ')
                                {
                                    Program.Boxes[i].previousx = Program.Boxes[i].x;
                                    Program.Boxes[i].previousy = Program.Boxes[i].y;
                                    Program.Boxes[i].y--;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y - 1, Program.Boxes[i].x] == 'U')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y - 1, Program.Boxes[i].x] == 'L')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y - 1, Program.Boxes[i].x] == 'R')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y - 1, Program.Boxes[i].x] == 'C')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y - 1, Program.Boxes[i].x] == '•')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y - 1, Program.Boxes[i].x] == '☐')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y - 1, Program.Boxes[i].x] == 'G')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                break;
                            case Movement.Down:
                                if (Buffer.gameMap[Program.Boxes[i].y + 1, Program.Boxes[i].x] == '#')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y + 1, Program.Boxes[i].x] == ' ')
                                {
                                    Program.Boxes[i].previousx = Program.Boxes[i].x;
                                    Program.Boxes[i].previousy = Program.Boxes[i].y;
                                    Program.Boxes[i].y++;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y + 1, Program.Boxes[i].x] == 'U')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y + 1, Program.Boxes[i].x] == 'L')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y + 1, Program.Boxes[i].x] == 'R')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y + 1, Program.Boxes[i].x] == 'C')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y + 1, Program.Boxes[i].x] == '•')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y + 1, Program.Boxes[i].x] == '☐')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y + 1, Program.Boxes[i].x] == 'G')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                break;
                            case Movement.Right:
                                if (Buffer.gameMap[Program.Boxes[i].y, Program.Boxes[i].x + 1] == '#')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y, Program.Boxes[i].x + 1] == ' ')
                                {
                                    Program.Boxes[i].previousx = Program.Boxes[i].x;
                                    Program.Boxes[i].previousy = Program.Boxes[i].y;
                                    Program.Boxes[i].x++;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y, Program.Boxes[i].x + 1] == 'U')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y, Program.Boxes[i].x + 1] == 'L')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y, Program.Boxes[i].x + 1] == 'R')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y, Program.Boxes[i].x + 1] == 'C')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y, Program.Boxes[i].x + 1] == '•')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y, Program.Boxes[i].x + 1] == '☐')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y, Program.Boxes[i].x + 1] == 'G')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                break;
                            case Movement.Left:
                                if (Buffer.gameMap[Program.Boxes[i].y, Program.Boxes[i].x - 1] == '#')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y, Program.Boxes[i].x - 1] == ' ')
                                {
                                    Program.Boxes[i].previousx = Program.Boxes[i].x;
                                    Program.Boxes[i].previousy = Program.Boxes[i].y;
                                    Program.Boxes[i].x--;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y, Program.Boxes[i].x - 1] == 'U')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y, Program.Boxes[i].x - 1] == 'L')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y, Program.Boxes[i].x - 1] == 'R')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y, Program.Boxes[i].x - 1] == 'C')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y, Program.Boxes[i].x - 1] == '•')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y, Program.Boxes[i].x - 1] == '☐')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                else if (Buffer.gameMap[Program.Boxes[i].y, Program.Boxes[i].x - 1] == 'G')
                                {
                                    x = previousx;
                                    y = previousy;
                                }
                                break;
                            default:
                                break;
                        }
                        Program.Boxes[i].DrawToGameMap();
                    }
                }
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
            for (int i = 17; i < 19; i++)
            {
                Buffer.gameMap[i, 5] = '#';
            }
            for (int i = 13; i < 18; i++)
            {
                Buffer.gameMap[i, 21] = '#';
            }
            for (int i = 13; i < 18; i++)
            {
                Buffer.gameMap[i, 29] = '#';
            }

            
            Buffer.gameMap[18, 25] = '#';
            Buffer.gameMap[16, 25] = '#';
            Buffer.gameMap[14, 3] = '#';


            for (int i = 22; i < 41; i++)
            {
                Buffer.gameMap[13, i] = '#';
            }
            for (int i = 2; i < 42; i++)
            {
                Buffer.gameMap[10, i] = '#';
            }

            for (int i = 0; i < 22; i++)
            {
                Buffer.gameMap[15, i] = '#';
            }
            for (int i = 11; i < 14; i++)
            {
                Buffer.gameMap[i, 19] = '#';
            }
            for (int i = 12; i < 15; i++)
            {
                Buffer.gameMap[i, 14] = '#';
            }
            for (int i = 11; i < 13; i++)
            {
                Buffer.gameMap[i, 3] = '#';
            }
            for (int i = 4; i < 13; i++)
            {
                Buffer.gameMap[i, 2] = '#';
            }
            for (int i = 1; i < 9; i++)
            {
                Buffer.gameMap[i, 7] = '#';
            }
            for (int i = 2; i < 10; i++)
            {
                Buffer.gameMap[i, 11] = '#';
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
            else if (Buffer.gameMap[y, x] == '☐')
            {
                x = previousx;
                y = previousy;
                goingUp = !goingUp;
            }
            else if (Buffer.gameMap[y, x] == 'L')
            {
                x = previousx;
                y = previousy;
                goingUp = !goingUp;
            }
            else if (Buffer.gameMap[y, x] == 'G')
            {
                x = previousx;
                y = previousy;
                goingUp = !goingUp;
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
            else if (Buffer.gameMap[y, x] == '☐')
            {
                x = previousx;
                y = previousy;
                goingLeft = !goingLeft;
            }
            else if (Buffer.gameMap[y, x] == 'U')
            {
                x = previousx;
                y = previousy;
                goingLeft = !goingLeft;
            }
            else if (Buffer.gameMap[y, x] == 'G')
            {
                x = previousx;
                y = previousy;
                goingLeft = !goingLeft;
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
            else if (Buffer.gameMap[y, x] == '☐')
            {
                x = previousx;
                y = previousy;
            }
            else if (Buffer.gameMap[y, x] == 'G')
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
                Environment.Exit(0);
            }
            else if (Buffer.gameMap[y, x] == 'G')
            {
                Buffer.gameMap[previousy, previousx] = ' ';
                x = 0;
                y = 0;
                previousx = 0;
                previousy = 0;
                display = '#';
            }
            else if (Buffer.gameMap[y, x] == '☐')
            {
                Buffer.gameMap[previousy, previousx] = ' ';
                x = 0;
                y = 0;
                previousx = 0;
                previousy = 0;
                display = '#';
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

        public CannonEnemy(int startingx1, int startingy1, int BulletTimer, int BulletTimerMax)
        {
            display = 'C';
            x = startingx1;
            y = startingy1;
            previousx = startingx1;
            previousy = startingy1;
            bulletTimer = BulletTimer;
            bulletTimerMax = BulletTimerMax;
        }
    }

    class Box : Enemy
    {
        public Box(int startingx1, int startingy1)
        {
            display = '☐';
            x = startingx1;
            y = startingy1;
            previousx = startingx1;
            previousy = startingy1;
        }
    }

    static class Goal
    {
        public static int x = 41;
        public static int y = 9;

        public static char display = 'G';

        public static void Victory()
        {
            Console.SetCursorPosition(30, 10);
            Console.Write("CONGRATULATIONS YOU HAVE WON!!!");
            Console.ReadLine();
        }

        public static void EnemyCollision()
        {
            
        }

        public static void DrawToGameMap()
        {
            Buffer.gameMap[y, x] = display;
        }
        
    }

    //Main class
    class Program
    {
        public static List<Box> Boxes = new List<Box>();

        static object Baton = new object();

        static void Instructions()
        {
            string space = "                                             ";
            string instructions = "Welcome to the Maze Game\n" + space +
                "Use the arrow keys to control your player\n" + space +
                "Reach the goal post to win\n" + space +
                "If you get hit by the enemies you will die\n\n" + space +
                "$  ->  Player character\n" + space +
                "U  ->  Enemy that moves up and down\n" + space +
                "L  ->  Enemy that moves left and right\n" + space +
                "R  ->  Enemy that moves randomly\n" + space +
                "C  ->  Cannon that shoots bullets\n" + space +
                "•  ->  Bullet\n" + space +
                "☐  ->  Box that is moveable\n" + space +
                "G  ->  Goal, reach this point to win the game";

            Console.SetCursorPosition(45, 0);
            //Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(instructions);
            Console.ResetColor();
        }

        static void SetUpConsoleWindow()
        {
            //Sets up the basics of the console window
            Console.Title = "Maze Game";
            Console.CursorVisible = false;
            Console.SetWindowSize(95, 22);
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }

        static void EnemiesMovement()
        {
            List<UpDownEnemy> UpDownEnemies = new List<UpDownEnemy>();
            List<LeftRightEnemy> LeftRightEnemies = new List<LeftRightEnemy>();
            List<RandomEnemy> RandomEnemies = new List<RandomEnemy>();
            List<CannonEnemy> CannonEnemies = new List<CannonEnemy>();

            UpDownEnemies.Add(new UpDownEnemy(6, 18, true));
            UpDownEnemies.Add(new UpDownEnemy(8, 16, false));
            UpDownEnemies.Add(new UpDownEnemy(10, 18, true));
            UpDownEnemies.Add(new UpDownEnemy(12, 16, false));
            UpDownEnemies.Add(new UpDownEnemy(14, 18, true));
            UpDownEnemies.Add(new UpDownEnemy(16, 16, false));
            UpDownEnemies.Add(new UpDownEnemy(18, 18, true));
            UpDownEnemies.Add(new UpDownEnemy(20, 16, false));

            UpDownEnemies.Add(new UpDownEnemy(8, 1, true));
            UpDownEnemies.Add(new UpDownEnemy(10, 1, true));

            UpDownEnemies.Add(new UpDownEnemy(12, 1, false));
            UpDownEnemies.Add(new UpDownEnemy(15, 2, false));
            UpDownEnemies.Add(new UpDownEnemy(18, 8, true));
            UpDownEnemies.Add(new UpDownEnemy(21, 4, true));
            UpDownEnemies.Add(new UpDownEnemy(24, 5, false));
            UpDownEnemies.Add(new UpDownEnemy(27, 7, true));
            UpDownEnemies.Add(new UpDownEnemy(30, 3, false));
            UpDownEnemies.Add(new UpDownEnemy(33, 6, true));
            UpDownEnemies.Add(new UpDownEnemy(36, 6, true));
            UpDownEnemies.Add(new UpDownEnemy(39, 9, false));

            LeftRightEnemies.Add(new LeftRightEnemy(25, 17, true));
            LeftRightEnemies.Add(new LeftRightEnemy(25, 15, false));

            LeftRightEnemies.Add(new LeftRightEnemy(41, 11, true));
            LeftRightEnemies.Add(new LeftRightEnemy(20, 12, false));

            LeftRightEnemies.Add(new LeftRightEnemy(3, 4, true));
            LeftRightEnemies.Add(new LeftRightEnemy(4, 5, true)); 
            LeftRightEnemies.Add(new LeftRightEnemy(5, 6, true));
            LeftRightEnemies.Add(new LeftRightEnemy(6, 7, true));

            LeftRightEnemies.Add(new LeftRightEnemy(12, 2, true));
            LeftRightEnemies.Add(new LeftRightEnemy(15, 4, false));
            LeftRightEnemies.Add(new LeftRightEnemy(39, 6, true));
            LeftRightEnemies.Add(new LeftRightEnemy(30, 8, false));

            RandomEnemies.Add(new RandomEnemy(34, 16));
            RandomEnemies.Add(new RandomEnemy(36, 14));
            RandomEnemies.Add(new RandomEnemy(38, 18));

            RandomEnemies.Add(new RandomEnemy(25, 6));
            RandomEnemies.Add(new RandomEnemy(30, 9));
            RandomEnemies.Add(new RandomEnemy(38, 3));

            CannonEnemies.Add(new CannonEnemy(4, 11, 0, 3));
            CannonEnemies.Add(new CannonEnemy(6, 11, 0, 3));
            CannonEnemies.Add(new CannonEnemy(8, 11, 0, 3));
            CannonEnemies.Add(new CannonEnemy(10, 11, 0, 3));
            CannonEnemies.Add(new CannonEnemy(12, 11, 0, 3));
            
            CannonEnemies.Add(new CannonEnemy(1, 1, 0, 1));

            CannonEnemies.Add(new CannonEnemy(9, 1, 0, 7));

            Boxes.Add(new Box(29, 18));

            Boxes.Add(new Box(41, 13));

            Boxes.Add(new Box(3, 13));

            Boxes.Add(new Box(15, 14));
            Boxes.Add(new Box(16, 14));
            Boxes.Add(new Box(19, 14));
            Boxes.Add(new Box(16, 13));
            Boxes.Add(new Box(18, 13));
            Boxes.Add(new Box(16, 12));
            Boxes.Add(new Box(17, 12));
            Boxes.Add(new Box(17, 11));
            Boxes.Add(new Box(18, 11));

            Boxes.Add(new Box(11, 1));
            Boxes.Add(new Box(14, 3));
            Boxes.Add(new Box(25, 5));
            Boxes.Add(new Box(30, 7));

            CannonEnemies[0].DrawToGameMap();
            CannonEnemies[1].DrawToGameMap();
            CannonEnemies[2].DrawToGameMap();
            CannonEnemies[3].DrawToGameMap();
            CannonEnemies[4].DrawToGameMap();
            CannonEnemies[5].DrawToGameMap();
            CannonEnemies[6].DrawToGameMap();

            while (true)
            {
                for (int i = 0; i < UpDownEnemies.Count; i++)
                {
                    UpDownEnemies[i].Move();
                    UpDownEnemies[i].EnemyCollision();
                    
                    UpDownEnemies[i].Delete();
                    UpDownEnemies[i].DrawToGameMap();
                }
                for (int i = 0; i < LeftRightEnemies.Count; i++)
                {
                    LeftRightEnemies[i].Move();
                    LeftRightEnemies[i].EnemyCollision();

                    LeftRightEnemies[i].Delete();
                    LeftRightEnemies[i].DrawToGameMap();
                }
                for (int i = 0; i < RandomEnemies.Count; i++)
                {
                    RandomEnemies[i].Move();
                    RandomEnemies[i].EnemyCollision();

                    RandomEnemies[i].Delete();
                    RandomEnemies[i].DrawToGameMap();
                }
                for (int i = 0; i < CannonEnemies.Count; i++)
                {
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
                for (int i = 0; i < Boxes.Count; i++)
                {
                    Boxes[i].DrawToGameMap();
                }
                
                Goal.DrawToGameMap();
                
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