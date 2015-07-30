using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Word_Print
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();

            richTextBoxInformations.Rtf = @"{\rtf1\ansi
This simple utility allows you to print documents from webpages with a single click.\line 
It works by creating new protocol handler \b word-print \b0 \line\line
We recommand that you test installation before using the utility in automated processes since file association check can sometimes hang the loading.\line\line
Marek Vavrecan (vavrecan@gmail.com) \line
https://github.com/vavrecan/word-print \line\line

Usage:\line 
\deff0 {\fonttbl {\f0 Consolas;}}\fs18 " + Program.protocol + @":url=[doc/docx url]&preview=[preview before print]&printer=[printer]\line\line

Example:\line
\deff0 {\fonttbl {\f0 Consolas;}}\fs18 " + Program.protocol + @":url=http://example.me/file.docx&preview=1&printer=Slips+Printer\line
<a href=" + "\"" + Program.protocol + @":url=http://example.me/file.docx&preview=1" + "\"" + @">Print</a>\line\line
}";
            richTextBoxInformations.LinkClicked += new LinkClickedEventHandler((object sender, LinkClickedEventArgs e) => {
                Process.Start(e.LinkText);
            });

            comboBoxPrinters.Items.Add("");
            foreach (String printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                comboBoxPrinters.Items.Add(printer);

            object defaultPrinter = Microsoft.Win32.Registry.CurrentUser.GetValue("DefaultPrinter");
            if (defaultPrinter != null)
                comboBoxPrinters.Text = defaultPrinter.ToString();
        }

        private void buttonInstall_Click(object sender, EventArgs e)
        {
            RunWithUAC(System.Reflection.Assembly.GetExecutingAssembly().Location, "install");
        }

        static void RunWithUAC(string ExecutableFileName, string Args)
        {
            ProcessStartInfo info = new ProcessStartInfo(ExecutableFileName, Args);

            info.CreateNoWindow = true;
            info.Verb = "runas";

            try
            {
                Process.Start(info);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            Process.Start(Program.protocol + @":url=http://www.easychair.org/publications/easychair.docx&preview=1");
        }

        private void comboBoxPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            Microsoft.Win32.Registry.CurrentUser.SetValue("DefaultPrinter", comboBoxPrinters.SelectedItem.ToString());
        }
    }
}
