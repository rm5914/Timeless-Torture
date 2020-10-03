using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Timeless_Torture
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
     
    //enum GameState
    enum GameState { Menu, Options, Instructions, Game };

    public class Game1 : Game
    {
        //enum as data type
        GameState gameState;

        //keyboard state
        KeyboardState keyState;
        KeyboardState previousKeyState;

        //mouse state
        MouseState mouseState;
        MouseState previousMouseState;

        //sprite font
        SpriteFont mainFont;

        // Basic game stuff
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Textures
        private Texture2D texture;
        private Texture2D button;
        private Texture2D title;

        // positions and rectangles
        private Vector2 position;
        private Vector2 titlePosition;
        private Rectangle startButton;
        private Rectangle instructionsButton;
        private Rectangle instructionsBackButton;
        private Rectangle optionsButton;
        private Rectangle exitButton;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // Making the initial Game State the menu
            gameState = GameState.Menu;

            // Changing screen size
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();

            // Making mouse visible
            this.IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // All of the textures
            texture = Content.Load<Texture2D>("Player1");
            button = Content.Load<Texture2D>("TT Buttons");
            title = Content.Load<Texture2D>("Title");

            // All positions
            position = new Vector2(0, 0);
            titlePosition = new Vector2(graphics.PreferredBackBufferWidth / 2 - 13 * title.Width / 25, graphics.PreferredBackBufferHeight / 5 - title.Height / 2);

            // All Rectangles
            startButton = new Rectangle(graphics.PreferredBackBufferWidth / 2 - 3 * button.Width / 2, 6 * graphics.PreferredBackBufferHeight / 10 - button.Height / 2, 3 * button.Width, button.Height / 2);
            instructionsButton = new Rectangle(graphics.PreferredBackBufferWidth / 2 - 3 * button.Width / 2, 7 * graphics.PreferredBackBufferHeight / 10 - button.Height / 2, 3 * button.Width, button.Height / 2);
            instructionsBackButton = new Rectangle(graphics.PreferredBackBufferWidth / 2 - 3 * button.Width / 2, 9 * graphics.PreferredBackBufferHeight / 10 - button.Height / 2, 3 * button.Width, button.Height / 2);
            optionsButton = new Rectangle(graphics.PreferredBackBufferWidth / 2 - 3 * button.Width / 2, 8 * graphics.PreferredBackBufferHeight / 10 - button.Height / 2, 3 * button.Width, button.Height / 2);
            exitButton = new Rectangle(graphics.PreferredBackBufferWidth / 2 - 3 * button.Width / 2, 9 * graphics.PreferredBackBufferHeight / 10 - button.Height / 2, 3 * button.Width, button.Height / 2);

            //load sprite font
            mainFont = Content.Load<SpriteFont>("mainFont");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //see if buttons are pressed
            previousKeyState = keyState;
            keyState = Keyboard.GetState();

            previousMouseState = mouseState;
            mouseState = Mouse.GetState();

            // Checking the current game State
            switch (gameState)
            {
                case GameState.Menu:
                    {
                        // Checking if they click the start button
                        if (MouseClick(startButton))
                        {
                            gameState = GameState.Game;
                        }

                        // checking if they click the instructions button
                        if (MouseClick(instructionsButton))
                        {
                            gameState = GameState.Instructions;
                        }

                        // Checking if they want to edit the options of the game
                        if (MouseClick(optionsButton))
                        {
                            gameState = GameState.Options;
                        }

                        // Checking if they want to exit the game
                        if (MouseClick(exitButton))
                        {
                            this.Exit();
                        }
                        break;
                    }

                case GameState.Instructions:
                    {
                        //checking if they click the back button
                        if (MouseClick(instructionsBackButton))
                        {
                            gameState = GameState.Menu;
                        }
                        else if (SingleKeyPress(Keys.Back))
                        {
                            gameState = GameState.Menu;
                        }
                        break;
                    }

                case GameState.Options:
                    {
                        if (SingleKeyPress(Keys.Back))
                        {
                            gameState = GameState.Menu;
                        }
                        break;
                    }

                case GameState.Game:
                    {
                        MovePlayer();
                        break;
                    }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            switch (gameState)
            {
                case GameState.Menu:
                    {
                        //Changing Background Color
                        GraphicsDevice.Clear(Color.Black);

                        // Title
                        spriteBatch.Draw(title, titlePosition, Color.White);

                        // Start Button
                        PressButton(startButton, Color.MediumAquamarine, Color.DarkTurquoise, Color.RoyalBlue, Color.DarkGreen, new Vector2(startButton.X + 10 * startButton.Width / 25, startButton.Y + startButton.Height / 4), "START");

                        // Istructions Button
                        PressButton(instructionsButton, Color.MediumAquamarine, Color.DarkTurquoise, Color.RoyalBlue, Color.DarkGreen, new Vector2(instructionsButton.X + 1 * instructionsButton.Width / 4, instructionsButton.Y + instructionsButton.Height / 4), "INSTRUCTIONS");

                        // Options Button
                        PressButton(optionsButton, Color.MediumAquamarine, Color.DarkTurquoise, Color.RoyalBlue, Color.DarkGreen, new Vector2(optionsButton.X + 35 * optionsButton.Width / 100, optionsButton.Y + optionsButton.Height / 4), "OPTIONS");

                        // Exit Button
                        PressButton(exitButton, Color.MediumAquamarine, Color.DarkTurquoise, Color.RoyalBlue, Color.DarkGreen, new Vector2(exitButton.X + 21 * exitButton.Width / 50, exitButton.Y + exitButton.Height / 4), "EXIT");

                        break;
                    }

                    //create instructions for game
                case GameState.Instructions:
                    {
                        //Changing Background Color
                        GraphicsDevice.Clear(Color.Black);

                        // Back Button
                        PressButton(instructionsBackButton, Color.MediumAquamarine, Color.DarkTurquoise, Color.RoyalBlue, Color.DarkGreen, new Vector2(instructionsBackButton.X + 21 * instructionsBackButton.Width / 50, instructionsBackButton.Y + instructionsBackButton.Height / 4), "BACK");

                        spriteBatch.DrawString(mainFont, "WASD for character movement", new Vector2(400, 300), Color.DarkTurquoise);
                        
                    }
                    break;

                case GameState.Options:
                    {
                        break;
                    }

                case GameState.Game:
                    {
                        spriteBatch.Draw(texture, position, Color.White);
                        break;
                    }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        // Start of Helper Methods

        /// <summary>
        /// Makes the player move, should be called in Update
        /// </summary>
        protected void MovePlayer()
        {
            if (keyState.IsKeyDown(Keys.W))
            {
                position.Y -= 5;
            }

            if (keyState.IsKeyDown(Keys.S))
            {
                position.Y += 5;
            }

            if (keyState.IsKeyDown(Keys.A))
            {
                position.X -= 5;
            }

            if (keyState.IsKeyDown(Keys.D))
            {
                position.X += 5;
            }
        }

        /// <summary>
        /// Determines if a key has been pressed once
        /// </summary>
        /// <param name="key"> The selected key to determine if it's been pressed </param>
        /// <returns> Returns true if a key was pressed once, returns false otherwise </returns>
        protected bool SingleKeyPress (Keys key)
        {
            if (previousKeyState.IsKeyDown(key) && keyState.IsKeyUp(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the mouse has clicked (left-clicked) a button
        /// </summary>
        /// <param name="rectButton"> The rectangle of the button calling the method </param>
        /// <returns> True if the button was clicked, false otherwise </returns>
        protected bool MouseClick(Rectangle rectButton)
        {
            if ((mouseState.X >= rectButton.X && mouseState.X <= rectButton.X + rectButton.Width) && (mouseState.Y > rectButton.Y && mouseState.Y < rectButton.Y + rectButton.Height)
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
        /// Checks if the mouse is currently pressed on a button
        /// </summary>
        /// <param name="rectButton"> The rectangle of the button calling the method </param>
        /// <returns> True if the button is being pressed </returns>
        protected bool IsMouseDown(Rectangle rectButton)
        {
            if ((mouseState.X >= rectButton.X && mouseState.X <= rectButton.X + rectButton.Width) && (mouseState.Y > rectButton.Y && mouseState.Y < rectButton.Y + rectButton.Height)
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
        /// If a button is pressed it will change it's color
        /// </summary>
        /// <param name="rect"> The rectangle of the button that is being pushed </param>
        /// <param name="initialButtonColor"> The color the button is without being pressed </param>
        /// <param name="initialtextColor"> The color the text is without being pressed </param>
        /// <param name="pressedButtonColor"> The color the button is when being pressed </param>
        /// <param name="pressedTextColor"> The color the text is when being pressed </param>
        /// <param name="vector"> The position of the button </param>
        /// <param name="text"> The text of the button </param>
        protected void PressButton (Rectangle rect, Color initialButtonColor, Color initialtextColor, Color pressedButtonColor, Color pressedTextColor, Vector2 vector, string text)
        {
            if (IsMouseDown(rect))
            {
                spriteBatch.Draw(button, rect, pressedButtonColor);
                spriteBatch.DrawString(mainFont, text, vector, pressedTextColor); 
            }
            else
            {
                spriteBatch.Draw(button, rect, initialButtonColor);
                spriteBatch.DrawString(mainFont, text, vector, initialtextColor);
            }
        }
    }
}
