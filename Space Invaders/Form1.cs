using Space_Invaders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Invaders
{
    public partial class Form1 : Form
    {
        Bitmap frame_buffer;
        Graphics g;

        Timer timer;
        public GlobalKeyManager keyManager = new GlobalKeyManager();
        public MouseManager mouseManager = new MouseManager();
        public int points = 1;

        public MainMenu menu;

        public bool ingame = false;

        public StringFormat sf;

        public CollisionGroups colliders = new CollisionGroups();

        public List<Sprite> sprites = new List<Sprite>();
        public List<Sprite> enemies = new List<Sprite>();

        StarBg stars;

        Player player;
        public Random rand = new Random();

        public PrivateFontCollection font_collection = new PrivateFontCollection();

        public bool pause_enemies = false;

        public float difficulty_scaler = 1.2f;

        public int curr_frame = 0;

        public Form1()
        {
            //sprite  = Image.FromFile("sprites\\c0c0.png");
            InitializeComponent();
            this.BackColor = Color.Black;
            this.DoubleBuffered = true;
            this.Visible = true;
            this.Width = 1280;
            this.Height = 720;

            this.frame_buffer = new Bitmap(384 * 2, 216 * 2);
            this.g = Graphics.FromImage(this.frame_buffer);

            font_collection.AddFontFile(@"..\..\fonts\highquality-pixel-font.ttf");

            stars = new StarBg(this);

            player = new Player(this);

            //for centering the font
            sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            //starts the game loop in 60 fps.
            timer = new Timer();
            timer.Interval = 1000 / 60;
            timer.Tick += new EventHandler(step);
            timer.Start();

            //makes the main menu
            this.menu = new MainMenu(this, this.keyManager);
        }

        public void start_game(float difficulty_scaler, MainMenu.Difficulties difficulty)
        {
            //List<Sprite> collisions = new List<Sprite>(); Just in case.

            this.ingame = true;
            this.sprites.Clear();
            this.sprites.Add(new enemy(this, player));
            this.pause_enemies = false;
            this.difficulty_scaler = difficulty_scaler;
        }

        public void step(Object myObject, EventArgs myEventArgs)
        {
            Invalidate();
            Application.DoEvents();

            this.curr_frame ++;

            if (this.keyManager.possible_keys[4].pressed)
            {
                menu.Update();
                ingame = false;

                this.points = 1;
                this.sprites.Clear();
                this.colliders.collisions_damage.Clear();
                this.colliders.collisions_player_damage.Clear();
                this.enemies.Clear();
                this.player.health = 20;
            }

            if (ingame)
            {
                stars.Update();
                player.Update();
                for (int i = 0; i < this.sprites.Count; i++)
                {
                    this.sprites[i].Update();
                }

                if(this.enemies.Count == 0)
                {
                    this.points++;
                    for (int j = 0;j < this.points*this.difficulty_scaler;j++)
                    {
                        this.sprites.Add(new enemy(this, player));
                    }
                }
            }
            else
            {
                stars.Update();
                menu.Update();
            }

            this.mouseManager.Update();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics Form_g = e.Graphics;

            g.Clear(this.BackColor);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            if (ingame)
            {
                stars.Draw(g);

                //Change this for a points display.
                using (SolidBrush pen = new SolidBrush(Color.FromArgb(0, 0, 50)))
                {
                    g.DrawString("WAVE "+this.points.ToString(), new Font(font_collection.Families[0], 30), pen, 384 + 5, 216 + 5, sf);
                }
                using (SolidBrush pen = new SolidBrush(Color.DarkBlue))
                {
                    g.DrawString("WAVE " + this.points.ToString(), new Font(font_collection.Families[0], 30), pen, 384, 216, sf);
                }

                player.Draw(g);
                for (int i = 0; i < sprites.Count; i++)
                {
                    sprites[i].Draw(g);
                }
            }
            else
            {
                stars.Draw(g);
                menu.draw(g);
            }
            Form_g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            float scaler = this.ClientSize.Width / 384f;
            Form_g.ScaleTransform(scaler / 2, scaler / 2);
            Form_g.DrawImage(frame_buffer,0,0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ignore this
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ignore this
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (var key in keyManager.possible_keys)
            {
                if (e.KeyData == key.key)
                {
                    key.pressed = true;
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            foreach (var key in keyManager.possible_keys)
            {
                if (e.KeyData == key.key)
                {
                    key.pressed = false;
                }
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            this.mouseManager.is_down = true;
            this.mouseManager.clicked = true;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            this.mouseManager.is_down = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            float scaler = this.ClientSize.Width / 384f;
            this.mouseManager.x = (int)(e.X / scaler);
            this.mouseManager.y = (int)(e.Y / scaler);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
    public class MouseManager
    {
        public int x;
        public int y;
        public bool is_down;
        public bool clicked;
        public MouseManager()
        {

        }

        public void Update()
        {
            if (this.clicked)
            {
                this.clicked = false;
            }
        }
    }
}