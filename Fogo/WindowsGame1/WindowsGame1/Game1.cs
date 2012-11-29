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

        Random random;
        List<Objeto2D> listaParticulas;

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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            listaParticulas = new List<Objeto2D>();
            random = new Random();

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
            
            Objeto2D particula = new Objeto2D(Content.Load<Texture2D>("penis_borra"));
            particula.x = random.Next(Mouse.GetState().X - 5, Mouse.GetState().X + 5);
            particula.y = random.Next(Mouse.GetState().Y - 5, Mouse.GetState().Y + 5);
            particula.ScaleX = 0.4f;
            particula.ScaleY = 0.5f;
            particula.alpha = 0.5f;
            particula.color = Color.Lerp(Color.Lerp(Color.Yellow, Color.Red, (float)random.NextDouble()), Color.White, 0.4f);
            listaParticulas.Add(particula);

            Objeto2D particula2 = new Objeto2D(Content.Load<Texture2D>("penis_borra"));
            particula2.x = random.Next(400 - 5, 400 + 5);
            particula2.y = random.Next(400 - 5, 400 + 5);
            particula2.ScaleX = 0.4f;
            particula2.ScaleY = 0.5f;
            particula2.alpha = 0.5f;
            particula2.color = Color.Lerp(Color.Lerp(Color.Yellow, Color.Red, (float)random.NextDouble()), Color.White, 0.4f);
            listaParticulas.Add(particula2);

            for (int i = 0; i < listaParticulas.Count; i++)
            {
                listaParticulas[i].y -= 3;
                listaParticulas[i].alpha -= 0.01f;

                float posSoma = 0;
                int numSomados= 0;
                float posMedia = 0;

                for (int o = 0; o < listaParticulas.Count; o++)
                {
                    if (distancia(new Vector2(listaParticulas[i].x, listaParticulas[i].y), new Vector2(listaParticulas[o].x, listaParticulas[o].y)) < 80)
                    {
                        posSoma += listaParticulas[o].x;
                        numSomados++;
                    }
                }

                if (numSomados > 0)
                {
                    posMedia = posSoma / numSomados;
                    listaParticulas[i].x = MathHelper.Lerp(listaParticulas[i].x, posMedia, 0.05f);
                }

                if (listaParticulas[i].alpha < 0.01f)
                {
                    listaParticulas.Remove(listaParticulas[i]);
                }
            }

            base.Update(gameTime);

            
        }


        public float distancia(Vector2 ponto1, Vector2 ponto2)
        {
            float distanciaX = MathHelper.Distance(ponto1.X, ponto2.X);
            float distanciaY = MathHelper.Distance(ponto1.X, ponto2.X);

            return (float)Math.Sqrt(distanciaX * distanciaX + distanciaY * distanciaY);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            for (int i = 0; i < listaParticulas.Count; i++)
            {
                listaParticulas[i].Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
