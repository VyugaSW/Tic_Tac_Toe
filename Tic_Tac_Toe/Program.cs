using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe
{
    internal class Program
    {

        static void Main(string[] args)
        {
            MainTTT main = new MainTTT(new Human { Name = "Carl" }, new Bot { Name = "BOT" });
            //MainTTT main = new MainTTT(new Human { Name = "Carl" }, new Human { Name = "Carl2" }); if you wanna play with another human :)
            main.Game();



        }
    }
}
