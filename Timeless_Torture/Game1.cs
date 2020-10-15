using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.ComponentModel.Design;
using System.Threading;

namespace Timeless_Torture
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
     
    //enum GameState
    enum GameState { Menu, Options, Instructions, Game, Pause, GameOver };

    public class Game1 : Game
    {
        // FIELDS

        //enum as data type
        GameState gameState;
        GameState previousGameState;

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
        double timer;
        Player player;

        // Textures
        private Texture2D texture;
        private Texture2D button;
        private Texture2D title;
        private Texture2D pauseTitle;

        // positions
        private Vector2 position;
        private Vector2 titlePosition;

        // Rectangles
        private Rectangle startButton;
        private Rectangle instructionsButton;
        private Rectangle backButton;
        private Rectangle optionsButton;
        private Rectangle exitButton;
        private Rectangle pauseContinueButton;

        // CONSTRUCTOR
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        // CORE GAME METHODS

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            timer = 10;
            
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

            // All of the textures
            texture = Content.Load<Texture2D>("PlayerSprite");
            player = new Player(texture, position);

            button = Content.Load<Texture2D>("TT Buttons");
            title = Content.Load<Texture2D>("Title");
            pauseTitle = Content.Load<Texture2D>("Pause");

            // All positions
            position = new Vector2(0, 0);
            titlePosition = new Vector2(graphics.PreferredBackBufferWidth / 2 - 13 * title.Width / 25, graphics.PreferredBackBufferHeight / 5 - title.Height / 2);

            // All Rectangles
            startButton = new Rectangle(graphics.PreferredBackBufferWidth / 2 - 3 * button.Width / 2, 6 * graphics.PreferredBackBufferHeight / 10 - button.Height / 2, 3 * button.Width, button.Height / 2);
            instructionsButton = new Rectangle(graphics.PreferredBackBufferWidth / 2 - 3 * button.Width / 2, 7 * graphics.PreferredBackBufferHeight / 10 - button.Height / 2, 3 * button.Width, button.Height / 2);
            backButton = new Rectangle(graphics.PreferredBackBufferWidth / 2 - 3 * button.Width / 2, 9 * graphics.PreferredBackBufferHeight / 10 - button.Height / 2, 3 * button.Width, button.Height / 2);
            optionsButton = new Rectangle(graphics.PreferredBackBufferWidth / 2 - 3 * button.Width / 2, 8 * graphics.PreferredBackBufferHeight / 10 - button.Height / 2, 3 * button.Width, button.Height / 2);
            exitButton = new Rectangle(graphics.PreferredBackBufferWidth / 2 - 3 * button.Width / 2, 9 * graphics.PreferredBackBufferHeight / 10 - button.Height / 2, 3 * button.Width, button.Height / 2);
            pauseContinueButton = new Rectangle(graphics.PreferredBackBufferWidth / 2 - 3 * button.Width / 2, 6 * graphics.PreferredBackBufferHeight / 10 - button.Height / 2, 3 * button.Width, button.Height / 2);

            //load sprite font
            mainFont = Content.Load<SpriteFont>("mainFont");
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
                            previousGameState = gameState;
                            gameState = GameState.Game;
                        }

                        // checking if they click the instructions button
                        if (MouseClick(instructionsButton))
                        {
                            previousGameState = gameState;
                            gameState = GameState.Instructions;
                        }

                        // Checking if they want to edit the options of the game
                        if (MouseClick(optionsButton))
                        {
                            previousGameState = gameState;
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
                        if (MouseClick(backButton) && previousGameState == GameState.Menu)
                        {
                            previousGameState = gameState;
                            gameState = GameState.Menu;
                        }
                        else if (SingleKeyPress(Keys.Back) && previousGameState == GameState.Menu)
                        {
                            previousGameState = gameState;
                            gameState = GameState.Menu;
                        }
                        else if (MouseClick(backButton) && previousGameState == GameState.Pause)
                        {
                            previousGameState = gameState;
                            gameState = GameState.Pause;
                        }
                        else if (SingleKeyPress(Keys.Back) && previousGameState == GameState.Pause)
                        {
                            previousGameState = gameState;
                            gameState = GameState.Pause;
                        }
                        break;
                    }

                case GameState.Options:
                    {
                        //checking if they click the back button
                        if (MouseClick(backButton) && previousGameState == GameState.Menu)
                        {
                            previousGameState = gameState;
                            gameState = GameState.Menu;
                        }
                        else if (SingleKeyPress(Keys.Back) && previousGameState == GameState.Menu)
                        {
                            previousGameState = gameState;
                            gameState = GameState.Menu;
                        }
                        else if (MouseClick(backButton) && previousGameState == GameState.Pause)
                        {
                            previousGameState = gameState;
                            gameState = GameState.Pause;
                        }
                        else if (SingleKeyPress(Keys.Back) && previousGameState == GameState.Pause)
                        {
                            previousGameState = gameState;
                            gameState = GameState.Pause;
                        }
                        break;
                    }

                case GameState.Game:
                    {
                        //Starting the timer
                        timer = timer - gameTime.ElapsedGameTime.TotalSeconds;

                        if (timer <= 0)
                        {
                            previousGameState = gameState;
                            gameState = GameState.GameOver;
                        }

                        if (SingleKeyPress(Keys.Escape))
                        {
                            previousGameState = gameState;
                            gameState = GameState.Pause;
                        }
                        player.MovePlayer(keyState);
                        break;
                    }

                case GameState.Pause:
                    {
                        
                        // Checking if they click the continue button
                        if (MouseClick(pauseContinueButton))
                        {
                            previousGameState = gameState;
                            gameState = GameState.Game;
                        }
                        

                        // checking if they click the instructions button
                        if (MouseClick(instructionsButton))
                        {
                            previousGameState = gameState;
                            gameState = GameState.Instructions;
                        }

                        // Checking if they want to edit the options of the game
                        if (MouseClick(optionsButton))
                        {
                            previousGameState = gameState;
                            gameState = GameState.Options;
                        }

                        // Checking if they want to exit to the menu
                        if (MouseClick(exitButton))
                        {
                            previousGameState = gameState;
                            gameState = GameState.Menu;
                        }
                        break;
                    }

                case GameState.GameOver:
                    {
                        if(SingleKeyPress(Keys.Enter))
                        {
                            previousGameState = gameState;
                            gameState = GameState.Menu;
                        }

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
                        PressButton(backButton, Color.MediumAquamarine, Color.DarkTurquoise, Color.RoyalBlue, Color.DarkGreen, new Vector2(backButton.X + 21 * backButton.Width / 50, backButton.Y + backButton.Height / 4), "BACK");
                        spriteBatch.DrawString(mainFont, "WASD for character movement", new Vector2(400, 300), Color.DarkTurquoise);

                        // Making the buttons to display wasd and changes color when their corresponding button is pressed
                        if (keyState.IsKeyDown(Keys.W))
                        {
                            spriteBatch.Draw(button, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 25, graphics.PreferredBackBufferHeight / 2 - 25, 50, 50), Color.RoyalBlue);
                            spriteBatch.DrawString(mainFont, "w", new Vector2(), Color.White);
                        }
                        else
                        {
                            spriteBatch.Draw(button, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 25, graphics.PreferredBackBufferHeight / 2 - 25, 50, 50), Color.MediumAquamarine);
                        }

                        if (keyState.IsKeyDown(Keys.S))
                        {
                            spriteBatch.Draw(button, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 25, graphics.PreferredBackBufferHeight / 2 + 30, 50, 50), Color.RoyalBlue);
                        }
                        else
                        {
                            spriteBatch.Draw(button, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 25, graphics.PreferredBackBufferHeight / 2 + 30, 50, 50), Color.MediumAquamarine);
                        }

                        if (keyState.IsKeyDown(Keys.D))
                        {
                            spriteBatch.Draw(button, new Rectangle(graphics.PreferredBackBufferWidth / 2 + 30, graphics.PreferredBackBufferHeight / 2 + 30, 50, 50), Color.RoyalBlue);
                        }
                        else
                        {
                            spriteBatch.Draw(button, new Rectangle(graphics.PreferredBackBufferWidth / 2 + 30, graphics.PreferredBackBufferHeight / 2 + 30, 50, 50), Color.MediumAquamarine);
                        }

                        if (keyState.IsKeyDown(Keys.A))
                        {
                            spriteBatch.Draw(button, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 80, graphics.PreferredBackBufferHeight / 2 + 30, 50, 50), Color.RoyalBlue);
                        }
                        else
                        {
                            spriteBatch.Draw(button, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 80, graphics.PreferredBackBufferHeight / 2 + 30, 50, 50), Color.MediumAquamarine);
                        }
                    }
                    break;

                case GameState.Options:
                    {
                        // Back Button
                        PressButton(backButton, Color.MediumAquamarine, Color.DarkTurquoise, Color.RoyalBlue, Color.DarkGreen, new Vector2(backButton.X + 21 * backButton.Width / 50, backButton.Y + backButton.Height / 4), "BACK");

                        break;
                    }

                case GameState.Game:
                    {
                        //Displaying the timer
                        string time = string.Format("{0:0.00}", timer);
                        spriteBatch.DrawString(mainFont, time, new Vector2(GraphicsDevice.Viewport.Width / 2, 0), Color.Black);
                        
                        spriteBatch.Draw(texture, position, Color.White);
                        player.Draw(spriteBatch);
                        break;
                    }
                case GameState.Pause:
                    {
                        // Making the background
                        spriteBatch.Draw(button, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.RoyalBlue); // BlueViolet, DarkMagenta, MediumPurple, CadetBlue, DodgerBlue

                        // Pause titel
                        spriteBatch.Draw(pauseTitle, titlePosition, Color.White);

                        // Continue Button
                        PressButton(pauseContinueButton, Color.Blue, Color.DarkGoldenrod, Color.Black, Color.DarkGreen, new Vector2(startButton.X + 33 * startButton.Width / 100, startButton.Y + startButton.Height / 4), "CONTINUE");

                        // Istructions Button
                        PressButton(instructionsButton,  Color.Blue, Color.DarkGoldenrod, Color.Black, Color.DarkGreen, new Vector2(instructionsButton.X + 1 * instructionsButton.Width / 4, instructionsButton.Y + instructionsButton.Height / 4), "INSTRUCTIONS");

                        // Options Button
                        PressButton(optionsButton,  Color.Blue, Color.DarkGoldenrod, Color.Black, Color.DarkGreen, new Vector2(optionsButton.X + 35 * optionsButton.Width / 100, optionsButton.Y + optionsButton.Height / 4), "OPTIONS");

                        // Exit Button
                        PressButton(exitButton, Color.Blue, Color.DarkGoldenrod, Color.Black, Color.DarkGreen, new Vector2(exitButton.X + 21 * exitButton.Width / 50, exitButton.Y + exitButton.Height / 4), "EXIT");

                        break;
                    }

                case GameState.GameOver:
                    {
                        spriteBatch.DrawString(mainFont, "Game Over, Press enter to continue", new Vector2(GraphicsDevice.Viewport.Width / 2, 0), Color.Black);
                        break;
                    }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        // HELPER METHODS

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
