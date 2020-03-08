using BabelEngine4.Rendering;
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
        public Action<Effect, RenderTarget> Update;

        public Shader(string _Filename, Action<Effect, RenderTarget> _Update = null) : base(_Filename)
        {
            Update = _Update;
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
