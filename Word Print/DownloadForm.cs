using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Word_Print
{
    public partial class DownloadForm : Form
    {
        public bool downloaded = false;
        public string downloadError = "Download cancelled";

        public DownloadForm(String url, String path)
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();

            WebClient client = new WebClient();
            Uri uri = new Uri(url);

            client.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCallback);
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);

            client.DownloadFileAsync(uri, path);
        }

        private void DownloadFileCallback(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            { 
                downloadError = e.Error.Message;
                downloaded = false;
            }
            else
            {
                downloaded = true;
            }

            Close();
        }

        private void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBarDownload.Maximum = (int)100;
            progressBarDownload.Value = (int)e.ProgressPercentage;
        }

        private void DownloadForm_Load(object sender, EventArgs e)
        {

        }
    }
}
