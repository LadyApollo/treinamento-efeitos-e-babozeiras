using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Treinamento_e_Exemplos.SceneManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Treinamento_e_Exemplos.Scenes.FOGO;

namespace Treinamento_e_Exemplos.Scenes
{
    class Fogo : SceneBase
    {
        Random random;
        List<FireParticle> listaParticulas;

        Texture2D texturaFogo;

     

        public override void start()
        {
            listaParticulas = new List<FireParticle>();
            random = new Random();

            texturaFogo = Game1.Self.Content.Load<Texture2D>("FOGO/penis_borra");
            FireManager.Initialize();
            sceneName = "Little Inferno's Fire";
        }

        public override void update(GameTime gameTime)
        {
            if (Controller.MouseLeftDown())
            {
                FireParticle particula = new FireParticle(texturaFogo, random);
                particula.x = random.Next(Mouse.GetState().X - 5, Mouse.GetState().X + 5);
                particula.y = random.Next(Mouse.GetState().Y - 5, Mouse.GetState().Y + 5);
                listaParticulas.Add(particula);
                FireManager.AddFireParticle(particula);
            }

            FireParticle particula2 = new FireParticle(texturaFogo, random);
            particula2.x = random.Next(400 - 5, 400 + 5);
            particula2.y = random.Next(400 - 5, 400 + 5);
            listaParticulas.Add(particula2);
            FireManager.AddFireParticle(particula2);

            FireManager.Update(gameTime);


        }

        public override void draw(SpriteBatch spriteBatch)
        {
            FireManager.Draw(spriteBatch);
        }

        public override void terminate()
        {
            
        }
    }
}
