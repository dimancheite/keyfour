using System.Drawing;
using System.Windows.Forms;

namespace Step1MoveForm
{
    public partial class Form1 : Form
    {
        //State that we currently click and hold mouse button
        private bool mouseIsDown = false;
        //First mouse down point
        private Point firstPoint;
        //Form location of the screen
        private Point formLocation;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //Store location of mouse down it will be used to calculate moving
            firstPoint = e.Location;
            mouseIsDown = true;
            formLocation = this.Location;
        }


        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            //State that the mouse is up so no need to move form anymore
            mouseIsDown = false;
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
        }
    }
}
