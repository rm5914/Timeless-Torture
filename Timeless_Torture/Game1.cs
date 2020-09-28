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
        //MouseState mouseState;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D texture;
        private Texture2D button;
        private Vector2 position;

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

            gameState = GameState.Menu;

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
            texture = Content.Load<Texture2D>("Player1");
            position = new Vector2(0, 0);
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
            //mouseState = Mouse.GetState();

            // Checking the current game State
            switch (gameState) 
            {
                case GameState.Menu:
                    {
                        if (keyState.IsKeyDown(Keys.Enter)) 
                        {
                            gameState = GameState.Game;
                        }
                    }
                case GameState.Game:
                    {
                        MovePlayer();
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
