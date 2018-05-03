using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Jack_Compiler
{
    class Program
    {
        public static bool printSource = false;
        public static bool tokensOnly = false;
        public static StreamWriter XMLOutFile;
        public static StreamWriter VMOutFile;
        public static StreamWriter ErrorFile;
        public static bool MakeXMLOutFile = true;
        public static bool MakeVMOutFile = false;
        public static string shortFileName = null;
        public static CompileEngine ce = null;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter input 'filename.jack' or 'directory_name' (No .jack extension in this case):");
            string inFileNameOrDir = Console.ReadLine();

            Console.WriteLine("Do you want to produce tokens only (Chapter 10, Part 1)? [Y/N]:");
            string justTokens = Console.ReadLine();
            if ((justTokens == "Y") || (justTokens == "y") || (justTokens == "YES") || (justTokens == "Yes") || (justTokens == "yes")) tokensOnly = true;

            Console.WriteLine("Do you want source information in the .vm and/or .xml output file? [Y/N]:");
            string doDebug = Console.ReadLine();
            if ((doDebug == "Y") || (doDebug == "y") || (doDebug == "YES") || (doDebug == "Yes") || (doDebug == "yes")) printSource = true;

            if (File.Exists(inFileNameOrDir))
            {
                // This path is a file, strip off the ".jack" for the output name
                shortFileName = inFileNameOrDir.Substring(0, inFileNameOrDir.Length - 5);

                if (MakeXMLOutFile)
                {
                    if (tokensOnly) XMLOutFile = new StreamWriter(shortFileName + "T.xml");
                    else XMLOutFile = new StreamWriter(shortFileName + ".xml");
                }
                if (MakeVMOutFile) VMOutFile = new StreamWriter(shortFileName + ".vm");
                ErrorFile = new StreamWriter(shortFileName + ".log");

                ProcessFile(inFileNameOrDir);

                // close the output files
                if (MakeXMLOutFile) XMLOutFile.Close();
                if (MakeVMOutFile) VMOutFile.Close();
            }
            else if (Directory.Exists(inFileNameOrDir))
            {
                // This path is a directory; process all (and only) '.jack' files in the directory, and ignore subdirectories
                int lastSlash = inFileNameOrDir.LastIndexOf("\\", 0);
                string dirName = inFileNameOrDir.Substring(lastSlash + 1);
                Console.WriteLine("Processing directory: {0}", dirName);

                string[] fileEntries = Directory.GetFiles(inFileNameOrDir, "*.jack", SearchOption.TopDirectoryOnly);
                foreach (string fileName in fileEntries)
                {
                    // Get rid of the ".jack"; we don't look for "jack", we just delete the last two characters
                    int dirSlash = fileName.IndexOf("\\", 0);
                    if ((dirSlash < 0)) // no leading dir name
                    {
                        shortFileName = fileName.Substring(0, fileName.Length - 4);
                    }
                    else
                    {
                        shortFileName = fileName.Substring((dirSlash + 1), fileName.Length - dirSlash - 6);
                    }

                    if (MakeXMLOutFile)
                    {
                        if (tokensOnly) XMLOutFile = new StreamWriter(shortFileName + "T.xml");
                        else XMLOutFile = new StreamWriter(shortFileName + ".xml");
                    }
                    if (MakeVMOutFile) VMOutFile = new StreamWriter(shortFileName + ".vm");
                    ErrorFile = new StreamWriter(shortFileName + ".log");

                    ProcessFile(fileName);
                    // close the output files
                    if (MakeXMLOutFile) XMLOutFile.Close();
                    if (MakeVMOutFile) VMOutFile.Close();
                }
            }
            else
            {
                Console.WriteLine("{0} is not a valid file or directory.", inFileNameOrDir);
                Console.WriteLine("Press return to exit\n");
                Console.ReadLine();
                if (ErrorFile != null) ErrorFile.Close();
                Environment.Exit(-1);
            }
        }

        static void ProcessFile(string file)
        {
            Console.WriteLine("Processing file: {0}", file);
            // process the file
            ce = new CompileEngine(file, VMOutFile, XMLOutFile, printSource, tokensOnly);
            try
            {
                ce.CompileClass();
            }
            catch (Exception)
            {
                Console.WriteLine("Class failed to correctly compile\n");
                if (MakeXMLOutFile) XMLOutFile.Close();
                if (MakeVMOutFile) VMOutFile.Close();
            }
            Console.WriteLine("Press return to exit\n");
            Console.ReadLine();
            ErrorFile.Close();
        }
    }
}
