using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Treinamento_e_Exemplos.Scenes.FOGO
{
    class FireParticle : Objeto2D
    {
        Random random;
        public Vector2 Velocity;

        public FireParticle(Texture2D textura, Random random)
            : base(textura)
        {
            this.random = random;

            ScaleX = 0.4f;
            ScaleY = 0.5f;
            alpha = 0.5f;
            color = Color.Lerp(Color.Lerp(Color.Yellow, Color.Red, (float)random.NextDouble()), Color.White, 0.4f);

            Velocity = new Vector2(0, -3);
        }

        public override void Update(GameTime gameTime)
        {
            this.y += Velocity.Y;
            this.x += Velocity.X;
            ScaleX = MathHelper.Lerp(ScaleX, 0.2f, 0.02f);
            ScaleY = MathHelper.Lerp(ScaleY, 0.3f, 0.02f);
            color = Color.Lerp(color, Color.Lerp(Color.Lerp(Color.Yellow, Color.Red, 0.4f), Color.White, 0.4f), 0.03f);

            this.alpha -= 0.01f;
            base.Update(gameTime);
        }
    }
}
