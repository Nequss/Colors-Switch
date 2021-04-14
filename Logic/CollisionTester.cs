using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;

namespace Colors_Switch.Logic
{
    public static class CollisionTester
    {
        public static bool BoundingBoxTest(Sprite firstObj, Sprite secondObj)
        {
            OrientedBoundingBox firstObb = new OrientedBoundingBox(firstObj);
            OrientedBoundingBox secondObb = new OrientedBoundingBox(secondObj);

            Vector2f[] axes = new Vector2f[4] {
                                    new Vector2f(firstObb.Points[1].X - firstObb.Points[0].X, firstObb.Points[1].Y - firstObb.Points[0].Y),
                                    new Vector2f(firstObb.Points[1].X - firstObb.Points[2].X, firstObb.Points[1].Y - firstObb.Points[2].Y),
                                    new Vector2f(secondObb.Points[0].X - secondObb.Points[3].X, secondObb.Points[0].Y - secondObb.Points[3].Y),
                                    new Vector2f(secondObb.Points[0].X - firstObb.Points[1].X, firstObb.Points[0].Y - firstObb.Points[1].Y)
                               };

            for (int i = 0; i < 4; ++i)
            {
                float firstMinObb, firstMaxObb, secondMinObb, secondMaxObb;

                firstObb.ProjectOntoAxis(axes[i], out firstMinObb, out firstMaxObb);
                secondObb.ProjectOntoAxis(axes[i], out secondMinObb, out secondMaxObb);

                if (!((secondMinObb <= firstMaxObb) && (secondMaxObb >= firstMinObb))) return false;
            }

            return true;
        }
    }
}
