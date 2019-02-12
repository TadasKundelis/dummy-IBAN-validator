using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace IBAN_validator
{
    public static class Validator
    {
        public static string Run(string code)
        {
            if(code.Length < 4 || code.Length > 34)
            {
                return "invalid";
            };
            Dictionary<char, string> letterToNumber = new Dictionary<char, string>
            {
                {'A', "10"},
                {'B', "11"},
                {'C', "12"},
                {'D', "13"},
                {'E', "14"},
                {'F', "15"},
                {'G', "16"},
                {'H', "17"},
                {'I', "18"},
                {'J', "19"},
                {'K', "20"},
                {'L', "21"},
                {'M', "22"},
                {'N', "23"},
                {'O', "24"},
                {'P', "25"},
                {'Q', "26"},
                {'R', "27"},
                {'S', "28"},
                {'T', "29"},
                {'U', "30"},
                {'V', "31"},
                {'W', "32"},
                {'X', "33"},
                {'Y', "34"},
                {'Z', "35"}
            };

            string rearranged = code.Substring(4, code.Length - 4) + code.Substring(0, 4);
            StringBuilder stringBuilder = new StringBuilder();
            
            // loop over rearranged string and convert letters to numbers using letterToNumber dictionary
            for (int i = 0; i < code.Length; i++)
            {
                char character = rearranged[i];
                if (Char.IsLetter(character))
                {
                    if (!letterToNumber.ContainsKey(character))
                    {
                        return "invalid";
                    }
                    stringBuilder.Append(letterToNumber[character]);
                }
                else if(Char.IsDigit(character))
                {
                    stringBuilder.Append(character);
                } else
                {
                    return "invalid";
                }
            }
            //trim leading zeros
            string trimmedString = stringBuilder.ToString().TrimStart(new Char[] { '0' });

            //convert to bigint
            BigInteger convertedToInt = 0;
            try
            {
                convertedToInt = BigInteger.Parse(trimmedString);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"\n{exception.Message}");
            };

            if (BigInteger.Remainder(convertedToInt, 97) == 1)
            {
                return "valid";
            }
            else
            {
                return "invalid";
            }
        }
    }
}
