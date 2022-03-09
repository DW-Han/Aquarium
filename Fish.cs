using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Even_Better_Aquarium
{
    class Fish
    {
        
        private PointF target, position;
        private Tank theTank;
        private int hunger;
        private bool isAlive;
        private int speed;
        private Random randy;
        private Image fishImage;
        private float scaleFactor;
        private Image Lfishimage, Rfishimage;
        private Point[] cords = new Point[3];
        public Fish(PointF startPoint, Tank theTank, Image Lfishimage,Image Rfishimage,int scaleFactor)
        {
            this.Lfishimage = Lfishimage;
            this.Rfishimage = Rfishimage;
            fishImage = Rfishimage;
            this.scaleFactor = scaleFactor;
            randy = new Random();
            hunger = 1500;
            speed = randy.Next(5, 20);
            isAlive = true;
            position = startPoint;
            this.theTank = theTank;
            int targX = randy.Next(1, theTank.Width);
            int targY = randy.Next(1, theTank.Height);
            this.target = new PointF(targX, targY);
            //theTank.Controls.Add(fish123);
           

        }

        public int getY()
        {
            return (int)position.Y;
        }

        public void swim()
        {

            
            hunger = hunger - 5;

            if (hunger < 0)
            {
                isAlive = false;
                //if (position.Y < -5)
                //{
                //    theTank.Controls.Remove(fishLabel);
                //}

            }


            if (hunger < 1000)
            {
                //fishLabel.BackColor = Color.Orange;
            }

            float deltaX = target.X - position.X;
            float deltaY = target.Y - position.Y;

            if (isAlive)
            {
                float angleToTarget = (float)Math.Atan2(deltaY, deltaX);

                position.X += (float)Math.Cos(angleToTarget) * speed;

                position.Y += (float)Math.Sin(angleToTarget) * speed;
            }

            else
            {
                position.Y--;
                fishImage = Properties.Resources.fishbone_removebg_preview;
            }



            
        }

        private float getDistance(PointF spot, PointF dest)
        {
            float distance = (float)Math.Sqrt(Math.Pow(spot.X - dest.X, 2) + Math.Pow(spot.Y - dest.Y, 2));
            return distance;
        }

        public PointF getTarget()
        {
            float distance = getDistance(position, target);
            if (distance < 10)
            {
                int x = randy.Next(0, theTank.Width);
                int y = randy.Next(0, theTank.Height);
                target = new PointF(x, y);
                
            }

            
            return target;

        }


        public int TargetFood(Food[] allFood)
        {
            float min = 1000000;
            int spot = 0;
            for (int i = 0; i < allFood.Length; i++)
            {
                float distance = getDistance(position, allFood[i].Position);
                if (distance < min)
                {
                    min = distance;
                    spot = i;
                }
            }

            return spot;

        }
        public int TargetFish(Fish[] allFish)
        {
            float min = 1000000;
            int spot = 0;
            for (int i = 0; i < allFish.Length; i++)
            {
                float distance = getDistance(position, allFish[i].position);
                if (distance < min)
                {
                    min = distance;
                    spot = i;
                }
            }

            return spot;
        }


        public void update(ref Food[] allFood, ref Fish[] allFish)
        {
            if (target.X < position.X)
            {
                fishImage = Lfishimage;
            }
            else
            {
                fishImage = Rfishimage;
            }
            if (fishImage == Properties.Resources.Rshark_removebg_preview || fishImage == Properties.Resources.Lshark_removebg_preview__1_)
            {
                if (allFish.Length > 0)
                {
                    target = allFish[TargetFish(allFish)].position;
                    int spot = TargetFish(allFish);

                    if (getDistance(target, position) < 20)
                    {
                        hunger = 2000;

                        for (int i = spot; i < allFish.Length - 1; i++)
                        {
                            allFish[i] = allFish[i + 1];
                        }

                        Array.Resize<Fish>(ref allFish, allFish.Length - 1);
                    }

                }
            }
            if (hunger < 1000)
            {
                if (fishImage != Properties.Resources.Rshark_removebg_preview && fishImage != Properties.Resources.Lshark_removebg_preview__1_)
                {

                    if (allFood.Length > 0)
                    {
                        target = allFood[TargetFood(allFood)].Position;
                        int spot = TargetFood(allFood);

                        if (getDistance(target, position) < 10)
                        {
                            hunger = 2000;

                            for (int i = spot; i < allFood.Length - 1; i++)
                            {
                                allFood[i] = allFood[i + 1];
                            }

                            Array.Resize<Food>(ref allFood, allFood.Length - 1);
                        }

                    }
                }
            }
            
            
            target = getTarget();
                          
            

            swim();
        }



       
        public void Draw(PaintEventArgs e)
        {

            e.Graphics.DrawImage(fishImage, position.X, position.Y, fishImage.Width / scaleFactor, fishImage.Height / scaleFactor);
  
        }

        public float angleFinder()
        {
            Point center = new Point((int)position.X, (int)position.Y);
            float xLength = (center.X - target.Y);
            float yLength = (center.Y - target.Y);
            return (float)Math.Atan2(yLength, xLength);
        }
    }
}