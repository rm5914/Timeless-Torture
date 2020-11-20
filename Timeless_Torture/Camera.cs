using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Timeless_Torture
{
    class Camera
    { 
        public Matrix transform;
        public Rectangle position;

        private float zoom;
        private float rotation;
        private Vector2 centre;
        private Viewport viewport;

        public float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
                if (zoom < 0.1f)
                {
                    zoom = 0.1f;
                }
            }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public int X
        {
            get { return position.X; }
            set
            { 
                position.X = value; 
            }
        }

        public int Y
        {
            get { return position.Y; }
            set
            {
                position.Y = value;
            }
        }

        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
            zoom = 1.0f;
            rotation = 0.0f;
        }

        public void Move(Rectangle center)
        {
            position = center;
            transform = Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0)) *
                                                Matrix.CreateRotationZ(Rotation) *
                                                Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                                Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0));
        }
    }
}
