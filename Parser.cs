using System.Text.RegularExpressions;

namespace CSAssembler
{
    internal class Parser
    {
        private int currentPosition = -1;
        private string currentCommand = string.Empty;
        private const string ENDOFFILE = "END_OF_FILE";
        private string[] lines;
        private const string PATTERN = @"[^;=]+";
        private Regex regex = new Regex(PATTERN);
        private MatchCollection? matches;

        public Parser(string filePath)
        {
            lines = File.ReadAllLines(filePath);
        }

        public bool hasMoreLines()
        {
            if (lines.Length - 1 > currentPosition)
            {
                return true;
            }
            return false;
        }

        public void advance()
        {
            if (hasMoreLines())
            {
                currentPosition += 1;
                currentCommand = lines[currentPosition];
                currentCommand = currentCommand.Replace("\n", string.Empty);
                currentCommand = currentCommand.Replace(" ", string.Empty);
                currentCommand = currentCommand.Replace("\t", string.Empty);

                if (currentCommand.Contains("//"))
                {
                    int i = 0;
                    foreach (char symbol in currentCommand)
                    {
                        if (symbol == '/' && currentCommand.Length >= 0 + i)
                        {
                            currentCommand = currentCommand.Substring(0, i);
                            break;
                        }
                        i++;
                    }
                }

                if (currentCommand == string.Empty)
                {
                    advance();
                }
            }
            else { currentCommand = ENDOFFILE; }
        }

        public string instructionType()
        {
            if (currentCommand.Contains("@"))
            {
                return "A_INSTRUCTION";
            }
            else if (currentCommand.Contains("(") && currentCommand.Contains(")"))
            {
                string check = currentCommand.Replace("(", string.Empty).Replace(")", string.Empty);
                if (int.TryParse(check, out int i))
                {
                    throw new Exception($"\nLine {currentPosition + 1}: syntaxis error for L_INSTRUCTION: {i} can't be a number");
                }
                return "L_INSTRUCTION";
            }
            else
            {
                return "C_INSTRUCTION";
            }
        }

        public string symbol()
        {
            if (currentCommand[0] == '@')
            {
                return currentCommand.Replace("@", string.Empty);
            }
            else
            {
                return currentCommand.Replace("(", string.Empty).Replace(")", string.Empty);
            }
        }

        public string dest()
        {
            bool containsEq = currentCommand.Contains("=");
            if (!containsEq)
            {
                return "null";
            }
            else
            {
                matches = regex.Matches(currentCommand);
                return matches[0].ToString();
            }
        }

        public string comp()
        {
            bool containsEq = currentCommand.Contains("=");
            bool containsSemicol = currentCommand.Contains(";");
            if (!containsEq && !containsSemicol)
            {
                return currentCommand;
            }
            else
            {
                matches = regex.Matches(currentCommand);
                if (containsEq)
                {
                    return matches[1].ToString();
                }
                else
                {
                    return matches[0].ToString();
                }
            }
        }

        public string jump()
        {
            bool containsSemicol = currentCommand.Contains(";");
            if (!containsSemicol)
            {
                return "null";
            }
            else
            {
                bool containsEq = currentCommand.Contains("=");
                matches = regex.Matches(currentCommand);
                if (containsEq)
                {
                    return matches[2].ToString();
                }
                else
                {
                    return matches[1].ToString();
                }
            }
        }

        public void reset()
        {
            currentPosition = -1;
            currentCommand = string.Empty;
        }
    }
}