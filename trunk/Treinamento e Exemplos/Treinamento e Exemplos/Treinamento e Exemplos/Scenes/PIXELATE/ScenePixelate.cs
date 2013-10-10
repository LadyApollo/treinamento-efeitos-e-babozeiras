using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Treinamento_e_Exemplos.SceneManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Xml;

namespace Treinamento_e_Exemplos.Scenes.PIXELATE
{
    class ScenePixelate : SceneBase
    {
        Texture2D textura;
        Color[] data;
        Color[,] data2D;

        public override void start()
        {
            textura = Game1.Self.Content.Load<Texture2D>("pone");
            
            data = new Color[textura.Width * textura.Height];
            data2D = new Color[textura.Width, textura.Height];

            textura.GetData<Color>(data);

            for (int x = 0; x < textura.Width; x++)
                for (int y = 0; y < textura.Height; y++)
                    data2D[x, y] = data[x + y * textura.Width];
             
            for(int x = 0; x < textura.Width / 2; x++)
            {
                for (int y = 0; y < textura.Height / 2; y++)
                {

                }
            }
            
        }

        public override void update(GameTime gameTime)
        {
            
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            
        }

        public override void terminate()
        {
            
        }
    }
}
