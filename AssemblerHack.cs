namespace CSAssembler
{
    internal class AssemblerHack
    {
        private Parser parser;
        private Code code = new Code();
        private Table table = new Table();
        private string path = "";
        private int freeMemoryIndex = 16;
        private string convertToBinString(int num)
        {
            string bin = Convert.ToString(num, 2);
            int binLength = bin.Length;
            for (int j = 0; j < 15 - binLength; j++)
            {
                bin = "0" + bin;
            }
            return bin;
        }
        public AssemblerHack(string filePath)
        {
            path = filePath;
            parser = new Parser(filePath);
        }
        public void firstPass()
        { 
            int lineNumber = -1;
            while (true)
            {
                if (!parser.hasMoreLines()) { break; }
                else
                {
                    parser.advance();
                    string instructionType = parser.instructionType();
                    if (instructionType == "A_INSTRUCTION" || instructionType == "C_INSTRUCTION") 
                    { 
                        lineNumber++; 
                    }
                    else 
                    {
                        table.addEntry(parser.symbol(), lineNumber + 1);
                    }
                }
            }
            parser.reset();
        }
        public void secondPass()
        {
            string output = "";
            int line = 0;
            while (true)
            {
                line++;
                if (!parser.hasMoreLines()) { break; }
                else
                {
                    parser.advance();
                    string instructionType = parser.instructionType();
                    if (instructionType == "A_INSTRUCTION")
                    {
                        string symbol = parser.symbol();
                        if (int.TryParse(symbol, out var i))
                        {
                            output += $"0{convertToBinString(i)}\n";
                        }
                        else
                        {
                            if (table.contains(symbol))
                            {
                                output += $"0{convertToBinString(table.getAdress(symbol))}\n";
                            }
                            else
                            {
                                table.addEntry(symbol, freeMemoryIndex);
                                output += $"0{convertToBinString(freeMemoryIndex)}\n";
                                freeMemoryIndex++;
                            }
                        }
                    }
                    else if (instructionType == "C_INSTRUCTION")
                    {
                        string dest = parser.dest();
                        string comp = parser.comp();
                        string jump = parser.jump();
                        output += $"111{code.a(comp)}{code.comp(comp)}{code.dest(dest)}{code.jump(jump)}\n";
                    }
                }
            }
            File.WriteAllText(path.Replace(".asm", ".hack"), output);
            Console.WriteLine($"New file: {path.Replace(".asm", ".hack")}");
        }
    }
}
