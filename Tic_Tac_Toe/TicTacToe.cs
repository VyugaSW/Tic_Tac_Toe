using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe
{
    interface IPlayer
    {
        string Sign { get; set; }
        String Name { get; set; }
        
        int[] Step();
    }

    internal class Bot : IPlayer
    {
        public string Sign { get; set; }
        public string Name { get; set; }

        public int[] Step()
        {
            Random random = new Random();
            int[] arrXY = new int[2];
            arrXY[0] = random.Next(1, 4);
            arrXY[1] = random.Next(1, 4);


            return arrXY;
        }
    }

    internal class Human : IPlayer
    {
        public string Sign { get; set; }
        public string Name { get; set; }

        public int[] Step()
        {
            Console.WriteLine("Choose the next step:");
            int[] arrXY = new int[2];
            try
            {
                Console.Write("X: ");
                arrXY[0] = int.Parse(Console.ReadLine());

                if (arrXY[0] > 3 || arrXY[0] < 0)
                    throw new IndexOutOfRangeException();

                Console.Write("Y: ");
                arrXY[1] = int.Parse(Console.ReadLine());

                if (arrXY[1] > 3 || arrXY[0] < 0)
                    throw new IndexOutOfRangeException();
            }
            catch (FormatException)
            {
                throw;
            }

            return arrXY;
        }
    }


    internal class MainTTT
    {
        private List<IPlayer> ListOfPlayers { get; set; }
        FieldTTT field = new FieldTTT();

        public MainTTT(IPlayer PlayerOne, IPlayer PlayerTwo)
        {
            field = new FieldTTT();
            ListOfPlayers = new List<IPlayer>
            {
                PlayerOne,
                PlayerTwo
            };

        }


        public int Game() 
        {
            int i = 0;
            try
            {
                bool fGame = Start();
                while (fGame)
                {

                    Console.Clear();
                    Console.WriteLine(field.ShowField());

                    if (field.UpdateField(ListOfPlayers[i].Step(), ListOfPlayers[i].Sign) == 0)
                    {
                        if (ListOfPlayers[i] is Human)
                        {
                            Console.WriteLine("!Wrong! Try again\nEnter any key to repeat the step...  ");
                            Console.ReadKey();
                        }
                        continue;
                    }

                    if (CheckWin())
                    {
                        Console.Clear();
                        Console.WriteLine(field.ShowField());
                        break;
                    }

                    else
                    {
                        if (i == 0)
                            i++;
                        else
                            i--;
                    }

                }
                End(ListOfPlayers[i].Name);
            }

            catch (FormatException)
            {
                Console.WriteLine("Format Exception!");
                throw;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Index out of range exception!");
                throw;
            }

            return 1;

        }


        private bool Start()
        {
            ListOfPlayers[0].Sign = "X";
            ListOfPlayers[1].Sign = "O";
            
            int k = 0;
            Console.WriteLine("Would you like to start?");
            Console.WriteLine("1 - yes    0 - no");

            try
            {
                k = int.Parse(Console.ReadLine());
                if (k != 1 && k != 0)
                    throw new FormatException();
            }
            catch (FormatException)
            {
                throw;
            }

            if (k == 1)
                return true;
            return false;
        }
        private void End(string name)
        {
            Console.WriteLine($"{name} have been won!\n");

            Console.WriteLine("Enter any key to continue...");
            Console.ReadKey();

        }

        private bool CheckWin()
        {
            if(((field.Field[1,2] == field.Field[1,4] && field.Field[1, 4] == field.Field[1,6]) &&
                field.Field[1, 2] != "/") ||
                (field.Field[2, 2] == field.Field[2, 4] && field.Field[2, 4] == field.Field[2, 6] &&
                field.Field[2, 2] != "/") ||
                (field.Field[3, 2] == field.Field[3, 4] && field.Field[3, 4] == field.Field[3, 6] &&
                field.Field[3, 2] != "/") ||
                (field.Field[1, 2] == field.Field[2, 2] && field.Field[2, 2] == field.Field[3, 2] &&
                field.Field[1, 2] != "/") ||
                (field.Field[1, 4] == field.Field[2, 4] && field.Field[2, 4] == field.Field[3, 4] &&
                field.Field[1, 4] != "/") ||
                (field.Field[1, 6] == field.Field[2, 6] && field.Field[2, 6] == field.Field[3, 6] &&
                field.Field[1, 6] != "/") ||
                (field.Field[1, 2] == field.Field[2, 4] && field.Field[2, 4] == field.Field[3, 6] &&
                field.Field[1, 2] != "/") ||
                (field.Field[3, 2] == field.Field[2, 4] && field.Field[2, 4] == field.Field[1, 6] &&
                field.Field[3, 2] != "/")) 
                return true;
            return false;
        }

    }


    internal class FieldTTT
    {
        string[,] _field;

        public FieldTTT()
        {
            _field = new string[4, 8]
            {
                {"0  ", " ","1"," ","2"," ","3"," " },
                {"1  ", "|","/","|","/","|","/","|" },
                {"2  ", "|","/","|","/","|","/","|" },
                {"3  ", "|","/","|","/","|","/","|" }
            };
        }
        public string[,] Field 
        { 
            get { return _field; } 
            set { _field = value; } 
        }

        public string ShowField()
        {
            string fieldStr = "";
            for (int i = 0; i < _field.GetLength(0); i++)
            {
                for (int j = 0; j < _field.GetLength(1); j++)
                    fieldStr += _field[i, j];
                fieldStr += '\n';
            }
            return fieldStr;
        }
        public int UpdateField(int[] arrXY, string sign)
        {

            if (arrXY[0] == 1 && _field[arrXY[1], arrXY[0] + 1] == "/")
                _field[arrXY[1], arrXY[0] + 1] = sign;
            else if (arrXY[0] == 2 && _field[arrXY[1], arrXY[0] + 2] == "/")
                _field[arrXY[1], arrXY[0] + 2] = sign;
            else if (arrXY[0] == 3 && _field[arrXY[1], arrXY[0] + 3] == "/")
                _field[arrXY[1], arrXY[0] + 3] = sign;
            else
                return 0;

            return 1;
        }
    }

}
