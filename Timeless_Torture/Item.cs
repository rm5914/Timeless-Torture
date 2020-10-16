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
        Rectangle rectangle;
        Texture2D texture;
        Color color;

        // Constructor
        public Item(Rectangle rectangle, Texture2D texture, Color color)
        {
            this.rectangle = rectangle;
            this.texture = texture;
            this.color = color;
            isPickedUp = false;
            isBurned = false;
        }

        /// <summary>
        /// Picks up the item
        /// </summary>
        public void PickUp()
        {
            isPickedUp = true;
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
                spriteBatch.Draw(texture, rectangle, Color.White);
            }
        }
    }
}
