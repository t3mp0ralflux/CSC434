using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace LexAnalyzeCS
{
    internal class Program
    {
        public static string Numbers = "0123456789";
        public static string Alpha = "abcdefghijklmnopqrstuvwxyz";
        public static string Operators = "+-*/";
        public static string Assignment = "=";

        public static List<string> StartList = new List<string>();
        public static List<string> IdentyList = new List<string>();
        public static List<string> NumberList = new List<string>();
        public static List<string> OperateList = new List<string>();
        public static List<string> AssignmentList = new List<string>();
        public static List<string> ErrorsList = new List<string>();
        public static List<string> lhsRule = new List<string>();
        public static List<string> rhsRule = new List<string>();

        private static void Main()
        {
            try
            {
                //get the states from the text file
                var path = Directory.GetCurrentDirectory() + @"\states.txt";
                var lines = File.ReadAllLines(path);

                foreach (var line in lines)
                {
                    string[] split = line.Split(',');
                    switch (split[0])
                    {
                        case ("Start"):
                            StartList.AddRange(split);
                            StartList.RemoveAt(0);
                            break;

                        case ("Ident"):
                            IdentyList.AddRange(split);
                            IdentyList.RemoveAt(0);
                            break;

                        case ("Number"):
                            NumberList.AddRange(split);
                            NumberList.RemoveAt(0);
                            break;

                        case ("Operate"):
                            OperateList.AddRange(split);
                            OperateList.RemoveAt(0);
                            break;

                        case ("Assign"):
                            AssignmentList.AddRange(split);
                            AssignmentList.RemoveAt(0);
                            break;

                        case ("Error"):
                            ErrorsList.AddRange(split);
                            ErrorsList.RemoveAt(0);
                            break;
                        case ("lhsRule"):
                            lhsRule.AddRange(split);
                            lhsRule.RemoveAt(0);
                            break;
                        case ("rhsRule"):
                            rhsRule.AddRange(split);
                            rhsRule.RemoveAt(0);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            
            //delete output file to prevent duplicates
            if (File.Exists(Directory.GetCurrentDirectory() + @"\output.txt"))
            {
                File.Delete(Directory.GetCurrentDirectory()+@"\output.txt");
            }

            //checks path if valid
            var validPath = false;
            while (!validPath)
            {
                try
                {
                    Console.WriteLine("Please enter the location of the file you wish to tokenize");
                    var path = Console.ReadLine();
                    var lines = File.ReadAllLines(path);

                    //yeah, stole this from the internet.  hate me if you want to.
                    if (Regex.IsMatch(path, @"^(?:[a-zA-Z]\:|\\\\[\w\.]+\\[\w.$]+)\\(?:[\w]+\\)*\w([\w.])+$"))
                    {
                        validPath = true;
                        foreach (var line in lines)
                        {
                            //Console.WriteLine(line);

                            var token = Tokenize(line);
                            string tokenResults = string.Empty;
                            foreach (var t in token)
                            {
                                tokenResults += (t + " ");
                            }

                            var output = $"{line} tokenizes as: {tokenResults}";
                            Console.WriteLine(output);
                            Logger(output);

                            if (token.Contains("ERROR"))
                            {
                                output = "Error.  Cannot be sent to recognizer.\n";
                                Console.WriteLine(output);
                                Logger(output);
                            }
                            else
                            {
                                var recognized = Recognize(token);
                                output = (recognized ? "Valid Statement\n" : "Invalid Statement\n");
                                Console.WriteLine(output);
                                Logger(output);
                            }


                        }
                    }
                    else
                    {
                        validPath = false;
                        throw new Exception("Path entered was invalid, try again...");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + Environment.NewLine);
                    
                }
            }

            Console.ReadKey();
        }

        private static List<string> Tokenize(string token)
        {
            //start at the start
            var currentList = StartList;

            var tokenList = new List<string>();

            foreach (var character in token.ToCharArray())
            {
                string currentState;
                if (Alpha.IndexOf(character) != -1)
                {
                    currentState = currentList[0]; //if Alpha Character, state changes to Identifier
                }
                else if (Numbers.IndexOf(character) != -1)
                {
                    currentState = currentList[1]; //if Number Character, state changes to Number
                }
                else if (Operators.IndexOf(character) != -1)
                {
                    currentState = currentList[2]; //if Operator, state changes to operator
                }
                else if (Assignment.IndexOf(character) != -1)
                {
                    currentState = currentList[3]; //if assignment, state changes to assignment
                }
                else if (char.IsWhiteSpace(character))
                {
                    currentState = currentList[4]; //if whitespace, go back to start and try it again (ignores whitespace)
                }
                else
                {
                    currentState = currentList[5]; //if other, it's an error and you should feel bad
                }

                if (currentState.Equals("START"))
                {
                    currentList = StartList;
                }
                else if (currentState.Equals("NUMBER"))
                {
                    currentList = NumberList;
                }
                else if (currentState.Equals("IDENTIFIER"))
                {
                    currentList = IdentyList;
                }
                else if (currentState.Equals("OPERATOR"))
                {
                    currentList = OperateList;
                }
                else if (currentState.Equals("ASSIGNMENT"))
                {
                    currentList = AssignmentList;
                }
                else if (currentState.Equals("ERROR"))
                {
                    currentList = ErrorsList;
                }

                if (currentState != "START")
                {
                    tokenList.Add(currentState);  //if not whitespace add the state
                    if (currentState.Equals("ERROR")) //if error, there's no point in continuing on...
                    {
                        break;
                    }
                }

                //this little function clears up extra Idents
                //Essentially, this makes num1 == "Identifier" instead of 3 idents and a number
                if (tokenList.Count - 1 > 0) 
                {
                    var currentItem = tokenList[0];
                    for (var i = 1; tokenList.Count > i; i++)
                    {
                        if (currentItem.CompareTo(tokenList[i]) == 0)
                        {
                            tokenList.RemoveAt(i);
                        }
                        else
                        {
                            currentItem = tokenList[i];
                        }
                    }
                }
            }

            return tokenList;
        }

        private static bool Recognize(List<string> token)
        {
            var listSize = token.Count - 1;

            if (token.Count == 1)
            {
                if (token[0] == rhsRule[0] || token[0] == rhsRule[1])
                {
                    return true;
                }
            }
            else if (token.Count > 2)
            {
                if (token[listSize] == rhsRule[0] || token[listSize] == rhsRule[1])
                {
                    token.RemoveAt(listSize);
                    token.Add(lhsRule[2]);
                    while (token[listSize - 1] != rhsRule[3])
                    {
                        if (token[listSize - 1] == rhsRule[2])
                        {
                            if (token[listSize - 2] == rhsRule[0] || token[listSize - 2] == rhsRule[1])
                            {
                                token.RemoveAt(listSize - 1);
                                token.RemoveAt(listSize - 2);
                                listSize -= 2;
                                if (listSize == 0 && token[listSize] == "expression")
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                return false;
                            }

                            if (listSize == 1)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }

                    if (listSize == 2)
                    {
                        if (token[listSize - 2] == rhsRule[1] && token[listSize - 1] == rhsRule[3] && token[listSize] == lhsRule[2])
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// This just logs the output to a file named 'output.txt'
        /// </summary>
        /// <param name="line"></param>
        /// <param name="tokens"></param>
        private static void Logger(string line)
        {
            try
            {
                var path = Directory.GetCurrentDirectory() + @"\output.txt";
                File.AppendAllText(path, line + Environment.NewLine);
                }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}