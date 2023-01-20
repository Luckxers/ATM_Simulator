using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Console_Application.UI
{
    public static class Validator
    {
        public static T Convert<T>(string prompt) // -> Generic
        {
            bool valid = false;
            string userInput;


            while (!valid)
            {

                userInput = Utility.GetUser(prompt);

                try
                {
                   var converter = TypeDescriptor.GetConverter(typeof(T));

                    if (converter != null)
                    {
                        return (T)converter.ConvertFromString(userInput);
                    }
                    else
                    {
                        return default;
                    }
                }
                catch
                {
                    Utility.PrintMessage("Invalid input.\nTry again.", false);
                }
            }

            return default;
        }
    }
}
