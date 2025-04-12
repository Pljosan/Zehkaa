using Microsoft.Xna.Framework;

namespace Zehkaa.Utils
{
    internal class CollisionDetectionUtils
    {
        public static bool IsCollisionFromTop(Rectangle boundingRectangle, Rectangle groundRectangle)
        {
            return boundingRectangle.Bottom <= groundRectangle.Top + ConstantsUtil.DELTA &&
                boundingRectangle.Bottom >= groundRectangle.Top - ConstantsUtil.DELTA &&
                boundingRectangle.Right > groundRectangle.Left &&
                boundingRectangle.Left < groundRectangle.Right;
        }

        public static bool IsCollisionFromBottom(Rectangle boundingRectangle, Rectangle groundRectangle)
        {
            return boundingRectangle.Top >= groundRectangle.Bottom - ConstantsUtil.DELTA &&
                boundingRectangle.Top < groundRectangle.Bottom + ConstantsUtil.DELTA &&
                boundingRectangle.Right > groundRectangle.Left &&
                boundingRectangle.Left < groundRectangle.Right;
        }

        public static bool IsCollisionFromLeft(Rectangle boundingRectangle, Rectangle groundRectangle)
        {
            return ((boundingRectangle.Bottom > groundRectangle.Top && boundingRectangle.Bottom < groundRectangle.Bottom) ||
                (boundingRectangle.Top > groundRectangle.Top && boundingRectangle.Top < groundRectangle.Bottom)) &&
                boundingRectangle.Right >= groundRectangle.Left - ConstantsUtil.DELTA &&
                boundingRectangle.Right <= groundRectangle.Left + ConstantsUtil.DELTA;
        }

        public static bool IsCollisionFromRight(Rectangle boundingRectangle, Rectangle groundRectangle)
        {
            return ((boundingRectangle.Bottom > groundRectangle.Top && boundingRectangle.Bottom < groundRectangle.Bottom) ||
                (boundingRectangle.Top > groundRectangle.Top && boundingRectangle.Top < groundRectangle.Bottom)) &&
                boundingRectangle.Left >= groundRectangle.Right - ConstantsUtil.DELTA &&
                boundingRectangle.Left <= groundRectangle.Right + ConstantsUtil.DELTA;
        }
    }
}
