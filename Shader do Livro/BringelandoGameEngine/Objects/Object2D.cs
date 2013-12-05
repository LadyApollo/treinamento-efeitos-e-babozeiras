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
using BringelandoGameEngine.Core;
using BringelandoGameEngine.Utils;


namespace BringelandoGameEngine.Objects
{
    public class Object2D
    {
        #region Private Atributtes

        /// <summary>
        /// Controla o tamanho horizontal do objeto, baseado em uma porcentagem; 
        /// 1 = tamanho original; 2 = dobro do tamanho; 0.5 = metade do tamanho; 
        /// NÃO USE ESSA VARIÁVEL, AO INVÉS, USE O METODO "Scale";
        /// </summary>
        private Vector2 _scale;

        /// <summary>
        /// Controla o tamanho horizontal do objeto, baseado em pixels; 
        /// NÃO USE ESSA VARIÁVEL, AO INVÉS, USE O METODO "Width";
        /// </summary>
        private float _width;

        /// <summary>
        /// Controla o tamanho vertical do objeto, baseado em pixels; 
        /// NÃO USE ESSA VARIÁVEL, AO INVÉS, USE O METODO "Width";
        /// </summary>
        private float _height;

        private float _rotation;

        #endregion

        #region Protected Atributtes

        /// <summary>
        /// Retangulo de corte, usado para animação
        /// </summary>
        protected Rectangle collisionBounds;

        /// <summary>
        /// 
        /// </summary>
        public SpriteEffects effect;

        #endregion

        #region Public Atributtes

        /// <summary>
        /// Object texture.
        /// </summary>
        public Texture2D texture;

        /// <summary>
        /// Object position.
        /// </summary>
        public Vector2 position;

        /// <summary>
        /// Controls if the object will be rendered or not.
        /// </summary>
        public bool visible;

        /// <summary>
        /// If enebled, will render the collision bounds.
        /// Not yet implemented.
        /// </summary>
        public bool showCollisionBounds;

        /// <summary>
        /// A zero to one value that controls what object will be rendered in front of other.
        /// The default is 0.5f;
        /// In order to use this, you'll need to change the SpriteBatch begin arguments.
        /// </summary>
        public float depth;

        /// <summary>
        /// A zero to one value that controls the texture alpha channel.
        /// </summary>
        public float alpha;

        /// <summary>
        /// Color of the object.
        /// White for any change on texture color.
        /// </summary>
        public Color color;

        /// <summary>
        /// Retângulo de corte
        /// </summary>
        public Rectangle sourceRectangle;

        /// <summary>
        /// The sprite origin; the default is (0,0) which represents the upper-left corner.
        /// </summary>
        public Vector2 origin;

        #endregion


        public Object2D(Texture2D texture)
        {
            this.texture = texture;
            position = Vector2.Zero;
            _scale = Vector2.One;
            _rotation = 0;
            alpha = 1;
            color = Color.White;
            visible = true;
            depth = 0.5f;


            if (texture != null)
            {
                this.sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
                origin = new Vector2(texture.Width / 2, texture.Height / 2);
            }

            calcularWidth();
            calcularHeight();
        }

        

        public void setOrigin(float xFactor, float yFactor)
        {
            origin = new Vector2(
                (float)Math.Round(texture.Width * xFactor),
                (float)Math.Round(texture.Height * yFactor));
        }

        public Vector2 getOriginFactor()
        {
            return new Vector2(origin.X / Width, origin.Y / Height);
        }

        public Boolean hitTestObject(Object2D obj)
        {
            this.CalcularRetangulo();
            obj.CalcularRetangulo();

            return this.collisionBounds.Intersects(obj.collisionBounds);
        }

        public virtual Boolean Clicked()
        {
            //TODO: Considerar foco da tela
            CalcularRetangulo();
            return (this.collisionBounds.Intersects(Controller.MouseRect) && Controller.MouseLeftPressed());
        }

        public virtual Boolean MouseOver()
        {
            //TODO: Considerar foco da tela
            CalcularRetangulo();
            return (this.collisionBounds.Intersects(Controller.MouseRect));
        }

        public virtual void CalcularRetangulo()
        {
            //TODO: Bateria de teste
            collisionBounds = new Rectangle(
                (int)(this.position.X - (Width * getOriginFactor().X) * ScaleX),
                (int)(this.position.Y - (Height * getOriginFactor().Y) * ScaleY),
                (int)(texture.Width * ScaleX),
                (int)(texture.Height * ScaleY));
        }

        #region width, height e scales

        public Vector2 Scale
        {
            get { return _scale; }
            set
            {
                _scale = value;
                calcularWidth();
                calcularHeight();
            }
        }

        public float ScaleX
        {
            get { return _scale.X; }
            set
            {
                _scale.X = value;
                calcularWidth();
            }
        }

        public float ScaleY
        {
            get { return _scale.Y; }
            set
            {
                _scale.Y = value;
                calcularHeight();
            }
        }

        public float Width
        {
            get { return _width; }
            set
            {
                _width = value;
                calcularScaleX();
            }
        }

        public float Height
        {
            get { return _height; }
            set
            {
                _height = value;
                calcularScaleY();
            }
        }

        private void calcularScaleX()
        {
            ScaleX = _width / sourceRectangle.Width;
        }

        private void calcularScaleY()
        {
            ScaleY = _height / sourceRectangle.Height;
        }

        private void calcularWidth()
        {
            _width = sourceRectangle.Width * _scale.X;
            if (_width < 0)
            {
                //TODO: Testar
                _width *= -1;
            }
        }

        private void calcularHeight()
        {
            _height = sourceRectangle.Height * _scale.Y;
            if (Height < 0)
            {
                //TODO: Testar
                _height *= -1;
            }
        }

        #endregion

        public float Rotation
        {
            get { return MathHelper.ToDegrees(_rotation); }
            set { _rotation = MathHelper.ToRadians(value) + RelativeRotation; }
        }

        public float RelativeRotation = 0f;     

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {
                spriteBatch.Draw(texture, position, sourceRectangle, Color.Multiply(color, alpha), _rotation, origin, _scale, effect, depth);

                if (showCollisionBounds)
                {
                    CalcularRetangulo();

                    #region Draw Collision Lines

                    //Linha cima
                    LineBatch.DrawLine(spriteBatch,
                        Color.Red,
                        new Vector2(collisionBounds.X, collisionBounds.Y),
                        new Vector2(collisionBounds.X + collisionBounds.Width, collisionBounds.Y));

                    //Linha Direita
                    LineBatch.DrawLine(spriteBatch,
                        Color.Red,
                        new Vector2(collisionBounds.X + collisionBounds.Width, collisionBounds.Y),
                        new Vector2(collisionBounds.X + collisionBounds.Width, collisionBounds.Y + collisionBounds.Height));

                    //Linha Baixo
                    LineBatch.DrawLine(spriteBatch,
                        Color.Red,
                        new Vector2(collisionBounds.X + collisionBounds.Width, collisionBounds.Y + collisionBounds.Height),
                        new Vector2(collisionBounds.X, collisionBounds.Y + collisionBounds.Height));

                    //Linha Esquerda
                    LineBatch.DrawLine(spriteBatch,
                        Color.Red,
                        new Vector2(collisionBounds.X, collisionBounds.Y + collisionBounds.Height),
                        new Vector2(collisionBounds.X, collisionBounds.Y));

                    #endregion

                    #region Draw Pivot Piont

                    LineBatch.DrawLine(spriteBatch,
                        Color.Red,
                        new Vector2(position.X - 1, position.Y - 1),
                        new Vector2(position.X + 1, position.Y + 1));

                    LineBatch.DrawLine(spriteBatch,
                        Color.Red,
                        new Vector2(position.X + 1, position.Y - 1),
                        new Vector2(position.X - 1, position.Y + 1));

                    #endregion
                }
            }
        }






       
    }
}
