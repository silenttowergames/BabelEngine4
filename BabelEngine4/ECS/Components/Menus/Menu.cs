using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Components.Menus
{
    public struct Menu
    {
        public Color HoverColor, BlurColor;

        public int SelectedID;

        public string Name;
    }
}
