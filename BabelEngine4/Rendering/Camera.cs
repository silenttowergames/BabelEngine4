﻿using BabelEngine4.Misc;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.Rendering
{
    public struct Camera
    {
        public float
            Rotation,
            Zoom
        ;

        public RectangleF Bounds => new RectangleF(
            Position.X,
            Position.Y,
            renderTarget.Resolution.X,
            renderTarget.Resolution.Y
        );

        public RenderTarget renderTarget;

        public Vector2 Position;

        public Matrix matrix => (
            Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0))
            *
            Matrix.CreateRotationZ(Rotation)
            *
            Matrix.CreateScale(new Vector3(Zoom, Zoom, 1))
            *
            Matrix.CreateTranslation(new Vector3())
            *
            Matrix.CreateScale(new Vector3(App.windowManager.Zoom, App.windowManager.Zoom, 1))
        );
    }
}
