using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// The place where the player burns the items

namespace Timeless_Torture
{
    class Fireplace
    {
        private int burnedItems;
        private Texture2D texture;
        private Texture2D glowTexture;
        private Rectangle rectangle;
        private Color color;

        // Constructor
        public Fireplace (Texture2D texture, Texture2D glowTexture, Rectangle rectangle, Color color)
        {
            burnedItems = 0;
            this.texture = texture;
            this.glowTexture = glowTexture;
            this.rectangle = rectangle;
            this.color = color;
        }

        // Start of properties

        public int BurnedItems
        {
            get
            {
                return burnedItems;
            }
        }

        // End of  properties

        /// <summary>
        /// Burns the first item the player has
        /// </summary>
        /// <param name="player"> The item to be burned </param>
        public void BurnItem(Player player)
        {
            if (IsPlayerClose(player.Position))
            {
                player.Inventory[0].Burn();
                player.Remove();
                burnedItems++;
            }
        }

        /// <summary>
        /// Resets the number of burned items
        /// </summary>
        public void Reset()
        {
            burnedItems = 0;
        }

        /// <summary>
        /// Checks if the player is close to the fireplace
        /// </summary>
        /// <param name="player"></param>
        public bool IsPlayerClose(Rectangle player)
        {
            if ((((player.X + player.Width / 2) + 80 > (rectangle.X + rectangle.Width / 2) && (player.X + player.Width / 2) - 80 < (rectangle.X + rectangle.Width / 2)) && (player.Y + player.Height / 2) + 90 > (rectangle.Y + rectangle.Height / 2) && (player.Y + player.Height / 2) - 90 < (rectangle.Y + rectangle.Height / 2)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Draws the fireplace
        /// </summary>
        /// <param name="spriteBatch">  </param>
        public void Draw(SpriteBatch spriteBatch, Rectangle player)
        {
            if (IsPlayerClose(player))
            {
                spriteBatch.Draw(glowTexture, rectangle, color);
            }
            else
            {
                spriteBatch.Draw(texture, rectangle, color);
            }
        }
    }
}
