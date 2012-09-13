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
    static class platformList
    {
        static List<CollisionSprite> platforms;
        static platformList()
        {
            platforms = new List<CollisionSprite>();
        }

        public static List<CollisionSprite> list()
        {
            return platforms;
        }
    }

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
        Sprite background;

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
        private Texture2D myBottomLine;

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
            background = new Sprite();

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

            background.LoadContent(Content, "background");
            PlayerSprite dummyPlayer = new PlayerSprite(PlayerIndex.One);
            dummyPlayer.LoadContent(Content);
            playerList.list().Add(dummyPlayer);

            CollisionSprite dummyPlatform = new CollisionSprite(GraphicsDevice.Viewport.Width);
            dummyPlatform.LoadContent(Content);
            platformList.list().Add(dummyPlatform);
            normalText = Content.Load<SpriteFont>("NormalText");

            myBottomLine = new Texture2D(GraphicsDevice, 1, 1);
            myBottomLine.SetData(new[] { Color.Black });
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
                        gameState = State.RUNNING;
                    break;
                case State.RUNNING:
                    if (currentKeyboardState.IsKeyDown(Keys.P) || currentGamePadState.IsButtonDown(Buttons.Start))
                        gameState = State.PAUSE;
                    else
                    {
                       foreach (CollisionSprite curPlatform in platformList.list())
                            curPlatform.Update(gameTime);

                        foreach (PlayerSprite curPlayer in playerList.list())
                            curPlayer.Update(gameTime);
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
            spriteBatch.Draw(background.getTexture(), GraphicsDevice.Viewport.Bounds, Color.White);

            if (gameState == State.TITLE)
                spriteBatch.Draw(vPlayer.GetTexture(), GraphicsDevice.Viewport.Bounds, Color.White);
            else
            {
                spriteBatch.Draw(myBottomLine, bottomPoint, null, Color.Black, 0, Vector2.Zero, new Vector2(GraphicsDevice.Viewport.Width, 1), SpriteEffects.None, 0);

                foreach (CollisionSprite curPlatform in platformList.list())
                    spriteBatch.Draw(curPlatform.getTexture(), curPlatform.position, null, curPlatform.color, curPlatform.rotation, Vector2.Zero, curPlatform.scale, SpriteEffects.None, 0f);
                foreach (PlayerSprite curPlayer in playerList.list())
                    spriteBatch.Draw(curPlayer.getTexture(), curPlayer.position, null, curPlayer.color, curPlayer.rotation, Vector2.Zero, curPlayer.scale, SpriteEffects.None, 0f);


                if (gameState == State.PAUSE)
                {
                    Vector2 v1 = new Vector2(200, 100);
                    spriteBatch.DrawString(normalText, "Game Paused", v1, Color.White);
                }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
