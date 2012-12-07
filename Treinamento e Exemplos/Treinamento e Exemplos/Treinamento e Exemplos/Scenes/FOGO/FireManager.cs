using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Treinamento_e_Exemplos.Scenes.FOGO
{
    static class FireManager
    {
        static Random random;
        static List<FireParticle> fireParticles;

        

        static Boolean inialized = false;

        public static void Initialize()
        {
            if (!inialized)
            {
                fireParticles = new List<FireParticle>();
                
                random = new Random();
                inialized = true;
            }
        }

        public static void AddFireParticle(FireParticle fireParticle)
        {
            fireParticles.Add(fireParticle);
        }

        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < fireParticles.Count; i++)
            {
                fireParticles[i].Update(gameTime);

                float posSoma = 0;
                int numSomados = 0;
                float posMedia = 0;

                for (int o = 0; o < fireParticles.Count; o++)
                {
                    if (BringelUtils.Distance(new Vector2(fireParticles[i].x, fireParticles[i].y), new Vector2(fireParticles[o].x, fireParticles[o].y)) < 60)
                    {
                        if (fireParticles[o].y < fireParticles[i].y - 20)
                        {
                            posSoma += fireParticles[o].x;
                            numSomados++;
                        }
                    }
                }

                

                if (numSomados > 0)
                {
                    posMedia = posSoma / numSomados;
                    fireParticles[i].x = MathHelper.Lerp(fireParticles[i].x, posMedia, 0.05f);

                }

                
                if (fireParticles[i].alpha < 0.12f)
                {
                    fireParticles.Remove(fireParticles[i]);
                }
                
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            for (int i = 0; i < fireParticles.Count; i++)
            {
                fireParticles[i].Draw(spriteBatch);
            }
            spriteBatch.End();
        }


    }
}
