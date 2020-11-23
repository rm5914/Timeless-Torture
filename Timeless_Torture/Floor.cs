using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace Timeless_Torture
{
    class Floor
    {
        // All fields
        Texture2D floorTexture;
        List<Item> items;
        Rectangle[,] floorTiles;
        String[,] floor;
        List<ItemPosition> itemPositions;

        // Constructor
        // width and height are the width and height of one block
        public Floor(Texture2D floorTexture, String levelText, int width, int height)
        {
            this.floorTexture = floorTexture;
            items = new List<Item>();
            floorTiles = new Rectangle[20, 20];
            floor = new String[20, 20];
            itemPositions = new List<ItemPosition>();

            // Reeading in the maps and their data
            StreamReader sr = new StreamReader(levelText);

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    floor[i, j] = sr.ReadLine();
                }
            }
            sr.Close();

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (floor[i, j] == "SkyBlue")
                    {
                        itemPositions.Add(new ItemPosition(new Vector2(j * width, i * height)));
                    }
                    floorTiles[i, j] = new Rectangle(j * width, i * height, width, height);
                }
            }
        }

        // Start of constructors

        public Texture2D Texture
        {
            get
            {
                return floorTexture;
            }
        }

        public List<Item> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
            }
        }

        public Rectangle[,] FloorTiles
        {
            get
            {
                return floorTiles;
            }
        }

        public String[,] FloorData
        {
            get
            {
                return floor;
            }
        }

        public List<ItemPosition> ItemPositions
        {
            get
            {
                return itemPositions;
            }
        }

        // End of constructors
    }
}
