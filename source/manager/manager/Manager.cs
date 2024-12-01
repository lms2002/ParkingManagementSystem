using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace manager
{
    public partial class Manager : Form
    {
        private readonly string connectionString = "User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)))";
        public Manager()
        {
            InitializeComponent();
        }

        private void 차량관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Vehiclemanager form1 = new Vehiclemanager())
            {
                form1.ShowDialog();
            }
        }

        private void 매출관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SalesManagement form1 = new SalesManagement(connectionString))
            {
                form1.ShowDialog();
            }
        }
    }
}
