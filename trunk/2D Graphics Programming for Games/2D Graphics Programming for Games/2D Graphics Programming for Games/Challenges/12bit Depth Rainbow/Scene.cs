using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BringelandoGameEngine.Core.SceneManagement;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace _2D_Graphics_Programming_for_Games.Challenges._12bit_Depth_Rainbow
{
	class Scene : SceneBase
	{
        int numberOfColours;
        int textureSize;

        Texture2D texture;
        

        public override void  start()
        {
            numberOfColours = (int)(16 * 16 * 16);
            textureSize = (int)Math.Sqrt(numberOfColours);

            texture = new Texture2D(Game1.self.GraphicsDevice, textureSize, textureSize);

            List<Color> coloursList = new List<Color>();

            for (int red = 0; red < 16; red++)
            {
                for (int green = 0; green < 16; green++)
                {
                    for (int blue = 0; blue < 16; blue++)
                    {
                        coloursList.Add(new Color(red * 17, green * 17, blue * 17));
                    }
                }
            }



            Color[] colourArray = coloursList.ToArray();


            texture.SetData<Color>(colourArray);
                
            
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
 	        
        }

        public override void  draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Game1.self.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
            spriteBatch.Draw(texture, Game1.self.GraphicsDevice.Viewport.Bounds, Color.White);
            spriteBatch.End();
        }

        public override void  terminate()
        {
 	        
        }
	}
}
