using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KeyFour
{
    public partial class frmKeyFour : Form
    {
        StringFormat stringFormat = new StringFormat();

        public frmKeyFour()
        {
            InitializeComponent();

            stringFormat.Alignment = StringAlignment.Center;      // Horizontal Alignment
            stringFormat.LineAlignment = StringAlignment.Center;  // Vertical Alignment
        }

        private void SendKeyboard(string sign)
        {
            this.Hide();
            IntPtr hWnd = GetForegroundWindow();
            SetForegroundWindow(hWnd);

            SendKeys.SendWait(sign);

            this.Show();
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        // import the function in your class
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.Hide();
            }
            else
            {
                this.Show();
            }
        }

        private bool mouseIsDown = false;
        private Point firstPoint;
        private Point formLocation;
        private int _formState = 0;
        private Font small = new Font("Consolas", 12, FontStyle.Regular);
        private Font big = new Font("Consolas", 20, FontStyle.Regular);
        double lower = 0.5;

        public int FormState
        {
            get { return _formState; }
            set {
                if (_formState != value)
                {
                    _formState = value;
                    this.Invalidate();
                }
            }
        }

        Rectangle four = new Rectangle(0, 0, 40, 40);
        Rectangle dollar = new Rectangle(40, 0, 40, 40);

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            firstPoint = e.Location;
            mouseIsDown = true;
            formLocation = this.Location;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
            if (formLocation == this.Location)
            {
                formLocation = this.Location;

                if (new Rectangle(e.Location, new Size(1, 1)).IntersectsWith(four))
                {
                    SendKeyboard("4");
                }
                else
                {
                    SendKeyboard("$");
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                // Get the difference between the two points
                int xDiff = firstPoint.X - e.Location.X;
                int yDiff = firstPoint.Y - e.Location.Y;

                // Set the new point
                int x = this.Location.X - xDiff;
                int y = this.Location.Y - yDiff;
                this.Location = new Point(x, y);
            }
            else
            {
                if (new Rectangle(e.Location, new Size(1, 1)).IntersectsWith(four))
                {
                    FormState = -1;
                }
                else
                {
                    FormState = 1;
                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Size = new Size(80, 40);
            this.Opacity = lower;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Font fourFont;
            Font dollarFont;

            if (FormState == -1)
            {
                fourFont = big;
                dollarFont = small;
            }
            else if (FormState == 1)
            {
                fourFont = small;
                dollarFont = big;
            }
            else
            {
                dollarFont = fourFont = small;
            }

            e.Graphics.DrawString("4", fourFont, Brushes.Gray, four, stringFormat);
            e.Graphics.DrawString("$", dollarFont, Brushes.Gray, dollar, stringFormat);
        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            this.Opacity = lower;
            this.FormState = 0;
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
