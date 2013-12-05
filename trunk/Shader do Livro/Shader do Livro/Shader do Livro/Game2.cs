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
    public class Game2 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

       
        Effect blurEffect;
        EffectParameter center, backGroundTexture;
        Texture2D bg, pon3;

        public Game2()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pon3 = Content.Load<Texture2D>("text");
            bg = Content.Load<Texture2D>("bg");

            graphics.PreferredBackBufferWidth = pon3.Width;
            graphics.PreferredBackBufferHeight = pon3.Height;
            graphics.ApplyChanges();
  

            blurEffect = Content.Load<Effect>("blur");
            center = blurEffect.Parameters["center"];
            backGroundTexture = blurEffect.Parameters["backgroundTexture"];
            IsMouseVisible = true;
        }


        protected override void Update(GameTime gameTime)
        {
            center.SetValue(new Vector2(Mouse.GetState().X / (float)graphics.PreferredBackBufferWidth, Mouse.GetState().Y / (float)graphics.PreferredBackBufferWidth));
            backGroundTexture.SetValue((Texture)bg);
            
            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(95, 105, 122));

            GraphicsDevice.Clear(Color.WhiteSmoke);

            
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            blurEffect.CurrentTechnique.Passes[0].Apply();
            spriteBatch.Draw(pon3, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
