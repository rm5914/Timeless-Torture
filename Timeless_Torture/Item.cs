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
        private bool isPickedUp;
        private bool isBurned;
        private bool playerClose;
        private Rectangle rectangle;
        private Rectangle inventoryRectangle;
        private Texture2D texture;
        private Texture2D closeTexture;
        private Color color;
        

        // Constructor
        public Item(Rectangle rectangle, Rectangle inventoryRectangle, Texture2D texture, Texture2D closeTexture, Color color)
        {
            this.rectangle = rectangle;
            this.inventoryRectangle = inventoryRectangle;
            this.texture = texture;
            this.closeTexture = closeTexture;
            this.color = color;
            isPickedUp = false;
            isBurned = false;
            playerClose = false;
        }

        // Start of Properties

        public Rectangle Position
        {
            get
            {
                return rectangle;
            }
        }

        public Rectangle InventoryPosition
        {
            get
            {
                return inventoryRectangle;
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

        // End of Properties

        //methods
        /// <summary>
        /// Picks up the item
        /// </summary>
        public bool PickUp()
        {
            if (playerClose && !isBurned && !isPickedUp)
            {
                Console.WriteLine("it ran");
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
            //display items - bottom left corner
            else if (isPickedUp && !isBurned)
            {
                spriteBatch.Draw(texture, inventoryRectangle, color);
            }
        }
    }
}
