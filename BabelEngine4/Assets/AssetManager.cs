using BabelEngine4.Assets.Audio;
using BabelEngine4.Assets.Fonts;
using BabelEngine4.Assets.Shaders;
using BabelEngine4.Assets.Sprites;
using BabelEngine4.Assets.Tiled;
using Microsoft.Xna.Framework.Audio;
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

        Dictionary<string, Font> fonts = new Dictionary<string, Font>();

        Dictionary<string, Map> maps = new Dictionary<string, Map>();

        Dictionary<string, Music> musics = new Dictionary<string, Music>();

        Dictionary<string, Shader> shaders = new Dictionary<string, Shader>();

        Dictionary<string, SFX> sfxs = new Dictionary<string, SFX>();

        Dictionary<string, SpriteSheet> sprites = new Dictionary<string, SpriteSheet>();

        public AssetManager(ContentManager _Content, string _RootDir = "Content")
        {
            Content = _Content;

            Content.RootDirectory = _RootDir;
        }

        public void Load()
        {
            load<Font, SpriteFont>(fonts);
            load<Map, TiledMapContainer>(maps);
            load<Music, SoundEffect>(musics);
            load<Shader, Effect>(shaders);
            load<SFX, SoundEffect>(sfxs);
            load<SpriteSheet, Texture2D>(sprites);
        }

        void SetVolume<T>(float V, Dictionary<string, T> Assets) where T : IHasVolume
        {
            foreach (T Asset in Assets.Values)
            {
                Asset.SetVolume(V);
            }
        }

        public void SetVolumeMusic(float V)
        {
            SetVolume(V, musics);
        }

        public void SetVolumeSFX(float V)
        {
            SetVolume(V, sfxs);
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

        public void addFonts(params Font[] _fonts)
        {
            add<Font, SpriteFont>(fonts, _fonts);
        }

        public void addMaps(params Map[] _maps)
        {
            add<Map, TiledMapContainer>(maps, _maps);
        }

        public void addMusic(params Music[] _musics)
        {
            add<Music, SoundEffect>(musics, _musics);
        }

        public void addShaders(params Shader[] _shader)
        {
            add<Shader, Effect>(shaders, _shader);
        }

        public void addSFX(params SFX[] _sfx)
        {
            add<SFX, SoundEffect>(sfxs, _sfx);
        }

        public void addSprites(params SpriteSheet[] spriteSheets)
        {
            add<SpriteSheet, Texture2D>(sprites, spriteSheets);
        }

        public Font font(string Filename)
        {
            return get<Font, SpriteFont>(fonts, Filename);
        }

        public Map map(string Filename)
        {
            return get<Map, TiledMapContainer>(maps, Filename);
        }

        public Music music(string Filename)
        {
            return get<Music, SoundEffect>(musics, Filename);
        }

        public Shader shader(string Filename)
        {
            return get<Shader, Effect>(shaders, Filename);
        }

        public SFX sfx(string Filename)
        {
            return get<SFX, SoundEffect>(sfxs, Filename);
        }

        public SpriteSheet sprite(string Filename)
        {
            return get<SpriteSheet, Texture2D>(sprites, Filename);
        }
    }
}
