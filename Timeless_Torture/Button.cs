using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// Holds data for the button and methods

namespace Timeless_Torture
{
    class Button
    {
        private Rectangle position;
        private Texture2D texture;
        private String text;
        private SpriteFont font;
        private Color buttonColor;
        private Color pressedButtonColor;
        private Color textColor;
        private Color pressedTextColor;
        private Vector2 textPosition;
        private bool isPressed;

        // Constructor
        public Button (Rectangle position, Texture2D texture, String text, SpriteFont font, Color buttonColor,  Color textColor, Color pressedButtonColor, Color pressedTextColor, Vector2 textPosition)
        {
            this.position = position;
            this.texture = texture;
            this.text = text;
            this.font = font;
            this.buttonColor = buttonColor;
            this.pressedButtonColor = pressedButtonColor;
            this.textColor = textColor;
            this.pressedTextColor = pressedTextColor;
            this.textPosition = textPosition;
            isPressed = false;
        }

        // Start of properties
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

        public Vector2 TextPosition
        {
            get
            {
                return textPosition;
            }
            set
            {
                textPosition = value;
            }
        }

        public bool IsPressed
        {
            get
            {
                return isPressed;
            }
            set
            {
                isPressed = value;
            }
        }

        // End of properties
       
        //methods
        /// <summary>
        /// Checks if the mouse has clicked (left-clicked) the button
        /// </summary>
        /// <param name="mouseState"> The current mouse state </param>
        /// <param name="previousMouseState"> The previouse mouse state </param>
        /// <returns> True if the button was clicked, false otherwise </returns>
        public bool MouseClick(MouseState mouseState, MouseState previousMouseState)
        {
            if ((mouseState.X >= position.X && mouseState.X <= position.X + position.Width) && (mouseState.Y > position.Y && mouseState.Y < position.Y + position.Height)
                            && mouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the mouse is currently pressed on the button
        /// </summary>
        /// <param name="mouseState"> The current state of the mouse </param>
        /// <returns> True if the button is being pressed </returns>
        public bool IsMouseDown(MouseState mouseState)
        {
            if ((mouseState.X >= position.X && mouseState.X <= position.X + position.Width) && (mouseState.Y > position.Y && mouseState.Y < position.Y + position.Height)
                            && mouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the button is pressed
        /// <param name="mouseState"> The current state of the mouse </param>
        /// </summary>
        public void PressButton(MouseState mouseState)
        {
            if (IsMouseDown(mouseState))
            {
                isPressed = true;
            }
            else
            {
                isPressed = false;
            }
        }

        /// <summary>
        /// Sees if the selected associated key is being pressed and presses the button if it is
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="keyState"></param>
        /// <param name="key"></param>
        public void KeyboardPressButton(KeyboardState keyState, Keys key)
        {
            if (keyState.IsKeyDown(key))
            {
                isPressed = true;
            }
            else
            {
                isPressed = false;
            }
        }

        /// <summary>
        /// Draws the button
        /// </summary>
        /// <param name="spriteBatch"> The spritebatch where the button will be drawn </param>
        public void Draw (SpriteBatch spriteBatch)
        {
            if (isPressed)
            {
                spriteBatch.Draw(texture, position, pressedButtonColor);
                spriteBatch.DrawString(font, text, textPosition, pressedTextColor);
            }
            else
            {
                spriteBatch.Draw(texture, position, buttonColor);
                spriteBatch.DrawString(font, text, textPosition, textColor);
            }
        }
    }
}
