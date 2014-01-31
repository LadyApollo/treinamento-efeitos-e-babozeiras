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
    public class Game3 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

       
        Effect blurEffect;
        EffectParameter filterValue, backGroundTexture;
        Texture2D bg, pon3;

        public Game3()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pon3 = Content.Load<Texture2D>("PONY");
            bg = Content.Load<Texture2D>("filtro");

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();


            blurEffect = Content.Load<Effect>("grandientFilter");
            filterValue = blurEffect.Parameters["filterValue"];
            backGroundTexture = blurEffect.Parameters["backgroundTexture"];

            IsMouseVisible = true;
        }


        protected override void Update(GameTime gameTime)
        {
            backGroundTexture.SetValue((Texture)pon3);
            filterValue.SetValue(Mouse.GetState().X / 800f);
            Console.WriteLine(Mouse.GetState().X / 800f);

            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(95, 105, 122));

            GraphicsDevice.Clear(Color.WhiteSmoke);

            
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            blurEffect.CurrentTechnique.Passes[0].Apply();
            spriteBatch.Draw(bg, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
