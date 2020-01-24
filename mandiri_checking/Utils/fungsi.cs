using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace mandiri_checking.Util
{
    class Animate
    {
        public enum Effect { Roll, Slide, Center, Blend }
        public void animasi(Control ctl, Effect effect, int msec, int angle)
        {
            int flags = effmap[(int)effect];
            if (ctl.Visible) { flags |= 0x10000; angle += 180; }
            else
            {
                if (ctl.TopLevelControl == ctl) flags |= 0x20000;
                else if (effect == Effect.Blend) throw new ArgumentException();
            }
            flags |= dirmap[(angle % 360) / 45];
            bool ok = AnimateWindow(ctl.Handle, msec, flags);
            if (!ok) throw new Exception("Animation failed");
            ctl.Visible = !ctl.Visible;
        }
        private static int[] dirmap = { 1, 5, 4, 6, 2, 10, 8, 9 };
        private static int[] effmap = { 0, 0x40000, 0x10, 0x80000 };
        [DllImport("user32.dll")]
        private static extern bool AnimateWindow(IntPtr handle, int msec, int flags);
    }

    class Transition
    {
        static System.Timers.Timer _timer;

        private Form frm;
        public enum Effect { Fadein, Fadeout}
        private Effect fungsi;

        private void time_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (fungsi == Effect.Fadein)
            {
                if (frm.Opacity <= 1.0)
                {
                    frm.Opacity += 0.025;
                }
                else
                {
                    _timer.Stop();
                    _timer.Enabled = false;
                }
            }
            else if (fungsi == Effect.Fadeout)
            {
                if (frm.Opacity >= 1.0)
                {
                    frm.Opacity -= 0.025;
                }
                else
                {
                    _timer.Stop();
                    _timer.Enabled = false;
                }
            }
        }

        public void FadeOut(Form _frm)
        {
            frm = _frm;
            frm.Opacity = 0.01;
            fungsi = Effect.Fadeout;
            var tm = new System.Timers.Timer(30000);
            tm.Elapsed += new System.Timers.ElapsedEventHandler(time_Elapsed);
            tm.Enabled = true;
            _timer = tm;
            
        }

        private void time_Elapsed(object state)
        {
            throw new NotImplementedException();
        }

        public void FadeIn(Form _frm)
        {
            frm = _frm;
            frm.Opacity = 1.0;
            fungsi = Effect.Fadein;
            var tm = new System.Timers.Timer(30000);
            tm.Elapsed += new System.Timers.ElapsedEventHandler(time_Elapsed);
            tm.Enabled = true;
            _timer = tm;
        }

    }

}
