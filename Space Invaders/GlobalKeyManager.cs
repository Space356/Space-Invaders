using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Invaders
{
    public class GlobalKeyManager
    {
        public List<KeyManager> possible_keys = new List<KeyManager>();
        public GlobalKeyManager()
        {
            possible_keys.Add(new KeyManager(Keys.A));
            possible_keys.Add(new KeyManager(Keys.D));
            possible_keys.Add(new KeyManager(Keys.Left));
            possible_keys.Add(new KeyManager(Keys.Right));
            possible_keys.Add(new KeyManager(Keys.Escape));
        }
    }
}