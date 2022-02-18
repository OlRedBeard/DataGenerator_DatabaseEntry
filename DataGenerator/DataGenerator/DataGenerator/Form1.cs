using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Concurrent; // For Concurrent Queue
using System.Configuration; // For AppConfig Values
using System.IO; // For StreamWriter

namespace DataGenerator
{
    public partial class Form1 : Form
    {
        BackgroundWorker bgw1 = new BackgroundWorker();
        BackgroundWorker bgw2 = new BackgroundWorker();

        ConcurrentQueue<IoTData> cQ = new ConcurrentQueue<IoTData>();

        bool cancel = false; // Flag to stop background worker

        string filePath = ConfigurationManager.AppSettings.Get("filePath");
        string fileDir = ConfigurationManager.AppSettings.Get("fileDir");
        int deviceCount = int.Parse(ConfigurationManager.AppSettings.Get("deviceCount"));
        int dataDelay = int.Parse(ConfigurationManager.AppSettings.Get("dataDelayMillis"));

        public Form1()
        {
            InitializeComponent();

            bgw1.WorkerSupportsCancellation = true;
            bgw1.WorkerReportsProgress = true;
            bgw2.WorkerSupportsCancellation = true;
            bgw2.WorkerReportsProgress = true;

            bgw1.ProgressChanged += Bgw1_ProgressChanged;
            bgw1.DoWork += Bgw1_DoWork;
            bgw1.RunWorkerCompleted += Bgw1_RunWorkerCompleted;

            bgw2.DoWork += Bgw2_DoWork;
            bgw2.RunWorkerCompleted += Bgw2_RunWorkerCompleted;
        }

        private void Bgw2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("File Was Written");
        }

        private void Bgw2_DoWork(object sender, DoWorkEventArgs e)
        {
            IoTData tmp;
            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
                System.Threading.Thread.Sleep(1000);
            }

            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                FileInfo info = new FileInfo(filePath);
                while(cQ.Count != 0)
                {
                    cQ.TryDequeue(out tmp);
                    writer.Write(tmp.ToString());
                }
            }
        }

        private void Bgw1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Bgw1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Make an infinite loop
            while (true)
            {
                // Make IoT device and add it to the queue for each device type
                for(int i = 1; i <= deviceCount; i++)
                {
                    cQ.Enqueue(new IoTData("ELEC" + i.ToString(), "ELECTRIC"));
                    cQ.Enqueue(new IoTData("GEO" + i.ToString(), "GPS"));
                    cQ.Enqueue(new IoTData("GMETER" + i.ToString(), "GAS"));
                    cQ.Enqueue(new IoTData("H2O" + i.ToString(), "H2O"));
                }
                if (cancel)
                {
                    bgw1.CancelAsync();
                    break;
                }
                // Start BGW2 here to do something
                if (bgw2.IsBusy == false)
                    bgw2.RunWorkerAsync();

                System.Threading.Thread.Sleep(dataDelay);
            }
        }

        private void Bgw1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            bgw1.RunWorkerAsync();
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {

        }
    }
}
