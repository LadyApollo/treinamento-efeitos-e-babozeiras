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

namespace Treinamento_e_Exemplos.SceneManagement
{
    public abstract class SceneBase
    {
        public string sceneName;

        public abstract void start();
        public abstract void update(GameTime gameTime);
        public abstract void draw(SpriteBatch spriteBatch);
        public abstract void terminate();
    }
}
