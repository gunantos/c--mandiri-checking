using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mandiri_checking.Controller;

namespace mandiri_checking
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.BackColor = Color.Transparent;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public Keys key;
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr extra;
        }
        //System level functions to be used for hook and unhook keyboard input  
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int id, LowLevelKeyboardProc callback, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hook);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hook, int nCode, IntPtr wp, IntPtr lp);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string name);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern short GetAsyncKeyState(Keys key);
        //Declaring Global objects     
        private IntPtr ptrHook;
        private LowLevelKeyboardProc objKeyboardProcess;

        private IntPtr captureKey(int nCode, IntPtr wp, IntPtr lp)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)System.Runtime.InteropServices.Marshal.PtrToStructure(lp, typeof(KBDLLHOOKSTRUCT));

                // Disabling Windows keys 

                if (objKeyInfo.key == Keys.RWin || objKeyInfo.key == Keys.LWin || objKeyInfo.key == Keys.Tab && HasAltModifier(objKeyInfo.flags) || objKeyInfo.key == Keys.Escape && (ModifierKeys & Keys.Control) == Keys.Control)
                {
                    return (IntPtr)1; // if 0 is returned then All the above keys will be enabled
                }
            }
            return CallNextHookEx(ptrHook, nCode, wp, lp);
        }

        bool HasAltModifier(int flags)
        {
            return (flags & 0x20) == 0x20;
        }

        private void getTiket()
        {
            var api = new Api();
            if (textBox1.Text != "")
            {
                Models.TicketMdl tiket_data = api.getTiket(textBox1.Text);
                if (tiket_data.status == true)
                {
                    var fm = new frmPenumpang();
                    fm.nmPO.Text = tiket_data.data[0].po_name;
                    fm.tujuan.Text = tiket_data.data[0].destination_name;
                    fm.tgl.Text = $"{tiket_data.data[0].date_of_departure} {tiket_data.data[0].time_of_departure}";
                    const int gap = 20;
                    int count = 0;
                    if (tiket_data.data_detail != null)
                    {
                        foreach (Models.TicketMdl_detail hasil in tiket_data.data_detail)
                        {
                            string julukan = "";
                            if (hasil.sex == "l")
                            {
                                julukan = "Tn. ";
                            }
                            else if (hasil.sex == "p")
                            {
                                julukan = "Ny. ";
                            }
                            var add = new pnpUC();
                            add.checkbox1.Text = $"{julukan} {hasil.name}";
                            add.noreg.Text = $"{tiket_data.data[0].ticket_id}{(count + 1)}";
                            add.Top = count * (add.Height + gap);
                            fm.panel2.Controls.Add(add);
                            count++;
                        }
                    }
                    else
                    {
                        var add = new pnpUC();
                        add.checkbox1.Text = "No name";
                        add.noreg.Text = $"{tiket_data.data[0].ticket_id}{(count + 1)}";
                        add.Top = count * (add.Height + gap);
                        fm.panel2.Controls.Add(add);
                    }
                    fm.tiket = tiket_data;
                    this.textBox1.Text = "";
                    fm.ShowDialog(this);
                }
                else
                {
                    MessageBox.Show("Tiket ID Tidak Ditemukan", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Tiket ID Tidak Ditemukan", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            getTiket();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessModule objCurrentModule = System.Diagnostics.Process.GetCurrentProcess().MainModule;
            objKeyboardProcess = new LowLevelKeyboardProc(captureKey);
            ptrHook = SetWindowsHookEx(13, objKeyboardProcess, GetModuleHandle(objCurrentModule.ModuleName), 0);
            var frmPrinter = new settings();
            frmPrinter.Show(this);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                getTiket();
            }else if (e.Control && e.Shift && e.KeyCode == Keys.P)
            {
                var frmPrinter = new settings();
                frmPrinter.Show(this);
            }
        }
    }
}
