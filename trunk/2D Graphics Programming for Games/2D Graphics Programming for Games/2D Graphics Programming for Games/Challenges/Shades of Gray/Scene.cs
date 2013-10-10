using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BringelandoGameEngine.Core.SceneManagement;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace _2D_Graphics_Programming_for_Games.Challenges.Shades_of_Gray
{
	class Scene : SceneBase
	{
        Texture2D texture;
        int numberOfGrays;

        public override void  start()
        {
            texture = new Texture2D(Game1.self.GraphicsDevice, Game1.self.GraphicsDevice.Viewport.Width, Game1.self.GraphicsDevice.Viewport.Height);
            numberOfGrays = 256;

            for (int i = 0; i < numberOfGrays; i++)
            {
                Rectangle grayArea = new Rectangle(i, 0, 1, texture.Height);

                Color[] gray = new Color[texture.Height];

                for (int y = 0; y < texture.Height; y++)
                {
                    gray[y] = new Color(i, i, i);
                }

                texture.SetData<Color>(0, grayArea, gray, 0, gray.Count());
                
            }
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
 	        
        }

        public override void  draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Game1.self.GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            spriteBatch.End();
        }

        public override void  terminate()
        {
 	        
        }
	}
}
