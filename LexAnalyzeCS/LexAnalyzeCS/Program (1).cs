using System;
using System.Collections.Generic;
using System.IO;

namespace LexAnalyzeCS
{
    internal class Program
    {
        public static String Numbers = "0123456789";
        public static String Alpha = "abcdefghijklmnopqrstuvwxyz";
        public static String Operators = "+-*/";
        public static String Assignment = "=";

        //= {previous state, cur after number, cur after identifier, cur after operator, cur after assignment, cur after start, cur after error}
        public static String[] StartList = { "IDENTIFIER", "NUMBER", "OPERATOR", "ASSIGNMENT", "START", "ERROR" };

        public static String[] IdentyList = { "IDENTIFIER", "IDENTIFIER", "OPERATOR", "ASSIGNMENT", "START", "ERROR" };
        public static String[] NumberList = { "ERROR", "NUMBER", "OPERATOR", "ASSIGNMENT", "START", "ERROR" };
        public static String[] OperateList = { "IDENTIFIER", "NUMBER", "ERROR", "ASSIGNMENT", "START", "ERROR" };
        public static String[] AssignmentList = { "IDENTIFIER", "NUMBER", "OPERATOR", "ERROR", "START", "ERROR" };
        public static String[] ErrorsList = { "ERROR", "ERROR", "ERROR", "ERROR", "ERROR", "ERROR" };

        public static String[] lhsRule = { "statement", "assign_stmt", "expression" };
        public static String[] rhsRule = { "NUMBER", "IDENTIFIER", "OPERATOR", "ASSIGNMENT" };

        private static void Main(string[] args)
        {
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

        private static List<String> Tokenize(String token)
        {
            String currentState;
            String[] currentList = StartList;

            List<String> tokenList = new List<string>();

            foreach (char character in token.ToCharArray())
            {
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