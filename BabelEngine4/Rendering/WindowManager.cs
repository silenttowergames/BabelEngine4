using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Rendering
{
    public class WindowManager
    {
        public GameWindow window;

        public string Title
        {
            get
            {
                return window.Title;
            }

            set
            {
                window.Title = value;
            }
        }
    }
}
