using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Localization;

namespace MyEmlBld
{
    public partial class MailForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int _navigatingCountdown = 3;

        public MailForm()
        {
            InitializeComponent();
        }

        private void toolStripBuild_Click(object sender, EventArgs e)
        {
            this.textBox3.Text = "";
            foreach (String line1 in this.textBox1.Lines)
            {
                line1.Replace(" ", "");
                if (line1.Length > 0)
                {
                    foreach (String line2 in this.textBox2.Lines)
                    {
                        line2.Replace(" ", "");
                        if (line2.Length > 0)
                        {
                            this.textBox3.AppendText(line1 + "@" + line2 + "\r\n");
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
                SHDocVw.InternetExplorer IE = new SHDocVw.InternetExplorer();
                object Empty = null;
                String URL = e.Url.ToString();
                IE.Visible = true;
                IE.Navigate(URL, ref Empty, ref Empty, ref Empty, ref Empty);
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            _navigatingCountdown--;
        }

        private void MailForm_Load(object sender, EventArgs e)
        {

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.textBox3.Text = "";
            foreach (String line1 in this.textBox1.Lines)
            {
                line1.Replace(" ", "");
                if (line1.Length > 0)
                {
                    foreach (String line2 in this.textBox2.Lines)
                    {
                        line2.Replace(" ", "");
                        if (line2.Length > 0)
                        {
                            this.textBox3.AppendText(line1 + "@" + line2 + "\r\n");
                        }
                    }
                }
            }
            tabControl1.SelectedIndex = 1;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog1.FileName))
                {
                    foreach (string line in textBox3.Lines)
                    {
                        file.WriteLine(line);
                    }
                }
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AboutBox1 dlg = new AboutBox1();
            dlg.ShowDialog();
        }
    }
}
