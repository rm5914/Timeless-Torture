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

        public bool IsPickedUp
        {
            get
            {
                return isPickedUp;
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
                    spriteBatch.Draw(closeTexture, rectangle, color);
                }
                else
                {
                    spriteBatch.Draw(texture, rectangle, color);
                }
            }
        }

        /// <summary>
        /// Draws an item in the inventory
        /// </summary>
        /// <param name="spriteBatch"> The spritebatch to draw the item </param>
        /// <param name="position"> The position of the item in the inventory </param>
        /// <param name="x"> The x position of the player </param>
        /// <param name="y"> The y position of the player </param>
        public void DrawInventory(SpriteBatch spriteBatch, int position, int x, int y)
        {
            spriteBatch.Draw(closeTexture, new Rectangle(x - 160 + position * 20 , y + 195, 20, 20), color);
        }
    }
}
