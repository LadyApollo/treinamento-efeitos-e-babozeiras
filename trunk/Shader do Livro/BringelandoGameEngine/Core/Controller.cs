/*XNA Game - Pirate Blast
 * Developed by Marcos T.B. Soarers
 * e-mail: markosthiago@gmail.com
 * Controller class
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace BringelandoGameEngine.Core
{
    public static class Controller
    {
        //Variables to store the current and previous states of both the Keyboard and Mouse
        public static KeyboardState previousKeyboardState, currentKeyboardState;
        public static MouseState previousMouseState, currentMouseState;

        //X-Coordinate of the mouse cursor
        public static int MouseX
        {
            get { return currentMouseState.X; }
            set { Mouse.SetPosition(value, currentMouseState.X); }
        }

        //Y-coordinate of the mouse cursor
        public static int MouseY
        {
            get { return currentMouseState.Y; }
            set { Mouse.SetPosition(currentMouseState.Y, value); }
        }

        public static Vector2 MousePosition
        {
            get { return new Vector2(currentMouseState.X, currentMouseState.Y); }
            set { Mouse.SetPosition((int)value.X, (int)value.Y); }
        }

        public static Vector2 LastMousePosition
        {
            get { return new Vector2(previousMouseState.X, previousMouseState.Y); }
            
        }

        //Updates the mouse and keyboard states
        public static void Update(GameTime gameTime)
        {
            previousKeyboardState = currentKeyboardState;
            previousMouseState = currentMouseState;
            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
        }

        public static bool KeyReleased(Keys key)
        {
            return (currentKeyboardState.IsKeyUp(key) && previousKeyboardState.IsKeyDown(key));
        }


        /// <summary>
        /// Detects if key has just been pressed
        /// </summary>
        /// <param name="key">The key to be analyzed</param>
        /// <returns>Returns true if pressed</returns>
        public static bool KeyPressed(Keys key)
        {
            return (currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key));
        }

        /// <summary>
        /// Detects if Key is currently pressed
        /// </summary>
        /// <param name="key">The key to be analyzed</param>
        /// <returns>Returns true if pressed</returns>
        public static bool KeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Detects if left button of the mouse is currently pressed
        /// </summary>
        /// <returns></returns>
        public static bool MouseLeftPressed()
        {
            return (currentMouseState.LeftButton == ButtonState.Pressed) && (previousMouseState.LeftButton == ButtonState.Released);
        }

        /// <summary>
        /// Detects if right button of the mouse is currently pressed
        /// </summary>
        /// <returns></returns>
        public static bool MouseRightPressed()
        {
            return (currentMouseState.RightButton == ButtonState.Pressed) && (previousMouseState.RightButton == ButtonState.Released);
        }

        /// <summary>
        /// Detects if middle mouse button is currently pressed
        /// </summary>
        /// <returns></returns>
        public static bool MouseMiddlePressed()
        {
            return (currentMouseState.MiddleButton == ButtonState.Pressed) && (previousMouseState.MiddleButton == ButtonState.Released);
        }

        public static bool MouseLeftDown()
        {
            return (currentMouseState.LeftButton == ButtonState.Pressed);
        }

        public static bool MouseRightDown()
        {
            return (currentMouseState.RightButton == ButtonState.Pressed);
        }

        public static bool MouseMiddleDown()
        {
            return (currentMouseState.MiddleButton == ButtonState.Pressed);
        }

        public static bool MouseLeftReleased()
        {
            return (currentMouseState.LeftButton == ButtonState.Released) && (previousMouseState.LeftButton == ButtonState.Pressed);
        }

        public static bool MouseRightReleased()
        {
            return (currentMouseState.RightButton == ButtonState.Released) && (previousMouseState.RightButton == ButtonState.Pressed);
        }

        public static bool MouseMiddleReleased()
        {
            return (currentMouseState.MiddleButton == ButtonState.Released) && (previousMouseState.MiddleButton == ButtonState.Pressed);
        }


        #region Mouse Button 4
        public static bool Mouse4Pressed()
        {
            return (currentMouseState.XButton1 == ButtonState.Pressed) && (previousMouseState.XButton1 == ButtonState.Released);
        }

        public static bool Mouse4Down()
        {
            return (currentMouseState.XButton1 == ButtonState.Pressed);
        }

        public static bool Mouse4Released()
        {
            return (currentMouseState.XButton1 == ButtonState.Released) && (previousMouseState.XButton1 == ButtonState.Pressed);
        }
        #endregion

        #region Mouse Button 5
        public static bool Mouse5Pressed()
        {
            return (currentMouseState.XButton2 == ButtonState.Pressed) && (previousMouseState.XButton2 == ButtonState.Released);
        }

        public static bool Mouse5Down()
        {
            return (currentMouseState.XButton2 == ButtonState.Pressed);
        }

        public static bool Mouse5Released()
        {
            return (currentMouseState.XButton2 == ButtonState.Released) && (previousMouseState.XButton2 == ButtonState.Pressed);
        }
        #endregion

        public static Rectangle MouseRect
        {
            get
            {
                return new Rectangle(MouseX, MouseY, 1, 1);
            }
        }

        

    }
}
