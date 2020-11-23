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
        private Rectangle bounds;
        
        public Camera(Viewport viewport)
        {
            zoom = 2f;
            bounds = viewport.Bounds;
        }

        public void Move(Player player)
        {

            var position = Matrix.CreateTranslation(new Vector3(-player.Position.X, -player.Position.Y, 0));

            var offset = Matrix.CreateTranslation(new Vector3(bounds.Width /4, bounds.Height /4, 0));

            transform = (position * offset * Matrix.CreateScale(zoom));
        }
    }
}
