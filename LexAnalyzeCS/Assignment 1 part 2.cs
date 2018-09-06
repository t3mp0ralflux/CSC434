Add this back in at line 44:

if (token.Contains("ERROR"))
                    {
                        Console.WriteLine("\nError.  Cannot be sent to recognizer.");
                    }
                    else
                    {
                        Boolean recognized = Recognize(token);
                        if (recognized)
                        {
                            Console.WriteLine("\nValid Statement\n");
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid Statement\n");
                        }
                    }
					
Add this method back in as well:

private static bool Recognize(List<String> token)
        {
            int listSize = token.Count - 1;

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
                                if (listSize == 0 && token[listSize].ToString() == "expression")
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
                        if (token[listSize-2] == rhsRule[1] && token[listSize-1] == rhsRule[3] && token[listSize] == lhsRule[2])
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }					