using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Even_Better_Aquarium
{
    class Food
    {
        private PointF position;
        private float size;
        private Color foodColor;
        private Tank theTank;
        public Food(float xPosition, float size, Tank theTank)
        {
            this.size = size;
            this.position.X = xPosition;
            this.position.Y = position.Y;
            this.theTank = theTank;
        }

        public void Draw(PaintEventArgs e)
        {
            SolidBrush myBrush = new SolidBrush(Color.SaddleBrown);
            e.Graphics.FillEllipse(myBrush, position.X, position.Y, size, size);
        }

        public PointF Position
        {
            get { return position; }
        }

        public void drop()
        {
            float deltaY = Math.Abs(theTank.Height - position.Y);
            if (deltaY > 50)
            {
                position.Y++;
            }
        }

        public void upDate()
        {
          
            drop();
        }

    }
}