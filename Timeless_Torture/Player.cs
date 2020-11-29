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
        private Texture2D front;
        private Texture2D back;
        private Texture2D right;
        private Texture2D left;
        private Texture2D currentTexture;
        private List<Item> inventory;
        private int inventoryLimit;
        private int playerMovementX;
        private int playerMovementY;
        private int frame;
        private int frameInterval;
        private bool blockedLeft;
        private bool blockedRight;
        private bool blockedUp;
        private bool blockedDown;
        private bool moved;
        private bool faceSwitched;

        // Constructor
        public Player(Texture2D playerFront, Texture2D playerBack, Texture2D playerRight, Texture2D playerLeft, Rectangle pos, int inventoryLimit)
        {
            // Setting variables
            position = pos;
            front = playerFront;
            back = playerBack;
            right = playerRight;
            left = playerLeft;

            // Starts the player facing down
            currentTexture = front;

            // Initializing inventory
            this.inventoryLimit = inventoryLimit;
            inventory = new List<Item>();

            // Initiliazing player movement and frame data
            playerMovementX = 2;
            playerMovementY = 2;
            frame = 1;
            frameInterval = 0;

            // Initializing booleans, all should be false
            blockedLeft = false;
            blockedRight = false;
            blockedUp = false;
            blockedDown = false;
            moved = false;
            faceSwitched = false;

            // Filling the inventory
            for (int i = 0; i < inventoryLimit; i++)
            {
                inventory.Add(null);
            }
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

                for (int i = 0; i < inventoryLimit; i++)
                {
                    inventory.Add(null);
                }
            }
        }
        // End of properties

        //methods
        /// <summary>
        /// Makes the player move, should be called in Update
        /// </summary>
        public void MovePlayer(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.S) && !blockedDown)
            {
                position.Y += playerMovementY;
                moved = true;
                if (!faceSwitched)
                {
                    faceSwitched = true;
                    currentTexture = front;
                }
            }
            if (keyState.IsKeyDown(Keys.W) && !blockedUp)
            {
                position.Y -= playerMovementY;
                moved = true;
                if (!faceSwitched)
                {
                    faceSwitched = true;
                    currentTexture = back;
                }
            }
            if (keyState.IsKeyDown(Keys.D) && !blockedRight)
            {
                position.X += playerMovementX;
                moved = true;
                if (!faceSwitched)
                {
                    faceSwitched = true;
                    currentTexture = right;
                }
            }
            if (keyState.IsKeyDown(Keys.A) && !blockedLeft)
            {
                position.X -= playerMovementX;
                moved = true;
                if (!faceSwitched)
                {
                    faceSwitched = true;
                    currentTexture = left;
                }
            }

            // Changing the frames
            if (moved)
            {
                frameInterval++;

                if (frameInterval % 8 == 0)
                {
                    frame++;

                    if (frame >= 5)
                    {
                        frame = 2;
                    }
                }

            }
            else
            {
                frame = 1;
            }

            blockedLeft = false;
            blockedRight = false;
            blockedUp = false;
            blockedDown = false;
            moved = false;
            faceSwitched = false;
        }

        /// <summary>
        /// detects if player is touching an object
        /// </summary>
        /// <param name="rectangle">the rectangle to be checked if the player is close</param>
        /// <returns>true if its touching or within a certain range of an object</returns>
        public void PlayerCollision(Rectangle rectangle)
        {
            // Checking the left side of the player
            if (((position.Y > rectangle.Y && position.Y < rectangle.Y + rectangle.Height) || (position.Y + position.Height > rectangle.Y && position.Y + position.Height < rectangle.Y + rectangle.Height)) && position.X < rectangle.X + rectangle.Width && position.X > rectangle.X)
            {
                if (!(position.Y + position.Height - 3 < rectangle.Y || position.Y + 3 > rectangle.Y + rectangle.Height))
                {
                    blockedLeft = true;
                }
            }

            // Checking the left side of the player
            if (((position.Y > rectangle.Y && position.Y < rectangle.Y + rectangle.Height) || (position.Y + position.Height > rectangle.Y && position.Y + position.Height < rectangle.Y + rectangle.Height)) && position.X + position.Width < rectangle.X + rectangle.Width && position.X + position.Width > rectangle.X)
            {
                if (!(position.Y + position.Height - 3 < rectangle.Y || position.Y + 3 > rectangle.Y + rectangle.Height))
                {
                    blockedRight = true;
                }
            }
            
            // Checking the up side of the player
            if (((position.X > rectangle.X && position.X < rectangle.X + rectangle.Width) || (position.X + position.Width > rectangle.X && position.X + position.Height < rectangle.X + rectangle.Width)) && position.Y < rectangle.Y + rectangle.Height && position.Y > rectangle.Y)
            {
                if (!(position.X + position.Width - 3 < rectangle.X || position.X + 3 > rectangle.X + rectangle.Width))
                {
                    blockedUp = true;
                }
            }
            
            // Checking the down side of the player
            if (((position.X > rectangle.X && position.X < rectangle.X + rectangle.Width) || (position.X + position.Width > rectangle.X && position.X + position.Height < rectangle.X + rectangle.Width)) && position.Y + position.Height < rectangle.Y + rectangle.Height && position.Y + position.Height > rectangle.Y)
            {
                if (!(position.X + position.Width - 3 < rectangle.X || position.X + 3 > rectangle.X + rectangle.Width))
                {
                    blockedDown = true;
                }
            }

        }

        /// <summary>
        /// Adds the given item to the players inventory
        /// </summary>
        /// <param name="item"> The item to be added </param>
        public void AddItem(Item item)
        {
            for (int i = 0; i < inventoryLimit; i++)
            {
                if (inventory[i] == null)
                {
                    inventory.RemoveAt(i);
                    inventory.Insert(i, item);
                    return;
                }
            }
        }

        /// <summary>
        /// Removes the first item in the inventory
        /// </summary>
        public void Remove()
        {
            inventory.RemoveAt(0);
            inventory.Add(null);
        }

        /// <summary>
        /// Draws the player to the screen
        /// </summary>
        /// <param name="spriteBatch"> The spritebatch used to draw the player </param>
        public void Draw(SpriteBatch spriteBatch)
        {
            switch (frame)
            {
                case 1:
                    spriteBatch.Draw(currentTexture, position, new Rectangle(0, 0, 256, 256), Color.White);
                    break;
                case 2:
                    spriteBatch.Draw(currentTexture, position, new Rectangle(256, 0, 256, 256), Color.White);
                    break;
                case 3:
                    spriteBatch.Draw(currentTexture, position, new Rectangle(0, 256, 256, 256), Color.White);
                    break;
                case 4:
                    spriteBatch.Draw(currentTexture, position, new Rectangle(256, 256, 256, 256), Color.White);
                    break;
                case 5:
                    spriteBatch.Draw(currentTexture, position, new Rectangle(0, 768, 256, 256), Color.White);
                    break;


            }
        }

        /// <summary>
        /// Draws the inventory of the player
        /// </summary>
        /// <param name="spriteBatch"> The spritebatch used to draw the inventory </param>
        /// <param name="spriteFont"> The font of the text for the inventory </param>
        public void DrawInventory(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString(spriteFont, "Inventory", new Vector2(position.X - 290, position.Y + 190), Color.White);

            int currentItem = 0;

            while (inventory[currentItem] != null)
            {
                inventory[currentItem].DrawInventory(spriteBatch, currentItem, position.X, position.Y);
                currentItem++;
            }
        }

        /// <summary>
        /// Resets the players inventory so it holds 0 items
        /// </summary>
        public void ResetInventory()
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                inventory[i] = null;
            }
        }

    }
}
