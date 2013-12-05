using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BringelandoGameEngine.Core
{
    public class Camera2D
    {
        protected float _zoom; // Camera Zoom
        public Matrix _transform; // Matrix Transform
        public Vector2 _pos; // Camera Position
        protected float _rotation; // Camera Rotation

        public Camera2D()
        {
            _zoom = 1.0f;
            _rotation = 0.0f;
            _pos = Vector2.Zero;
        }

        // Sets and gets zoom
        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; if (_zoom < 0.1f) _zoom = 0.1f; } // Negative zoom will flip image
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        // Auxiliary function to move the camera
        public void Move(Vector2 amount)
        {
            _pos += amount;
        }

        public void Constraint(Vector2 min, Vector2 max)
        {
            //_pos.X = MathHelper.Clamp(_pos.X, min.X, max.X);
            //_pos.Y = MathHelper.Clamp(_pos.Y, min.Y, max.Y);
            
        }

        // Get set position
        public Vector2 Pos
        {
            get
            {
                return new Vector2((int)_pos.X, (int)_pos.Y);
            }
            set { _pos = value; }
        }

        //public Matrix get_transformation(GraphicsDevice graphicsDevice)
        public Matrix get_transformation(GraphicsDeviceManager graphicsDevice)
        {
            _transform =       
              Matrix.Identity * Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(
                                             graphicsDevice.PreferredBackBufferWidth * 0.5f,
                                             graphicsDevice.PreferredBackBufferHeight * 0.5f,
                                             0));
            return _transform;
        }


        //?
        public Vector2 get_mouse_vpos()
        {
            Vector2 mp = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            return (mp + this._pos); // Camera is a member of the class in this example
            //return (mp  + _camera.pos);
        }

        //??
        public Vector2 get_mouse_vpos2()
        {
            Vector2 mp = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            return ((mp * this._zoom) + this._pos); // Camera is a member of the class in this example
            //return ((mp * _camera.Zoom) + _camera.pos);
        }



    }
}