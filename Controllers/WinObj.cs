using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MethodHelper.BD;

namespace MethodHelper.Controllers
{
    internal class WinObj
    {
        public static app_settings settings;

        public static string generateToken()
        {
            Random random = new Random();
            char[] tokenMas = new char[]
            {
                    'A','b','c','D','e','F',
                    'f','J','j','w','W','g',
                    '0','1','2','C','V','G',
                    '9','5','3','7','#','@',
                    '>','N','q','Q','L','!',
                    '<','.','s','S','Z','z',
                    '=','o','O','a','P','p',
                    '-','n','i','I','4','8'
            };

            string token = "";

            for (int i = 0; i <= 32; i++)
            {
                int ti = random.Next(0, tokenMas.Length - 1);
                token += tokenMas[ti];
            }

            var check_token = Connect.data.users.Where(x => x.token == token).FirstOrDefault();
            
            if (check_token == null)
            {
                return token.ToString();
            }
            return "";
        }

        public static void fatalError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Fatal error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
