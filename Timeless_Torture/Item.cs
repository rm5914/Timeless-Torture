using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// Items the player will pick up

namespace Timeless_Torture
{
    class Item
    {
        bool isPickedUp;
        bool isBurned;
        bool playerClose;
        Rectangle rectangle;
        Texture2D texture;
        Texture2D closeTexture;
        Color color;

        // Constructor
        public Item(Rectangle rectangle, Texture2D texture, Texture2D closeTexture, Color color)
        {
            this.rectangle = rectangle;
            this.texture = texture;
            this.closeTexture = closeTexture;
            this.color = color;
            isPickedUp = false;
            isBurned = false;
            playerClose = false;
        }

        /// <summary>
        /// Checks to see if the player is close
        /// </summary>
        /// <param name="player"> The position of the player </param>
        public void IsPlayerClose(Rectangle player)
        {
            if ((((player.X + player.Width / 2) + 100 > (rectangle.X + rectangle.Width / 2) && (player.X + player.Width / 2) - 100 < (rectangle.X + rectangle.Width / 2)) && (player.Y + player.Height / 2) + 110 > (rectangle.Y + rectangle.Height / 2) && (player.Y + player.Height / 2) - 110 < (rectangle.Y + rectangle.Height / 2)))
            {
                playerClose = true;
            }
            else
            {
                playerClose = false;
            }
        }

        /// <summary>
        /// Picks up the item
        /// </summary>
        public bool PickUp()
        {
            if (playerClose)
            {
                isPickedUp = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Burns the item
        /// </summary>
        public void Burn()
        {
            isPickedUp = false;
            isBurned = true;
        }

        /// <summary>
        /// Draws the item if it isn't picked up or burned
        /// </summary>
        /// <param name="spriteBatch"> The SpriteBatch that  </param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isPickedUp && !isBurned)
            {
                if (playerClose)
                {
                    // The Color.DarkRed is temporary, once we get the second sprite the color will just be color and the texture will change
                    spriteBatch.Draw(closeTexture, rectangle, color);
                }
                else
                {
                    spriteBatch.Draw(texture, rectangle, color);
                }
            }
            
        }
    }
}
