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
using BringelandoGameEngine.Core.SceneManagement;
using _2D_Graphics_Programming_for_Games.Challenges.Shades_of_Gray;
using BringelandoGameEngine.Core;

namespace _2D_Graphics_Programming_for_Games
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static Game1 self;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";


            self = this;
        }




        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            SceneManager.setScene(new Challenges._12bit_Depth_Rainbow.Scene(), false);

        }


        protected override void Update(GameTime gameTime)
        {
            SceneManager.update(gameTime);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SceneManager.draw(spriteBatch);

            

            base.Draw(gameTime);
        }
    }
}
