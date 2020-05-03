using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Components
{
    public struct CameraFollow
    {
        public int RenderTargetID;

        public float RoundToX, RoundToY;

        public Vector2 Offset;

        bool
            DontFollowX,
            DontFollowY
        ;

        public bool FollowX
        {
            get
            {
                return !DontFollowX;
            }

            set
            {
                DontFollowX = !value;
            }
        }

        public bool FollowY
        {
            get
            {
                return !DontFollowY;
            }

            set
            {
                DontFollowY = !value;
            }
        }
    }
}
