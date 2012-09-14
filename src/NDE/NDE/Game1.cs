using System;
using System.Collections.Generic;
using System.Linq;
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
            currentLevel = new Level(GraphicsDevice);

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
                    if (currentKeyboardState.IsKeyDown(Keys.Enter) || currentGamePadState.IsButtonDown(Buttons.Start))
                    {
                        // Initialize all the levels
                        gameState = State.RUNNING;
                        initLevels();
                    }
                    break;
                case State.RUNNING:
                    if (currentKeyboardState.IsKeyDown(Keys.P) || currentGamePadState.IsButtonDown(Buttons.Start))
                        gameState = State.PAUSE;
                    else
                    {
                        // Check the level status
                        if (currentLevel.runningState == CompletionState.complete)
                        {
                            currentLevel = currentLevel.nextLevel;
                            if (currentLevel == null)   // YOU WIN!!!  Game Over
                                this.Exit();
                        }

                        // Begin loading the new level
                        if (currentLevel.loadedState == LoadingState.uninitialized)
                            currentLevel.LoadLevel(Content);

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
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            if (gameState == State.TITLE)
                spriteBatch.Draw(vPlayer.GetTexture(), GraphicsDevice.Viewport.Bounds, Color.White);
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
        private void initLevels()
        {
            // TODO parse directory of levels
            currentLevel.levelId = "level1.xml";

            currentLevel.LoadLevel(Content);
        }
    }
}
