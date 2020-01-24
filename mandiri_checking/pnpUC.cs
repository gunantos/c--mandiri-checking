using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mandiri_checking
{
    public partial class pnpUC : UserControl
    {
        public pnpUC()
        {
            InitializeComponent();
        }

        private void checkbox1_CheckedChanged(object sender, EventArgs e)
        {
            schecked();
        }

        private void schecked()
        {
            var prN = Properties.Settings.Default.NoregPrint;
            if(checkbox1.CheckState == CheckState.Checked)
            {
                Properties.Settings.Default.NoregPrint.Add(noreg.Text);
            }
            else
            {
                Properties.Settings.Default.NoregPrint.Remove(noreg.Text);
            }
        }

    }
}
