using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Timeless_Torture
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
     
    //enum GameState
    enum GameState { Menu, Instructions, Game };

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
            startButton = new Rectangle(graphics.PreferredBackBufferWidth / 2 - 3 * button.Width / 2, 3 * graphics.PreferredBackBufferHeight / 5 - button.Height / 2, 3 * button.Width, button.Height / 2);
            instructionsButton = new Rectangle(graphics.PreferredBackBufferWidth / 2 - 3 * button.Width / 2, 5 * graphics.PreferredBackBufferHeight / 7 - button.Height / 2, 3 * button.Width, button.Height / 2);

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
                        //checking if they click the instructions button
                        if (MouseClick(instructionsButton))
                        {
                            gameState = GameState.Instructions;
                        }
                        break;
                    }

                case GameState.Instructions:
                    {
                        //checking if they click the instructions button
                        if (MouseClick(instructionsButton))
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

                        // Start button, draws differently if it's being pressed
                        if (IsMouseDown(startButton))
                        {
                            spriteBatch.Draw(button, startButton, Color.RoyalBlue); // Lavender, royal blue, MediummAquamarine, turqoise
                            spriteBatch.DrawString(mainFont, "START", new Vector2(startButton.X + 10 * startButton.Width / 25, startButton.Y + startButton.Height / 4), Color.DarkGreen); // DarkSeaGreen,  DarkOrchid/Orchid, DodgerBlue, DarkTurquoise
                        }
                        else
                        {
                            spriteBatch.Draw(button, startButton, Color.MediumAquamarine); // Lavender, royal blue, MediummAquamarine, turqoise
                            spriteBatch.DrawString(mainFont, "START", new Vector2(startButton.X + 10 * startButton.Width / 25, startButton.Y + startButton.Height / 4), Color.DarkTurquoise); // DarkSeaGreen,  DarkOrchid/Orchid, DodgerBlue, DarkTurquoise
                        }
                        // Start button, draws differently if it's being pressed
                        if (IsMouseDown(instructionsButton))
                        {
                            spriteBatch.Draw(button, instructionsButton, Color.RoyalBlue); // Lavender, royal blue, MediummAquamarine, turqoise
                            spriteBatch.DrawString(mainFont, "INSTRUCTIONS", new Vector2(instructionsButton.X + 13 * instructionsButton.Width / 50, instructionsButton.Y + instructionsButton.Height / 4), Color.DarkGreen); // DarkSeaGreen,  DarkOrchid/Orchid, DodgerBlue, DarkTurquoise
                        }
                        else
                        {
                            spriteBatch.Draw(button, instructionsButton, Color.MediumAquamarine); // Lavender, royal blue, MediummAquamarine, turqoise
                            spriteBatch.DrawString(mainFont, "INSTRUCTIONS", new Vector2(instructionsButton.X + 13 * instructionsButton.Width / 50, instructionsButton.Y + instructionsButton.Height / 4), Color.DarkTurquoise); // DarkSeaGreen,  DarkOrchid/Orchid, DodgerBlue, DarkTurquoise
                        }
                        break;
                    }

                    //create instructions for game
                case GameState.Instructions:
                    {
                        spriteBatch.DrawString(mainFont, "WASD for character movement", new Vector2(400, 300), Color.Black);
                        
                    }
                    break;

                case GameState.Game:
                    {
                        spriteBatch.Draw(texture, position, Color.White);
                        break;
                    }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

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
    }
}
