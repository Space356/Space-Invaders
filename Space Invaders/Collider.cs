using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    internal class Collider
    {
        public Sprite obj;
        public Collider(Sprite obj)
        {
            this.obj = obj;
        }

        public bool place_meeting(float x, float y, List<Sprite> collisions)
        {
            bool val = false;
            for (int i = 0; i < collisions.Count; i++)
            {
                Sprite inst = collisions[i];
                if (x + this.obj.width > inst.x &&
                x < inst.x + inst.width &&
                y + this.obj.height > inst.y &&
                y < inst.y + inst.height)
                {
                    val = true;
                }
            }
            return (val);
        }

        public Sprite instance_meeting(float x, float y, List<Sprite> collisions)
        {
            Sprite val = null;
            for (int i = 0; i < collisions.Count; i++)
            {
                Sprite inst = collisions[i];
                if (x + this.obj.width > inst.x &&
                x < inst.x + inst.width &&
                y + this.obj.height > inst.y &&
                y < inst.y + inst.height)
                {
                    val = inst;
                }
            }
            return (val);
        }

        public void draw_instances(Graphics g, List<Sprite> collisions)
        {
            using (SolidBrush pen = new SolidBrush(Color.Green))
            {
                g.DrawString(collisions.Count.ToString(), new Font("Arial", 12), pen, 256, 108);
                for (int i = 0; i < collisions.Count; i++)
                {
                    Sprite inst = collisions[i];
                    g.DrawString(inst.x.ToString() + " " + inst.y.ToString(), new Font("Arial", 12), pen, 256, 200 + (i * 16));
                }
            }
        }
    }
}