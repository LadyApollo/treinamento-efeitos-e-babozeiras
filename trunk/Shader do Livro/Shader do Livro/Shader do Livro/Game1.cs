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
using BringelandoGameEngine.Objects;

namespace Shader_do_Livro
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Object2D pon3;
        RenderTarget2D tempRenderTarget;
        Texture2D tempText;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pon3 = new Object2D(Content.Load<Texture2D>("text"));
            pon3.position.X = graphics.PreferredBackBufferWidth / 2;
            pon3.position.Y = graphics.PreferredBackBufferHeight / 2;

            tempRenderTarget = new RenderTarget2D(GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            tempText = new Texture2D(GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }


        protected override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(tempRenderTarget);          
            GraphicsDevice.Clear(new Color(95, 105, 122));

            spriteBatch.Begin();
            pon3.Draw(spriteBatch);
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.WhiteSmoke);

            int width = tempRenderTarget.Width;
            int height = tempRenderTarget.Height;
            Vector2 center = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            Color[] colors = new Color[width * height];
            tempRenderTarget.GetData<Color>(colors);
            double maxDistSQR = Math.Sqrt(Math.Pow(center.X, 2) + Math.Pow(center.Y, 2));

            for (int v = 0; v < height; v++)
            {
                for (int u = 0; u < width; u++)
                {
                    double distSQR = Math.Sqrt(Math.Pow(u - center.X, 2) + Math.Pow(v - center.Y, 2));
                    int blurAmount = (int)Math.Floor(20 * distSQR / maxDistSQR);

                    int currElement = u + (width * v);
                    int prevElement = currElement - blurAmount;
                    int nextElement = currElement + blurAmount;

                    if (((currElement - blurAmount) > 0) && ((currElement + blurAmount) < width * height))
                    {
                        colors[currElement].R = (byte)((colors[currElement].R + colors[prevElement].R + colors[nextElement].R) / 3.0f);
                        colors[currElement].G = (byte)((colors[currElement].G + colors[prevElement].G + colors[nextElement].G) / 3.0f);
                        colors[currElement].B = (byte)((colors[currElement].B + colors[prevElement].B + colors[nextElement].B) / 3.0f);
                    }
                }
            }
            tempText.SetData<Color>(colors);

            spriteBatch.Begin();
            spriteBatch.Draw(tempText   , Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
