using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace NDE
{
    /// <summary>
    /// Globals for the player and collision sprites
    /// </summary>
    static class playerList
    {
        static List<PlayerSprite> players;
        static playerList()
        {
            players = new List<PlayerSprite>();
        }

        public static List<PlayerSprite> list()
        {
            return players;
        }
    }
    /// <summary>
    /// This is the main type for your game
    /// </summary>

    enum State
    {
        TITLE,
        RUNNING,
        PAUSE,
        GAME_OVER
    }

    public delegate void ChangedEventHandler(object sender, bool e);

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Video variables
        VideoPlayer vPlayer;
        Video titleVideo;

        // Keyboard states used to determine key presses
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        // Set state of game
        State gameState = State.TITLE;
        
        // For generic text writing
        SpriteFont normalText;

        static public Vector2 bottomPoint;
        Level currentLevel;
        private Texture2D myBlankTexture;
        private int currentButtonSequence;

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
            currentLevel = new Level(GraphicsDevice, "invalid");
            currentButtonSequence = 0;

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

            titleVideo = Content.Load<Video>("start_menu");
            vPlayer = new VideoPlayer();
            vPlayer.IsLooped = true;
            vPlayer.Play(titleVideo);

            normalText = Content.Load<SpriteFont>("NormalText");
            bottomPoint = new Vector2(0, GraphicsDevice.Viewport.Height - 20);
            myBlankTexture = new Texture2D(GraphicsDevice, 1, 1);
            myBlankTexture.SetData(new[] { Color.White });
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);

            // Allows the game to exit
            if (currentGamePadState.Buttons.Back == ButtonState.Pressed || currentKeyboardState.IsKeyDown(Keys.Escape))
                this.Exit();

            // State transitions
            switch (gameState)
            {
                case State.TITLE:
                    // Capture button sequence
                    if (currentKeyboardState.GetPressedKeys().Length > 0 ||
                        currentGamePadState.Buttons.A == ButtonState.Pressed ||
                        currentGamePadState.Buttons.B == ButtonState.Pressed ||
                        currentGamePadState.Buttons.X == ButtonState.Pressed ||
                        currentGamePadState.Buttons.Y == ButtonState.Pressed)
                    {
                        checkButtonSequence(currentKeyboardState, previousKeyboardState, currentGamePadState, previousGamePadState);
                    }

                    if (currentKeyboardState.IsKeyDown(Keys.Enter) || currentGamePadState.IsButtonDown(Buttons.Start))
                    {
                        // Initialize all the levels
                        gameState = State.RUNNING;
                        initLevels(false);
                    }
                    break;
                case State.RUNNING:
                    if (currentKeyboardState.IsKeyDown(Keys.P) || currentGamePadState.IsButtonDown(Buttons.Start))
                        gameState = State.PAUSE;
                    else
                    {
                        // Check the level status
                        if (currentLevel == null)
                            break;
                        if (currentLevel.runningState == CompletionState.complete)
                        {
                            currentLevel = currentLevel.nextLevel;
                            if (currentLevel == null)   // YOU WIN!!!  Game Over
                                this.Exit();
                        }

                        // Begin loading the new level
                        if (currentLevel.loadedState == LoadingState.uninitialized)
                            currentLevel.LoadLevel(Content, PlayerIndex.One);

                        // Update the current level
                        else
                            currentLevel.Update(gameTime);
                    }
                    break;
                case State.PAUSE:
                    if (currentKeyboardState.IsKeyDown(Keys.R) || (currentGamePadState.IsButtonDown(Buttons.Start) && !previousGamePadState.IsButtonDown(Buttons.Start)))
                        gameState = State.RUNNING;
                    break;
                default:
                    break;
            }

            // Save previous keyboard state
            previousKeyboardState = currentKeyboardState;
            previousGamePadState = currentGamePadState;

            base.Update(gameTime);
        }

        /// <summary>
        /// Checks the button sequence at the start menu for the sole purpose of loading the developer room
        /// </summary>
        /// <param name="currentKeyboardState"></param>
        /// <param name="previousKeyboardState"></param>
        /// <param name="currentGamePadState"></param>
        /// <param name="previousGamePadState"></param>
        private void checkButtonSequence(KeyboardState currentKeyboardState, KeyboardState previousKeyboardState, GamePadState currentGamePadState, GamePadState previousGamePadState)
        {
            switch (currentButtonSequence)
            {
                case 0:     // look for A
                    if (previousKeyboardState.IsKeyDown(Keys.A) || previousGamePadState.IsButtonDown(Buttons.A))
                        break;
                    else if (currentKeyboardState.IsKeyDown(Keys.A) || currentGamePadState.IsButtonDown(Buttons.A))
                        ++currentButtonSequence;
                    break;
                case 1:     // look for B
                    if (previousKeyboardState.IsKeyDown(Keys.A) || previousGamePadState.IsButtonDown(Buttons.A))
                        break;
                    else if (currentKeyboardState.IsKeyDown(Keys.B) || currentGamePadState.IsButtonDown(Buttons.B))
                        ++currentButtonSequence;
                    else
                        currentButtonSequence = 0;
                    break;
                case 2:     // look for A
                    if (previousKeyboardState.IsKeyDown(Keys.B) || previousGamePadState.IsButtonDown(Buttons.B))
                        break;
                    else if (currentKeyboardState.IsKeyDown(Keys.A) || currentGamePadState.IsButtonDown(Buttons.A))
                        ++currentButtonSequence;
                    else
                        currentButtonSequence = 0;
                    break;
                case 3:     // look for Y
                    if (previousKeyboardState.IsKeyDown(Keys.A) || previousGamePadState.IsButtonDown(Buttons.A))
                        break;
                    else if (currentKeyboardState.IsKeyDown(Keys.Y) || currentGamePadState.IsButtonDown(Buttons.Y))
                        ++currentButtonSequence;
                    else
                        currentButtonSequence = 0;
                    break;
                case 4:     // look for A
                    if (previousKeyboardState.IsKeyDown(Keys.Y) || previousGamePadState.IsButtonDown(Buttons.Y))
                        break;
                    else if (currentKeyboardState.IsKeyDown(Keys.A) || currentGamePadState.IsButtonDown(Buttons.A))
                        ++currentButtonSequence;
                    else
                        currentButtonSequence = 0;
                    break;
                case 5:     // look for B
                    if (previousKeyboardState.IsKeyDown(Keys.A) || previousGamePadState.IsButtonDown(Buttons.A))
                        break;
                    else if (currentKeyboardState.IsKeyDown(Keys.B) || currentGamePadState.IsButtonDown(Buttons.B))
                        ++currentButtonSequence;
                    else
                        currentButtonSequence = 0;
                    break;
                case 6:     // look for B
                    if (previousKeyboardState.IsKeyDown(Keys.B) || previousGamePadState.IsButtonDown(Buttons.B))
                        break;
                    else if (currentKeyboardState.IsKeyDown(Keys.B) || currentGamePadState.IsButtonDown(Buttons.B))
                    {
                        // Developer room!!
                        // Initialize developer level
                        gameState = State.RUNNING;
                        initLevels(true);
                    }
                    else
                        currentButtonSequence = 0;
                    break;
                default:   // Should never be here
                    currentButtonSequence = 0;
                    break;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (currentLevel == null)
            {
                spriteBatch.Draw(vPlayer.GetTexture(), GraphicsDevice.Viewport.Bounds, Color.White);
                Vector2 v1 = new Vector2(150, 300);
                spriteBatch.DrawString(normalText, "No levels to load", v1, Color.White);
            }
            else if (gameState == State.TITLE || currentLevel.loadedState == LoadingState.loading)
            {
                spriteBatch.Draw(vPlayer.GetTexture(), GraphicsDevice.Viewport.Bounds, Color.White);
                if (currentLevel.loadedState == LoadingState.loading)
                {
                    Vector2 v1 = new Vector2(200, 300);
                    spriteBatch.DrawString(normalText, "Loading Level...", v1, Color.White);
                }
            }
            else
            {
                // Draw items on current level
                currentLevel.Draw(spriteBatch);
                if (gameState == State.PAUSE)
                {
                    Vector2 v1 = new Vector2(200, 100);
                    spriteBatch.DrawString(normalText, "Game Paused", v1, Color.White);
                }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Initializes all the levels on disk. Also loads the first level
        /// </summary>
        private void initLevels(bool isdeveloper)
        {
            currentLevel = null;
            if (isdeveloper)
            {
                vPlayer.Pause();
                currentLevel = new Level(GraphicsDevice, "dummyLevel");
                currentLevel.LoadLevel(Content, PlayerIndex.One);
                return;
            }

            // parse directory of levels
            int i = 1;
            Level previousLevel = null;
            while (i <= Directory.GetFiles(Content.RootDirectory + "/levels", "level*.xml").Length)
            {
                Level nextLevel;

                // Only increment previous level after the second loop
                if (i > 2)
                    previousLevel = previousLevel.nextLevel;

                // Check for the next level
                string curLevelName = "level" + i;
                if (File.Exists(Content.RootDirectory + "/levels/" + curLevelName))
                {
                    nextLevel = new Level(GraphicsDevice, curLevelName);
                    if (currentLevel == null)
                        currentLevel = previousLevel = nextLevel;
                    else
                        previousLevel.nextLevel = nextLevel;
                }
                else
                    break;
                ++i;
            }

            if (currentLevel != null)
                currentLevel.LoadLevel(Content, PlayerIndex.One);
        }
    }
}
