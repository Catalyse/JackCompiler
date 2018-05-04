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

        public static StreamWriter XMLOutFile;
        public static string[] tags;

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
        {
            tokenEngine = new TokenEngine(file);
            if (XMLOutFile != null)
            {

            }
        }

        private static void WriteToFile(string foldername, string filename)
        {
            string newFile = foldername;

            // This should be VM?
            newFile += "\\" + filename + ".asm";

            TextWriter tw = new StreamWriter(newFile);
            for (int i = 0; i < assembledLines.Count; i++)
            {
                tw.WriteLine(assembledLines[i]);
            }
            tw.Close();
        }

        public void CompileClass()
        {
            if (writeTokens)
            {
                OpenXMLTag("tokens");
                while (tokenEngine.HasMoreTokens())
                {
                    writeXMLTag();
                }
                CloseXMLTag();
            }
            else
            {
                if (tokenEngine.GetKeyword() == "class")
                {
                    OpenXMLTag("class");

                    if (tokenEngine.GetKeyword() == "identifier")
                    {
                        writeXMLTag();

                        if (tokenEngine.GetSymbol() == "{")
                        {
                            while (tokenEngine.HasMoreTokens())
                            {
                                if ((tokenEngine.GetKeyword() == "static") || (tokenEngine.GetKeyword() == "field"))
                                {
                                    writeXMLTag();
                                    CompileClassVarDec();
                                    continue;
                                }
                                else if ((tokenEngine.GetKeyword() == "constructor") || (tokenEngine.GetKeyword() == "function") || (tokenEngine.GetKeyword() == "method"))
                                {
                                    writeXMLTag();
                                    CompileSubroutine();
                                    continue;
                                }
                                else if (tokenEngine.GetSymbol() == "}")
                                {
                                    writeXMLTag();
                                    break;
                                }
                                else if (tokenEngine.GetKeyword() == "var")
                                {
                                    writeXMLTag();
                                    CompileClassVarDec();
                                }
                            }
                        }
                    }
                    CloseXMLTag();
                }
            }
        }

        public void CompileClassVarDec()
        {

        }

        public void CompileSubroutine()
        {

        }

        public void CompileParameterlist()
        {

        }

        public void CompileVarDec()
        {

        }

        public void CompileStatements()
        {

        }

        public void CompileDo()
        {

        }

        public void CompileLet()
        {

        }

        public void CompileWhile()
        {

        }

        public void ComplieReturn()
        {

        }

        public void CompileIf()
        {

        }

        public void CompileExpression()
        {

        }

        public void CompileTerm()
        {

        }

        public void CompileExpressionList()
        {

        }

        public void Check()
        {

        }

        public void Indent()
        {
            for(int i = 0; i < indentLevel; i++)
            {
                XMLOutFile.Write('\t');
            }
        }

        public void writeXMLTag()
        {
            if (writeXML == true)
            {
                Indent();
                XMLOutFile.Write('<' + tokenEngine.GetTokenType().ToString() + '>' + tokenEngine.GetStringVal() + "</" + tokenEngine.GetTokenType().ToString() + '>' + '\n');
                tokenEngine.Advance();
            }
        }

        public void OpenXMLTag(string tag)
        {
            if (writeXML == true)
            {
                Indent();
                XMLOutFile.Write('<' + tag + '>' + '\n');
                indentLevel++;
                tags[indentLevel] = tag;
                tokenEngine.Advance();
            }
        }

        public void CloseXMLTag()
        {
            if (writeXML == true)
            {
                indentLevel--;
                Indent();
                XMLOutFile.Write("</" + tags[indentLevel+1] + '>' + '\n');
            }
        }
    }
}
