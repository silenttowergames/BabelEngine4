using BabelEngine4.Assets.Sprites;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Assets
{
    public class AssetManager
    {
        ContentManager Content;

        Dictionary<string, SpriteSheet> sprites = new Dictionary<string, SpriteSheet>();

        public AssetManager(ContentManager _Content, string _RootDir = "Content")
        {
            Content = _Content;

            Content.RootDirectory = _RootDir;
        }

        public void Load()
        {
            load<SpriteSheet, Texture2D>(sprites);
        }

        void add<T, C>(Dictionary<string, T> assetMap, params T[] assets) where T : Asset<C>
        {
            for(int i = 0; i < assets.Length; i++)
            {
                assetMap.Add(assets[i].Filename, assets[i]);
            }
        }

        T get<T, C>(Dictionary<string, T> assetMap, string Filename) where T : Asset<C>
        {
            if(!assetMap.ContainsKey(Filename))
            {
                return null;
            }

            return assetMap[Filename];
        }

        void load<T, C>(Dictionary<string, T> assetMap) where T : Asset<C>
        {
            for(int i = 0; i < assetMap.Values.Count; i++)
            {
                assetMap.Values.ElementAt(i).Load(Content);
            }
        }

        public void addSprites(params SpriteSheet[] spriteSheets)
        {
            add<SpriteSheet, Texture2D>(sprites, spriteSheets);
        }

        public SpriteSheet sprite(string Filename)
        {
            return get<SpriteSheet, Texture2D>(sprites, Filename);
        }
    }
}
