using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace MyEmlBld
{
    public partial class MainForm : RibbonForm
    {
        private int _navigatingCountdown = 3;
        private const String FileDomains = @"domains.txt";
        private const String FileUsers = @"users.txt";

        public MainForm()
        {
            InitializeComponent();
            textBoxUsers.Text = File.ReadAllText(FileUsers);
            textBoxDomains.Text = File.ReadAllText(FileDomains);
        }

        private void toolStripBuild_Click(object sender, EventArgs e)
        {
            textBoxResults.Text = "";
            foreach (String username in textBoxUsers.Lines)
            {
                username.Replace(" ", "");
                if (username.Length > 0)
                {
                    foreach (String domain in textBoxDomains.Lines)
                    {
                        domain.Replace(" ", "");
                        if (domain.Length > 0)
                        {
                            textBoxResults.AppendText(username + "@" + domain + "\r\n");
                        }
                    }
                }
            }
            tabControl1.SelectedIndex = 1;
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (_navigatingCountdown == 0)
            {
                e.Cancel = true;
                String url = e.Url.ToString();
                Process.Start(url);
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            _navigatingCountdown--;
        }

        private void MailForm_Load(object sender, EventArgs e)
        {
        }

        private void ExitItemClick(object sender, ItemClickEventArgs e)
        {
            File.WriteAllLines(FileUsers, textBoxUsers.Lines);
            File.WriteAllLines(FileDomains, textBoxDomains.Lines);
            Close();
        }

        private void BuildItemClick(object sender, ItemClickEventArgs e)
        {
            textBoxResults.Text = "";
            foreach (String username in textBoxUsers.Lines)
            {
                username.Replace(" ", "");
                if (username.Length > 0)
                {
                    foreach (String domain in textBoxDomains.Lines)
                    {
                        domain.Replace(" ", "");
                        if (domain.Length > 0)
                        {
                            textBoxResults.AppendText(username + "@" + domain + "\r\n");
                        }
                    }
                }
            }
            tabControl1.SelectedIndex = 1;
        }

        private void SaveItemClick(object sender, ItemClickEventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllLines(saveFileDialog1.FileName, textBoxResults.Lines);
            }
        }

        private void AboutItemClick(object sender, ItemClickEventArgs e)
        {
            AboutBox1 dlg = new AboutBox1();
            dlg.ShowDialog();
        }
    }
}