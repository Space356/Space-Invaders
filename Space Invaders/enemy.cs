using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    internal class enemy : Sprite
    {
        float target_x = 0;
        float target_y = 0;

        Form1 client;

        Alarm move_alarm;
        Alarm shoot_alarm;

        bool can_get_hit = true;

        Collider collider;

        Player player;
        public float health = 2;
        Random rand_seed = new Random();

        public enemy(Form1 client, Player player)
        {
            set_sprite(@"..\..\sprites\spr_enemy.png",16,16);
            this.client = client;
            this.player = player;

            //this.client.sprites.Add(this);

            collider = new Collider(this);

            this.client.enemies.Add(this);

            this.x = 192-8;
            this.y = -64;

            this.target_x = this.x;
            this.target_y = this.y;

            move_alarm = new Alarm(()=>
            {
                this.target_x = (float)client.rand.Next(384);
                this.target_y = (float)client.rand.Next(108);
                this.move_alarm.Start(client.rand.Next(30, 120));
            });
            this.move_alarm.Start(15);
            shoot_alarm = new Alarm(() =>
            {
                this.client.colliders.collisions_damage.Add(new bullet(this.client, this.x, y, GameMath.point_direction(x, this.player.x, this.y, this.player.y), 8, this.client.colliders.collisions_damage));
                this.shoot_alarm.Start(client.rand.Next(30,120));
            });
            this.shoot_alarm.Start(this.client.rand.Next(60,120));
        }
        public override void Update()
        {
            if (!this.client.pause_enemies)
            {
                this.move_alarm.Update();
                this.shoot_alarm.Update();
                this.x = GameMath.lerp(this.x, this.target_x, 0.1f);
                this.y = GameMath.lerp(this.y, this.target_y, 0.1f);

                bullet collide_bullet = (bullet)collider.instance_meeting(this.x, this.y, this.client.colliders.collisions_player_damage);
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
                    this.client.sprites.Remove(this);
                    this.client.enemies.Remove(this);
                }
            }
        }

        public override void Draw(Graphics g)
        {
            draw_self(g);
            using (SolidBrush pen = new SolidBrush(Color.Red))
            {
                g.DrawString(this.health.ToString(), new Font(this.client.font_collection.Families[0], 8), pen, (this.x*2)+8, (this.y*2)-16);
            }
        }
    }
}