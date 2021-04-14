using SFML.Graphics;
using System.Collections.Generic;

namespace Colors_Switch.Logic
{
    internal class BitmaskManager
    {
        private Dictionary<Texture, uint[]> _bitmasks = new Dictionary<Texture, uint[]>();

        public uint GetPixel(Texture tex, uint x, uint y)
        {
            if (x > tex.Size.X || y > tex.Size.Y)
            {
                return 0;
            }
            else
            {
                return Get(tex)[x + y * tex.Size.X];
            }
        }

        public uint[] Get(Texture tex)
        {
            uint[] mask;

            if (!_bitmasks.TryGetValue(tex, out mask))
            {
                mask = Create(tex);
            }

            return mask;
        }

        public uint[] Create(Texture tex)
        {
            Image img = tex.CopyToImage();
            uint[] mask = new uint[tex.Size.Y * tex.Size.X];

            for (uint y = 0; y < tex.Size.Y; ++y)
            {
                for (uint x = 0; x < tex.Size.X; ++x)
                {
                    mask[x + y * tex.Size.X] = img.GetPixel(x, y).A;
                }
            }

            _bitmasks.Add(tex, mask);

            return mask;
        }
    }
}