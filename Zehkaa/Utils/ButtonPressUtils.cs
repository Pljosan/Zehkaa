using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Zehkaa.Utils
{
    internal class ButtonPressUtils
    {
        public static bool IsMoveLeft(KeyboardState kstate)
        {
            return kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.A) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < -0.1f);
        }

        public static bool IsMoveRight(KeyboardState kstate)
        {
            return kstate.IsKeyDown(Keys.Right) || kstate.IsKeyDown(Keys.D) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0.1f);
        }

        public static bool IsLookUp(KeyboardState kstate)
        {
            //TODO: Use this as looking up
            return kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0.1f);
        }

        public static bool IsHoldLookDown(KeyboardState kstate)
        {
            return kstate.IsKeyDown(Keys.Down) || kstate.IsKeyDown(Keys.S) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < -0.1f);
        }

        public static bool IsLetGoLookDown(KeyboardState kstate)
        {
            return kstate.IsKeyUp(Keys.Down) && kstate.IsKeyUp(Keys.S) && (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y >= -0.1f);
        }

        public static bool IsJump(KeyboardState kstate)
        {
            return kstate.IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed;
        }

        internal static bool AnyOtherButtonThanDownPressed(KeyboardState kstate)
        {
            Keys key = Array.Find(kstate.GetPressedKeys(), key => !key.Equals(Keys.Down));
            if (key.Equals(Keys.None)) return false;

            return true;
        }
    }
}
