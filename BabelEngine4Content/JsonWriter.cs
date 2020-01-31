using BabelEngine4.Assets.Json;
using BabelEngine4.PipelineImporter;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.PipelineImporter
{
    [ContentTypeWriter]
    public class JsonWriter : ContentTypeWriter<JsonProcessor>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "BabelEngine4.Assets.Json.JsonContainer, BabelEngine4";
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(JsonContainer).AssemblyQualifiedName;
        }

        protected override void Write(ContentWriter output, JsonProcessor value)
        {
            output.Write(value.Data);
        }
    }
}
