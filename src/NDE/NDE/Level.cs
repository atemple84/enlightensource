using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

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
        complete,
        dead
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

        private SpriteFont GameOverTextLarge;
        private SpriteFont GameOverTextSmall;
        private List<string> deathStrings;
        private Random deathStringRandomizer = new Random();
        private string failString;
        private PlayerSprite myPlayer;
        public event ChangedEventHandler loadingSignal;

        private List<MovingSprite> collisionSprites = new List<MovingSprite>();
        private MovingSprite myCollidedSprite;

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
            deathStrings = new List<string> {"humiliated", "pwned", "destroyed", "demolished", "embarrassed", "O.J. Simpson'ed", "gutted", "fatalatied", "slapped" };
        }

        /// <summary>
        /// Loads the current level. Build all the sprites on the screen
        /// </summary>
        /// <param name="theContent"></param>
        public void LoadLevel(ContentManager theContent, PlayerIndex player)
        {
            myPlayer = new PlayerSprite(player);
            LoadLevel(theContent);
        }

        private void LoadLevel(ContentManager theContent)
        {
            // Start level loading thread
            loadedState = LoadingState.loading;
            myContent = theContent;
            GameOverTextLarge = myContent.Load<SpriteFont>("Fixedsys_large");
            GameOverTextSmall = myContent.Load<SpriteFont>("Fixedsys_small");

            // Emit signal to let the game know to start loading video
            if (loadingSignal != null)
                loadingSignal(this, true);
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
                    // Loads the sprite
                    MovingSprite dummySprite = new MovingSprite(curSprite.textureName, myGraphicsDevice.Viewport.Width, (collisionTypes)curSprite.obstacleType);
                    dummySprite.LoadContent(myContent);

                    // Set the sprite properties
                    dummySprite.scale = curSprite.scale;
                    dummySprite.color = curSprite.color;
                    dummySprite.position = curSprite.position;
                    dummySprite.repeat = curSprite.repeat;
                    dummySprite.setSpeed(curSprite.gameSpeed);
                    dummySprite.rotation = curSprite.rotation;
                    dummySprite.rotationSpeed = curSprite.rotationSpeed;
                    dummySprite.Changed += new ChangedEventHandler(catchCollisionPhase);

                    // ****** TEMPORARY!!! Until proper collision detection is implemented ******* /
                    if (myCollidedSprite == null && dummySprite.getCollisionType() == collisionTypes.OBSTACLE)
                        myCollidedSprite = dummySprite;

                    myLevelSprites.Add(dummySprite);
                }

                // Load players
                myPlayer.LoadContent(myContent);
                myPlayer.Changed += new ChangedEventHandler(catchPlayerState);
                if (!playerList.list().Contains(myPlayer))
                    playerList.list().Add(myPlayer);

                loadedState = LoadingState.complete;
            }
        }

        private void catchPlayerState(object sender, bool isCompleted)
        {
            if (!isCompleted)
                runningState = CompletionState.dead;
            else
                runningState = CompletionState.complete;
            myPlayer.Changed -= new ChangedEventHandler(catchPlayerState);
        }

        private void catchCollisionPhase(object sender, bool isCollisionState)
        {
            MovingSprite spriteSender = (MovingSprite)sender;
            if (isCollisionState)
                collisionSprites.Add(spriteSender);
            else
                collisionSprites.Remove(spriteSender);
        }

        public void Update(GameTime gameTime)
        {
            if (loadedState != LoadingState.complete)
                return;

            if (runningState == CompletionState.dead)
            {
                KeyboardState currentKeyboardState = Keyboard.GetState();
                GamePadState currentPadState = GamePad.GetState(myPlayer.getPlayerIndex());
                if (currentKeyboardState.IsKeyDown(Keys.Enter) || currentPadState.IsButtonDown(Buttons.Start))
                {
                    // Reload the level
                    runningState = CompletionState.running;
                    collisionSprites.Clear();
                    myCollidedSprite = null;
                    failString = null;
                    LoadLevel(myContent);
                }
            }

            else
            {
                // Update all the level sprites for the level
                foreach (Sprite curSprite in myLevelSprites)
                    curSprite.Update(gameTime, myContent);

                foreach (PlayerSprite curPlayer in playerList.list())
                    curPlayer.Update(gameTime, myContent);

                // TODO detect level detection
                detectLevelCollisions();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (loadedState != LoadingState.complete)
                return;

            if (runningState == CompletionState.dead)
            {
                myGraphicsDevice.Clear(Color.Black);
                Vector2 failLength = GameOverTextLarge.MeasureString("FAIL");
                float failPos = (myGraphicsDevice.Viewport.Width / 2) - (failLength.X / 2);
                spriteBatch.Draw(myBlankTexture, new Vector2(failPos, 40), null, Color.White, 0, Vector2.Zero, new Vector2(failLength.X, failLength.Y - 40), SpriteEffects.None, 0);
                spriteBatch.DrawString(GameOverTextLarge, "FAIL", new Vector2(failPos, 25), Color.Black);
                if (failString == null)
                {
                    failString = "You have been ";
                    failString += deathStrings[deathStringRandomizer.Next(0, deathStrings.Count())] + " by:";
                }

                // Center fail string
                Vector2 failStringLength = GameOverTextSmall.MeasureString(failString);
                float failStringPos = (myGraphicsDevice.Viewport.Width/2) - (failStringLength.X / 2);
                spriteBatch.DrawString(GameOverTextSmall, failString, new Vector2(failStringPos, 200), Color.White);

                float failObjPos = (myGraphicsDevice.Viewport.Width / 2) - (myCollidedSprite.boundingBox.Width / 2);
                spriteBatch.Draw(myCollidedSprite.getTexture(), new Vector2(failObjPos, 280), null, myCollidedSprite.color, 0f, Vector2.Zero, myCollidedSprite.scale, SpriteEffects.None, 0f);
                spriteBatch.DrawString(GameOverTextSmall, "Push Start to restart", new Vector2(225, 380), Color.White);
            }

            else
            {
                // Draw all the level sprites
                foreach (Sprite curPlatform in myLevelSprites)
                    spriteBatch.Draw(curPlatform.getTexture(), curPlatform.position, null, curPlatform.color, curPlatform.rotation, curPlatform.center(), curPlatform.scale, SpriteEffects.None, 0f);
                foreach (PlayerSprite curPlayer in playerList.list())
                    spriteBatch.Draw(curPlayer.getTexture(), curPlayer.position, null, curPlayer.color, curPlayer.rotation, curPlayer.center(), curPlayer.scale, SpriteEffects.None, 0f);

                // Draw any extra borders, etc.
                spriteBatch.Draw(myBlankTexture, Game1.bottomPoint, null, Color.Black, 0, Vector2.Zero, new Vector2(myGraphicsDevice.Viewport.Width, 1), SpriteEffects.None, 0);
            }
        }

        private void detectLevelCollisions()
        {
        }
    }
}
