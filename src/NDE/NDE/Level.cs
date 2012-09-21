using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using LevelDataTypes;

namespace NDE
{
    enum LoadingState
    {
        uninitialized,
        loading,
        complete
    }

    enum CompletionState
    {
        running,
        complete
    }

    class Level
    {
        public LoadingState loadedState;
        public CompletionState runningState;

        private Texture2D myBlankTexture;
        private GraphicsDevice myGraphicsDevice;
        private ContentManager myContent;

        private List<Sprite> myLevelSprites = new List<Sprite>();
        static readonly object locker = new object();

        public Level nextLevel = null;
        private string myLevelId;

        /// <summary>
        /// Constructor. MUST use graphics device for creating extra textures, and
        ///              using the viewport
        /// </summary>
        /// <param name="theGraphicsDevice"></param>
        public Level(GraphicsDevice theGraphicsDevice, string levelId)
        {
            myGraphicsDevice = theGraphicsDevice;
            loadedState = LoadingState.uninitialized;
            runningState = CompletionState.running;
            myLevelId = levelId;
        }

        /// <summary>
        /// Loads the current level. Build all the sprites on the screen
        /// </summary>
        /// <param name="theContent"></param>
        public void LoadLevel(ContentManager theContent)
        {
            // Start level loading thread
            loadedState = LoadingState.loading;
            myContent = theContent;
            Thread loadLevelThread = new Thread(LoadingThread);
            loadLevelThread.Start();
        }

        void LoadingThread()
        {
            // Border
            myBlankTexture = new Texture2D(myGraphicsDevice, 1, 1);
            myBlankTexture.SetData(new[] { Color.White });

            // load level by parsing XML
            lock (locker)
            {
                myLevelSprites.Clear();
                LevelData levelData = myContent.Load<LevelData>("levels/" + myLevelId);
                foreach (SpriteData curSprite in levelData.sprites)
                {
                    MovingSprite dummySprite = new MovingSprite(myGraphicsDevice.Viewport.Width, (collisionType)curSprite.obstacleType);
                    dummySprite.scale = curSprite.scale;
                    dummySprite.spriteName = curSprite.textureName;
                    dummySprite.color = curSprite.color;
                    dummySprite.position = curSprite.position;
                    dummySprite.setSpeed(curSprite.gameSpeed);
                    dummySprite.LoadContent(myContent);
                    myLevelSprites.Add(dummySprite);
                }

                // Load players
                PlayerSprite dummyPlayer = new PlayerSprite(PlayerIndex.One);
                dummyPlayer.scale = 0.15f;
                dummyPlayer.spriteName = "little guy";
                dummyPlayer.LoadContent(myContent);
                playerList.list().Add(dummyPlayer);

                loadedState = LoadingState.complete;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (loadedState != LoadingState.complete)
                return;

            // Update all the level sprites for the level
            foreach (Sprite curSprite in myLevelSprites)
                curSprite.Update(gameTime, myContent);

            foreach (PlayerSprite curPlayer in playerList.list())
                curPlayer.Update(gameTime, myContent);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (loadedState != LoadingState.complete)
                return;

            // Draw all the level sprites
            foreach (Sprite curPlatform in myLevelSprites)
                spriteBatch.Draw(curPlatform.getTexture(), curPlatform.position, null, curPlatform.color, curPlatform.rotation, Vector2.Zero, curPlatform.scale, SpriteEffects.None, 0f);
            foreach (PlayerSprite curPlayer in playerList.list())
                spriteBatch.Draw(curPlayer.getTexture(), curPlayer.position, null, curPlayer.color, curPlayer.rotation, Vector2.Zero, curPlayer.scale, SpriteEffects.None, 0f);

            // Draw any extra borders, etc.
            spriteBatch.Draw(myBlankTexture, Game1.bottomPoint, null, Color.Black, 0, Vector2.Zero, new Vector2(myGraphicsDevice.Viewport.Width, 1), SpriteEffects.None, 0);
        }
    }
}
