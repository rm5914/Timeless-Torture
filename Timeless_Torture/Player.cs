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
        private List<Item> inventory;
        private int inventoryLimit;
        private int playerMovementX;
        private int playerMovementY;

        // Constructor
        public Player(Texture2D texture, Rectangle pos, int inventoryLimit)
        {
            position = pos;
            playerTexture = texture;
            inventory = new List<Item>();
            this.inventoryLimit = inventoryLimit;
            playerMovementX = 5;
            playerMovementY = 5;
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

        public List<Item> Inventory
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

        /// <summary>
        /// Makes the player move, should be called in Update
        /// </summary>
        public void MovePlayer(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.W))
            {
                position.Y -= playerMovementY;
                int upSide = playerTexture.Height - 250;
                if (position.Y < upSide)
                {
                    position.Y = upSide;
                }
            }

            if (keyState.IsKeyDown(Keys.A))
            {
                position.X -= playerMovementX;
                int leftSide = playerTexture.Width - 270;
                if (position.X < leftSide)
                {
                    position.X = leftSide;
                }
            }

            if (keyState.IsKeyDown(Keys.S))
            {
                position.Y += playerMovementY;
                int downSide = 1030 - playerTexture.Height;
                if (position.Y > downSide)
                {
                    position.Y = downSide;
                }
            }

            if (keyState.IsKeyDown(Keys.D))
            {
                position.X += playerMovementX;
                int rightSide = 1330 - playerTexture.Width;
                if (position.X > rightSide)
                {
                    position.X = rightSide;
                }
            }
        }

        /// <summary>
        /// Adds the given item to the players inventory
        /// </summary>
        /// <param name="item"> The item to be added </param>
        public void AddItem(Item item)
        {
            if (inventory.Count < inventoryLimit)
            {
                inventory.Add(item);
            }
        }

        /// <summary>
        /// Removes the first item in the inventory
        /// </summary>
        public void Remove()
        {
            inventory.RemoveAt(0);
        }

        /// <summary>
        /// Draws the player to the screen
        /// </summary>
        /// <param name="spriteBatch"> The spritebatch used to draw the player </param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, position, Color.White);
        }
        
    }
}
