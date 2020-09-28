using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Timeless_Torture
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
     
    //enum GameState
    enum GameState { Menu, Game };

    public class Game1 : Game
    {
        //enum as data type
        GameState gameState;

        //keyboard state
        KeyboardState keyState;
        KeyboardState previousKeyState;

        //mouse state
        MouseState mouseState;

        //sprite font
        SpriteFont mainFont;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D texture;
        private Texture2D button;

        private Vector2 position;
        private Rectangle startButton;

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

            // All rectangles
            position = new Vector2(0, 0);
            startButton = new Rectangle(graphics.PreferredBackBufferWidth / 2 - 3 * button.Width / 4, graphics.PreferredBackBufferHeight / 3 - button.Height / 2, 3 * button.Width / 2, button.Height / 2);

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
            mouseState = Mouse.GetState();

            // Checking the current game State
            switch (gameState) 
            {
                case GameState.Menu:
                    {
                        if ((mouseState.X >= startButton.X && mouseState.X <= startButton.X + startButton.Width) && (mouseState.Y > startButton.Y && mouseState.Y < startButton.Y + startButton.Height) && mouseState.LeftButton == ButtonState.Pressed) 
                        {
                            gameState = GameState.Game;
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
                        spriteBatch.Draw(button, startButton, Color.White);
                        spriteBatch.DrawString(mainFont, "START", new Vector2(graphics.PreferredBackBufferWidth / 2 - 1 * button.Width / 3, graphics.PreferredBackBufferHeight / 3 - 3 * button.Height / 8), Color.White);
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
    }
}
