using Convertor.API.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convertor.API.Services.Models
{
    public class ConvertProcess : IConvertProcess
    {
        static readonly List<string> firstNumbersStrings = new()
        { 
            "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten",
            "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"
        };

        static readonly List<string> tensStrings = new() 
        { 
            "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninty"
        };

        static readonly List<string> thoMilStrings = new() 
        { 
            "thousand", "million"
        };

        public IConvertNumber ConvertProcessNumber(string number)
        {
            var convertedNumber = new ConvertNumber
            {
                WordNumber = ProcessWords(number)
            };

            return convertedNumber;
    }

        private static string ProcessWords(string number)
        {
            double intNumber;
            double decimalNumber = 0;
            string words = "";

            try
            {
                string[] splitNumber = number.ToString().Split(',');
                intNumber = double.Parse(splitNumber[0]);
                if (splitNumber[1].Length > 2)
                    throw new ArgumentException("Maximum number of cents is 99");

                if (splitNumber[1].Length == 1)
                    splitNumber[1] += "0";

                decimalNumber = double.Parse(splitNumber[1]);
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(ArgumentException))
                    throw;

                intNumber = int.Parse(number);
            }

            var wordsBaseResult = ToWords(intNumber);
            
            if (string.IsNullOrEmpty(wordsBaseResult))
            {
                words = "zero dollars";
            }
            else if (wordsBaseResult == "one")
            {
                words = "one dollar";
            }
            else
            {
                words = wordsBaseResult + " dollars";
            }

            if (decimalNumber <= 0) 
            { 
               return words; 
            }

            if (words != "")
            {
                words += " and ";
            }

            words += ToWords(decimalNumber) + (decimalNumber == 1 ? " cent" : " cents");

            return words;
        }

        private static string ToWords(double number)
        {
            string resultWords = "";
            bool tens = false;
            int power = 12;

            while (power > 3)
            {
                double pow = Math.Pow(10, power);
                if (number >= pow)
                {
                    if (number % pow > 0)
                    {
                        resultWords += ToWords(Math.Floor(number / pow)) + " " + thoMilStrings[(power / 3) - 1] + " ";
                    }
                    else if (number % pow == 0)
                    {
                        resultWords += ToWords(Math.Floor(number / pow)) + " " + thoMilStrings[(power / 3) - 1];
                    }

                    number %= pow;
                }

                power -= 3;
            }

            if (number >= 1000)
            {
                if (number % 1000 > 0)
                {
                    resultWords += ToWords(Math.Floor(number / 1000)) + " thousand ";
                }
                else
                {
                    resultWords += ToWords(Math.Floor(number / 1000)) + " thousand";                   
                }

                number %= 1000;
            }

            if (0 <= number && number <= 999)
            {
                if ((int)number / 100 > 0)
                {
                    resultWords += ToWords(Math.Floor(number / 100)) + " hundred";
                    number %= 100;
                }

                if ((int)number / 10 > 1)
                {
                    if (resultWords != "")
                    {
                        resultWords += " ";
                    }

                    resultWords += tensStrings[(int)number / 10 - 2];
                    tens = true;
                    number %= 10;
                }

                if (number < 20 && number > 0)
                {
                    if (resultWords != "" && tens == false)
                    {
                        resultWords += " ";
                    }

                    resultWords += (tens ? "-" + firstNumbersStrings[(int)number - 1] : firstNumbersStrings[(int)number - 1]);
                    number -= Math.Floor(number);
                }
            }

            return resultWords;
        }
    }
}
