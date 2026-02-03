using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    internal class StarBg
    {
        List<Sprite> stars = new List<Sprite>();
        Random rand = new Random();
        public StarBg(Form1 client)
        {
            for (int i = 0; i < 50; i++)
            {
                stars.Add(new Sprite(@"..\..\sprites\TransparentWhitePixel.png", rand.Next(384), rand.Next(216),1,1,client));
            }
        }
        public void Update()
        {
            foreach (Sprite inst in stars)
            {
                inst.y += 0.5f;
                if(inst.y >= 216)
                {
                    inst.y = 0;
                    inst.x = rand.Next(384);
                }
            }
        }
        public void Draw(Graphics g)
        {
            foreach (Sprite inst in stars)
            {
                inst.draw_self(g);
            }
        }
    }
}
