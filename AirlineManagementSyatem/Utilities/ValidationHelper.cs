using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AirlineManagementSyatem.Utilities
{
    public static class InputValidator
    {
        // Username validation
        public static bool IsValidUserName(string userName, out string error)
        {
            error = string.Empty;

            if (string.IsNullOrWhiteSpace(userName))
            {
                error = "Username cannot be empty.";
                return false;
            }

            if (userName.Length < 3 || userName.Length > 30)
            {
                error = "Username must be between 3 and 30 characters.";
                return false;
            }

            // Only letters, numbers, underscore
            if (!Regex.IsMatch(userName, @"^[a-zA-Z0-9_]+$"))
            {
                error = "Username can contain only letters, numbers, and underscore.";
                return false;
            }

            return true;
        }

        // Password validation
        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                //Each keystroke from the user, replaces with astrick *
                //and add it to the password string
                // until the user presses the enter key

                if (key.Key != ConsoleKey.Enter
                    && key.Key != ConsoleKey.Backspace)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace
                    && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b"); // represents backspace
                }

            } while (key.Key != ConsoleKey.Enter); // Exit password
            Console.WriteLine();
            return password;

        }
        public static bool IsValidPassword(string password, out string error)
        {
            error = string.Empty;

            if (string.IsNullOrWhiteSpace(password))
            {
                error = "Password cannot be empty.";
                return false;
            }

            if (password.Length < 6)
            {
                error = "Password must be at least 6 characters long.";
                return false;
            }

            // At least one letter
            if (!Regex.IsMatch(password, @"[a-zA-Z]"))
            {
                error = "Password must contain at least one letter.";
                return false;
            }

            // At least one digit
            if (!Regex.IsMatch(password, @"[0-9]"))
            {
                error = "Password must contain at least one digit.";
                return false;
            }

            return true;
        }


    }
}
