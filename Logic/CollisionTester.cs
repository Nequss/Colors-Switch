using SFML.Graphics;
using SFML.System;

namespace Colors_Switch.Logic
{
    public static class CollisionTester
    {
        private static BitmaskManager _bitmasks = new BitmaskManager();

        public static Color firstCollisionColor;
        public static Color secondCollisionColor;

        public static void AddBitMask(Texture tex)
        {
            _bitmasks.Create(tex);
        }

        public static bool PixelPerfectTest(Sprite firstObj, Sprite secondObj, uint alphaLimit)
        {
            FloatRect intersection;
            IntRect firstSubRect, secondSubRect;

            if (firstObj.GetGlobalBounds().Intersects(secondObj.GetGlobalBounds(), out intersection))
            {
                firstSubRect = firstObj.TextureRect;
                secondSubRect = secondObj.TextureRect;

                for (int i = (int)intersection.Left; i < intersection.Left + intersection.Width; ++i)
                {
                    for (int j = (int)intersection.Top; j < intersection.Top + intersection.Height; ++j)
                    {
                        Vector2f firstVector = firstObj.InverseTransform.TransformPoint(i, j);
                        Vector2f secondVector = secondObj.InverseTransform.TransformPoint(i, j);

                        if (firstVector.X > 0 && firstVector.Y > 0
                           && secondVector.X > 0 && secondVector.Y > 0
                           && firstVector.X < firstSubRect.Width && firstVector.Y < firstSubRect.Height
                           && secondVector.X < secondSubRect.Width && secondVector.Y < secondSubRect.Height)
                        {
                            if (_bitmasks.GetPixel(firstObj.Texture, (uint)(firstVector.X + firstSubRect.Left), (uint)(firstVector.Y + firstSubRect.Top)) > alphaLimit
                                && _bitmasks.GetPixel(secondObj.Texture, (uint)(secondVector.X + secondSubRect.Left), (uint)(secondVector.Y + secondSubRect.Top)) > alphaLimit)
                            {
                                Image firstImg = firstObj.Texture.CopyToImage();
                                Image secondImg = secondObj.Texture.CopyToImage();

                                firstCollisionColor = firstImg.GetPixel((uint)(firstVector.X + firstSubRect.Left), (uint)(firstVector.Y + firstSubRect.Top));
                                secondCollisionColor = secondImg.GetPixel((uint)(secondVector.X + secondSubRect.Left), (uint)(secondVector.Y + secondSubRect.Top));

                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}