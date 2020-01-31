using BabelEngine4.Assets.Tiled;
using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.IO;
using System.Xml.Serialization;

namespace BabelEngine4.PipelineImporter
{
    [ContentImporter(".tmx", DefaultProcessor = "TiledProcessor", DisplayName = "BabelEngine4 Monogame Tiled Importer")]
    public class TiledImporter : ContentImporter<TiledMapProcessor>
    {
        public override TiledMapProcessor Import(string filename, ContentImporterContext context)
        {
            context.Logger.LogMessage("Importing Tiled Map: {0}", filename);

            TiledMapProcessor TM = new TiledMapProcessor(System.IO.File.ReadAllText(filename));

            return TM;
        }
    }
}
