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
using Treinamento_e_Exemplos.SceneManagement;
using Treinamento_e_Exemplos.Scenes;

namespace Treinamento_e_Exemplos
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static Game1 Self;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Self = this;
        }




        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            SceneManager.setScene(new Fogo(), false);
        }


        protected override void Update(GameTime gameTime)
        {
            Controller.Update(gameTime);
            SceneManager.update(gameTime);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SceneManager.draw(spriteBatch, GraphicsDevice);

            base.Draw(gameTime);
        }
    }
}
