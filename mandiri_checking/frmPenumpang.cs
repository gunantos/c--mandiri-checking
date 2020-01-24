using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using mandiri_checking.Models;
using ESC_POS_USB_NET.Printer;
using QRCoder;
using ESC_POS_USB_NET.Enums;

namespace mandiri_checking
{
    public partial class frmPenumpang : Form
    {
        private bool fadeIn = false;
        public TicketMdl tiket = null;
        private frmProgress frmProgress;
        private bool isPrint = false;
        public frmPenumpang()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fadeIn = false;
            timer1.Interval = 100;
            timer1.Start();
            timer1.Enabled = true;
        }

        private void frmPenumpang_Load(object sender, EventArgs e)
        {

            this.Opacity = 0;
            backgroundWorker1.RunWorkerAsync();
            frmProgress = new frmProgress();
            frmProgress.ShowDialog();
                frmProgress.progressBar1.Value = 0;
            tiket.data_print = new List<TicketMdl_print>();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (fadeIn)
            {
                if (this.Opacity <= 1.0)
                {
                    this.Opacity += 0.300;
                }
                else
                {
                    timer1.Stop();
                    timer1.Enabled = false;
                }
            }
            else
            {
                if (this.Opacity >= 0.1)
                {
                    this.Opacity -= 0.300;
                }
                else
                {
                    timer1.Stop();
                    timer1.Enabled = false;
                    Close();
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for(int i=1; i <= 100; i++)
            {
                backgroundWorker1.ReportProgress(i);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           frmProgress.Close();
           timer1.Interval = 100;
           fadeIn = true;
           timer1.Start();
           timer1.Enabled = true;
        }

        private void backgroundWorker1_ProgressChanged_1(object sender, ProgressChangedEventArgs e)
        {
            frmProgress.progressBar1.Value = e.ProgressPercentage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (Properties.Settings.Default.NoregPrint == null)
            {
                MessageBox.Show("Pilih salah satu penumpang");

            }
            else
            {
                if (Properties.Settings.Default.NoregPrint.Count > 0)
                {
                    foreach (TicketMdl_data ts in tiket.data)
                    {
                        int no = 1;
                        if (tiket.data_detail != null)
                        {
                            foreach (TicketMdl_detail zs in tiket.data_detail)
                            {
                                String noreg = $"{ts.ticket_id}{no}";
                                if (Properties.Settings.Default.NoregPrint.Contains(noreg))
                                {
                                    TicketMdl_print param = new TicketMdl_print();
                                    param.boarding_id = noreg;
                                    param.address = zs.address;
                                    param.city = zs.city;
                                    param.date_of_departure = ts.date_of_departure;
                                    param.destination_id = ts.destination_id;
                                    param.destination_name = ts.destination_name;
                                    param.email = zs.email;
                                    param.name = zs.name;
                                    param.nik = zs.nik;
                                    param.owner_id = ts.owner_id;
                                    param.po_id = ts.po_id;
                                    param.po_name = ts.po_name;
                                    param.province = zs.province;
                                    param.sex = zs.sex;
                                    param.status_pnp = "print";
                                    param.telp = zs.telp;
                                    param.ticket_id = ts.ticket_id;
                                    param.time_of_departure = ts.time_of_departure;
                                    tiket.data_print.Add(param);
                                    isPrint = true;
                                }
                            }
                        }
                        else
                        {
                            String noreg = $"{ts.ticket_id}{no}";
                            if (Properties.Settings.Default.NoregPrint.Contains(noreg))
                            {
                                TicketMdl_print param = new TicketMdl_print();
                                param.boarding_id = noreg;
                                param.address = "";
                                param.city = "";
                                param.date_of_departure = ts.date_of_departure;
                                param.destination_id = ts.destination_id;
                                param.destination_name = ts.destination_name;
                                param.email = "";
                                param.name = "";
                                param.nik = "";
                                param.owner_id = ts.owner_id;
                                param.po_id = ts.po_id;
                                param.po_name = ts.po_name;
                                param.province = "";
                                param.sex = "";
                                param.status_pnp = "print";
                                param.telp = "";
                                param.ticket_id = ts.ticket_id;
                                param.time_of_departure = ts.time_of_departure;
                                tiket.data_print.Add(param);
                                isPrint = true;
                            }
                        }
                    }
                }
                if (this.isPrint == false)
                {
                    MessageBox.Show("Pilih salah satu penumpang");
                }
                else
                {
                    var api = new Controller.Api();
                    TicketMdl_status  status = api.saveTiket(tiket.data_print);
                    
                        print(tiket.data_print);
                    
                }
            }
        }

        private void print(List<TicketMdl_print> _data)
        {
            foreach(TicketMdl_print q in _data)
            {
                var cmd = new ESC_POS_USB_NET.EpsonCommands.BitmapData();
                QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qRCodeGenerator.CreateQrCode(q.boarding_id, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, true);
                Bitmap newImage = new Bitmap(qrCodeImage, 50, 50);
                Bitmap image = new Bitmap(Bitmap.FromFile("test.bmp"));
                Printer printer = new Printer(Properties.Settings.Default.PrinterSetting);
                printer.AlignCenter();
                printer.BoldMode("BOARDING PASS");
                printer.BoldMode("TERMINAL PULO GEBANG");
                printer.Append(DateTime.Now.ToString("dddd, dd MMMM yyyy"));
                printer.Separator('=');
                printer.AlignLeft();
                printer.ExpandedMode(PrinterModeState.On);
                printer.Image(newImage);
                printer.NewLines(1);
                printer.Append($"BOARDING ID: {q.boarding_id}");
                printer.Append($"TUJUAN     : {q.destination_name}");
                printer.Append($"NAMA PO    : {q.po_name}");
                printer.Append($"TANGGAL    : {q.date_of_departure}");
                printer.Append($"JAM        : {q.time_of_departure}");
                printer.Append($"PENUMPANG  : {q.name}");
                printer.NewLines(1);
                printer.Separator();
                printer.PartialPaperCut();
                printer.AlignCenter();
                printer.BoldMode("BOARDING PASS");
                printer.BoldMode("TERMINAL PULO GEBANG");
                printer.Append(DateTime.Now.ToString("dddd, dd MMMM yyyy"));
                printer.Separator('=');
                printer.Code128(q.boarding_id);
                printer.AlignLeft();
                printer.Append($"ID       : {q.boarding_id}");
                printer.Append($"NAMA PO  : {q.po_name}");
                printer.Append($"TUJUAN   : {q.destination_name}");
                printer.Append($"TANGGAL  : {q.date_of_departure}");
                printer.Append($"JAM      : {q.time_of_departure}");
                printer.Append($"PENUMPANG: {q.name}");
                printer.FullPaperCut();
                printer.PrintDocument();
            }
            
            Close();
            Properties.Settings.Default.NoregPrint.Clear();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
