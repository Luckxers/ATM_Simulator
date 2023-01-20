using ATM_Console_Application.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Console_Application.App
{
    class Entry
    {
         static void Main(string[] args)
         {
            
            Program ATMapp = new Program();
            ATMapp.InitializeData();
            ATMapp.Run();
         }

    }

}
