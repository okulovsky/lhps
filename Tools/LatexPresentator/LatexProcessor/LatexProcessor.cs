using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace LatexPresentator.Model
{
    class LatexProcessor
    {
        public List<FileInfo> Bitmaps { get; private set; }


        const string Ghostscript = @"C:\Program Files\gs\gs9.16\bin\gswin64.exe";
        const string temporalFileName = "presentator.temp.tex";
        const string pdfFileName = "presentator.temp.pdf";

        static void ConvertToPng(FileInfo pdfFile, DirectoryInfo directory)
        {
            directory.Delete(true);
            directory.Create();
            pdfFile = pdfFile.CopyTo(Path.Combine(directory.FullName, pdfFile.Name));
            var process = new Process();
            process.StartInfo.FileName = Ghostscript;
            process.StartInfo.Arguments =
                @"-dBATCH -dNOPAUSE -sDEVICE=png16m -r600 -dUseCropBox -sOutputFile=""%03d.png"" " + pdfFile.Name;
            process.StartInfo.WorkingDirectory = directory.FullName;
            process.Start();
            process.WaitForExit();
            pdfFile.Delete();
        }

        static bool StartLatex(FileInfo latexFile)
        {
            var process = new Process();
            process.StartInfo.FileName = "pdflatex";
            process.StartInfo.WorkingDirectory = latexFile.Directory.FullName;
            process.StartInfo.Arguments = latexFile.Name;
            process.Start();
            process.WaitForExit();
            return process.ExitCode == 0;
        }


       
        public static void Compile(string latexEntry, string latexHeader, FileInfo sourceFile, int frameNumber, DirectoryInfo output)
        {
            var latexSource = new FileInfo(Path.Combine(sourceFile.Directory.FullName, temporalFileName));
            var builder = new StringBuilder();
            builder.Append(latexHeader);
            builder.Append("\\begin{document}\n\\begin{frame}\n\n");
            builder.Append(latexEntry);
            builder.Append("\\end{frame}\n\\end{document}");
            File.WriteAllText(latexSource.FullName, builder.ToString());
            StartLatex(latexSource);

            output=output.CreateSubdirectory(sourceFile.Name);
            output = output.CreateSubdirectory(frameNumber.ToString());
            ConvertToPng(new FileInfo(Path.Combine(sourceFile.Directory.FullName, pdfFileName)), output);
        }


    }
}
