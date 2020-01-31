using BabelEngine4.Assets.Tiled;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System;
using System.Collections.Generic;
using System.Text;

namespace BabelEngine4.PipelineImporter
{
    [ContentTypeWriter]
    public class TiledMapWriter : ContentTypeWriter<TiledMapProcessor>
    {
        protected override void Write(ContentWriter output, TiledMapProcessor value)
        {
            output.Write(value.Data);
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(TiledMapContainer).AssemblyQualifiedName;
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "BabelEngine4.Assets.Tiled.TileMapContainer, BabelEngine4";
        }
    }
}
