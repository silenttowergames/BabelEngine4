using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BabelEngine4.Assets.Tiled
{
    public class TiledObjectPolygon
    {
        [XmlAttribute(AttributeName = "points")]
        public string XMLPointsString
        {
            set
            {
                string[] _points = value.Split(' ');

                foreach(string point in _points)
                {
                    string[] _params = point.Split(',');

                    if(_params.Length != 2)
                    {
                        throw new Exception(string.Format("Cannot convert string {0} into Vector2 in TiledObjectPolygon", point));
                    }

                    points.Add(new Vector2(
                        float.Parse(_params[0], System.Globalization.CultureInfo.InvariantCulture),
                        float.Parse(_params[1], System.Globalization.CultureInfo.InvariantCulture)
                    ));
                }
            }
        }

        public List<Vector2> points = new List<Vector2>();
    }
}
