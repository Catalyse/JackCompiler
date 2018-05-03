using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Jack_Compiler
{
    class CompileEngine
    {
        int xmlIndent = 0;
        bool doXML = false;
        bool OnlyDoTokens = false;
        StreamWriter XMLFile;
        Tokenizer tokenizer;
        const bool VOID_OK = true;
        const bool VOID_NOT_OK = false;
        bool peekAhead = false;
        string savedToken;
        Tokenizer.TokenType savedTokenType;

        public CompileEngine(string file, StreamWriter VMOutFile, StreamWriter XMLOutFile, bool includeSource, bool tokensOnly)
        {
            tokenizer = new Tokenizer(file);
            if (XMLOutFile != null)
            {
                xmlIndent = 0;
                doXML = true;
                OnlyDoTokens = tokensOnly;
                XMLFile = XMLOutFile;
            }
        }

        public void CompileClass()
        {
            if (OnlyDoTokens)
            {
                WriteXMLTag("tokens");
                while (Tokenizer.advance())
                {
                    WriteXML(convert(Tokenizer.tokenType), Tokenizer.currentToken);
                }
                writeXMLTag("/tokens");
            }
            else
            {
                writeXMLTag("class");
                expect("keyword", "class");
                expect("identifier");
                expect("symbol", "{");
                while (GetNextToken())
                {
                    if ((tokenizer.currentToken == "static") || (tokenizer.currentToken == "field"))
                    {
                        CompileClassVarDec();
                        continue;
                    }
                    else if ((tokenizer.currentToken == "constructor") || (tokenizer.currentToken == "function")
                            || (tokenizer.currentToken == "method"))
                    {
                        CompileSubroutineDec();
                        continue;
                    }
                    else if (tokenizer.currentToken == "}")
                    {
                        WriteXML("symbol", "}");
                        break;
                    }
                    else if (tokenizer.currentToken == "var")
                    {
                        CompileClassVarDec();
                    }
                }
            }
        }

        public void CompileClassVarDec()
        {

        }

        public void CompileSubroutineDec()
        {

        }

        public void CompileParameterlist()
        {

        }

        public void CompileVarDec()
        {

        }
    }
}
