using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Jack_Compiler
{
    public class TokenEngine
    {
        //bool hasMoreTokens = false;
        public enum TokenType { KEYWORD, SYMBOL, IDENTIFIER, INT_CONST, STRING_CONST, UNKNOWN };
        public string currentToken;
        public static TokenType tokenType;
        int charLine;

        public static Dictionary<string, int> keyWord = new Dictionary<string, int>()
          {
               {"class", 0},
               {"method", 0},
               {"function", 0},
               {"constructor", 0},
               {"int", 0 },
               {"boolean", 0 },
               {"char", 0 },
               {"void", 0 },
               {"static", 0 },
               {"field", 0 },
               {"let",0 },
               {"do", 0 },
               {"if", 0 },
               {"else", 0 },
               {"while", 0 },
               {"return", 0 },
               {"true", 0 },
               {"false", 0 },
               {"null", 0 },
               {"this", 0 }
          };


        public TokenEngine(string file)
        {

            currentToken = " ";
            tokenType = TokenType.UNKNOWN;
            charLine = 0;
        }

        public void advance()
        {
            //unicode category.something


        }



        public void symbol()
        {

        }
    }
}
