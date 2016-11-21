using System;
using System.Windows.Forms;

namespace Step2OpacityForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            this.Opacity = 0.5;
        }
    }
}
