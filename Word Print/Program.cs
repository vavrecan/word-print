using System;
using System.Linq;
using System.Windows.Forms;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace Word_Print
{
    static class Program
    {
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hwnd);
        public const String protocol = "word-print";
        
        [STAThread]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(DefaultExceptionHandler);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length == 0) {
                Application.Run(new MainForm());
            }
            else if (args[0] == "install")
            {
                Install();
            }
            else
            {
                Execute(args[0]);
            }
        }

        static void DefaultExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = e.ExceptionObject as Exception;
            MessageBox.Show(exception.Message, "Word Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(1); // everything else from 0 is failure
        }

        static void Install()
        {
            var key = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(protocol);
            key.SetValue("URL Protocol", "");
            key.SetValue("", "URL:StarPrint Protocol");

            var shellKey = key.CreateSubKey("shell");
            var openKey = shellKey.CreateSubKey("open");
            var commandKey = openKey.CreateSubKey("command");

            commandKey.SetValue("", "\"" + System.Reflection.Assembly.GetExecutingAssembly().Location + "\" \"%1\"");

            key.Close();
            MessageBox.Show("Word Print Protocol successfully installed", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        static void Execute(String param)
        {
            if (!param.StartsWith(protocol + ":"))
                throw new Exception("Invalid protocol: " + param);

            param = param.Substring((protocol + ":").Length);

            String printer = ""; 
            String url = HttpUtility.ParseQueryString(param).Get("url");
            String preview = HttpUtility.ParseQueryString(param).Get("preview");


            object defaultPrinter = Microsoft.Win32.Registry.CurrentUser.GetValue("DefaultPrinter");
            if (defaultPrinter != null)
                printer = defaultPrinter.ToString();


            if (HttpUtility.ParseQueryString(param).Get("printer") != null)
                printer = HttpUtility.ParseQueryString(param).Get("printer");

            PrintDocument(url, (preview == "1"), printer);
        }

        static void PrintDocument(String url, Boolean preview, String printer)
        {
            var uri = new Uri(url);
            var extension = System.IO.Path.GetExtension(uri.AbsolutePath);
            
            // TODO support more extensions
            if (extension != ".docx" && extension != ".doc")
                throw new Exception("Unsupported extension: " + extension);
            
            var path = String.Format("{0}{1}{2}", System.IO.Path.GetTempPath(), Hash(url), extension);

            // Download file
            DownloadForm downloadForm = new DownloadForm(url, path);
            downloadForm.ShowDialog();

            if (!downloadForm.downloaded)
                throw new Exception(downloadForm.downloadError);

            // Do the job
            object missing = System.Reflection.Missing.Value;
            object readOnly = true;
            object isVisible = false;
            object confirmConversion = false;
            object fileName = path;
            object addRecent = false;
            object obOpenAndRepair = false;

            var word = new Microsoft.Office.Interop.Word.Application();

            if (preview) { 
                word.Visible = true;
                isVisible = true;
            }
            else { 
                word.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone;
                word.FileValidation = Microsoft.Office.Core.MsoFileValidationMode.msoFileValidationSkip;
            }

            var doc = word.Documents.Open(ref fileName, ref confirmConversion, ref readOnly, ref addRecent, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref isVisible, ref obOpenAndRepair, ref missing, ref missing, ref missing);

            BringAppToFront();
            RunWithOutRejected(() => doc.Activate());
            
            if (printer != "") {
                Boolean found = false;
                foreach (String systemPrinter in System.Drawing.Printing.PrinterSettings.InstalledPrinters) { 
                    if (systemPrinter.Equals(printer))
                        found = true;
                }

                if (!found) {
                    MessageBox.Show("Printer " + printer + " not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    printer = "";
                }

                doc.Application.ActivePrinter = printer;
            }

            if (preview) {
                RunWithOutRejected(() => doc.PrintPreview());
            }
            else {
                RunWithOutRejected(() => doc.PrintOut());
                doc.Close();
                word.Quit();

                Marshal.FinalReleaseComObject(word);
               
                // delete file
                System.IO.File.Delete(path);
            }    
        }

        private static void BringAppToFront()
        {
            foreach (var p in System.Diagnostics.Process.GetProcesses().Where(p => p.ProcessName == "WINWORD"))
            {
                if (p.MainWindowHandle.ToInt32() != 0)
                    SetForegroundWindow(p.MainWindowHandle);
            }
        }

        static string Hash(string input)
        {
            var hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
        }

        public static void RunWithOutRejected(Action func)
        {
            bool hasException;

            do
            {
                try
                {
                    func();
                    hasException = false;
                }
                catch (COMException e)
                {
                    if (e.ErrorCode == -2147418111)
                    {
                        hasException = true;
                        Thread.Sleep(100); // calm down
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            } while (hasException);
        }
    }
}
