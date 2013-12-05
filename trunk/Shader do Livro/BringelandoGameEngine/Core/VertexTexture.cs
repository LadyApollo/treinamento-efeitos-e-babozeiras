using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BringelandoGameEngine.Core
{
    public class VertexTexture
    {
        public Effect effect;
        public Texture2D texture;

        public Vector3 topLeftVertex, topRightVertex, bottomRightVertex, bottomLeftVertex;

        VertexPositionTexture[] vertices;




        public VertexTexture(Texture2D texture, ContentManager content)
        {
            effect = content.Load<Effect>("effects");
            this.texture = texture;

            topLeftVertex = new Vector3(-6f, 2f, 2f);
            topRightVertex = new Vector3(6f, 2f, 2f);
            bottomRightVertex = new Vector3(6f, -2f, 2f);
            bottomLeftVertex = new Vector3(-6f, -2f, 2f);

            SetUpVertices();
        }

        public void Update(GameTime gameTime)
        {
            SetUpVertices();
        }

        public void movetopLeftVertex(Vector3 amount)
        {
            topLeftVertex += amount;
        }

        public void movetopRightVertex(Vector3 amount)
        {
            topRightVertex += amount;
        }

        public void movebottomRightVertex(Vector3 amount)
        {
            bottomRightVertex += amount;
        }

        public void movebottomLeftVertex(Vector3 amount)
        {
            bottomLeftVertex += amount;
        }

        public void moveRightEdge(Vector3 amount)
        {
            topRightVertex += amount;
            bottomRightVertex += amount;
        }

        public void moveLeftEdge(Vector3 amount)
        {
            topLeftVertex += amount;
            bottomLeftVertex += amount;
        }

        public void moveTopEdge(Vector3 amount)
        {
            topLeftVertex += amount;
            topRightVertex += amount;
        }

        public void moveDownEdge(Vector3 amount)
        {
            bottomRightVertex += amount;
            bottomLeftVertex += amount;
        }

        public void moveAll(Vector3 amount)
        {
            topRightVertex += amount;
            bottomRightVertex += amount;
            topLeftVertex += amount;
            bottomLeftVertex += amount;
        }

        public void SetUpVertices()
        {
            vertices = new VertexPositionTexture[6];

            vertices[0].Position = topLeftVertex;
            vertices[0].TextureCoordinate.X = 0;
            vertices[0].TextureCoordinate.Y = 0;

            vertices[1].Position = bottomRightVertex;
            vertices[1].TextureCoordinate.X = 1;
            vertices[1].TextureCoordinate.Y = 1;

            vertices[2].Position = bottomLeftVertex;
            vertices[2].TextureCoordinate.X = 0;
            vertices[2].TextureCoordinate.Y = 1;

            vertices[3].Position = bottomRightVertex;
            vertices[3].TextureCoordinate.X = 1;
            vertices[3].TextureCoordinate.Y = 1;

            vertices[4].Position = topLeftVertex;
            vertices[4].TextureCoordinate.X = 0;
            vertices[4].TextureCoordinate.Y = 0;

            vertices[5].Position = topRightVertex;
            vertices[5].TextureCoordinate.X = 1;
            vertices[5].TextureCoordinate.Y = 0;
        }


        public void Draw(Matrix view, Matrix projection, GraphicsDevice graphics)
        {
           
            graphics.BlendState = BlendState.AlphaBlend;

            Matrix worldMatrix = Matrix.Identity;
            effect.CurrentTechnique = effect.Techniques["TexturedNoShading"];
            effect.Parameters["xWorld"].SetValue(worldMatrix);
            effect.Parameters["xView"].SetValue(view);
            effect.Parameters["xProjection"].SetValue(projection);
            effect.Parameters["xTexture"].SetValue(texture);
            
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphics.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 2, VertexPositionTexture.VertexDeclaration);
            }

            
            
        }
    }
}
