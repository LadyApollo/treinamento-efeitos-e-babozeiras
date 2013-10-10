using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Xml;

namespace BringelandoGameEngine.Core
{
    public static class InputManager
    {
        //States
        private static GamePadState currentGamePadState, previousGamePadState;

        public class Command
        {
            public object keyboard;
            public GamepadInputList gamepad;

            public Command(object keyboardInput, GamepadInputList gamepadInput)
            {
                keyboard = keyboardInput;
                gamepad = gamepadInput;
            }
        }

        public static Dictionary<String, Command> inputList, savedInputList;

        public enum GamepadInputList
        {
            GAMEPAD_LEFTSTICK_BUTTON,
            GAMEPAD_LEFTSTICK_UP,
            GAMEPAD_LEFTSTICK_DOWN,
            GAMEPAD_LEFTSTICK_LEFT,
            GAMEPAD_LEFTSTICK_RIGHT,
            GAMEPAD_RIGHTSTICK_BUTTON,
            GAMEPAD_RIGHTSTICK_UP,
            GAMEPAD_RIGHTSTICK_DOWN,
            GAMEPAD_RIGHTSTICK_LEFT,
            GAMEPAD_RIGHTSTICK_RIGHT,
            GAMEPAD_DPAD_UP,
            GAMEPAD_DPAD_DOWN,
            GAMEPAD_DPAD_RIGHT,
            GAMEPAD_DPAD_LEFT,
            GAMEPAD_LEFT_SHOULDER,
            GAMEPAD_RIGHT_SHOULDER,
            GAMEPAD_LEFT_TRIGGER,
            GAMEPAD_RIGHT_TRIGGER,
            GAMEPAD_BUTTONS_X,
            GAMEPAD_BUTTONS_Y,
            GAMEPAD_BUTTONS_B,
            GAMEPAD_BUTTONS_A,
            GAMEPAD_BACK,
            GAMEPAD_BIG_BUTTON,
            GAMEPAD_START,
        }

        public enum MouseInputList
        {
            MOUSE_LEFT_BUTTON,
            MOUSE_RIGHT_BUTTON,
            MOUSE_MIDDLE_BUTTON,
            MOUSE_4,
            MOUSE_5
        }

        public enum InputType
        {
            KEYBOARD,
            GAMEPAD
        }

        public static InputType lastInput;


        public static void Initialize()
        {
            ReadCustomInputs();
        }



        public static void setOnMenuControls()
        {
            inputList = new Dictionary<String, Command>();

            inputList["up"] = new Command(Keys.Up, GamepadInputList.GAMEPAD_LEFTSTICK_UP);
            inputList["down"] = new Command(Keys.Down, GamepadInputList.GAMEPAD_LEFTSTICK_DOWN);
            inputList["select"] = new Command(Keys.Enter, GamepadInputList.GAMEPAD_BUTTONS_A);
            inputList["back"] = new Command(Keys.Escape, GamepadInputList.GAMEPAD_BUTTONS_B);
            inputList["left"] = new Command(Keys.Left, GamepadInputList.GAMEPAD_LEFTSTICK_LEFT);
            inputList["right"] = new Command(Keys.Right, GamepadInputList.GAMEPAD_LEFTSTICK_RIGHT);
        }

        public static void setInGameControls()
        {
            if (savedInputList == null)
            {
                inputList = new Dictionary<String, Command>();

                inputList["up"] = new Command(Keys.W, GamepadInputList.GAMEPAD_LEFTSTICK_UP);
                inputList["down"] = new Command(Keys.S, GamepadInputList.GAMEPAD_LEFTSTICK_DOWN);
                inputList["left"] = new Command(Keys.A, GamepadInputList.GAMEPAD_LEFTSTICK_LEFT);
                inputList["right"] = new Command(Keys.D, GamepadInputList.GAMEPAD_LEFTSTICK_RIGHT);
                inputList["jump"] = new Command(Keys.W, GamepadInputList.GAMEPAD_LEFT_TRIGGER);
                inputList["shoot"] = new Command(MouseInputList.MOUSE_LEFT_BUTTON, GamepadInputList.GAMEPAD_RIGHT_TRIGGER);
                inputList["roll"] = new Command(Keys.LeftShift, GamepadInputList.GAMEPAD_LEFT_SHOULDER);
                inputList["switchWeapons"] = new Command(Keys.E, GamepadInputList.GAMEPAD_BUTTONS_Y);
                inputList["melee"] = new Command(Keys.Z, GamepadInputList.GAMEPAD_BUTTONS_Y);
                inputList["jump"] = new Command(Keys.W, GamepadInputList.GAMEPAD_LEFT_TRIGGER);

            }
            else
            {
                inputList = savedInputList;
            }
        }


        public static void Update(GameTime gameTime)
        {
            Controller.Update(gameTime);
            previousGamePadState = currentGamePadState;
            currentGamePadState = GamePad.GetState(PlayerIndex.One);

            verifyLastInputType();
        }

        private static void verifyLastInputType()
        {
            //Essa função verifica se o último input veio do teclado, do mouse ou do Gamepad
            //Para permitir que ambos sejam usáveis durante o jogo.

            if (Keyboard.GetState().GetPressedKeys().Count() > 0)
                lastInput = InputType.KEYBOARD;

            if(Controller.currentMouseState != Controller.previousMouseState)
                lastInput = InputType.KEYBOARD;

            if (currentGamePadState != previousGamePadState)
                lastInput = InputType.GAMEPAD;
        }

        public static Boolean InputByNameIsDown(String inputName)
        {
            if(!inputList.ContainsKey(inputName))
                throw new Exception("There's no input named "+inputName);

            if (lastInput == InputType.GAMEPAD)
            {
                #region GAMEPAD KEY MAPPING
                switch (inputList[inputName].gamepad)
                {
                    case GamepadInputList.GAMEPAD_BACK:
                        return currentGamePadState.Buttons.Back == ButtonState.Pressed;
                        break;

                    case GamepadInputList.GAMEPAD_BIG_BUTTON:
                        return currentGamePadState.Buttons.BigButton == ButtonState.Pressed;
                        break;

                    case GamepadInputList.GAMEPAD_BUTTONS_A:
                        return currentGamePadState.Buttons.A == ButtonState.Pressed;
                        break;

                    case GamepadInputList.GAMEPAD_BUTTONS_B:
                        return currentGamePadState.Buttons.B == ButtonState.Pressed;
                        break;

                    case GamepadInputList.GAMEPAD_BUTTONS_X:
                        return currentGamePadState.Buttons.X == ButtonState.Pressed;
                        break;

                    case GamepadInputList.GAMEPAD_BUTTONS_Y:
                        return currentGamePadState.Buttons.Y == ButtonState.Pressed;
                        break;

                    case GamepadInputList.GAMEPAD_DPAD_DOWN:
                        return currentGamePadState.DPad.Down == ButtonState.Pressed;
                        break;

                    case GamepadInputList.GAMEPAD_DPAD_LEFT:
                        return currentGamePadState.DPad.Left == ButtonState.Pressed;
                        break;

                    case GamepadInputList.GAMEPAD_DPAD_RIGHT:
                        return currentGamePadState.DPad.Right == ButtonState.Pressed;
                        break;

                    case GamepadInputList.GAMEPAD_DPAD_UP:
                        return currentGamePadState.DPad.Up == ButtonState.Pressed;
                        break;

                    case GamepadInputList.GAMEPAD_LEFT_SHOULDER:
                        return currentGamePadState.Buttons.LeftShoulder == ButtonState.Pressed;
                        break;

                    case GamepadInputList.GAMEPAD_LEFTSTICK_BUTTON:
                        return currentGamePadState.Buttons.LeftStick == ButtonState.Pressed;
                        break;

                    case GamepadInputList.GAMEPAD_RIGHT_SHOULDER:
                        return currentGamePadState.Buttons.RightShoulder == ButtonState.Pressed;
                        break;

                    case GamepadInputList.GAMEPAD_RIGHTSTICK_BUTTON:
                        return currentGamePadState.Buttons.RightStick == ButtonState.Pressed;
                        break;

                    case GamepadInputList.GAMEPAD_START:
                        return currentGamePadState.Buttons.Start == ButtonState.Pressed;
                        break;

                    case GamepadInputList.GAMEPAD_LEFTSTICK_DOWN:
                        return currentGamePadState.ThumbSticks.Left.Y <= -0.3f;
                        break;

                    case GamepadInputList.GAMEPAD_LEFTSTICK_UP:
                        return currentGamePadState.ThumbSticks.Left.Y >= 0.3f;
                        break;

                    case GamepadInputList.GAMEPAD_LEFTSTICK_LEFT:
                        return currentGamePadState.ThumbSticks.Left.X <= -0.3f;
                        break;

                    case GamepadInputList.GAMEPAD_LEFTSTICK_RIGHT:
                        return currentGamePadState.ThumbSticks.Left.X >= 0.3f;
                        break;

                    case GamepadInputList.GAMEPAD_RIGHTSTICK_DOWN:
                        return currentGamePadState.ThumbSticks.Right.Y <= -0.3f;
                        break;

                    case GamepadInputList.GAMEPAD_RIGHTSTICK_UP:
                        return currentGamePadState.ThumbSticks.Right.Y >= 0.3f;
                        break;

                    case GamepadInputList.GAMEPAD_RIGHTSTICK_LEFT:
                        return currentGamePadState.ThumbSticks.Right.X <= -0.3f;
                        break;

                    case GamepadInputList.GAMEPAD_RIGHTSTICK_RIGHT:
                        return currentGamePadState.ThumbSticks.Right.X >= 0.3f;
                        break;

                    case GamepadInputList.GAMEPAD_RIGHT_TRIGGER:
                        return currentGamePadState.Triggers.Right >= 0.3f;
                        break;

                    case GamepadInputList.GAMEPAD_LEFT_TRIGGER:
                        return currentGamePadState.Triggers.Left >= 0.3f;
                        break;
                }
                #endregion
            }
            else
            {
                if (inputList[inputName].keyboard is MouseInputList)
                {
                    #region MOUSE KEY MAPPING
                    switch ((MouseInputList)inputList[inputName].keyboard)
                    {
                        case MouseInputList.MOUSE_LEFT_BUTTON:
                            return Controller.MouseLeftDown();
                            break;

                        case MouseInputList.MOUSE_MIDDLE_BUTTON:
                            return Controller.MouseMiddleDown();
                            break;

                        case MouseInputList.MOUSE_RIGHT_BUTTON:
                            return Controller.MouseRightDown();
                            break;

                        case MouseInputList.MOUSE_4:
                            return Controller.Mouse4Down();
                            break;

                        case MouseInputList.MOUSE_5:
                            return Controller.Mouse5Down();
                            break;
                    }
                    #endregion
                }
                else if (inputList[inputName].keyboard is Keys)
                {
                    return Controller.KeyDown((Keys)inputList[inputName].keyboard);
                }
            }

            Console.WriteLine("O input " + inputName + " não foi verificado");
            return false;
        }

        public static Boolean InputByNameIsPressed(String inputName)
        {
            if (!inputList.ContainsKey(inputName))
                throw new Exception("There's no input named " + inputName);

            if (lastInput == InputType.GAMEPAD)
            {
                #region GAMEPAD KEY MAPPING
                switch (inputList[inputName].gamepad)
                {
                    case GamepadInputList.GAMEPAD_BACK:
                        return (currentGamePadState.Buttons.Back == ButtonState.Pressed) && (previousGamePadState.Buttons.Back == ButtonState.Released);
                        break;

                    case GamepadInputList.GAMEPAD_BIG_BUTTON:
                        return currentGamePadState.Buttons.BigButton == ButtonState.Pressed && (previousGamePadState.Buttons.BigButton == ButtonState.Released);
                        break;

                    case GamepadInputList.GAMEPAD_BUTTONS_A:
                        return currentGamePadState.Buttons.A == ButtonState.Pressed && (previousGamePadState.Buttons.A == ButtonState.Released);
                        break;

                    case GamepadInputList.GAMEPAD_BUTTONS_B:
                        return currentGamePadState.Buttons.B == ButtonState.Pressed && (previousGamePadState.Buttons.B == ButtonState.Released);
                        break;

                    case GamepadInputList.GAMEPAD_BUTTONS_X:
                        return currentGamePadState.Buttons.X == ButtonState.Pressed && (previousGamePadState.Buttons.X == ButtonState.Released);
                        break;

                    case GamepadInputList.GAMEPAD_BUTTONS_Y:
                        return currentGamePadState.Buttons.Y == ButtonState.Pressed && (previousGamePadState.Buttons.Y == ButtonState.Released);
                        break;

                    case GamepadInputList.GAMEPAD_DPAD_DOWN:
                        return currentGamePadState.DPad.Down == ButtonState.Pressed && (previousGamePadState.DPad.Down == ButtonState.Released);
                        break;

                    case GamepadInputList.GAMEPAD_DPAD_LEFT:
                        return currentGamePadState.DPad.Left == ButtonState.Pressed && (previousGamePadState.DPad.Left == ButtonState.Released);
                        break;

                    case GamepadInputList.GAMEPAD_DPAD_RIGHT:
                        return currentGamePadState.DPad.Right == ButtonState.Pressed && (previousGamePadState.DPad.Right == ButtonState.Released);
                        break;

                    case GamepadInputList.GAMEPAD_DPAD_UP:
                        return currentGamePadState.DPad.Up == ButtonState.Pressed && (previousGamePadState.DPad.Up == ButtonState.Released);
                        break;

                    case GamepadInputList.GAMEPAD_LEFT_SHOULDER:
                        return currentGamePadState.Buttons.LeftShoulder == ButtonState.Pressed && (previousGamePadState.Buttons.LeftShoulder == ButtonState.Released);
                        break;

                    case GamepadInputList.GAMEPAD_LEFTSTICK_BUTTON:
                        return currentGamePadState.Buttons.LeftStick == ButtonState.Pressed && (previousGamePadState.Buttons.LeftStick == ButtonState.Released);
                        break;

                    case GamepadInputList.GAMEPAD_RIGHT_SHOULDER:
                        return currentGamePadState.Buttons.RightShoulder == ButtonState.Pressed && (previousGamePadState.Buttons.RightShoulder == ButtonState.Released);
                        break;

                    case GamepadInputList.GAMEPAD_RIGHTSTICK_BUTTON:
                        return currentGamePadState.Buttons.RightStick == ButtonState.Pressed && (previousGamePadState.Buttons.RightStick == ButtonState.Released);
                        break;

                    case GamepadInputList.GAMEPAD_START:
                        return currentGamePadState.Buttons.Start == ButtonState.Pressed && (previousGamePadState.Buttons.Start == ButtonState.Released);
                        break;

                    case GamepadInputList.GAMEPAD_LEFTSTICK_DOWN:
                        return currentGamePadState.ThumbSticks.Left.Y <= -0.3f && (previousGamePadState.ThumbSticks.Left.Y > -0.3f);
                        break;

                    case GamepadInputList.GAMEPAD_LEFTSTICK_UP:
                        return currentGamePadState.ThumbSticks.Left.Y >= 0.3f && (previousGamePadState.ThumbSticks.Left.Y < 0.3f);
                        break;

                    case GamepadInputList.GAMEPAD_LEFTSTICK_LEFT:
                        return currentGamePadState.ThumbSticks.Left.X <= -0.3f && (previousGamePadState.ThumbSticks.Left.X > -0.3f);
                        break;

                    case GamepadInputList.GAMEPAD_LEFTSTICK_RIGHT:
                        return currentGamePadState.ThumbSticks.Left.X >= 0.3f && (previousGamePadState.ThumbSticks.Left.X < 0.3f);
                        break;

                    case GamepadInputList.GAMEPAD_RIGHTSTICK_DOWN:
                        return currentGamePadState.ThumbSticks.Right.Y <= -0.3f && (previousGamePadState.ThumbSticks.Right.Y > -0.3f);
                        break;

                    case GamepadInputList.GAMEPAD_RIGHTSTICK_UP:
                        return currentGamePadState.ThumbSticks.Right.Y >= 0.3f && (previousGamePadState.ThumbSticks.Right.Y < 0.3f);
                        break;

                    case GamepadInputList.GAMEPAD_RIGHTSTICK_LEFT:
                        return currentGamePadState.ThumbSticks.Right.X <= -0.3f && (previousGamePadState.ThumbSticks.Right.X > -0.3f);
                        break;

                    case GamepadInputList.GAMEPAD_RIGHTSTICK_RIGHT:
                        return currentGamePadState.ThumbSticks.Right.X >= 0.3f && (previousGamePadState.ThumbSticks.Right.X < 0.3f);
                        break;

                    case GamepadInputList.GAMEPAD_RIGHT_TRIGGER:
                        return currentGamePadState.Triggers.Right >= 0.3f && (previousGamePadState.Triggers.Right < 0.3f);
                        break;

                    case GamepadInputList.GAMEPAD_LEFT_TRIGGER:
                        return currentGamePadState.Triggers.Left >= 0.3f && (previousGamePadState.Triggers.Left < 0.3f);
                        break;
                }
                #endregion
            }
            else
            {
                if (inputList[inputName].keyboard is MouseInputList)
                {
                    #region MOUSE KEY MAPPING
                    switch ((MouseInputList)inputList[inputName].keyboard)
                    {
                        case MouseInputList.MOUSE_LEFT_BUTTON:
                            return Controller.MouseLeftPressed();
                            break;

                        case MouseInputList.MOUSE_MIDDLE_BUTTON:
                            return Controller.MouseMiddlePressed();
                            break;

                        case MouseInputList.MOUSE_RIGHT_BUTTON:
                            return Controller.MouseRightPressed();
                            break;

                        case MouseInputList.MOUSE_4:
                            return Controller.Mouse4Pressed();
                            break;

                        case MouseInputList.MOUSE_5:
                            return Controller.Mouse5Pressed();
                            break;
                    }
                    #endregion
                }
                else if (inputList[inputName].keyboard is Keys)
                {
                    return Controller.KeyPressed((Keys)inputList[inputName].keyboard);
                }
            }

            Console.WriteLine("O input " + inputName + " não foi verificado");
            return false;
        }

        public static Boolean InputByNameIsReleased(String inputName)
        {
            if (!inputList.ContainsKey(inputName))
                throw new Exception("There's no input named " + inputName);

            if (lastInput == InputType.GAMEPAD)
            {
                #region GAMEPAD KEY MAPPING
                switch (inputList[inputName].gamepad)
                {
                    case GamepadInputList.GAMEPAD_BACK:
                        return (currentGamePadState.Buttons.Back == ButtonState.Released) && (previousGamePadState.Buttons.Back == ButtonState.Pressed);
                        break;

                    case GamepadInputList.GAMEPAD_BIG_BUTTON:
                        return currentGamePadState.Buttons.BigButton == ButtonState.Released && (previousGamePadState.Buttons.BigButton == ButtonState.Pressed);
                        break;

                    case GamepadInputList.GAMEPAD_BUTTONS_A:
                        return currentGamePadState.Buttons.A == ButtonState.Released && (previousGamePadState.Buttons.A == ButtonState.Pressed);
                        break;

                    case GamepadInputList.GAMEPAD_BUTTONS_B:
                        return currentGamePadState.Buttons.B == ButtonState.Released && (previousGamePadState.Buttons.B == ButtonState.Pressed);
                        break;

                    case GamepadInputList.GAMEPAD_BUTTONS_X:
                        return currentGamePadState.Buttons.X == ButtonState.Released && (previousGamePadState.Buttons.X == ButtonState.Pressed);
                        break;

                    case GamepadInputList.GAMEPAD_BUTTONS_Y:
                        return currentGamePadState.Buttons.Y == ButtonState.Released && (previousGamePadState.Buttons.Y == ButtonState.Pressed);
                        break;

                    case GamepadInputList.GAMEPAD_DPAD_DOWN:
                        return currentGamePadState.DPad.Down == ButtonState.Released && (previousGamePadState.DPad.Down == ButtonState.Pressed);
                        break;

                    case GamepadInputList.GAMEPAD_DPAD_LEFT:
                        return currentGamePadState.DPad.Left == ButtonState.Released && (previousGamePadState.DPad.Left == ButtonState.Pressed);
                        break;

                    case GamepadInputList.GAMEPAD_DPAD_RIGHT:
                        return currentGamePadState.DPad.Right == ButtonState.Released && (previousGamePadState.DPad.Right == ButtonState.Pressed);
                        break;

                    case GamepadInputList.GAMEPAD_DPAD_UP:
                        return currentGamePadState.DPad.Up == ButtonState.Released && (previousGamePadState.DPad.Up == ButtonState.Pressed);
                        break;

                    case GamepadInputList.GAMEPAD_LEFT_SHOULDER:
                        return currentGamePadState.Buttons.LeftShoulder == ButtonState.Released && (previousGamePadState.Buttons.LeftShoulder == ButtonState.Pressed);
                        break;

                    case GamepadInputList.GAMEPAD_LEFTSTICK_BUTTON:
                        return currentGamePadState.Buttons.LeftStick == ButtonState.Released && (previousGamePadState.Buttons.LeftStick == ButtonState.Pressed);
                        break;

                    case GamepadInputList.GAMEPAD_RIGHT_SHOULDER:
                        return currentGamePadState.Buttons.RightShoulder == ButtonState.Released && (previousGamePadState.Buttons.RightShoulder == ButtonState.Pressed);
                        break;

                    case GamepadInputList.GAMEPAD_RIGHTSTICK_BUTTON:
                        return currentGamePadState.Buttons.RightStick == ButtonState.Released && (previousGamePadState.Buttons.RightStick == ButtonState.Pressed);
                        break;

                    case GamepadInputList.GAMEPAD_START:
                        return currentGamePadState.Buttons.Start == ButtonState.Released && (previousGamePadState.Buttons.Start == ButtonState.Pressed);
                        break;

                    case GamepadInputList.GAMEPAD_LEFTSTICK_DOWN:
                        return previousGamePadState.ThumbSticks.Left.Y <= -0.3f && (currentGamePadState.ThumbSticks.Left.Y > -0.3f);
                        break;

                    case GamepadInputList.GAMEPAD_LEFTSTICK_UP:
                        return previousGamePadState.ThumbSticks.Left.Y >= 0.3f && (currentGamePadState.ThumbSticks.Left.Y < 0.3f);
                        break;

                    case GamepadInputList.GAMEPAD_LEFTSTICK_LEFT:
                        return previousGamePadState.ThumbSticks.Left.X <= -0.3f && (currentGamePadState.ThumbSticks.Left.X > -0.3f);
                        break;

                    case GamepadInputList.GAMEPAD_LEFTSTICK_RIGHT:
                        return previousGamePadState.ThumbSticks.Left.X >= 0.3f && (currentGamePadState.ThumbSticks.Left.X < 0.3f);
                        break;

                    case GamepadInputList.GAMEPAD_RIGHTSTICK_DOWN:
                        return previousGamePadState.ThumbSticks.Right.Y <= -0.3f && (currentGamePadState.ThumbSticks.Right.Y > -0.3f);
                        break;

                    case GamepadInputList.GAMEPAD_RIGHTSTICK_UP:
                        return previousGamePadState.ThumbSticks.Right.Y >= 0.3f && (currentGamePadState.ThumbSticks.Right.Y < 0.3f);
                        break;

                    case GamepadInputList.GAMEPAD_RIGHTSTICK_LEFT:
                        return previousGamePadState.ThumbSticks.Right.X <= -0.3f && (currentGamePadState.ThumbSticks.Right.X > -0.3f);
                        break;

                    case GamepadInputList.GAMEPAD_RIGHTSTICK_RIGHT:
                        return previousGamePadState.ThumbSticks.Right.X >= 0.3f && (currentGamePadState.ThumbSticks.Right.X < 0.3f);
                        break;

                    case GamepadInputList.GAMEPAD_RIGHT_TRIGGER:
                        return previousGamePadState.Triggers.Right >= 0.3f && (currentGamePadState.Triggers.Right < 0.3f);
                        break;

                    case GamepadInputList.GAMEPAD_LEFT_TRIGGER:
                        return previousGamePadState.Triggers.Left >= 0.3f && (currentGamePadState.Triggers.Left < 0.3f);
                        break;
                }
                #endregion
            }
            else
            {
                if (inputList[inputName].keyboard is MouseInputList)
                {
                    #region MOUSE KEY MAPPING
                    switch ((MouseInputList)inputList[inputName].keyboard)
                    {
                        case MouseInputList.MOUSE_LEFT_BUTTON:
                            return Controller.MouseLeftReleased();
                            break;

                        case MouseInputList.MOUSE_MIDDLE_BUTTON:
                            return Controller.MouseMiddleReleased();
                            break;

                        case MouseInputList.MOUSE_RIGHT_BUTTON:
                            return Controller.MouseRightReleased();
                            break;

                        case MouseInputList.MOUSE_4:
                            return Controller.Mouse4Released();
                            break;

                        case MouseInputList.MOUSE_5:
                            return Controller.Mouse5Released();
                            break;
                    }
                    #endregion
                }
                else if (inputList[inputName].keyboard is Keys)
                {
                    return Controller.KeyReleased((Keys)inputList[inputName].keyboard);
                }
            }

            Console.WriteLine("O input " + inputName + " não foi verificado");
            return false;
        }

        public static float RightAxisAngle()
        {
            return MathHelper.ToDegrees(-(float)Math.Atan2(currentGamePadState.ThumbSticks.Right.Y, currentGamePadState.ThumbSticks.Right.X));
        }

        public static float LeftAxisAngle()
        {
            return MathHelper.ToDegrees(-(float)Math.Atan2(currentGamePadState.ThumbSticks.Left.Y, currentGamePadState.ThumbSticks.Left.X));
        }


        private static void ReadCustomInputs()
        {
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load("inputs.xml");
            }
            catch
            {
                setInGameControls();
                CreateDefaultXML();
                document.Load("inputs.xml");
            }

            savedInputList = new Dictionary<String, Command>();

            //LeituraHats
            foreach (XmlElement elemento in document.SelectSingleNode("inputs").ChildNodes)
            {
                Command command = new Command(Keys.A, GamepadInputList.GAMEPAD_BUTTONS_A);
                command.gamepad = (GamepadInputList)(int.Parse(elemento.Attributes[0].Value));
                if (Boolean.Parse(elemento.Attributes[1].Value))
                    command.keyboard = (MouseInputList)(int.Parse(elemento.Attributes[2].Value));
                else
                    command.keyboard = (Keys)(int.Parse(elemento.Attributes[2].Value));

                savedInputList.Add(elemento.Name, command);
            }
        }

        private static void SaveCustomInputs()
        {

        }

        private static void CreateDefaultXML()
        {
            XmlWriterSettings settings = new XmlWriterSettings();

            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create("inputs.xml", settings);

            writer.WriteStartDocument();
            writer.WriteComment("TSRDFSAJPPRARITY");

            writer.WriteStartElement("inputs");
            foreach (KeyValuePair<String, Command> par in inputList)
            {
                writer.WriteStartElement(par.Key);
                writer.WriteAttributeString("gamepad", ((int)(par.Value.gamepad)).ToString());
                writer.WriteAttributeString("keyboardIsMouse", (par.Value.keyboard is MouseInputList).ToString());
                writer.WriteAttributeString("keyboard", ((int)(par.Value.keyboard)).ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }
     
    }
}
