using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Assets.Shaders
{
    public class Shader : Asset<Effect>
    {
        public Shader(string _Filename) : base(_Filename)
        {
        }

        public void Apply()
        {
            foreach(EffectPass Pass in Raw.CurrentTechnique.Passes)
            {
                Pass.Apply();
            }
        }
    }
}
