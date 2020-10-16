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

        // The player and their position
        Player player;
        private Rectangle playerPosition;

        // Item positions
        Vector2[] itemPositions;

        // Random Number Generator
        Random numGenerator;

        // All of the items
        Item placeholder;

        // Textures
        private Texture2D texture;
        private Texture2D button;
        private Texture2D title;
        private Texture2D pauseTitle;
        private Texture2D genericItem;

        // Vectors for positions
        private Vector2 titlePosition;

        // Main menu buttons
        private Button startButton;
        private Button instructionsButton;
        private Button optionsButton;
        private Button exitButton;

        // Pause buttons
        private Button pauseContinueButton;
        private Button pauseInstructionsButton;
        private Button pauseOptionsButton;
        private Button pauseExitButton;

        // Instructions buttons
        private Button backButton;
        private Button instructionsWButton;
        private Button instructionsAButton;
        private Button instructionsSButton;
        private Button instructionsDButton;

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
            numGenerator = new Random();
            
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

            // Making the player 
            texture = Content.Load<Texture2D>("PlayerSprite");
            playerPosition = new Rectangle(0, 0, texture.Width / 2, texture.Height / 2);
            player = new Player(texture, playerPosition);

            // All of the textures
            button = Content.Load<Texture2D>("TT Buttons");
            title = Content.Load<Texture2D>("Title");
            pauseTitle = Content.Load<Texture2D>("Pause");
            genericItem = Content.Load<Texture2D>("ItemPlaceholder");

            //load sprite font
            mainFont = Content.Load<SpriteFont>("mainFont");

            // All positions
            titlePosition = new Vector2(graphics.PreferredBackBufferWidth / 2 - 13 * title.Width / 25, graphics.PreferredBackBufferHeight / 5 - title.Height / 2);

            // All of the item positions
            itemPositions = new Vector2[3];
            itemPositions[0] = new Vector2(50, 50);
            itemPositions[1] = new Vector2(300, 300);
            itemPositions[2] = new Vector2(550, 550);

            int position = numGenerator.Next(0, itemPositions.Length);
            placeholder = new Item(new Rectangle((int)itemPositions[position].X, (int)itemPositions[position].Y, genericItem.Width / 25, genericItem.Height / 25), genericItem, Color.White);

            // Creating all of the buttons

            // Main Menu buttons

            // Start Button
            startButton = new Button(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 3 * button.Width / 2, 6 * graphics.PreferredBackBufferHeight / 10 - button.Height / 2, 3 * button.Width, button.Height / 2),button, "START",
                mainFont, Color.MediumAquamarine, Color.DarkTurquoise, Color.RoyalBlue, Color.DarkGreen, new Vector2());
            startButton.TextPosition = new Vector2(startButton.X + 10 * startButton.Position.Width / 25, startButton.Y + startButton.Position.Height / 4);

            // Instructions Button 
            instructionsButton = new Button(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 3 * button.Width / 2, 7 * graphics.PreferredBackBufferHeight / 10 - button.Height / 2, 3 * button.Width, button.Height / 2), button, "INSTRUCTIONS",
                mainFont, Color.MediumAquamarine, Color.DarkTurquoise, Color.RoyalBlue, Color.DarkGreen, new Vector2());
            instructionsButton.TextPosition = new Vector2(instructionsButton.X + 1 * instructionsButton.Position.Width / 4, instructionsButton.Y + instructionsButton.Position.Height / 4);

            // Options button
            optionsButton = new Button(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 3 * button.Width / 2, 8 * graphics.PreferredBackBufferHeight / 10 - button.Height / 2, 3 * button.Width, button.Height / 2), button, "OPTIONS",
                mainFont, Color.MediumAquamarine, Color.DarkTurquoise, Color.RoyalBlue, Color.DarkGreen, new Vector2());
            optionsButton.TextPosition = new Vector2(optionsButton.X + 35 * optionsButton.Position.Width / 100, optionsButton.Y + optionsButton.Position.Height / 4);
            
            // Exit button
            exitButton = new Button(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 3 * button.Width / 2, 9 * graphics.PreferredBackBufferHeight / 10 - button.Height / 2, 3 * button.Width, button.Height / 2), button, "EXIT",
                mainFont, Color.MediumAquamarine, Color.DarkTurquoise, Color.RoyalBlue, Color.DarkGreen, new Vector2());
            exitButton.TextPosition = new Vector2(exitButton.X + 21 * exitButton.Position.Width / 50, exitButton.Y + exitButton.Position.Height / 4);
            
            // Pause buttons

            // Continue button
            pauseContinueButton = new Button(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 3 * button.Width / 2, 6 * graphics.PreferredBackBufferHeight / 10 - button.Height / 2, 3 * button.Width, button.Height / 2), button, "CONTINUE",
                mainFont, Color.Blue, Color.DarkGoldenrod, Color.Black, Color.DarkGreen, new Vector2());
            pauseContinueButton.TextPosition = new Vector2(pauseContinueButton.Position.X + 33 * pauseContinueButton.Position.Width / 100, pauseContinueButton.Position.Y + pauseContinueButton.Position.Height / 4);

            // Instructions Button 
            pauseInstructionsButton = new Button(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 3 * button.Width / 2, 7 * graphics.PreferredBackBufferHeight / 10 - button.Height / 2, 3 * button.Width, button.Height / 2), button, "INSTRUCTIONS",
                mainFont, Color.Blue, Color.DarkGoldenrod, Color.Black, Color.DarkGreen, new Vector2());
            pauseInstructionsButton.TextPosition = new Vector2(pauseInstructionsButton.X + 1 * pauseInstructionsButton.Position.Width / 4, pauseInstructionsButton.Y + pauseInstructionsButton.Position.Height / 4);

            // Options button
            pauseOptionsButton = new Button(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 3 * button.Width / 2, 8 * graphics.PreferredBackBufferHeight / 10 - button.Height / 2, 3 * button.Width, button.Height / 2), button, "OPTIONS",
                mainFont, Color.Blue, Color.DarkGoldenrod, Color.Black, Color.DarkGreen, new Vector2());
            pauseOptionsButton.TextPosition = new Vector2(pauseOptionsButton.X + 35 * pauseOptionsButton.Position.Width / 100, pauseOptionsButton.Y + pauseOptionsButton.Position.Height / 4);

            // Exit button
            pauseExitButton = new Button(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 3 * button.Width / 2, 9 * graphics.PreferredBackBufferHeight / 10 - button.Height / 2, 3 * button.Width, button.Height / 2), button, "EXIT",
                mainFont, Color.Blue, Color.DarkGoldenrod, Color.Black, Color.DarkGreen, new Vector2());
            pauseExitButton.TextPosition = new Vector2(pauseExitButton.X + 21 * pauseExitButton.Position.Width / 50, pauseExitButton.Y + pauseExitButton.Position.Height / 4);

            // Instructions Button

            // Back button
            backButton = new Button(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 3 * button.Width / 2, 9 * graphics.PreferredBackBufferHeight / 10 - button.Height / 2, 3 * button.Width, button.Height / 2), button, "BACK",
                mainFont, Color.MediumAquamarine, Color.DarkTurquoise, Color.RoyalBlue, Color.DarkGreen, new Vector2());
            backButton.TextPosition = new Vector2(backButton.X + 21 * backButton.Position.Width / 50, backButton.Y + backButton.Position.Height / 4);

            // W button
            instructionsWButton = new Button(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 25, graphics.PreferredBackBufferHeight / 2 - 25, 50, 50), button, "W", 
                mainFont, Color.MediumAquamarine, Color.DarkTurquoise, Color.RoyalBlue, Color.DarkGreen, new Vector2());
            instructionsWButton.TextPosition = new Vector2(instructionsWButton.X + instructionsWButton.Position.Width / 4, instructionsWButton.Y + instructionsWButton.Position.Height / 5);

            // A Button
            instructionsAButton = new Button(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 80, graphics.PreferredBackBufferHeight / 2 + 30, 50, 50), button, "A",
                mainFont, Color.MediumAquamarine, Color.DarkTurquoise, Color.RoyalBlue, Color.DarkGreen, new Vector2());
            instructionsAButton.TextPosition = new Vector2(instructionsAButton.X + instructionsAButton.Position.Width / 3, instructionsAButton.Y + instructionsAButton.Position.Height / 5);
            

            // S Button
            instructionsSButton = new Button(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 25, graphics.PreferredBackBufferHeight / 2 + 30, 50, 50), button, "S",
                mainFont, Color.MediumAquamarine, Color.DarkTurquoise, Color.RoyalBlue, Color.DarkGreen, new Vector2());
            instructionsSButton.TextPosition = new Vector2(instructionsSButton.X + instructionsSButton.Position.Width / 3, instructionsSButton.Y + instructionsSButton.Position.Height / 5);
            ;

            // D Button
            instructionsDButton = new Button(new Rectangle(graphics.PreferredBackBufferWidth / 2 + 30, graphics.PreferredBackBufferHeight / 2 + 30, 50, 50), button, "D",
                mainFont, Color.MediumAquamarine, Color.DarkTurquoise, Color.RoyalBlue, Color.DarkGreen, new Vector2());
            instructionsDButton.TextPosition = new Vector2(instructionsDButton.X + instructionsDButton.Position.Width / 3, instructionsDButton.Y + instructionsDButton.Position.Height / 5); ;
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
                        if (startButton.MouseClick(mouseState, previousMouseState))
                        {
                            previousGameState = gameState;
                            gameState = GameState.Game;
                        }

                        // checking if they click the instructions button
                        if (instructionsButton.MouseClick(mouseState, previousMouseState))
                        {
                            previousGameState = gameState;
                            gameState = GameState.Instructions;
                        }

                        // Checking if they want to edit the options of the game
                        if (optionsButton.MouseClick(mouseState, previousMouseState))
                        {
                            previousGameState = gameState;
                            gameState = GameState.Options;
                        }

                        // Checking if they want to exit the game
                        if (exitButton.MouseClick(mouseState, previousMouseState))
                        {
                            this.Exit();
                        }
                        break;
                    }

                case GameState.Instructions:
                    {
                        //checking if they click the back button
                        if (backButton.MouseClick(mouseState, previousMouseState) && previousGameState == GameState.Menu)
                        {
                            previousGameState = gameState;
                            gameState = GameState.Menu;
                        }
                        else if (SingleKeyPress(Keys.Back) && previousGameState == GameState.Menu)
                        {
                            previousGameState = gameState;
                            gameState = GameState.Menu;
                        }
                        else if (backButton.MouseClick(mouseState, previousMouseState) && previousGameState == GameState.Pause)
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
                        if (backButton.MouseClick(mouseState, previousMouseState) && previousGameState == GameState.Menu)
                        {
                            previousGameState = gameState;
                            gameState = GameState.Menu;
                        }
                        else if (SingleKeyPress(Keys.Back) && previousGameState == GameState.Menu)
                        {
                            previousGameState = gameState;
                            gameState = GameState.Menu;
                        }
                        else if (backButton.MouseClick(mouseState, previousMouseState) && previousGameState == GameState.Pause)
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
                        if (pauseContinueButton.MouseClick(mouseState, previousMouseState))
                        {
                            previousGameState = gameState;
                            gameState = GameState.Game;
                        }
                        
                        // checking if they click the instructions button
                        if (pauseInstructionsButton.MouseClick(mouseState, previousMouseState))
                        {
                            previousGameState = gameState;
                            gameState = GameState.Instructions;
                        }

                        // Checking if they want to edit the options of the game
                        if (pauseOptionsButton.MouseClick(mouseState, previousMouseState))
                        {
                            previousGameState = gameState;
                            gameState = GameState.Options;
                        }

                        // Checking if they want to exit to the menu
                        if (pauseExitButton.MouseClick(mouseState, previousMouseState))
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
                        startButton.PressButton(mouseState);
                        startButton.Draw(spriteBatch);

                        // Istructions Button
                        instructionsButton.PressButton(mouseState);
                        instructionsButton.Draw(spriteBatch);

                        // Options Button
                        optionsButton.PressButton(mouseState);
                        optionsButton.Draw(spriteBatch);

                        // Exit Button
                        exitButton.PressButton(mouseState);
                        exitButton.Draw(spriteBatch);

                        break;
                    }

                    //create instructions for game
                case GameState.Instructions:
                    {
                        //Changing Background Color
                        GraphicsDevice.Clear(Color.Black);

                        // Writing actual instructions
                        spriteBatch.DrawString(mainFont, "Press WASD to move", new Vector2(6 * graphics.PreferredBackBufferWidth / 15, graphics.PreferredBackBufferHeight / 3), Color.White);

                        // Back Button
                        backButton.PressButton(mouseState);
                        backButton.Draw(spriteBatch);

                        // Making the buttons to display wasd and changes color when their corresponding button is pressed
                        // W button
                        instructionsWButton.KeyboardPressButton(keyState, Keys.W);
                        instructionsWButton.Draw(spriteBatch);

                        // A Button
                        instructionsAButton.KeyboardPressButton(keyState, Keys.A);
                        instructionsAButton.Draw(spriteBatch);

                        // S Button
                        instructionsSButton.KeyboardPressButton(keyState, Keys.S);
                        instructionsSButton.Draw(spriteBatch);

                        // D Button
                        instructionsDButton.KeyboardPressButton(keyState, Keys.D);
                        instructionsDButton.Draw(spriteBatch);
                    }
                    break;

                case GameState.Options:
                    {
                        // Back Button
                        backButton.PressButton(mouseState);
                        backButton.Draw(spriteBatch);

                        break;
                    }

                case GameState.Game:
                    {
                        //Displaying the timer
                        string time = string.Format("{0:0.00}", timer);
                        spriteBatch.DrawString(mainFont, time, new Vector2(GraphicsDevice.Viewport.Width / 2, 0), Color.Black);
                        player.Draw(spriteBatch);
                        placeholder.Draw(spriteBatch);

                        break;
                    }
                case GameState.Pause:
                    {
                        // Making the background
                        spriteBatch.Draw(button, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.RoyalBlue); // BlueViolet, DarkMagenta, MediumPurple, CadetBlue, DodgerBlue

                        // Pause title
                        spriteBatch.Draw(pauseTitle, titlePosition, Color.White);

                        // Continue Button
                        pauseContinueButton.PressButton(mouseState);
                        pauseContinueButton.Draw(spriteBatch);

                        // Istructions Button
                        pauseInstructionsButton.PressButton(mouseState);
                        pauseInstructionsButton.Draw(spriteBatch);

                        // Options Button
                        pauseOptionsButton.PressButton(mouseState);
                        pauseOptionsButton.Draw(spriteBatch);

                        // Exit Button
                        pauseExitButton.PressButton(mouseState);
                        pauseExitButton.Draw(spriteBatch);
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

    }
}
