using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Saving
{
    public static class Storage
    {
        public static string Load(string Filename)
        {
            return System.IO.File.ReadAllText(Filename);
        }

        public static T Load<T>(string Filename) where T : IStorable
        {
            T obj = JsonConvert.DeserializeObject<T>(Load(Filename));

            obj.Filename(Filename);

            obj.OnLoad();

            return obj;
        }

        public static void Save(string Filename, string Data)
        {
            System.IO.File.WriteAllText(Filename, Data);
        }

        public static void Save<T>(string Filename, T Data) where T : IStorable
        {
            Save(Filename, JsonConvert.SerializeObject(Data));
        }
    }
}
