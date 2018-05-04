using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Jack_Compiler
{
    class CompileEngine
    {
        private static bool writeXML = false;
        private static bool writeTokens = false;
        private static TokenEngine tokenEngine;

        private static List<string> fileLines = new List<string>();
        private static List<string> tokenList = new List<string>();
        private static List<string> assembledLines = new List<string>();
        private static string fileName = "";
        private static string functionName = "";
        private static int indentLevel = 0;
        
        public static void CompileFolder(string foldername, string filename)
        {
            List<string> fileList = new List<string>();

            string[] fileEntries = Directory.GetFiles(foldername);//Load all of the files in the target dir
            foreach (string file in fileEntries)
            {
                if (file.Contains(".jack"))//Load all files that can be translated
                {
                    fileList.Add(file);
                }
            }

            for (int count = 0; count < fileList.Count; count++)
            {
                fileName = Path.GetFileName(fileList[count]);
                fileName = fileName.Split('.')[0];
                tokenEngine = new TokenEngine(fileLines[count]);
            }

            WriteToFile(foldername, filename);
            Console.Write("Press any key to close the VMTranslator");
            Console.ReadKey();
        }

        public CompileEngine(string file, StreamWriter VMOutFile, StreamWriter XMLOutFile, bool includeSource, bool tokensOnly)
        {/*
            tokenEngine = new TokenEngine(file);
            if (XMLOutFile != null)
            {
                xmlIndent = 0;
                doXML = true;
                OnlyDoTokens = tokensOnly;
                XMLFile = XMLOutFile;
            }*/
        }

        public void CompileClass()
        {/*
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
                    if ((tokenEngine.currentToken == "static") || (tokenEngine.currentToken == "field"))
                    {
                        CompileClassVarDec();
                        continue;
                    }
                    else if ((tokenEngine.currentToken == "constructor") || (tokenEngine.currentToken == "function")
                            || (tokenEngine.currentToken == "method"))
                    {
                        CompileSubroutineDec();
                        continue;
                    }
                    else if (tokenEngine.currentToken == "}")
                    {
                        WriteXML("symbol", "}");
                        break;
                    }
                    else if (tokenEngine.currentToken == "var")
                    {
                        CompileClassVarDec();
                    }
                }
            }*/
        }

        private static void WriteToFile(string foldername, string filename)
        {
            string newFile = foldername;
            newFile += "\\" + filename + ".asm";
            TextWriter tw = new StreamWriter(newFile);
            for (int i = 0; i < assembledLines.Count; i++)
            {
                tw.WriteLine(assembledLines[i]);
            }
            tw.Close();
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
