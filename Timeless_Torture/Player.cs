using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Timeless_Torture
{
    class Player
    {
        // FIELDS
        private Rectangle position;
        private Texture2D playerTexture;

        public Player(Texture2D texture, Rectangle pos)
        {
            position = pos;
            playerTexture = texture;
        }

        /// <summary>
        /// Makes the player move, should be called in Update
        /// </summary>
        public void MovePlayer(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.W))
            {
                position.Y -= 5;
            }

            if (keyState.IsKeyDown(Keys.S))
            {
                position.Y += 5;
            }

            if (keyState.IsKeyDown(Keys.A))
            {
                position.X -= 5;
            }

            if (keyState.IsKeyDown(Keys.D))
            {
                position.X += 5;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, position, Color.White);
        }
        
    }
}
