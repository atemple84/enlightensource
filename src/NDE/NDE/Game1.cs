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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //static List<CollisionSprite> platforms;
        Sprite background;

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

            // TODO: use this.Content to load your game content here
            background.LoadContent(Content, "background");
            PlayerSprite dummyPlayer = new PlayerSprite();
            dummyPlayer.LoadContent(Content);
            playerList.list().Add(dummyPlayer);

            CollisionSprite dummyPlatform = new CollisionSprite(GraphicsDevice.Viewport.Width);
            dummyPlatform.LoadContent(Content);
            platformList.list().Add(dummyPlatform);
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

            foreach (CollisionSprite curPlatform in platformList.list())
                curPlatform.Update(gameTime);

            // TODO: Add your update logic here
            foreach (PlayerSprite curPlayer in playerList.list())
                curPlayer.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(background.getTexture(), GraphicsDevice.Viewport.Bounds, Color.White);
            foreach (CollisionSprite curPlatform in platformList.list())
                spriteBatch.Draw(curPlatform.getTexture(), curPlatform.position, curPlatform.getArea(), curPlatform.color, curPlatform.rotation, Vector2.Zero, curPlatform.scale, SpriteEffects.None, 0f);
            foreach (PlayerSprite curPlayer in playerList.list())
                spriteBatch.Draw(curPlayer.getTexture(), curPlayer.position, curPlayer.getArea(), curPlayer.color, curPlayer.rotation, Vector2.Zero, curPlayer.scale, SpriteEffects.None, 0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
