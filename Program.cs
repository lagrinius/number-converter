using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;

namespace ConsoleApp1
{
    internal class Program
    {

        public enum NumberFormatEnum
        {
            Binary = 2,
            Octal = 8,
            Decimal = 10,
            Hexadecimal = 16,
        }
        public static string CheckFormat(string? formatString) // Checks whether entered number applies for given conditions
        {
            if (int.TryParse(formatString, out int number))
            {
                switch (number)
                {
                    case (int)NumberFormatEnum.Binary:
                    case (int)NumberFormatEnum.Octal:
                    case (int)NumberFormatEnum.Decimal:
                    case (int)NumberFormatEnum.Hexadecimal:
                        return "goodFormatFromBinToHex";
                    default:
                        if (Convert.ToInt32(formatString) >= 17 && Convert.ToInt32(formatString) <= 99) return "goodFormatFrom17To36";
                        else return "badFormatLessThan17OrMoreThan99";
                }
            }
            else return "notIntegerFormat";
        }
        public static void EnterNumberFormat(string formatString) // Displays conditions for user and errors if requirements not met
        {
            if (formatString == "badFormatLessThan17OrMoreThan36")
            {
                Console.WriteLine("You entered wrong format: less than 17 (not types given above: bin, oct, dec, hex) or more than 36!\n");
            }
            if (formatString == "notIntegerFormat")
            {
                Console.WriteLine("You entered wrong format: not integer type!\n");
            }
            Console.Write("Please enter your number conversion base: \nAllowed types: \nBinary - 2 \nOctal - 8 \nDecimal - 10 \nHexadecimal - 16 \nOther type - 17-36\n");
        }

        public static bool RepeatOrNot(char decision) // Returns value : repeat program or not
        {
            if (decision == 'y') return true;
            if (decision == 'n') return false;
            else { return false; }
        }
        /* Converts entered number to decimal and then to estimated base*/
        public static List<string> ConvertNumber(string? formatString // Number format string
            , string? enteredNum, // Entered number string
            string? formatString1) // Converts entered number to decimal and then to estimated base
        {
            List<char> tempNumbers = new List<char>(); 
            List<string> formatedNum = new List<string>();
            List<string> finishNumbers = new List<string>();
            int tempNum = 0;
            int index = 1;
            int quotNumber;

            for (int i = 0; i < enteredNum.Length; i++) // Takes every number from enteredNum string and adds to temporary list
             tempNumbers.Add(enteredNum.ElementAt(i));
   
            for (int i = 0; i < tempNumbers.Count; i++)
            {
                foreach (KeyValuePair<int, char> value in allSymbols)
                {
                    if (tempNumbers.ElementAt(i) == value.Value)
                    {
                        formatedNum.Add(value.Key.ToString());
                    }
                }
            }
            foreach (var value in formatedNum) // Cycle converts entered number to Decimal
            {
                tempNum += Convert.ToInt32(value) * (int)Math.Pow(Convert.ToInt32(formatString), formatedNum.Count - index);
                index++;
            }
            quotNumber = tempNum;
            while (quotNumber != 0) // Cycle converts number to entered format. Example: Decimal to Hexadecimal
            {
                finishNumbers.Add((quotNumber % Convert.ToInt32(formatString1)).ToString());
                quotNumber = quotNumber / Convert.ToInt32(formatString1);
            }
            finishNumbers.Reverse();
            for (int i = 0; i < finishNumbers.Count; i++) // Cycle goes through dictionary 'allSymbols' and when number is equal to 'Key' it gives list 'finishNumbers' value of dictionary value
            {
                foreach (var value in allSymbols)
                {
                    if (finishNumbers.ElementAt(i) == value.Key.ToString())
                    {
                        finishNumbers[i] = value.Value.ToString();
                        break;
                    }
                }
            }
            return finishNumbers;
        }
        public static bool CheckEnteredNumber (string enteredNum) // Checks whether entered number is correct format (symbols fits between 0 to 36)
        {
            for(int i = 0; i <enteredNum.Length; i++)
            {
                if (!allSymbols.Values.Contains(enteredNum[i]))
                    {
                    return false;
                }
            }
            return true;
        }
        static void Main(string[] args)
        {
            string? errorOrSuccess = string.Empty;
            string? inputFormat = string.Empty;
            string? inputFormat1 = string.Empty;
            string? tempNum = string.Empty;
            char repeatProgram = 'y';

            while (RepeatOrNot(repeatProgram))
            {
                while (errorOrSuccess != "goodFormatFromBinToHex" && errorOrSuccess != "goodFormatFrom17To36")
                {
                    EnterNumberFormat(errorOrSuccess);
                    inputFormat = Console.ReadLine();
                    CheckFormat(inputFormat);
                    errorOrSuccess = CheckFormat(inputFormat);
                }
                errorOrSuccess = "";
                Console.WriteLine($"Success! you entered {inputFormat}.");
                Console.Write("Enter the number you want to convert: ");
                tempNum = Console.ReadLine();
                while(CheckEnteredNumber(tempNum) == false)
                {
                    Console.Write("You entered wrong number format. Enter number again: ");
                    tempNum= Console.ReadLine();
                }
                while (errorOrSuccess != "goodFormatFromBinToHex" && errorOrSuccess != "goodFormatFrom17To36")
                {
                    EnterNumberFormat(errorOrSuccess);
                    inputFormat1 = Console.ReadLine();
                    CheckFormat(inputFormat1);
                    errorOrSuccess = CheckFormat(inputFormat1);
                }
                errorOrSuccess = "";
                Console.Write("Your converted number is: ");
                foreach (var value in ConvertNumber(inputFormat, tempNum, inputFormat1)) // Prints all finished to convert numbers
                {
                    Console.Write(value);
                }

                Console.Write("\nDo you want to repeat the program? (y/n): ");
                repeatProgram = Convert.ToChar(Console.ReadLine());
            }

            Console.ReadKey();
        }

        public static Dictionary<int, char> allSymbols = new Dictionary<int, char>() // Dictionary for all allowed symbols(numbers paired with values)
    {
       { 0, '0' },{ 1, '1' },{ 2, '2' },{ 3, '3' },{ 4, '4' },{ 5, '5' },{ 6, '6' },
        { 7, '7' },{ 8, '8' },{ 9, '9' },{ 10, 'A' },{ 11, 'B' },{ 12, 'C' },{ 13, 'D' },
        { 14, 'E' },{ 15, 'F' },{ 16, 'G' },{ 17, 'H' },{ 18, 'I' },{ 19, 'J' },{ 20, 'K' },
        { 21, 'L' },{ 22, 'M' },{ 23, 'N' },{ 24, 'O' },{ 25, 'P' },{ 26, 'Q' },{ 27, 'R' },
        { 28, 'S' },{ 29, 'T' },{ 30, 'U' },{ 31, 'V' },{ 32, 'W' },{ 33, 'X' },{ 34, 'Y' },
        { 35, 'Z' }

    };
    }
    
} 
    


