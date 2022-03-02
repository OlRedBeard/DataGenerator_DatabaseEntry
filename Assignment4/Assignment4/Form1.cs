using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayers;

namespace Assignment4
{
    public partial class Form1 : Form
    {
        SQLServerDataLayer dl = new SQLServerDataLayer();
        SQLiteDatalayer dl2 = new SQLiteDatalayer();

        public Form1()
        {
            InitializeComponent();
            dl.TestConnection();
            lblStatus.Text = SQLServerDataLayer.connected.ToString();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dl.TestConnection();
            lblStatus.Text = SQLServerDataLayer.connected.ToString();

            if (SQLServerDataLayer.connected)
            {
                BackgroundWorker bgw = new BackgroundWorker();
                bgw.DoWork += Bgw_DoWork;
                bgw.RunWorkerCompleted += Bgw_RunWorkerCompleted;
                bgw.RunWorkerAsync();
            }
        }

        private void Bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable dt = dl2.GetTableData("ELECTRIC");
            string type = "ELECTRIC";
            foreach (DataRow rows in dt.Rows)
            {
                string id = rows[0].ToString();
                DateTime tStamp = Convert.ToDateTime(rows[1].ToString());
                decimal uom1 = Convert.ToDecimal(rows[2]);
                decimal uom2 = Convert.ToDecimal(rows[3]);
                dl.WriteLine(id, type, tStamp, uom1, uom2);
            }
            dl2.ClearTableData("ELECTRIC");

            dt = null;
            dt = dl2.GetTableData("GPS");
            type = "GPS";
            foreach (DataRow rows in dt.Rows)
            {
                string id = rows[0].ToString();
                DateTime tStamp = Convert.ToDateTime(rows[1].ToString());
                decimal uom1 = Convert.ToDecimal(rows[2]);
                decimal uom2 = Convert.ToDecimal(rows[3]);
                dl.WriteLine(id, type, tStamp, uom1, uom2);
            }
            dl2.ClearTableData("GPS");

            dt = null;
            dt = dl2.GetTableData("GAS");
            type = "GAS";
            foreach (DataRow rows in dt.Rows)
            {
                string id = rows[0].ToString();
                DateTime tStamp = Convert.ToDateTime(rows[1].ToString());
                decimal uom1 = Convert.ToDecimal(rows[2]);
                decimal uom2 = Convert.ToDecimal(rows[3]);
                dl.WriteLine(id, type, tStamp, uom1, uom2);
            }
            dl2.ClearTableData("GAS");

            dt = null;
            dt = dl2.GetTableData("H20");
            type = "H20";
            foreach (DataRow rows in dt.Rows)
            {
                string id = rows[0].ToString();
                DateTime tStamp = Convert.ToDateTime(rows[1].ToString());
                decimal uom1 = Convert.ToDecimal(rows[2]);
                decimal uom2 = Convert.ToDecimal(rows[3]);
                dl.WriteLine(id, type, tStamp, uom1, uom2);
            }
            dl2.ClearTableData("H20");
        }

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            string[] fileLines = File.ReadAllLines("E:/datatest/datafile.csv");
            foreach (string line in fileLines)
            {
                string id = line.Split(',')[0];
                string type = line.Split(',')[1];
                DateTime tStamp = Convert.ToDateTime(line.Split(',')[2]);
                decimal uom1 = Convert.ToDecimal(line.Split(',')[4]);
                decimal uom2 = Convert.ToDecimal(line.Split(',')[6]);
                timer1.Stop();
                bool connected = SQLServerDataLayer.connected;
                lblStatus.Text = connected.ToString();

                if (connected)
                {
                    dl.WriteLine(id, type, tStamp, uom1, uom2);
                }

                if (!connected)
                {
                    timer1.Start();
                    dl2.WriteLine(id, type, tStamp, uom1, uom2);
                }
            }
        }
    }
}
