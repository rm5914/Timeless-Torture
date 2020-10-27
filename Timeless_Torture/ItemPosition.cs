using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// Manages one possible position for an item

namespace Timeless_Torture
{
    class ItemPosition
    {
        // Fields
        Vector2 position;
        bool isPicked;

        // Constructor
        public ItemPosition (Vector2 position)
        {
            this.position = position;
        }

        /// <summary>
        /// Checks to see if it hasn't been picked by an item
        /// If it hasn't been picked it will become picked
        /// </summary>
        /// <returns> The position if it hasn't been picked, returns a Vector(0,0) otherwise </returns>
        public Vector2 GetPosition ()
        {
            if (!isPicked)
            {
                isPicked = true;
                return position;
            }
            else
            {
                return new Vector2(0, 0);
            }
        }

        /// <summary>
        /// Makes it so the item is not picked
        /// </summary>
        public void Reset()
        {
            isPicked = false;
        }
    }
}
