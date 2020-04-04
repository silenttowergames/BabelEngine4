using BabelEngine4.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Assets.Sprites
{
    public class BlankSpriteSheet : SpriteSheet
    {
        public override Point SizeEst => new Point(1);

        public BlankSpriteSheet(string _Filename) : base(_Filename)
        {
        }

        public override void Load(ContentManager Content)
        {
            Raw = new Texture2D(App.renderer.graphics.GraphicsDevice, 1, 1);
            Raw.SetData(new Color[] { Color.White });
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 Position, string Animation, int Frame, Color color, float Rotation, Vector2 Origin, Vector2 Scale, SpriteEffects Effect, float LayerDepth)
        {
            Draw(
                spriteBatch,
                Position,
                new Rectangle(0, 0, 1, 1),
                color,
                Rotation,
                Origin,
                Scale,
                Effect,
                LayerDepth
            );
        }
    }
}
