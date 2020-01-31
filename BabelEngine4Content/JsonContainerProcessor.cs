using BabelEngine4.Assets.Json;
using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.PipelineImporter
{
    [ContentProcessor(DisplayName = "BabelEngine4 Monogame Json Processor")]
    public class JsonContainerProcessor : ContentProcessor<JsonProcessor, JsonContainer>
    {
        public override JsonContainer Process(JsonProcessor input, ContentProcessorContext context)
        {
            return new JsonContainer(input.Data);
        }
    }
}
