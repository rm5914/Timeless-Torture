using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Timeless_Torture
{
    class Player
    {
        // FIELDS
        private Rectangle position;
        private Texture2D playerTexture;
        private Item[] inventory;
        private int inventoryLimit;
        private int playerMovementX;
        private int playerMovementY;
        private bool blockedLeft;
        private bool blockedRight;
        private bool blockedUp;
        private bool blockedDown;

        // Constructor
        public Player(Texture2D texture, Rectangle pos, int inventoryLimit)
        {
            position = pos;
            playerTexture = texture;
            inventory = new Item[inventoryLimit];
            this.inventoryLimit = inventoryLimit;
            playerMovementX = 5;
            playerMovementY = 5;
            blockedLeft = false;
            blockedRight = false;
            blockedUp = false;
            blockedDown = false;
        }

        // Properties 
        public Rectangle Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public int X
        {
            get
            {
                return position.X;
            }
            set
            {
                position.X = value;
            }
        }

        public int Y
        {
            get
            {
                return position.Y;
            }
            set
            {
                position.Y = value;
            }
        }

        public Item[] Inventory
        {
            get
            {
                return inventory;
            }
        }

        public int Limit
        {
            get
            {
                return inventoryLimit;
            }
        }

        public int XMovement
        {
            get
            {
                return playerMovementX;
            }
            set
            {
                playerMovementX = value;
            }
        }

        public int YMovement
        {
            get
            {
                return playerMovementY;
            }
            set
            {
                playerMovementY = value;
            }
        }

        public int InventoryLimit
        {
            get
            {
                return inventoryLimit;
            }
            set
            {
                inventoryLimit = value;
            }
        }

        // End of properties

        //methods
        /// <summary>
        /// Makes the player move, should be called in Update
        /// </summary>
        public void MovePlayer(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.W) && !blockedUp)
            {
                position.Y -= playerMovementY;
            }
            if (keyState.IsKeyDown(Keys.A) && !blockedLeft)
            {
                position.X -= playerMovementX;
            }
            if (keyState.IsKeyDown(Keys.S) && !blockedDown)
            {
                position.Y += playerMovementY;
            }
            if (keyState.IsKeyDown(Keys.D) && !blockedRight)
            {
                position.X += playerMovementX;
            }

            blockedLeft = false;
            blockedRight = false;
            blockedUp = false;
            blockedDown = false;
        }

        /// <summary>
        /// detects if player is touching an object
        /// </summary>
        /// <param name="rectangle">the rectangle to be checked if the player is close</param>
        /// <returns>true if its touching or within a certain range of an object</returns>
        public void PlayerCollision(Rectangle rectangle)
        {
            if (position.X <= rectangle.X + rectangle.Width && position.X > rectangle.X)
            {
                blockedLeft = true;
            }
            if (position.X + position.Width >= rectangle.X && position.X < rectangle.X + rectangle.Width)
            {
                blockedRight = true;
            }
            if (position.Y <= rectangle.Y + rectangle.Height && position.Y > rectangle.Y)
            {
                blockedUp = true;
            }
            if (position.Y + position.Height >= rectangle.Y && position.Y < rectangle.Y + rectangle.Height)
            {
                blockedDown = true;
            }
        }

        /// <summary>
        /// Adds the given item to the players inventory
        /// </summary>
        /// <param name="item"> The item to be added </param>
        public void AddItem(Item item)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                {
                    inventory[i] = item;
                    return;
                }
            }
        }

        /// <summary>
        /// Removes the first item in the inventory
        /// </summary>
        public void Remove()
        {
            inventory[0] = null;
            for (int i = 1; i < inventory.Length; i++)
            {
                inventory[i - 1] = inventory[i];
            }
        }

        /// <summary>
        /// Draws the player to the screen
        /// </summary>
        /// <param name="spriteBatch"> The spritebatch used to draw the player </param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, position, Color.White);
        }
        
        /// <summary>
        /// Resets the players inventory so it holds 0 items
        /// </summary>
        public void ResetInventory()
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                inventory[i] = null;
            }
        }
    }
}
