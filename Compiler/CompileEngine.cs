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

        private static List<string> operators = new List<string>()
        {
            "+",
            "-",
            "*",
            "/",
            "&",
            "|",
            "<",
            ">",
            "=",
            "~"
        };

        private static List<string> symbols = new List<string>()
        {
            "{",
            "}",
            "[",
            "]",
            "(",
            ")",
            ".",
            ",",
            ";",
            "\""
        };

        public static void CompileFolder(string foldername, string filename)
        {
            string line;
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
                fileLines.Clear();
                StreamReader file = new StreamReader(fileList[count]);
                while ((line = file.ReadLine()) != null)
                {
                    fileLines.Add(line);
                }
                file.Close();
                ClearWhitespace();
                GenerateTokenList();

                for (int i = 0; i < fileLines.Count; i++)
                {

                }
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

        private static void ClearWhitespace()
        {
            //Clear Comments
            for (int i = 0; i < fileLines.Count; i++)
            {
                int index = fileLines[i].IndexOf("//");
                if (index > -1)
                {
                    fileLines[i] = fileLines[i].Substring(0, index);
                }
            }
            RemoveBlockComments();
            //This clears extra lines.
            int counter = 0;
            while (true)
            {
                if (fileLines[counter] == "" || fileLines[counter] == "\t" || fileLines[counter] == "\r")
                {
                    fileLines.RemoveAt(counter);
                }
                else
                {
                    counter++;
                    if (counter >= fileLines.Count)
                    {
                        break;
                    }
                }
            }
        }

        private static void GenerateTokenList()
        {
            for (int i = 0; i < fileLines.Count; i++)
            {
                foreach(string op in operators)
                {
                    fileLines[i] = fileLines[i].Replace(op, " " + op + " ");//Add spaces to help with separation
                }
                foreach (string op in symbols)
                {
                    fileLines[i] = fileLines[i].Replace(op, " " + op + " ");//Add spaces to help with separation
                }
                List<string> lineTokens = fileLines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                tokenList.AddRange(lineTokens);
            }
        }

        private static void RemoveBlockComments()
        {
            while (true)
            {
                bool foundBlock = false;
                int startline = -1, endline = -1;
                for (int i = 0; i < fileLines.Count; i++)
                {
                    if(!foundBlock)
                    {
                        if (fileLines[i].Contains("/*"))
                        {
                            if (fileLines[i].Contains("*/"))
                            {
                                startline = i;
                                endline = i;
                                foundBlock = true;
                                break;
                            }
                            else
                            {
                                startline = i;
                                foundBlock = true;
                            }
                        }
                    }
                    else
                    {
                        if(fileLines[i].Contains("*/"))
                        {
                            endline = i;
                            break;
                        }
                    }
                }
                if(foundBlock)
                {
                    if(endline != -1)
                    {
                        fileLines.RemoveRange(startline, (1 + (endline - startline)));
                    }
                    else
                    {
                        Console.WriteLine("Error! Block comment was NOT closed!");
                        return;
                    }
                }
                else
                {
                    break;
                }
            }
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
