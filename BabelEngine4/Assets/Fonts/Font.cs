using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Assets.Fonts
{
    public class Font : Asset<SpriteFont>
    {
        public Font(string _Filename) : base(_Filename)
        {
        }

        public void Draw(
            SpriteBatch spriteBatch,
            string Text,
            Vector2 Position,
            Color color,
            float Rotation,
            Vector2 Origin,
            Vector2 Scale,
            SpriteEffects effect,
            float LayerDepth
        )
        {
            spriteBatch.DrawString(
                Raw,
                Text,
                Position,
                color,
                Rotation,
                Origin,
                Scale,
                effect,
                LayerDepth
            );
        }
    }
}
