using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Even_Better_Aquarium
{
    public partial class Tank : Form
    {
        public Tank()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }
        Fish[] skool;
        Fish[] shark ;
        Food[] fishFood;
        Random randy;
        Image fishImage;
        Image[] RightImages = { Properties.Resources.Rfish1, Properties.Resources.Rfish2, Properties.Resources.Rhappyfish_removebg_preview, Properties.Resources.Rfish3_removebg_preview, Properties.Resources.Rnemo_removebg_preview, Properties.Resources.Rpuffer_removebg_preview };
        Image[] LeftImages = { Properties.Resources.Lfish1, Properties.Resources.Lfish2, Properties.Resources.Lhappyfish___Copy_removebg_preview, Properties.Resources.Lfish3_removebg_preview, Properties.Resources.Lnemo_removebg_preview, Properties.Resources.Lpuffer_removebg_preview };
        int[] scaleFactor = { 5 };

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.aqua2;
            skool = new Fish[0];
            fishFood = new Food[0];
            randy = new Random();
            shark = new Fish[0];
            
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Array.Resize<Fish>(ref skool, skool.Length + 1);
            timer1.Enabled = true;
            PointF start = new PointF(e.X, e.Y);
            int chooser = randy.Next(0, 6);
            int size = randy.Next(4, 7);
            //LeftImages[0].RotateFlip(RotateFlipType.Rotate180FlipY);
            skool[skool.Length - 1] = new Fish(start, this, LeftImages[chooser], RightImages[chooser],size);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text ="Fish Number: " + skool.Length.ToString();
            label1.Text = "Food Number: " + fishFood.Length;
            for (int i = 0; i < skool.Length; i++)
            {
                skool[i].update(ref fishFood, ref skool);
                if (skool[i].getY() < -10)
                {
                    removeFish(i);
                }
            }
            for(int j = 0; j<shark.Length; j++)
            {
                shark[j].update(ref fishFood, ref skool);
                if (shark[j].getY() < -10)
                {
                    removeFish(j);
                }
            }
            foreach (Food f in fishFood)
            {
                f.upDate();
            }

            Invalidate();
        }
        private void removeFish(int spot) //from skool
        {
            for (int i = spot; i < skool.Length - 1; i++)
            {
                skool[i] = skool[i + 1];
            }

            Array.Resize<Fish>(ref skool, skool.Length - 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Array.Resize<Food>(ref fishFood, fishFood.Length + 1);
            Food fishyFood = new Food(randy.Next(1, this.Width), 25f, this);
            fishFood[fishFood.Length - 1] = fishyFood;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Food f in fishFood)
            {
                f.Draw(e);
            }

            foreach (Fish fish in skool)
            {
                fish.Draw(e);
            }
            foreach(Fish shark in shark)
            {
                shark.Draw(e);
            }
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PointF start = new PointF(50, 50);
            skool[skool.Length - 1] = new Fish(start, this, Properties.Resources.Lshark_removebg_preview__1_, Properties.Resources.Rshark_removebg_preview, 1);
        }
    }
}
