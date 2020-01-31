using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.PipelineImporter
{
    [ContentImporter(".json", DefaultProcessor = "JsonContainerProcessor", DisplayName = "BabelEngine4 Monogame Json Importer")]
    public class JsonImporter : ContentImporter<JsonProcessor>
    {
        public override JsonProcessor Import(string filename, ContentImporterContext context)
        {
            context.Logger.LogMessage("Importing Tiled Map: {0}", filename);

            JsonProcessor J = new JsonProcessor(System.IO.File.ReadAllText(filename));

            return J;
        }
    }
}
