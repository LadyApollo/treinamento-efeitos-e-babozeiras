using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BringelandoGameEngine.Objects
{
    class AnimatedObject : Object2D
    {
        public struct AnimationStructure
        {
            public int frameWidth;
            public int frameHeight;
            public int frameCount;
            public int framesPerSecond;
            public int startX;
            public int startY;
            public int framesPerLine;

        }

        public AnimationStructure currentAnimation;
        public Dictionary<String, AnimationStructure> animations;
        public int frame;
        public TimeSpan animationTime;


        public AnimatedObject(Texture2D texture, int Width, int Height)
            : base(texture)
        {
            animations = new Dictionary<String, AnimationStructure>();

            sourceRectangle = new Rectangle(0, 0, Width, Height);
            origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

            AnimationStructure animationDefault;
            animationDefault = new AnimationStructure();
            animationDefault.frameWidth = Width;
            animationDefault.frameHeight = Height;
            animationDefault.startX = 0;
            animationDefault.startY = 0;
            animationDefault.framesPerSecond = 100;
            animationDefault.frameCount = 1;
            animationDefault.framesPerLine = 1;
            this.AddAnimation("default", animationDefault);
        }


        public void AddAnimation(String name, AnimationStructure animation)
        {
            animations.Add(name, animation);
        }

        public void ChangeAnimation(String name)
        {
            currentAnimation = animations[name];
        }

        public virtual void Update(GameTime gameTime)
        {
            animationTime = animationTime.Add(gameTime.ElapsedGameTime);

            frame = (int)(animationTime.TotalSeconds * currentAnimation.framesPerSecond) % currentAnimation.frameCount;
        }

        protected virtual void calcularCorte()
        {
            sourceRectangle = new Rectangle(
                       currentAnimation.startX + (frame * currentAnimation.frameWidth),
                       currentAnimation.startY + currentAnimation.frameHeight * (int)((currentAnimation.startX + frame * currentAnimation.frameWidth)),
                       currentAnimation.frameWidth,
                       currentAnimation.frameHeight);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            calcularCorte();
            base.Draw(spriteBatch);
        }
    }
}
