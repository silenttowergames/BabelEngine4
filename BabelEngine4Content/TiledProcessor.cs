using BabelEngine4.Assets.Tiled;
using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BabelEngine4.PipelineImporter
{
    [ContentProcessor(DisplayName = "BabelEngine4 Monogame Tiled Processor")]
    public class TiledProcessor : ContentProcessor<TiledMapProcessor, TiledMapContainer>
    {
        public override TiledMapContainer Process(TiledMapProcessor input, ContentProcessorContext context)
        {
            TiledMapContainer Map = new TiledMapContainer(input.Data);

            return Map;
        }
    }
}
