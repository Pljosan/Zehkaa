using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return kstate.IsKeyDown(Keys.Space) || ButtonState.Pressed.Equals(GamePad.GetState(PlayerIndex.One).Buttons.A);
        }

        private bool IsAnyOtherButtonPressed(KeyboardState kstate)
        {
            Keys[] keys = Keyboard.GetState().GetPressedKeys();
            Keys pressedKey = Array.Find(keys, key => !key.Equals(Keys.Down) && !key.Equals(Keys.S));


            Debug.WriteLine("Pressed: " + pressedKey);

            return pressedKey != null;
        }
    }
}
