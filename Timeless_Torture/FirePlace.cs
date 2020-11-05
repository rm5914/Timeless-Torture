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
        private bool playerClose;
        private int burnedItems;
        private Texture2D texture;
        private Texture2D glowTexture;
        private Rectangle rectangle;
        private Color color;

        // Constructor
        public Fireplace (Texture2D texture, Texture2D glowTexture, Rectangle rectangle, Color color)
        {
            playerClose = false;
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

        public bool PlayerClose
        {
            get
            {
                return playerClose;
            }
            set
            {
                playerClose = value;
            }
        }

        public Rectangle Position
        {
            get
            {
                return rectangle;
            }
        }

        // End of  properties

        //methods
        /// <summary>
        /// Burns the first item the player has
        /// </summary>
        /// <param name="player"> The item to be burned </param>
        public void BurnItem(Player player)
        {
            if (playerClose)
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
        /// Draws the fireplace
        /// </summary>
        /// <param name="spriteBatch">  </param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (playerClose)
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
