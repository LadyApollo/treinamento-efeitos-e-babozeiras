using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Treinamento_e_Exemplos.SceneManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Treinamento_e_Exemplos.Scenes.SHADER2D
{
    class SceneShader2D : SceneBase
    {
        Texture2D textura;
        Effect efeito;

        public override void start()
        {
            textura = Game1.Self.Content.Load<Texture2D>("SHADER2D/waifu");
            efeito = Game1.Self.Content.Load<Effect>("SHADER2D/blur");
        }

        public override void update(GameTime gameTime)
        {
            
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            
            efeito.Techniques["PostEffect"].Passes["Blur"].Apply();
            efeito.Parameters["BlurActived"].SetValue(true);
            spriteBatch.Draw(textura, Vector2.Zero, Color.White);
            

            spriteBatch.End();
        }

        public override void terminate()
        {
            
        }
    }
}
