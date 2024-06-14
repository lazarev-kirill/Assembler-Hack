namespace CSAssembler
{
    internal class Code
    {
        private Dictionary<string, string> destDict = new Dictionary<string, string>()
        {
            { "null", "000" },
            { "M", "001" },
            { "D", "010" },
            { "MD", "011" },
            { "A", "100" },
            { "AM", "101" },
            { "AD", "110" },
            { "ADM", "111" }
        };
        private Dictionary<string, string> jumpDict = new Dictionary<string, string>()
        {
            { "null", "000" },
            { "JGT", "001" },
            { "JEQ", "010" },
            { "JGE", "011" },
            { "JLT", "100" },
            { "JNE", "101" },
            { "JLE", "110" },
            { "JMP", "111" }
        };
        private Dictionary<string, string> compDictA0 = new Dictionary<string, string>()
        {
            { "0", "101010" },
            { "1", "111111" },
            { "-1", "111010" },
            { "D", "001100" },
            { "A", "110000" },
            { "!D", "001101" },
            { "!A", "110001" },
            { "-D", "001111" },
            { "-A", "110011" },
            { "D+1", "011111" },
            { "A+1", "110111" },
            { "D-1", "001110" },
            { "A-1", "110010" },
            { "D+A", "000010" },
            { "D-A", "010011" },
            { "A-D", "000111" },
            { "D&A", "000000" },
            { "D|A", "010101" }
        };
        private Dictionary<string, string> compDictA1 = new Dictionary<string, string>()
        {
            { "M", "110000" },
            { "!M", "110001" },
            { "-M", "110011" },
            { "M+1", "110111" },
            { "M-1", "110010" },
            { "D+M", "000010" },
            { "D-M", "010011" },
            { "M-D", "000111" },
            { "D&M", "000000" },
            { "D|M", "010101" }
        };
        public string dest(string dest)
        {
            return destDict[dest];
        }
        public string jump(string jump)
        {
            return jumpDict[jump];
        }
        public string comp(string comp)
        {
            if (compDictA1.ContainsKey(comp))
            {
                return compDictA1[comp];
            }
            else
            {
                return compDictA0[comp];
            }
        }
        public string a(string comp)
        {
            if (compDictA1.ContainsKey(comp))
            {
                return "1";
            }
            else { return "0"; }
        }
    }
}
