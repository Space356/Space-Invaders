using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    internal class Player : Sprite
    {
        Form1 client;
        float hsp = 0;
        float target_hsp = 0;
        public List<bullet> bullets = new List<bullet>();

        Collider collider;

        bool can_get_hit = true;

        public float health = 20;
        List<Color> health_colors = new List<Color>();

        public float cooldown = 0;

        public Player(Form1 client)
        {
            this.client = client;
            this.x = 384/2;
            this.y = 216-32;
            set_sprite(@"..\..\sprites\spr_ship.png", 16,16);

            this.collider = new Collider(this);

            health_colors.Add(Color.Red);
            health_colors.Add(Color.Orange);
            health_colors.Add(Color.Yellow);
            health_colors.Add(Color.Green);
        }

        public override void Update()
        {
            if (!this.client.pause_enemies)
            {
                if (true)
                {
                    this.x = GameMath.lerp(this.x, this.client.mouseManager.x - 8f, 0.1f);
                    this.image_angle = ((this.client.mouseManager.x - 8f) - this.x) * 0.5f;
                }
                else
                {
                    this.target_hsp = (Convert.ToInt32(this.client.keyManager.possible_keys[1].pressed) - Convert.ToInt32(this.client.keyManager.possible_keys[0].pressed)) * 5;
                    //this.x += Convert.ToInt32(this.client.keyManager.possible_keys[1].pressed)*3f;
                    this.hsp = GameMath.lerp(this.hsp, this.target_hsp, 0.25f);

                    this.x += hsp; //this is for the keyboard mode.
                    this.image_angle = this.hsp * 2.5f;
                }

                if (client.mouseManager.clicked && this.cooldown == 15)
                {
                    this.client.colliders.collisions_player_damage.Add(new bullet(this.client, this.x + 1f, this.y, GameMath.deg_to_rad(270), 10, this.client.colliders.collisions_player_damage));
                    this.cooldown = 0;
                }

                bullet collide_bullet = (bullet)collider.instance_meeting(this.x, this.y, this.client.colliders.collisions_damage);
                if (collide_bullet != null)
                {
                    if (can_get_hit)
                    {
                        this.health -= collide_bullet.damage;
                        this.can_get_hit = false;
                    }
                }
                else if (can_get_hit == false)
                {
                    this.can_get_hit = true;
                }

                if (this.health <= 0)
                {
                    this.client.pause_enemies = true;
                    this.health = 0;
                }

                if(this.cooldown < 15)
                {
                    this.cooldown ++;
                }
            }
        }

        public override void Draw(Graphics g)
        {
            if (this.health > 0)
            {
                draw_self(g);
                g.DrawString(this.health.ToString(), new Font(this.client.font_collection.Families[0], 8), new SolidBrush(health_colors[(int)((this.health / 26f) * 4f)]), (this.x * 2) + 8, (this.y * 2) - 16);

                g.FillRectangle(new SolidBrush(Color.Gray),(16)*2, 32,64,4);
                g.FillRectangle(new SolidBrush(Color.White), (16) * 2, 32, 64*(this.cooldown/15), 4);
            }
            else
            {
                g.DrawString("GAME OVER", new Font(this.client.font_collection.Families[0], 8), new SolidBrush(Color.Red), (this.x * 2) + 8, (this.y * 2) - 16);
            }
        }
    }
}