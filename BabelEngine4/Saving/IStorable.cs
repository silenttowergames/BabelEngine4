using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Saving
{
    public interface IStorable
    {
        string Filename(string _Filename = null);

        /// <summary>
        /// Gets called manually when the object is deserialized
        /// </summary>
        void OnLoad();

        /// <summary>
        /// Call this to save the object
        /// </summary>
        void Save();
    }
}
