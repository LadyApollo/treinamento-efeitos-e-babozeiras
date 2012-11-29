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
    class Objeto2D
    {
        #region atributos_privados

        /// <summary>
        /// Controla o tamanho horizontal do objeto, baseado em uma porcentagem; 
        /// 1 = tamanho original; 2 = dobro do tamanho; 0.5 = metade do tamanho; 
        /// NÃO USE ESSA VARIÁVEL, AO INVÉS, USE O METODO "ScaleX";
        /// </summary>
        private float scaleX;

        /// <summary>
        /// Controla o tamanho vertical do objeto, baseado em uma porcentagem;
        /// 1 = tamanho original; 2 = dobro do tamanho; 0.5 = metade do tamanho; 
        /// NÃO USE ESSA VARIÁVEL, AO INVÉS, USE O METODO "ScaleY";
        /// </summary>
        private float scaleY;

        /// <summary>
        /// Controla o tamanho horizontal do objeto, baseado em pixels; 
        /// NÃO USE ESSA VARIÁVEL, AO INVÉS, USE O METODO "Width";
        /// </summary>
        private float width;

        /// <summary>
        /// Controla o tamanho vertical do objeto, baseado em pixels; 
        /// NÃO USE ESSA VARIÁVEL, AO INVÉS, USE O METODO "Width";
        /// </summary>
        private float height;

        

        

        #endregion

        #region atributos_protegidos

        /// <summary>
        /// Textura do Objeto
        /// </summary>
        public Texture2D textura;


        /// <summary>
        /// Retangulo de corte, usado para animação
        /// </summary>
        protected Rectangle collisionBounds;

        protected Rectangle sourceRectangle;

        /// <summary>
        /// Guarda a posição do ponto de referência.
        /// </summary>
        public Vector2 VetorDeOrigem;

        #endregion

        #region atributos_públicos

        /// <summary>
        /// Posição Horizontal
        /// </summary>
        public float x;

        /// <summary>
        /// Posição Vertical
        /// </summary>
        public float y;

        /// <summary>
        /// Rotação do objeto (em Graus)
        /// </summary>
        public float rotation;

        /// <summary>
        /// Controla o estádo de visibilidade do Objeto
        /// </summary>
        public bool visible;

        /// <summary>
        /// Esta variábel serve para desenhar o retangulo de colisão
        /// </summary>
        public bool drawCollisionBounds;

        /// <summary>
        /// 
        /// </summary>
        public float depth;

        /// <summary>
        /// 
        /// </summary>
        public float alpha;

        /// <summary>
        /// 
        /// </summary>
        public Color color;

        public bool inverted;

        /// <summary>
        /// 
        /// </summary>
        public SpriteEffects spriteEffects;

        
        



        #endregion


        //Dragon Festival
        public enum ActionColor
        {
            VERMELHO,
            VERDE,
            AZUL,
            DOURADO
        }

        public ActionColor actionColor = ActionColor.VERMELHO;

        /// <summary>
        /// Função construtora de penis gigantes!!
        /// </summary>
        /// <param name="textura"></param>
        public Objeto2D(Texture2D textura)
        {
            spriteEffects = SpriteEffects.None;
            this.textura = textura;
            x = y = 0;
            scaleX = scaleY = 1;
            rotation = 0;
            alpha = 1;
            color = Color.White;
            inverted = false;
            visible = true;

            
            this.sourceRectangle = new Rectangle(0, 0, textura.Width, textura.Height);
                
            
            

            VetorDeOrigem = new Vector2(textura.Width / 2, textura.Height / 2);

            this.depth = 0.5f;

            calcularWidth();
            calcularHeight();
        }

        

        public void setOrigin(float factorX, float factorY)
        {
            VetorDeOrigem = new Vector2(
                (float)Math.Round(textura.Width * factorX),
                (float)Math.Round(textura.Height * factorY));
        }


        #region width, height e scales

        public float ScaleX
        {
            get { return scaleX; }
            set
            {
                scaleX = value;
                calcularWidth();
            }
        }

        public float ScaleY
        {
            get { return scaleY; }
            set
            {
                scaleY = value;
                calcularHeight();
            }
        }

        public float Width
        {
            get { return width; }
            set
            {
                width = value;
                calcularScaleX();
            }
        }

        public float Height
        {
            get { return height; }
            set
            {
                height = value;
                calcularScaleY();
            }
        }

        private void calcularScaleX()
        {
            ScaleX = width / sourceRectangle.Width;
        }

        private void calcularScaleY()
        {
            ScaleY = height / sourceRectangle.Height;
        }

        private void calcularWidth()
        {
            width = sourceRectangle.Width * scaleX;
            if (width < 0)
            {
                width *= -1;
            }
        }

        private void calcularHeight()
        {
            height = sourceRectangle.Height * scaleY;
            if (Height < 0)
            {
                height *= -1;
            }
        }

        #endregion

        public Boolean hitTestObject(Objeto2D objeto)
        {
            this.CalcularRetangulo();
            objeto.CalcularRetangulo();

            if (this.collisionBounds.Intersects(objeto.collisionBounds))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public virtual void CalcularRetangulo()
        {
            collisionBounds = new Rectangle(
                (int)(this.x - (textura.Width / 2) * ScaleX),
                (int)(this.y - (textura.Height / 2) * ScaleY),
                (int)(textura.Width * ScaleX),
                (int)(textura.Height * ScaleY)
                );
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {

                spriteBatch.Draw
                (
                    textura,
                    new Vector2(x, y),
                    sourceRectangle,
                    Color.Multiply(color, alpha),
                    MathHelper.ToRadians(rotation),
                    VetorDeOrigem,
                    new Vector2(scaleX, scaleY),
                    (inverted ? SpriteEffects.FlipHorizontally : SpriteEffects.None),
                    depth
                );

                /*
                if (drawCollisionBounds)
                {
                    Texture2D dummyTexture = new Texture2D(Game1.Self.GraphicsDevice, 1, 1);
                    dummyTexture.SetData(new Color[] { Color.White });

                    spriteBatch.Draw(dummyTexture, collisionBounds, Color.White);
                }*/
            }
        }


        public virtual void Update(GameTime gameTime)
        {
            
        }
    }
}
