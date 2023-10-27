/*

Algorithm that converts a decimal number of less than nine million to an English language representation of that number.

So for example:

Given the input '3346780.54', the output should be "Three million, three hundred and forty six thousand, seven hundred and eighty, point fifty four.

Steps for algorithm:
    Step 1: check for decimal point in number.
    Step 2: Split number on decimal point and call function to convert to string.
    Step 3: In function to converto string take chunks of three digit from number and create strings of number based on their decimal position.
    Step 4: Merge string of letter before and after decimal points based on if they have value before and after desimal position.
    Step 5: Capitilise initial character.
    Step 6: Print result.


*/

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
                //seperating two digits number and converting
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

        static string ConvertNumberToWords(int number)
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
                //dividing number into chunks of three and converting 

                while (number / 100 > 0)
                {
                    lastThreeDigits = number % 1000;
                    numAtHundred = lastThreeDigits / 100;
                    result = twoDigitsToWords(numAtHundred) +
                      " hundred " + twoDigitsToWords(lastThreeDigits % 100) +
                      powerPosition[count] + result;
                    count++;
                    number /= 1000;

                }
                //converting if last chunk has less than three digits
                if (number > 0)
                {
                    result = twoDigitsToWords(number) + " " + powerPosition[count] + result;
                }
            }

            return result;
        }

        static string ConvertDecimalToWords(decimal number)
        {
            //seperating number before and after decimal
            string[] parts = number.ToString("").Split('.');
            int integerPart = int.Parse(parts[0]);
            int fractionalPart = 0;
            if (parts.Length > 1)
                fractionalPart = int.Parse(parts[1]);

            string integerWords = ConvertNumberToWords(integerPart);
            string fractionalWords = ConvertNumberToWords(fractionalPart);
            //removing appending comma
            fractionalWords = fractionalWords.Remove(fractionalWords.Length - 2);

            string result = string.Empty;
            //merging string based on value present before and after decimal
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
                Console.WriteLine("English language representation of " +
                  inputNumber.ToString() + " is " +
                  words.First().ToString().ToUpper() +
                  String.Join("", words.Skip(1)));
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }

        }

    }
}