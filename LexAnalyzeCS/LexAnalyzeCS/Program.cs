using System;
using System.Collections.Generic;
using System.IO;

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

        private static void Main(string[] args)
        {
            try
            {
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
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            try
            {
                var path = args[0];
                var lines = File.ReadAllLines(path);

                foreach (var line in lines)
                {
                    //Console.WriteLine(line);

                    List<String> token = Tokenize(line);
                    String tokenResults = string.Empty;
                    foreach (String t in token)
                    {
                        tokenResults += (t + " ");
                    }
                    Console.WriteLine("{0} tokenizes as: {1}", line, tokenResults);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            Console.ReadKey();
        }

        private static List<string> Tokenize(string token)
        {
            var currentList = StartList;

            var tokenList = new List<string>();

            foreach (var character in token.ToCharArray())
            {
                string currentState;
                if (Alpha.IndexOf(character) != -1)
                {
                    currentState = currentList[0];
                }
                else if (Numbers.IndexOf(character) != -1)
                {
                    currentState = currentList[1];
                }
                else if (Operators.IndexOf(character) != -1)
                {
                    currentState = currentList[2];
                }
                else if (Assignment.IndexOf(character) != -1)
                {
                    currentState = currentList[3];
                }
                else if (char.IsWhiteSpace(character))
                {
                    currentState = currentList[4];
                }
                else
                {
                    currentState = currentList[5];
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
                    tokenList.Add(currentState);
                    if (currentState.Equals("ERROR"))
                    {
                        break;
                    }
                }

                if (tokenList.Count - 1 > 0)
                {
                    String currentItem = tokenList[0];
                    for (int i = 1; tokenList.Count > i; i++)
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
    }
}