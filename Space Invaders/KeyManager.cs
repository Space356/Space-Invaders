using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Invaders
{
    public class KeyManager
    {
        public Keys key;
        public bool pressed = false;
        public KeyManager(Keys key)
        {
            this.key = key;
        }
    }
}