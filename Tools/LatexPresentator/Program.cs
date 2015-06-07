using LatexPresentator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LatexPresentator
{
    class Program
    {
        const string exampleHeader = @"\documentclass[aspectratio=169]{beamer}
\usepackage{lphs}

\SetupImages{L2-Images/}{jpg}
\newcommand{\firstcircle}{(-1,0) circle (1.5)}
\newcommand{\secondcircle}{(1,0) circle (1.5)}
\newcommand{\thirdcircle}{(0,1.73) circle (1.5)}
";

        const string exampleEntry = @"\begin{columns}
\column{0.4\textwidth}
\begin{center}
\begin{Reason}
	\from{Все кошачьи хищники}
	\from{Все львы кошачьи}
	\conc{Все львы хищники}
\end{Reason}
\end{center}
\column{0.6\textwidth}
\begin{center}
\begin{tikzpicture}[x=1.3cm,y=-1.3cm]

\uncover<3->{\begin{scope}[even odd rule]% first circle without the second
\clip \secondcircle (-2.5,-1.5) rectangle (2.5,3.25);
\fill[gray] \firstcircle;
\end{scope}}

\uncover<4->{\begin{scope}[even odd rule]% first circle without the second
\clip \firstcircle (-2.5,-1.5) rectangle (2.5,3.25);
\fill[gray] \thirdcircle;
\end{scope}}

\uncover<2->{\draw \firstcircle node {К};
\draw \secondcircle node {Х};
\draw \thirdcircle node {Л};
}
\end{tikzpicture}
\end{center}
\end{columns}";


        public static void RunSlideModel(ISlideModel model)
        {


            var wnd = new Window()
            {
                //WindowStyle = WindowStyle.None,
                //ResizeMode = ResizeMode.NoResize,
                //WindowStartupLocation = WindowStartupLocation.CenterScreen,
                //Width = 1024,
                Height = 576
            };

            wnd.Content = model.GetControl();
            new Application().Run(wnd);
            
        }

        [STAThread]
        public static void Main()
        {
            LatexProcessor.Compile(
                exampleEntry,
                exampleHeader,
                new System.IO.FileInfo("..\\..\\..\\..\\Latex\\L02-1 - Силлогизмы.tex"),
                1,
                new System.IO.DirectoryInfo("Temp"));
        }
    }
}
