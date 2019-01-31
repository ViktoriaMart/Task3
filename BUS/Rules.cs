using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BUS
{
    public class Rules
    {
        public static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]");
            return !regex.IsMatch(text);
        }

        public static bool IsCreditDataValid(string creditNumber, string creditPassword)
        {
            return IsCreditNumberValid(creditNumber) & IsCreditPasswordValid(creditPassword);
        }

        static bool IsCreditNumberValid(string creditNumber)
        {
            if (creditNumber.Length != 16)
            {
                throw new Exception("Неправильно введено номер картки");
            }
            else
            {
                return true;
            }
        }

        static bool IsCreditPasswordValid(string creditPassword)
        {
            if (creditPassword.Length != 4)
            {
                throw new Exception("Неправильно введено пароль");
            }
            else
            {
                return true;
            }
        }

        public static bool IsInputCashDataValid(string inputCash, double sum)
        {
            if (inputCash == String.Empty)
            {
                throw new Exception("Не введено внесені кошти");
            }
            else if (Double.Parse(inputCash) < sum)
            {
                throw new Exception("Недостатньо коштів");
            }
            else
            {
                return true;
            }
        }
    }
}
