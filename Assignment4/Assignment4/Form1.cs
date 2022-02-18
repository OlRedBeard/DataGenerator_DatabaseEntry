using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayers;

namespace Assignment4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Attempt to load connection to SQL Server
                // If successful, check SQLite for any records and copy them to SQL Server
                // Else, connect to SQLite and write to that database
        }

    }
}
