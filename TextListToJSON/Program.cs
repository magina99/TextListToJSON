using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TextListToJSON
{
    internal class Program
    {
        static string inputPath = null;
        static string outputPath = null;
        static List<string> allLinesText = new List<string>();

        [STAThread]
        static void Main(string[] args)
        {
            WriteToConsoleWithColor(ConsoleColor.DarkBlue, "Waiting for input file...");
            while (inputPath == null)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (DialogResult.OK == dialog.ShowDialog())
                {
                    inputPath = dialog.FileName;
                }
            }
            allLinesText = File.ReadAllLines(inputPath).ToList();
            WriteToConsoleWithColor(ConsoleColor.DarkGreen, $"Input file set! {allLinesText.Count} lines found");

            WriteToConsoleWithColor(ConsoleColor.DarkBlue, "Waiting for output location...");
            while (outputPath == null)
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    outputPath = fbd.SelectedPath;
                }
            }
            WriteToConsoleWithColor(ConsoleColor.DarkGreen, "Output location set!");

            try
            {
                File.WriteAllText(outputPath + @"\output.json", BuildJSON().ToString());
                WriteToConsoleWithColor(ConsoleColor.Green, $@"JSON file created succefully at: {outputPath}\output.json");
            }
            catch (Exception e)
            {
                WriteToConsoleWithColor(ConsoleColor.DarkRed, "Error creating JSON file");
            }
        }

        internal static void WriteToConsoleWithColor(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        internal static StringBuilder BuildJSON()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("{");

            int c = 0;
            foreach(string line in allLinesText)
            {
                if(c == allLinesText.Count - 1)
                    sb.AppendLine($"\"{c}\": " + $"\"{line}\"");
                else
                    sb.AppendLine($"\"{c}\": " + $"\"{line}\",");
                
                c++;
            }

            sb.AppendLine("}");

            return sb;
        }
    }
}
