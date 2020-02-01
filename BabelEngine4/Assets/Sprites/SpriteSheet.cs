using BabelEngine4.Assets.Aseprite;
using BabelEngine4.Assets.Json;
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
        AsepriteData Meta;

        public SpriteSheet(string _Filename) : base(_Filename)
        {
            //
        }

        public override void Load(ContentManager Content)
        {
            base.Load(Content);

            JsonContainer MetaJson = Content.Load<JsonContainer>(Filename + ".meta");
            Meta = JsonConvert.DeserializeObject<AsepriteData>(MetaJson.Data);
        }
    }
}
