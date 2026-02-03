using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    public class Sprite
    {
        public float x = 0;
        public float y = 0;
        public int width = 32;
        public int height = 32;

        public float direction = 0;
        public float speed = 0;
        public float x_speed = 0;
        public float y_speed = 0;

        public float image_angle = 360;

        Form1 client;
        Image sprite = null;
        
        public Sprite(float x, float y, int width, int height, Form1 client)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.client = client;

            //right now, this just works as a way to manage collisions.
        }
        public Sprite(String filepath, float x, float y, int width, int height, Form1 client)
        {
            this.x = x;
            this.y = y;
            this.client = client;

            set_sprite(filepath, width, height);
            //right now, this just works as a way to manage collisions.
        }
        public Sprite()
        {
            //To get errors to stop.
        }

        public bool mouse_inside(Form1 client)
        {
            return (client.mouseManager.x > this.x && client.mouseManager.y > this.y &&
                client.mouseManager.x < this.x + this.width && client.mouseManager.y < this.y + this.height);
        }

        public void draw_self(Graphics g)
        {
            /*int newWidth;
            int newHeight;
            if (this.image_angle <= 90)
            {
                newWidth = (int)(this.width * Math.Cos(GameMath.deg_to_rad(this.image_angle)) + this.height * Math.Sin(GameMath.deg_to_rad(this.image_angle)));
                newHeight = (int)(this.height * Math.Cos(GameMath.deg_to_rad(this.image_angle)) + this.width * Math.Sin(GameMath.deg_to_rad(this.image_angle)));
            }
            else if (this.image_angle <= 180)
            {
                newWidth = (int)(this.width * -Math.Cos(GameMath.deg_to_rad(this.image_angle)) + this.height * Math.Sin(GameMath.deg_to_rad(this.image_angle)));
                newHeight = (int)(this.height * -Math.Cos(GameMath.deg_to_rad(this.image_angle)) + this.width * Math.Sin(GameMath.deg_to_rad(this.image_angle)));
            }
            else if (this.image_angle <= 270)
            {
                newWidth = (int)(this.width * -Math.Cos(GameMath.deg_to_rad(this.image_angle)) + this.height * -Math.Sin(GameMath.deg_to_rad(this.image_angle)));
                newHeight = (int)(this.height * -Math.Cos(GameMath.deg_to_rad(this.image_angle)) + this.width * -Math.Sin(GameMath.deg_to_rad(this.image_angle)));
            }
            else
            {
                newWidth = (int)(this.width * Math.Cos(GameMath.deg_to_rad(this.image_angle)) + this.height * -Math.Sin(GameMath.deg_to_rad(this.image_angle)));
                newHeight = (int)(this.height * Math.Cos(GameMath.deg_to_rad(this.image_angle)) + this.width * -Math.Sin(GameMath.deg_to_rad(this.image_angle)));
            }

                using (SolidBrush pen = new SolidBrush(Color.FromArgb(0, 0, 50)))
                {
                    g.DrawString(newWidth.ToString(), new Font("Arial", 12), pen, this.x, this.y);
                    g.DrawString(newHeight.ToString(), new Font("Arial", 12), pen, this.x, this.y - 32);
                }
            Bitmap draw_result = new Bitmap((newWidth*2), (newHeight*2)); //Double just to account for rotation (for now)
            Graphics g2 = Graphics.FromImage(draw_result);
            using (SolidBrush pen = new SolidBrush(Color.FromArgb(0, 0, 50)))
            {
                g2.FillRectangle(pen, 0, 0, 100, 100);
            }
            g2.ScaleTransform(2, 2);
            g2.TranslateTransform(this.sprite.Width/2, this.sprite.Height/2);
            g2.RotateTransform(this.image_angle);
            g2.TranslateTransform(-this.sprite.Width/2, -this.sprite.Height/2);
            g2.InterpolationMode = InterpolationMode.NearestNeighbor;
            g2.DrawImage(this.sprite, 0, 0);

            g.DrawImage(draw_result, x, y);*/

            Matrix m = new Matrix();

            m.Translate(x*2 + this.width, y*2 + this.height);
            m.Rotate(this.image_angle);
            m.Translate(-this.width, -this.height);
            m.Scale(2, 2);

            g.Transform = m;
            g.DrawImage(this.sprite, 0, 0);
            g.ResetTransform();

            m.Dispose();
        }

        public void set_sprite(String file_path, int width, int height)
        {
            this.sprite = Image.FromFile(file_path);
            this.width = width;
            this.height = height;
        }

        public void update_direction()
        {
            this.x_speed = (float)Math.Cos(direction) * speed;
            this.y_speed = (float)Math.Sin(direction) * speed;

            this.x += x_speed;
            this.y += y_speed;
        }

        public virtual void Update(){}

        public virtual void Draw(Graphics g)
        {
            draw_self(g);
        }
    }
}