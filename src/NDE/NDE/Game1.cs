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
        List<PlayerSprite> players;
        List<CollisionSprite> platforms;
        Sprite background;

        // Video variables
        VideoPlayer vPlayer;
        Video titleVideo;

        // Keyboard states used to determine key presses
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        // Set state of game
        State gameState = State.TITLE;
        
        // For generic text writing
        SpriteFont normalText;

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
            players = new List<PlayerSprite>();
            platforms = new List<CollisionSprite>();
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
            PlayerSprite dummyPlayer = new PlayerSprite();
            dummyPlayer.LoadContent(Content);
            players.Add(dummyPlayer);

            CollisionSprite dummyPlatform = new CollisionSprite();
            dummyPlatform.LoadContent(Content);
            platforms.Add(dummyPlatform);

            normalText = Content.Load<SpriteFont>("NormalText");
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // State transitions
            if (currentKeyboardState.IsKeyDown(Keys.P) && gameState == State.RUNNING) { 
                gameState = State.PAUSE; 
            }

            if (currentKeyboardState.IsKeyDown(Keys.R) && gameState == State.PAUSE)
            {
                gameState = State.RUNNING;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Enter)){ gameState = State.RUNNING; }

            if (gameState == State.RUNNING)
            {
                foreach (PlayerSprite curPlayer in players)
                    curPlayer.Update(gameTime);
            }
            
            // Save previous keyboard state
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

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
            {
                spriteBatch.Draw(vPlayer.GetTexture(), GraphicsDevice.Viewport.Bounds, Color.White);
            }

            if (gameState == State.RUNNING)
            {    
                spriteBatch.Draw(background.getTexture(), GraphicsDevice.Viewport.Bounds, Color.White);
                foreach (CollisionSprite curPlatform in platforms)
                    spriteBatch.Draw(curPlatform.getTexture(), curPlatform.position, curPlatform.getArea(), curPlatform.color, curPlatform.rotation, Vector2.Zero, curPlatform.scale, SpriteEffects.None, 0f);
                foreach (PlayerSprite curPlayer in players)
                    spriteBatch.Draw(curPlayer.getTexture(), curPlayer.position, curPlayer.getArea(), curPlayer.color, curPlayer.rotation, Vector2.Zero, curPlayer.scale, SpriteEffects.None, 0f);
            }

            if (gameState == State.PAUSE)
            {
                spriteBatch.Draw(background.getTexture(), GraphicsDevice.Viewport.Bounds, Color.White);
                foreach (CollisionSprite curPlatform in platforms)
                    spriteBatch.Draw(curPlatform.getTexture(), curPlatform.position, curPlatform.getArea(), curPlatform.color, curPlatform.rotation, Vector2.Zero, curPlatform.scale, SpriteEffects.None, 0f);
                foreach (PlayerSprite curPlayer in players)
                    spriteBatch.Draw(curPlayer.getTexture(), curPlayer.position, curPlayer.getArea(), curPlayer.color, curPlayer.rotation, Vector2.Zero, curPlayer.scale, SpriteEffects.None, 0f);
                
                Vector2 v1 = new Vector2(200, 100);
                spriteBatch.DrawString(normalText, "Game Paused", v1, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
