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

        private float zoom;
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

        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
            zoom = 1.0f;
        }

        public void Move(Rectangle center)
        {
            var position = Matrix.CreateTranslation(
                -center.X - (center.Width / 2),
                -center.Y - (center.Height / 2),
                0);
            var offset = Matrix.CreateTranslation(
                Game1.ScreenWidth / 2,
                Game1.ScreenHeight / 2,
                0);
                                                
            transform = position * center;
        }
    }
}
