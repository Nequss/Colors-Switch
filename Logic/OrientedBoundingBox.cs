using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;

namespace Colors_Switch.Logic
{
    internal class OrientedBoundingBox
    {
        public Vector2f[] Points { get; private set; }

        public OrientedBoundingBox(Sprite obj)
        {
            Transform trans = obj.Transform;
            IntRect local = obj.TextureRect;

            Points = new Vector2f[4] {
                            trans.TransformPoint(0f, 0f),
                            trans.TransformPoint(local.Width, 0f),
                            trans.TransformPoint(local.Width, local.Height),
                            trans.TransformPoint(0f, local.Height)
                      };
        }

        public void ProjectOntoAxis(Vector2f axis, out float min, out float max)
        {
            min = (Points[0].X * axis.X) + (Points[1].Y * axis.Y);
            max = min;

            for (int i = 1; i < 4; ++i)
            {
                float projection = (Points[i].X * axis.X) + (Points[i].Y * axis.Y);

                if (projection < min) min = projection;
                if (projection > max) max = projection;
            }
        }
    }
}
