/*

Algorithm to Convert Decimal Number to English Language Representation

Input: A decimal number less than nine million.

Output: An English language representation of the input number.

Steps:

step 1: Check if the input number contains a decimal point.
step 2: If there is a decimal point, split the number into its integer and decimal parts.
step 3: Create a function to convert a number (up to three digits) to its English language representation.
step 4: In the conversion function, take chunks of three digits from the integer part of the number and create strings for each chunk based on their decimal position (i.e., units, thousands, millions).
step 5: Merge the strings for the chunks, ensuring proper punctuation and formatting (e.g., using commas and "and" appropriately).
step 6: If there is a decimal part, convert it to its English representation.
step 7: Capitalize the initial character of the resulting representation.
step 8: Print or return the final English representation of the input number.


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