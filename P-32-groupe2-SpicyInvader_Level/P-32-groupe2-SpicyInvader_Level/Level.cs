/*ETML
Author  : Merk Yann
Date    : 29.08.2016
Summary : Level Class for the Spicy Invader Project (P32_Dev)
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SpicyInvader
{
    public class Constant
    {
        public class Level
        {
            /// <summary>
            /// Default color used
            /// </summary>
            public const ConsoleColor DEFAULT_COLOR = ConsoleColor.White;

            public const int WINDOWS_WIDTH = 80;

            public const int WINDOWS_HEIGHT = 30;

            public class Barricades
            {

                public static string[] LIFE_STRING = new string[4] { " ", "x", "#", "█" };

                /// <summary>
                /// Give the look of the barricades, and his life points
                /// </summary>
                public static int[,] life = new int[3, 5]{{0,3,3,3,0},
                {3,3,3,3,3},
                {3,0,0,0,3}};

                /// <summary>
                /// Barricades origin coords, x,y   x = Barricade number, y = x/y
                /// </summary>
                public static int[,] coords = new int[4, 2] { { 15, WINDOWS_HEIGHT - 8 }, { 30, WINDOWS_HEIGHT - 8 }, { 45, WINDOWS_HEIGHT - 8 }, { 60, WINDOWS_HEIGHT - 8 } };
            }

            public const int ID_BARRICADE = 3;
            public const int ID_PLAYER = 1;
            public const int ID_ENNEMY = 2;
        }
    }

    public class Level
    {

        public static Mutex mut = new Mutex();

        /// <summary>
        /// Contains a Grid with all the object on the terrain, 0 = null, 1 = Player, 2 = Ennemy, 3 = Barricade
        /// </summary>
        public static int[,] objectPos = new int[Constant.Level.WINDOWS_HEIGHT, Constant.Level.WINDOWS_WIDTH];

        public class Baricade
        {
            int xPos = 0;
            int yPos = 0;
            /// <summary>
            /// Stock the life of every bloc of the barricade, x,y   x = ligne, y = row
            /// </summary>
            int[,] blocsLife = new int[1, 1] { { 0 } };

            public Baricade(int _xPos, int _yPos)
            {
                this.xPos = _xPos;
                this.yPos = _yPos;
                //Copy the base life values to the correct array
                blocsLife = new int[Constant.Level.Barricades.life.GetLength(0), Constant.Level.Barricades.life.GetLength(1)];

                for (int i = 0; i < blocsLife.GetLength(0); i++)
                {
                    for (int j = 0; j < blocsLife.GetLength(1); j++)
                    {
                        blocsLife[i, j] = Constant.Level.Barricades.life[i, j];
                    }
                }

                Refresh();
            }

            /// <summary>
            /// Rewrite the baricade
            /// </summary>
            public void Refresh()
            {
                //Go trough all the cases to write the string of the blocs according to their life
                string[] lineStringArray = new string[blocsLife.GetLength(0)];
                for (int iLine = 0; iLine < blocsLife.GetLength(0); iLine++)
                {
                    for (int iRow = 0; iRow < blocsLife.GetLength(1); iRow++)
                    {
                        lineStringArray[iLine] += Constant.Level.Barricades.LIFE_STRING[blocsLife[iLine, iRow]];
                    }
                }

                //Set the hitbox of the barricade
                SetHitBox(xPos, yPos, lineStringArray, Constant.Level.ID_BARRICADE);

                //Write the barricade
                Write(xPos, yPos, lineStringArray, ConsoleColor.DarkGreen);

            }

            /// <summary>
            /// Check if a shot that land here would damage us
            /// </summary>
            /// <param name="_xCoord">x Coord</param>
            /// <param name="_yCoord">y Coord</param>
            /// <returns>True if touched, else false</returns>
            public bool Hit(int _xCoord, int _yCoord)
            {
                //check if the hit arrived in our coords
                _xCoord -= xPos;
                _yCoord -= yPos;

                //X and Y are "inverted" in the blocslife array
                if (_xCoord < blocsLife.GetLength(1) && _yCoord < blocsLife.GetLength(0) && _xCoord >= 0 && _yCoord >= 0)
                {
                    //check if that hit an alive bloc
                    if (blocsLife[_yCoord, _xCoord] > 0)
                    {
                        //if yes, decrease the life of the bloc, refresh the view, and return true
                        blocsLife[_yCoord, _xCoord]--;
                        Refresh();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

        }

        public static Baricade[] barricades;

        public Level(int _nbLevel, int _difficulty)
        {

        }

        /// <summary>
        /// Write a string on the Console
        /// </summary>
        /// <param name="_coords"> Coords to write the string, in {Top, Left}</param>
        /// <param name="_x">x coords</param>
        /// <param name="_y">y coords</param>
        /// <param name="_color"> Color of the object to write, default set in constant</param>
        public static void Write(int _x, int _y, string[] _object, ConsoleColor _color = Constant.Level.DEFAULT_COLOR)
        {

            mut.WaitOne();
            //Set the color
            Console.ForegroundColor = _color;

            //Go trough all the text
            for (int i = 0; i < _object.Length; i++)
            {
                //Set the cursor to the correct position
                Console.SetCursorPosition(_x, _y + i);

                //Write the line
                Console.Write(_object[i]);
            }

            mut.ReleaseMutex();

        }

        /// <summary>
        /// Works like the write, but in the Level.objectPos array
        /// </summary>
        /// <param name="_x">x coords</param>
        /// <param name="_y">y coords</param>
        /// <param name="_object">string array used to get the size of the object</param>
        public static void Erase(int _x, int _y, string[] _object)
        {
            //Go trough the lines of the object
            for (int line = 0; line < _object.Length; line++)
            {
                mut.WaitOne();
                //set the cursor to the corect place and erase
                Console.SetCursorPosition(_x, _y + line);

                //Go trough the rows of the object
                for (int row = 0; row < _object[line].Length; row++)
                {

                    Console.Write(" ");

                }
                mut.ReleaseMutex();
            }

        }

        /// <summary>
        /// Works like the write, but in the Level.objectPos array
        /// </summary>
        /// <param name="_x">x coords</param>
        /// <param name="_y">y coords</param>
        /// <param name="_object">string array used to get the size of the object</param>
        /// <param name="_type">ID of the object type</param>
        public static void SetHitBox(int _x, int _y, string[] _object, int _type)
        {

            //Go trough the lines of the object
            for (int line = 0; line < _object.Length; line++)
            {
                //Go trough the rows of the object
                for (int row = 0; row < _object[line].Length; row++)
                {
                    //if this isn't a space, set the char
                    if (_object[line][row] != ' ')
                    {
                        //Set the correct values
                        objectPos[line + _y, row + _x] = _type;
                    }
                    else
                    {
                        //if this is a space, set 0
                        objectPos[line + _y, row + _x] = 0;
                    }

                }
            }

        }

        /// <summary>
        /// Refresh all the barricades
        /// </summary>
        public static void RefreshAllBaricades()
        {
            foreach (Baricade barricade in barricades)
            {
                barricade.Refresh();
            }
        }

        /// <summary>
        /// Initialize all the barricades
        /// </summary>
        public static void InitBaricades()
        {
            barricades = new Baricade[4];
            for (int i = 0; i < Constant.Level.Barricades.coords.GetLength(0); i++)
            {
                barricades[i] = new Baricade(Constant.Level.Barricades.coords[i, 0], Constant.Level.Barricades.coords[i, 1]);
            }
        }

        /// <summary>
        /// Works exactly like the SetHitBox, but set a value of 0 in the array
        /// </summary>
        /// <param name="_x">x coords</param>
        /// <param name="_y">y coords</param>
        /// <param name="_object">string array used to get the size of the object</param>
        public static void RemoveHitBox(int _x, int _y, string[] _object)
        {
            //Go trough the lines of the object
            for (int line = 0; line < _object.Length; line++)
            {
                //Go trough the rows of the object
                for (int row = 0; row < _object[line].Length; row++)
                {
                    //Set the correct values
                    objectPos[line + _y, row + _x] = 0;
                }
            }
        }

        /// <summary>
        /// Check if there's an object at those coords
        /// </summary>
        /// <param name="_x">x coords</param>
        /// <param name="_y">y coords</param>
        /// <returns>the Type of the object, or null if no object here hit</returns>
        public static int? CheckIfObjectHere(int _x, int _y)
        {

            if (objectPos[_y, _x] > 0)
            {
                return objectPos[_y, _x];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Call all the baricades to try to hit them
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        public static void ShootBaricade(int _x, int _y)
        {
            //test on all baricades
            foreach (Baricade b in barricades)
            {
                b.Hit(_x, _y);
            }
        }
    }

}