using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoCompress
{
    public partial class Form1 : Form
    {
        private string filename;
        private string path;

        Process process = new Process();


        public string Filename
        {
            get
            {
                return filename;
            }

            set
            {
                filename = value;
            }
        }

        public string Path
        {
            get
            {
                return path;
            }

            set
            {
                path = value;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stream datastream = null;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.filename = openFileDialog1.SafeFileName;
                this.path =System.IO.Path.GetDirectoryName(openFileDialog1.FileName);
                try
                {
                    if((datastream=openFileDialog1.OpenFile())!= null)
                    {
                        using (datastream)
                        {
                            pathHolder.Text=(openFileDialog1.SafeFileName);
                            textBox1.Text = filename.TrimEnd(new Char[] { '.','a','v','i'});
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error no se pudo leer el archivo" + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try {
                
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.WorkingDirectory = this.path;
                process.StartInfo.Arguments = "/C ffmpeg -i " + "\""+this.filename+ "\"" + " -vcodec libx264 -crf 20 " +"\"" +textBox1.Text + ".mp4"+"\"";
                process.EnableRaisingEvents = true;
                process.Exited += new EventHandler(OnProcessExit);
                process.Start(); }
            catch (Exception ex)
            {
                MessageBox.Show("error: " +ex.Message);
            }


            //Process.Start("cmd.exe");
        }

        private void OnProcessExit(object sender, EventArgs e)
        {
            Process proex = new Process();
            proex.StartInfo.FileName = this.path;
            proex.EnableRaisingEvents = true;
            proex.Start();
        }
    }
}
