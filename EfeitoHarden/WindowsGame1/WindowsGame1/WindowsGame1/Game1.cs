using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D textura;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            IsMouseVisible = true;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            textura = Content.Load<Texture2D>("megamilk");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            Color[] data1D = new Color[textura.Width * textura.Height];
            textura.GetData<Color>(data1D);

            for (int i = 0; i < data1D.Length; i++)
            {
                
                if (data1D[i].R < 128)
                {
                    data1D[i].R = (byte)MathHelper.Clamp(data1D[i].R - 1, 0, 255);
                }
                else
                {
                    data1D[i].R = (byte)MathHelper.Clamp(data1D[i].R + 1, 0, 255);
                }

                
                if (data1D[i].G < 128)
                {
                    data1D[i].G = (byte)MathHelper.Clamp(data1D[i].G - 1, 0, 255);
                }
                else
                {
                    data1D[i].G = (byte)MathHelper.Clamp(data1D[i].G + 1, 0, 255);
                }

                if (data1D[i].B < 128)
                {
                    data1D[i].B = (byte)MathHelper.Clamp(data1D[i].B - 1, 0, 255);
                }
                else
                {
                    data1D[i].B = (byte)MathHelper.Clamp(data1D[i].B + 1, 0, 255);
                }
            }
            GraphicsDevice.Textures[0] = null;

            textura.SetData<Color>(data1D);

            //Console.WriteLine(gameTime.ElapsedGameTime);

            /*
            Color[,] data2D = new Color[textura.Width, textura.Height];
            for (int x = 0; x < textura.Width; x++)
                for (int y = 0; y < textura.Height; y++)
                    data2D[x, y] = data1D[x + y * textura.Width];
             * */

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(textura, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
