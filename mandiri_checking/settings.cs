using ESC_POS_USB_NET.Printer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mandiri_checking
{
    public partial class settings : Form
    {
        public settings()
        {
            InitializeComponent();
        }

        private void settings_Load(object sender, EventArgs e)
        {
            listPrinter.Items.Clear();
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                listPrinter.Items.Add(printer);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Printer printer = new Printer(listPrinter.SelectedItem.ToString());
            printer.TestPrinter();
            printer.FullPaperCut();
            printer.PrintDocument();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string item = listPrinter.SelectedItem.ToString();
            Properties.Settings.Default.PrinterSetting = item;
            Close();
        }
    }
}
