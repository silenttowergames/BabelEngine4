using BabelEngine4.Assets.Aseprite;
using BabelEngine4.Assets.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Assets.Sprites
{
    public class SpriteSheet : Asset<Texture2D>
    {
        public AsepriteData Meta;

        Dictionary<string, AsepriteAnimation> Animations = new Dictionary<string, AsepriteAnimation>();

        public SpriteSheet(string _Filename) : base(_Filename)
        {
        }

        public AsepriteAnimation GetAnimation(string AnimationID)
        {
            if (!Animations.ContainsKey(AnimationID))
            {
                return null;
            }

            return Animations[AnimationID];
        }

        public override void Load(ContentManager Content)
        {
            base.Load(Content);

            JsonContainer MetaJson = Content.Load<JsonContainer>(Filename + ".meta");
            Meta = JsonConvert.DeserializeObject<AsepriteData>(MetaJson.Data);

            for(int i = 0; i < Meta.meta.frameTags.Length; i++)
            {
                Animations.Add(Meta.meta.frameTags[i].name, Meta.meta.frameTags[i]);
            }
        }

        public void Draw(
            SpriteBatch spriteBatch,
            Vector2 Position,
            string Animation,
            int Frame,
            Color color,
            float Rotation,
            Vector2 Origin,
            Vector2 Scale,
            SpriteEffects Effect,
            float LayerDepth
        )
        {
            int FrameID = Animations[Animation].from + Frame;

            Draw(
                spriteBatch,
                Position,
                Meta.frames.Values.ElementAt(FrameID).frame.ToRect(),
                color,
                Rotation,
                Origin,
                Scale,
                Effect,
                LayerDepth
            );
        }

        public void Draw(
            SpriteBatch spriteBatch,
            Vector2 Position,
            Rectangle sourceRect,
            Color color,
            float Rotation,
            Vector2 Origin,
            Vector2 Scale,
            SpriteEffects Effect,
            float LayerDepth
        )
        {
            spriteBatch.Draw(
                Raw,
                Position,
                sourceRect,
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
