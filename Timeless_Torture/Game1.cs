using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.ComponentModel.Design;
using System.Threading;
using System.IO;
using System.Collections.Generic;

namespace Timeless_Torture
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
     
    //enum GameState
    enum GameState { Menu, Options, Instructions, Game, Pause, GameOver };

    // Used to control the difficulty of the game, only affects the game when the game starts
    enum Difficulty { Easy, Medium, Hard };
    
    public class Game1 : Game
    {
        // FIELDS
        
        //enum as data type
        GameState gameState;
        GameState previousGameState;

        // Difficulty of the game
        Difficulty difficulty;

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

        // A list to hold the items currently being used
        Item[] items;

        //timer
        double timer;
        double timerMax;

        //list to keep track of rectangles of floor tiles
        Rectangle[,] currentFloorTiles;

        // Keeps track of the current level
        int currentLevel;

        // The player and their position
        Player player;
        private Rectangle playerPosition;

        // Random Number Generator
        Random numGenerator;

        // Room width and height (block wise)
        Floor floor1;
        Floor floor2;
        Floor[] floors;
        int currentFloor;

        // The fireplace
        Fireplace fireplace;

        // portal stuff
        bool shouldSpawnPortal = false;
        Rectangle portalRectangle;

        // The staircase Rectangle
        Rectangle staircaseRectangle;

        // Lists to hold time-specific items
        private List<Texture2D> seventiesItems;
        private List<Texture2D> eightiesItems;
        private List<Texture2D> ninetiesItems;
        private List<Texture2D> zerosItems;
        private List<Texture2D> tensItems;

        // Lists to hold the glowing texture for time-specific items
        private List<Texture2D> seventiesGlow;
        private List<Texture2D> eightiesGlow;
        private List<Texture2D> ninetiesGlow;
        private List<Texture2D> zerosGlow;
        private List<Texture2D> tensGlow;

        // Textures
        private Texture2D texture;
        private Texture2D button;
        private Texture2D title;
        private Texture2D pauseTitle;
        private Texture2D fireplaceTexture;
        private Texture2D fireplaceGlowTexture;
        private Texture2D portal;
        private Texture2D bed;
        private Texture2D staircase;

        // Floors
        private Texture2D currentFloorTexture;
        private Texture2D seventiesFloor;
        private Texture2D eightiesFloor;
        private Texture2D ninetiesFloor;
        private Texture2D zerosFloor;
        private Texture2D tensFloor;

        // item textures
        // 70's
        private Texture2D lightsaber;
        private Texture2D pongMachine;
        private Texture2D moodRing;

        // 80's
        private Texture2D slapBracelet;
        private Texture2D walkman;
        private Texture2D rubikCube;

        // 90's
        private Texture2D tamagotchi;
        private Texture2D playStation;
        private Texture2D beeper;

        // 00's
        private Texture2D ipodTouch;
        private Texture2D clubPenguin;
        private Texture2D zoopalPlate;

        // 10's
        private Texture2D wiiRemote;
        private Texture2D amazonEcho;
        private Texture2D facebook;

        // Glow Item Textures
        // 70's
        private Texture2D lightsaberGlow;
        private Texture2D pongMachineGlow;
        private Texture2D moodRingGlow;

        // 80's
        private Texture2D slapBraceletGlow;
        private Texture2D walkmanGlow;
        private Texture2D rubikCubeGlow;

        // 90's
        private Texture2D tamagotchiGlow;
        private Texture2D playStationGlow;
        private Texture2D beeperGlow;

        // 00's
        private Texture2D ipodTouchGlow;
        private Texture2D clubPenguinGlow;
        private Texture2D zoopalPlateGlow;

        // 10's
        private Texture2D wiiRemoteGlow;
        private Texture2D amazonEchoGlow;
        private Texture2D facebookGlow;

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

        // Options buttons
        private Button difficultyButton;
        private Button easyDifficulty;
        private Button mediumDifficulty;
        private Button hardDifficulty;

        private Camera camera;

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
            // Tries to read in the file data

            // Getting the directory to find the file
            //string path = Directory.GetParent(
            //Directory.GetCurrentDirectory()).Parent.FullName;
            //path = path.Substring(0, path.Length - 31);

            timerMax = timer;
            difficulty = Difficulty.Medium;
            // TODO: Add your initialization logic here
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
            //load in floor
            seventiesFloor = Content.Load<Texture2D>("floor pattern");
            eightiesFloor = Content.Load<Texture2D>("second floor pattern");
            ninetiesFloor = Content.Load<Texture2D>("third floor pattern");
            zerosFloor = Content.Load<Texture2D>("fourth floor pattern");
            tensFloor = Content.Load<Texture2D>("fifth floor pattern");
            currentFloorTexture = seventiesFloor;

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Making the player 
            texture = Content.Load<Texture2D>("player");

            //Making the camera
            camera = new Camera(GraphicsDevice.Viewport);

            // All of the textures
            button = Content.Load<Texture2D>("TT Buttons");
            title = Content.Load<Texture2D>("Title");
            pauseTitle = Content.Load<Texture2D>("Pause");
            fireplaceTexture = Content.Load<Texture2D>("fireplace");
            fireplaceGlowTexture = Content.Load<Texture2D>("fireplace glow");
            portal = Content.Load<Texture2D>("game portal");
            bed = Content.Load<Texture2D>("bed");
            staircase = Content.Load<Texture2D>("staircase");

            // Secenties item textures
            lightsaber = Content.Load<Texture2D>("lightsaber");
            pongMachine = Content.Load<Texture2D>("pong machine");
            moodRing = Content.Load<Texture2D>("mood ring");

            seventiesItems = new List<Texture2D>();
            seventiesItems.Add(lightsaber);
            seventiesItems.Add(pongMachine);
            seventiesItems.Add(moodRing);

            // Eighties item textures
            slapBracelet = Content.Load<Texture2D>("slap bracelet");
            walkman = Content.Load<Texture2D>("walkman");
            rubikCube = Content.Load<Texture2D>("rubik cube");

            eightiesItems = new List<Texture2D>();
            eightiesItems.Add(slapBracelet);
            eightiesItems.Add(walkman);
            eightiesItems.Add(rubikCube);

            // Nineties item textures
            tamagotchi = Content.Load<Texture2D>("tamagotchi");
            playStation = Content.Load<Texture2D>("play station");
            beeper = Content.Load<Texture2D>("beeper");

            ninetiesItems = new List<Texture2D>();
            ninetiesItems.Add(tamagotchi);
            ninetiesItems.Add(playStation);
            ninetiesItems.Add(beeper);

            // Zero's item textures
            ipodTouch = Content.Load<Texture2D>("ipod touch");
            clubPenguin = Content.Load<Texture2D>("club penguin");
            zoopalPlate = Content.Load<Texture2D>("zoopal plate");

            zerosItems = new List<Texture2D>();
            zerosItems.Add(ipodTouch);
            zerosItems.Add(clubPenguin);
            zerosItems.Add(zoopalPlate);

            // Ten's item textures
            wiiRemote = Content.Load<Texture2D>("wii remote");
            amazonEcho = Content.Load<Texture2D>("amazon echo");
            facebook = Content.Load<Texture2D>("facebook logo");

            tensItems = new List<Texture2D>();
            tensItems.Add(wiiRemote);
            tensItems.Add(amazonEcho);
            tensItems.Add(facebook);

            // Seventies glow textures
            lightsaberGlow = Content.Load<Texture2D>("lightsaber glow");
            pongMachineGlow = Content.Load<Texture2D>("pong machine glow");
            moodRingGlow = Content.Load<Texture2D>("mood ring glow");

            seventiesGlow = new List<Texture2D>();
            seventiesGlow.Add(lightsaberGlow);
            seventiesGlow.Add(pongMachineGlow);
            seventiesGlow.Add(moodRingGlow);

            // Eighties glow textures
            slapBraceletGlow = Content.Load<Texture2D>("slap bracelet glow");
            walkmanGlow = Content.Load<Texture2D>("walkman glow");
            rubikCubeGlow = Content.Load<Texture2D>("rubik cube glow");

            eightiesGlow = new List<Texture2D>();
            eightiesGlow.Add(slapBraceletGlow);
            eightiesGlow.Add(walkmanGlow);
            eightiesGlow.Add(rubikCubeGlow);

            // Nineties glow textures
            tamagotchiGlow = Content.Load<Texture2D>("tamagotchi glow");
            playStationGlow = Content.Load<Texture2D>("play station glow");
            beeperGlow = Content.Load<Texture2D>("beeper glow");

            ninetiesGlow = new List<Texture2D>();
            ninetiesGlow.Add(tamagotchiGlow);
            ninetiesGlow.Add(playStationGlow);
            ninetiesGlow.Add(beeperGlow);

            // Zero's glow textures
            ipodTouchGlow = Content.Load<Texture2D>("ipod touch glow");
            clubPenguinGlow = Content.Load<Texture2D>("club penguin glow");
            zoopalPlateGlow = Content.Load<Texture2D>("zoopal plate glow");

            zerosGlow = new List<Texture2D>();
            zerosGlow.Add(ipodTouchGlow);
            zerosGlow.Add(clubPenguinGlow);
            zerosGlow.Add(zoopalPlateGlow);

            // Ten's glow textures
            wiiRemoteGlow = Content.Load<Texture2D>("wii remote glow");
            amazonEchoGlow = Content.Load<Texture2D>("amazon echo glow");
            facebookGlow = Content.Load<Texture2D>("facebook logo glow");

            tensGlow = new List<Texture2D>();
            tensGlow.Add(wiiRemoteGlow);
            tensGlow.Add(amazonEchoGlow);
            tensGlow.Add(facebookGlow);

            //load sprite font
            mainFont = Content.Load<SpriteFont>("mainFont");

            // All positions
            titlePosition = new Vector2(graphics.PreferredBackBufferWidth / 2 - 13 * title.Width / 25, graphics.PreferredBackBufferHeight / 5 - title.Height / 2);

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
            
            // Pause Menu buttons
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
            

            // D Button
            instructionsDButton = new Button(new Rectangle(graphics.PreferredBackBufferWidth / 2 + 30, graphics.PreferredBackBufferHeight / 2 + 30, 50, 50), button, "D",
                mainFont, Color.MediumAquamarine, Color.DarkTurquoise, Color.RoyalBlue, Color.DarkGreen, new Vector2());
            instructionsDButton.TextPosition = new Vector2(instructionsDButton.X + instructionsDButton.Position.Width / 3, instructionsDButton.Y + instructionsDButton.Position.Height / 5); ;

            // Options Buttons
            // Difficulty Button
            difficultyButton = new Button(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 3 * button.Width / 2, 5 * graphics.PreferredBackBufferHeight / 10 - button.Height / 2, 3 * button.Width, button.Height / 2), button, "DIFFICULTY",
                mainFont, Color.MediumAquamarine, Color.DarkTurquoise, Color.RoyalBlue, Color.DarkGreen, new Vector2());
            difficultyButton.TextPosition = new Vector2(difficultyButton.X + 15 * difficultyButton.Position.Width / 50, difficultyButton.Y + difficultyButton.Position.Height / 4);

            // Easy Difficulty Button
            easyDifficulty = new Button(new Rectangle(graphics.PreferredBackBufferWidth / 2 -  button.Width, 6 * graphics.PreferredBackBufferHeight / 10 - button.Height / 2, 2 * button.Width, button.Height / 2), button, "EASY",
            mainFont, Color.MediumAquamarine, Color.DarkTurquoise, Color.RoyalBlue, Color.DarkGreen, new Vector2());
            easyDifficulty.TextPosition = new Vector2(easyDifficulty.X + 9 * easyDifficulty.Position.Width / 25, easyDifficulty.Y + easyDifficulty.Position.Height / 4);

            // Medium Difficulty Button
            mediumDifficulty = new Button(new Rectangle(graphics.PreferredBackBufferWidth / 2 - button.Width, 7 * graphics.PreferredBackBufferHeight / 10 - button.Height / 2, 2 * button.Width, button.Height / 2), button, "MEDIUM",
            mainFont, Color.MediumAquamarine, Color.DarkTurquoise, Color.RoyalBlue, Color.DarkGreen, new Vector2());
            mediumDifficulty.TextPosition = new Vector2(mediumDifficulty.X + 7 * mediumDifficulty.Position.Width / 25, mediumDifficulty.Y + mediumDifficulty.Position.Height / 4); ;

            // Hard Difficulty Button
            hardDifficulty = new Button(new Rectangle(graphics.PreferredBackBufferWidth / 2 - button.Width, 8 * graphics.PreferredBackBufferHeight / 10 - button.Height / 2, 2 * button.Width, button.Height / 2), button, "HARD",
            mainFont, Color.MediumAquamarine, Color.DarkTurquoise, Color.RoyalBlue, Color.DarkGreen, new Vector2());
            hardDifficulty.TextPosition = new Vector2(hardDifficulty.X + 9 * hardDifficulty.Position.Width / 25, hardDifficulty.Y + hardDifficulty.Position.Height / 4); ;
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
                            GameStart();
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

                        //updating the camera in game
                        camera.Move(player.Position);

                        if (timer <= 0)
                        {
                            previousGameState = gameState;
                            gameState = GameState.GameOver;
                        }

                        // Checking if they want to pause
                        if (SingleKeyPress(Keys.Escape))
                        {
                            previousGameState = gameState;
                            gameState = GameState.Pause;
                        }

                        // Checking if they use the interact button
                        if (SingleKeyPress(Keys.E))
                        {
                            // Checking if they want to pick up an item
                            for (int i = 0; i < items.Length; i++)
                            {
                                if (player.Inventory[player.InventoryLimit - 1] == null && items[i].PickUp())
                                {
                                    player.AddItem(items[i]);
                                    return;
                                }
                            }

                            // Checking if they want to use the fireplace
                            if (player.Inventory[0] != null)
                            {
                                fireplace.BurnItem(player);

                                if (fireplace.BurnedItems == items.Length)
                                {
                                    SpawnPortal();
                                }
                            }

                            // Checking if they want to use the portal
                            if (IsPlayerClose(player.Position, portalRectangle))
                            {
                                NextLevel();
                            }

                            else if (IsPlayerClose(player.Position, staircaseRectangle))
                            {
                                if (currentFloor == 0)
                                {
                                    currentFloor++;
                                }
                                else
                                {
                                    currentFloor = 0;
                                }
                            }
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
                        GraphicsDevice.Clear(Color.Black);

                        // Back Button
                        backButton.PressButton(mouseState);
                        backButton.Draw(spriteBatch);

                        // Making the difficulty button stay pressed after 1 click
                        if (difficultyButton.MouseClick(mouseState, previousMouseState))
                        {
                            difficultyButton.IsPressed = !difficultyButton.IsPressed;
                        }

                        // Difficulty Button
                        difficultyButton.Draw(spriteBatch);

                        // Displays all difficulty options only if the Difficulty button is clicked
                        if (difficultyButton.IsPressed)
                        {
                            // Easy Difficulty Button
                            if (easyDifficulty.MouseClick(mouseState, previousMouseState))
                            {
                                easyDifficulty.IsPressed = !easyDifficulty.IsPressed;
                                mediumDifficulty.IsPressed = false;
                                hardDifficulty.IsPressed = false;

                                difficulty = Difficulty.Easy;
                            }
                            else if (mediumDifficulty.MouseClick(mouseState, previousMouseState))
                            {
                                easyDifficulty.IsPressed = false;
                                mediumDifficulty.IsPressed = !mediumDifficulty.IsPressed;
                                hardDifficulty.IsPressed = false;

                                difficulty = Difficulty.Medium;
                            }
                            else if (hardDifficulty.MouseClick(mouseState, previousMouseState))
                            {
                                easyDifficulty.IsPressed = false;
                                mediumDifficulty.IsPressed = false;
                                hardDifficulty.IsPressed = !hardDifficulty.IsPressed;

                                difficulty = Difficulty.Hard;
                            }

                            // Easy Difficulty Button
                            easyDifficulty.Draw(spriteBatch);

                            // Medium Difficulty Button
                            mediumDifficulty.Draw(spriteBatch);

                            // Hard Difficulty Button
                            hardDifficulty.Draw(spriteBatch);
                        }
                        break;
                    }

                case GameState.Game:
                    {
                        //drawing the current floor pattern
                        currentFloorTexture = floor1.Texture;
                        currentFloorTiles = floors[currentFloor].FloorTiles;

                        for (int i = 0; i < 20; i++)
                        {
                            for (int j = 0; j < 20; j++)
                            {
                                if (floors[currentFloor].FloorData[i, j] == "Black")
                                {
                                    spriteBatch.Draw(currentFloorTexture, currentFloorTiles[i, j], Color.Black);
                                    player.PlayerCollision(currentFloorTiles[i, j]);
                                }
                                else if (!IsPlayerCloseLarge(player.Position, currentFloorTiles[i, j]))
                                {
                                    spriteBatch.Draw(currentFloorTexture, currentFloorTiles[i, j], Color.Black);
                                }
                                else
                                {
                                    spriteBatch.Draw(currentFloorTexture, currentFloorTiles[i, j], Color.White);
                                    if (floors[currentFloor].FloorData[i, j] == "DarkOliveGreen")
                                    {
                                        spriteBatch.Draw(bed, currentFloorTiles[i, j], Color.White);
                                    }
                                    else if (floors[currentFloor].FloorData[i, j] == "BurlyWood")
                                    {
                                        spriteBatch.Draw(staircase, currentFloorTiles[i, j], Color.White);
                                    }
                                    else if (floors[currentFloor].FloorData[i, j] == "Red")
                                    {
                                        fireplace.PlayerClose = IsPlayerClose(player.Position, fireplace.Position);
                                        fireplace.Draw(spriteBatch);
                                    }
                                    else if (shouldSpawnPortal && floors[currentFloor].FloorData[i, j] == "DarkBlue")
                                    {
                                        spriteBatch.Draw(portal, portalRectangle, Color.White);
                                    }
                                }
                            }
                        }

                        // Displaying the items on the floor
                        for (int i = 0; i < floors[currentFloor].Items.Count; i++)
                        {
                            floors[currentFloor].Items[i].PlayerClose = IsPlayerClose(player.Position, floors[currentFloor].Items[i].Position);
                            if(IsPlayerCloseLarge(player.Position, floors[currentFloor].Items[i].Position))
                                floors[currentFloor].Items[i].Draw(spriteBatch);
                        }

                        // Displaying the player inventory
                        for (int i = 0; i < items.Length; i++)
                        {
                            if (items[i].IsPickedUp)
                            {
                                items[i].Draw(spriteBatch);
                            }
                        }

                        //Displaying the timer
                        string time = string.Format("{0:0.00}", timer);
                        spriteBatch.DrawString(mainFont, time, new Vector2(GraphicsDevice.Viewport.Width / 2, 0), Color.White);
                        player.Draw(spriteBatch);


                        //display inventory
                        spriteBatch.DrawString(mainFont, "Inventory", new Vector2(350, 650), Color.White);

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
                        spriteBatch.DrawString(mainFont, "Game Over, Press enter to continue", new Vector2(GraphicsDevice.Viewport.Width / 3, 0), Color.Black);
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
        /// Used to start the game from 0
        /// </summary>
        protected void GameStart()
        {
            // Resetting the current level of the game of the game
            currentFloorTexture = seventiesFloor;
            currentLevel = 0;
            currentFloor = 0;
            shouldSpawnPortal = false;

            // Loading the first level
            floor1 = new Floor(seventiesFloor, "Level" + currentLevel + "Floor1UNFINISHED1.level", graphics.PreferredBackBufferWidth / 20, graphics.PreferredBackBufferHeight / 20);
            floor2 = new Floor(seventiesFloor, "Level" + currentLevel + "Floor2UNFINISHED1.level", graphics.PreferredBackBufferWidth / 20, graphics.PreferredBackBufferHeight / 20);

            // Initializing floors
            floors = new Floor[2];
            floors[0] = floor1;
            floors[1] = floor2;

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    // Fireplace
                    if (floor1.FloorData[i, j] == "Red")
                    {
                        fireplace = new Fireplace(fireplaceTexture, fireplaceGlowTexture, floor1.FloorTiles[i, j], Color.White);
                    }
                    // Player Spawn
                    else if (floor1.FloorData[i, j] == "DarkOliveGreen")
                    {
                        playerPosition = new Rectangle(floor1.FloorTiles[i, j].X, floor1.FloorTiles[i, j].Y, texture.Width / 8, texture.Height / 8);
                    }
                    // Portal Spawn
                    else if (floor1.FloorData[i, j] == "DarkBlue")
                    {
                        portalRectangle = floor1.FloorTiles[i, j];
                    }
                    // Staircase Spawn
                    else if (floor1.FloorData[i, j] == "BurlyWood")
                    {
                        staircaseRectangle = floor1.FloorTiles[i, j];
                    }
                }
            }

            // Spawning the player
            player = new Player(texture, playerPosition, 1);
            player.ResetInventory();

            // Resetting everything
            fireplace.Reset();

            // Changing the settings based on difficulty
            // These are the max settings, any higher speed would cause problems with the player collision
            if (difficulty == Difficulty.Easy)
            {
                timerMax = 330;
                player.XMovement = 2;
                player.YMovement = 2;
                player.InventoryLimit = 3;
            }
            else if (difficulty == Difficulty.Medium)
            {
                timerMax = 300;
                player.XMovement = 2;
                player.YMovement = 2;
                player.InventoryLimit = 2;
            }
            else if (difficulty == Difficulty.Hard)
            {
                timerMax = 240;
                player.XMovement = 2;
                player.YMovement = 2;
                player.InventoryLimit = 1;
            }

            timer = timerMax;
            PlaceItems(seventiesItems, seventiesGlow);

        }

        /// <summary>
        /// Takes in 2 textures for items and places them through the selected locations
        /// </summary>
        /// <param name="textures"> The textures of the items in their basic form </param>
        /// <param name="glowTextures"> The textures of the items but with a glow around them </param>
        protected void PlaceItems(List<Texture2D> textures, List<Texture2D> glowTextures)
        {
            items = new Item[3];

            // Picking randomly from the set positions
            for (int i = 0; i < items.Length; i++)
            {
                int floorPosition = numGenerator.Next(0, 2);
                int position;
                Floor current;

                if (floorPosition == 0)
                {
                    position = numGenerator.Next(0, floor1.ItemPositions.Count);
                    current = floor1;
                }
                else
                {
                    position = numGenerator.Next(0, floor2.ItemPositions.Count);
                    current = floor2;
                }

                // Don't have the positions overlap
                Vector2 itemVector = current.ItemPositions[position].GetPosition();
                while (itemVector.X == 0)
                {
                    position = numGenerator.Next(0, current.ItemPositions.Count);
                    itemVector = current.ItemPositions[position].GetPosition();
                }
                items[i] = new Item(new Rectangle((int)itemVector.X, (int)itemVector.Y, textures[i].Width / 5, textures[i].Height / 5), new Rectangle(350 + i * 20, 700 + i * 20, textures[i].Width / 10, textures[i].Height / 10), textures[i], glowTextures[i], Color.White);
                current.Items.Add(items[i]);
            }
        }

        /// <summary>
        /// Spawns the portal to allow access to the next level/era
        /// </summary>
        protected void SpawnPortal()
        {
            shouldSpawnPortal = true;
        }

        /// <summary>
        /// Starts the next level of the game
        /// </summary>
        protected void NextLevel()
        {
            // Setting the current level of the game
            currentLevel++;
            currentFloor = 0;

            // Getting the current floor texture
            switch (currentLevel)
            {
                case 1:
                    {
                        currentFloorTexture = eightiesFloor;
                        break;
                    }
                case 2:
                    {
                        currentFloorTexture = ninetiesFloor;
                        break;
                    }
                case 3:
                    {
                        currentFloorTexture = zerosFloor;
                        break;
                    }
                case 4:
                    {
                        currentFloorTexture = tensFloor;
                        break;
                    }
                case 5:
                    {
                        // Victory screen
                        gameState = GameState.GameOver;
                        break;
                    }
            }

            // Resetting item positions
            for (int i = 0; i < floor1.ItemPositions.Count; i++)
            {
                floor1.ItemPositions[i].Reset();
            }
            for (int i = 0; i < floor2.ItemPositions.Count; i++)
            {
                floor2.ItemPositions[i].Reset();
            }

            floor1 = new Floor(currentFloorTexture, "Level" + currentLevel + "Floor1UNFINISHED1.level", graphics.PreferredBackBufferWidth / 20, graphics.PreferredBackBufferHeight / 20);
            floor2 = new Floor(currentFloorTexture, "Level" + currentLevel + "Floor2UNFINISHED1.level", graphics.PreferredBackBufferWidth / 20, graphics.PreferredBackBufferHeight / 20);

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    // Fireplace
                    if (floor1.FloorData[i, j] == "Red")
                    {
                        fireplace = new Fireplace(fireplaceTexture, fireplaceGlowTexture, floor1.FloorTiles[i, j], Color.White);
                    }
                    // Player Spawn
                    else if (floor1.FloorData[i, j] == "DarkOliveGreen")
                    {
                        player.Position = new Rectangle(floor1.FloorTiles[i, j].X, floor1.FloorTiles[i, j].Y, texture.Width / 8, texture.Height / 8);
                    }
                    // Portal Spawn
                    else if (floor1.FloorData[i, j] == "DarkBlue")
                    {
                        portalRectangle = floor1.FloorTiles[i, j];
                    }
                    // Staircase spawn
                    else if (floor1.FloorData[i, j] == "BurlyWood")
                    {
                        staircaseRectangle = floor1.FloorTiles[i, j];
                    }
                }
            }

            // Placing the items of the current level
            switch (currentLevel)
            {
                case 1:
                    {
                        PlaceItems(eightiesItems, eightiesGlow);
                        break;
                    }
                case 2:
                    {
                        PlaceItems(ninetiesItems, ninetiesGlow);
                        break;
                    }
                case 3:
                    {
                        PlaceItems(zerosItems, zerosGlow);
                        break;
                    }
                case 4:
                    {
                        PlaceItems(tensItems, tensGlow);
                        break;
                    }
            }

            // Resetting everything
            player.ResetInventory();
            fireplace.Reset();
            shouldSpawnPortal = false;
            timer = timerMax;
        }

        /// <summary>
        /// Checks if the player is close to the given object
        /// </summary>
        /// <param name="player"> The player character </param>
        /// <param name="rectangle"> The object the player is close to </param>
        public bool IsPlayerClose(Rectangle player, Rectangle rectangle)
        {
            if ((((player.X + player.Width / 2) + 40 > (rectangle.X + rectangle.Width / 2) && (player.X + player.Width / 2) - 40 < (rectangle.X + rectangle.Width / 2)) && (player.Y + player.Height / 2) + 50 > (rectangle.Y + rectangle.Height / 2) && (player.Y + player.Height / 2) - 50 < (rectangle.Y + rectangle.Height / 2)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Checks if the player is close to the given object - has a larger radius than IsPlayerClose
        /// </summary>
        /// <param name="player"> The player character </param>
        /// <param name="rectangle"> The object the player is close to </param>
        public bool IsPlayerCloseLarge(Rectangle player, Rectangle rectangle)
        {
            if ((((player.X + player.Width / 2) + 70 > (rectangle.X + rectangle.Width / 2) && (player.X + player.Width / 2) - 70 < (rectangle.X + rectangle.Width / 2)) && (player.Y + player.Height / 2) + 55 > (rectangle.Y + rectangle.Height / 2) && (player.Y + player.Height / 2) - 65 < (rectangle.Y + rectangle.Height / 2)))
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
