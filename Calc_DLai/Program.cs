/*
* FILE: Program.cs
* PROJECT: S-Expression Calculator
* PROGRAMMERS: DANNY LAI
* FIRST VERSION: 09/13/2021
* DESCRIPTION:  A command line program that acts as a simple calculator.
*               It takes a single argument as an expression (a string) and prints
*               out the integer result of evaluating it
*/

using System;
using System.Collections.Generic;


namespace Calc_DLai
{
    class Program
    {
        static void Main(string[] args)
        {
            try {
                Console.WriteLine(Calc_Nested(args[0])); // GET STRING INPUT
            } catch {
                Console.WriteLine("ERROR: PLEASE USE THE CORRECT SYNTAX FOR YOUR INPUT STRING (e.g.: \"123\", \"(add 1 1)\")");
            }
            
        }

        // USE DICTIONARY TO STORE CALCULATED RESULTS FROM INDIVIDUAL FUNCTIONS (EACH WITHIN A PAIR OF PARENTHESES)
        public static Dictionary<string, int> SingleF_Results = new Dictionary<string, int>();


        /*
        * FUNCTION: Calc_Nested
        * DESCRIPTION: 
        *           Calculates the entire nested functions (the whole input string) 
        *           and returns an integer value.
        * PARAMETERS: 
        *           string iStr : input string from user
        * RETURN:  
        *           int : the calculated result
        */
        public static int Calc_Nested(string iStr)
        {
            while (iStr.Contains(')'))
            {
                // CHECK IN DICTIONARY IF THE FUNCTION(S) NOTED IN THE INPUT STRING HAS BEEN CALCULATED 
                if (SingleF_Results.ContainsKey(iStr))
                {
                    return SingleF_Results[iStr]; // RETURN CALCULATED RESULT
                }

                // SEARCH FOR FIRST OCCURENCE / INDEX OF THE CLOSE-PARENTHESIS 
                int close_parenth = iStr.IndexOf(')');

                // SEARCH FOR THE MATCHING OPEN-PARENTHESIS OF THE CLOSE-PARENTHESIS MENTIONED ABOVE
                int open_parenth = iStr.LastIndexOf('(', close_parenth); // TRAVERSE BACKWARDS
                int subStr_length = close_parenth - (open_parenth + 1); // DETERMINE LENGTH OF SUBSTRING / A SINGLE FUNCTION WITHIN A PAIR OF PARENTHESES

                // CALCULATE SINGLE FUNCTION
                int current_value = Calc_SingleF(iStr.Substring(open_parenth + 1, subStr_length));
               
                // CHECK IF THERE IS NO MORE FUNCTION TO CALCULATE
                if (open_parenth == 0) {
                    return current_value;
                } else {
                    // CREATE NEW STRING BY ADDING CALCULATED VALUES FROM THE INDIVIDUAL FUNCTIONS
                    iStr = iStr.Substring(0, open_parenth) + current_value.ToString() + iStr.Substring(close_parenth + 1);
                }
            }
            return int.Parse(iStr);
        }



        /*
        * FUNCTION: Calc_SingleF
        * DESCRIPTION: 
        *           Calculates a single function and returns an integer value.
        * PARAMETERS: 
        *           string iStr : input string (a single function determined by a pair of parentheses)
        * RETURN:  
        *           int result : the calculated result
        */
        public static int Calc_SingleF(string iStr)
        {
            int result = 0; // STORE THE CALCULATED RESULT

            // CHECK IN DICTIONARY IF THE FUNCTION NOTED IN THE INPUT STRING HAS BEEN CALCULATED 
            if (SingleF_Results.ContainsKey(iStr))
            {
                return SingleF_Results[iStr]; // RETURN CALCULATED RESULT
            }

            // ELEMENTS (MEANINGFUL SUBSTRINGS) THAT INVOVLE IN THE CALCULATION: FUNCTION & EXPRESSION
            string[] calc_elements = iStr.Split();

            // CHECK TO SEE WHAT FUNCTION IS BEING USED
            if (calc_elements[0] == "add")
            {
                int i = 1;
                while (i < calc_elements.Length) // CHECK FOR THE NUMBER OF EXPRESSIONS
                {
                    if (i == 1) {
                        result = int.Parse(calc_elements[i]) + int.Parse(calc_elements[i + 1]); // FOR FIRST-TIME CALCULATION
                        i += 2; // ADVANCE TO THE THIRD EXPRESSION
                    } else {
                        result += int.Parse(calc_elements[i]); // KEEP ADDING NEW EXPRESSION TO CURRENT TOTAL RESULT
                        i += 1;
                    }
                }
            }
            else if (calc_elements[0] == "multiply")
            {
                int j = 1;
                while (j < calc_elements.Length) 
                {
                    if (j == 1) {
                        result = int.Parse(calc_elements[j]) * int.Parse(calc_elements[j + 1]); // FOR FIRST-TIME CALCULATION
                        j += 2;
                    } else {
                        result = result * int.Parse(calc_elements[j]); // KEEP MULTIPLYING CURRENT RESULT WITH NEW EXPRESSION
                        j += 1;
                    }
                }
            }
            else // WHEN ONLY DIGITS ARE SPECIFIED (NO FUNCTION)
            {
                result = int.Parse(iStr);
            }

            // STORE THE CALCULATED RESULT IN DICTIONARY
            SingleF_Results[iStr] = result;
            return result;
        }
    }
}
