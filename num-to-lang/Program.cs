namespace num_to_lang
{
    internal class Program
    {
        static string twoDigitsToWords(int number)
        {
            string[] ones = {
        "one",
        "two",
        "three",
        "four",
        "five",
        "six",
        "seven",
        "eight",
        "nine"
      };
            string[] teens = {
        "eleven",
        "twelve",
        "thirteen",
        "fourteen",
        "fifteen",
        "sixteen",
        "seventeen",
        "eighteen",
        "nineteen"
      };
            string[] tens = {
        "ten",
        "twenty",
        "thirty",
        "forty",
        "fifty",
        "sixty",
        "seventy",
        "eighty",
        "ninety"
      };

            if (number > 10 && number < 20)
            {
                return teens[number - 11];
            }
            else
            {
                int onesDigit = number % 10;
                int tensDigit = number / 10;

                if (tensDigit > 1)
                {
                    if (onesDigit > 0)
                    {
                        return tens[tensDigit - 1] + " " + ones[onesDigit - 1];
                    }
                    else
                    {
                        return tens[tensDigit - 1];
                    }
                }
                else if (onesDigit > 0)
                {
                    return ones[onesDigit - 1];
                }
                else
                {
                    return "";
                }
            }
        }

        static string ConvertToWords(int number)
        {
            string[] powerPosition = {
        ", ",
        " thousand, ",
        " million, "
      };
            int numAtHundred = 0;
            int lastThreeDigits = 0;
            int count = 0;
            string result = "";
            if (number == 0)
                result = "Zero";
            else
            {
                while (number / 100 > 0)
                {
                    lastThreeDigits = number % 1000;
                    numAtHundred = lastThreeDigits / 100;
                    result = twoDigitsToWords(numAtHundred) 
                        + " hundred " + twoDigitsToWords(lastThreeDigits % 100) 
                        + powerPosition[count] + result;
                    count++;
                    number /= 1000;

                }
                if (number > 0)
                {
                    result = twoDigitsToWords(number) + " " + powerPosition[count] + result;
                }
            }

            return result;
        }

        static string ConvertDecimalToWords(decimal number)
        {
            string[] parts = number.ToString("").Split('.');
            int integerPart = int.Parse(parts[0]);
            int fractionalPart = 0;
            if (parts.Length > 1)
                fractionalPart = int.Parse(parts[1]);

            string integerWords = ConvertToWords(integerPart);
            string fractionalWords = ConvertToWords(fractionalPart);
            fractionalWords = fractionalWords.Remove(fractionalWords.Length - 2);

            string result = string.Empty;
            if (integerPart != 0 && fractionalPart != 0)
                result = integerWords + " point " + fractionalWords;
            else if (integerPart == 0 || fractionalPart != 0)
                result = "Zero point " + fractionalWords;
            else
                result = integerWords.Remove(integerWords.Length - 2);

            return result;
        }

        static void Main(string[] args)
        {
            Console.Write("Please enter a decimal number of less than nine million to convert : ");

            string input = Console.ReadLine();
            if (decimal.TryParse(input, out decimal inputNumber))
            {
                string words = ConvertDecimalToWords(inputNumber);
                Console.WriteLine("English language representation of " 
                    + inputNumber.ToString() + " is " 
                    + words.First().ToString().ToUpper() 
                    + String.Join("", words.Skip(1)));
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }

        }

    }
}