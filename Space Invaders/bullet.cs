using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    internal class bullet : Sprite
    {
        List<Sprite> bullet_list;
        public float damage = 1f;
        Form1 client;
        public bullet(Form1 client, float x,float y,float direction, float speed, List<Sprite> bullet_list)
        {
            this.x = x;
            this.y = y;
            this.direction = direction;
            this.speed = speed;
            set_sprite(@"..\..\sprites\spr_bullet.png", 14, 2);
            this.bullet_list = bullet_list;
            this.image_angle = GameMath.rad_to_deg(this.direction);
            this.client = client;
            this.client.sprites.Add(this);
        }
        public override void Update()
        {
            update_direction();
            if(this.y < -32f || this.y > 220f)
            {
                this.bullet_list.Remove(this);
                this.client.sprites.Remove(this);
                return;
            }
        }
    }
}