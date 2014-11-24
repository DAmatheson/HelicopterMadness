/* HelicopterGame.cs
 * Purpose: Main game class
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.05: Created
 */

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelicopterMadness
{
    /// <summary>
    ///     The main class for the Helicopter game
    /// </summary>
    public class HelicopterGame : Game
    {
#if DEBUG
        SpriteFont spriteFont;

        int frameRate = 0;
        int frameCounter = 0;
        TimeSpan elapsedTime = TimeSpan.Zero;
#endif

        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private readonly Color bufferColor = new Color(168, 146, 118);

        private SceneManager sceneManager;

        /// <summary>
        ///     Initializes a new instance of HelicopterGame
        /// </summary>
        public HelicopterGame()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
        }

        /// <summary>
        ///     Initializes the games state and non-graphic related content
        /// </summary>
        protected override void Initialize()
        {
            SharedSettings.Stage.X = graphics.GraphicsDevice.Viewport.Width;
            SharedSettings.Stage.Y = graphics.GraphicsDevice.Viewport.Height;

            SharedSettings.StageCenter.X = SharedSettings.Stage.X / 2f;
            SharedSettings.StageCenter.Y = SharedSettings.Stage.Y / 2f;

            base.Initialize();
        }

        /// <summary>
        ///     Loads all of the game's content and creates its components
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            sceneManager = new SceneManager(this, spriteBatch);

#if DEBUG
            spriteFont = Content.Load<SpriteFont>("Fonts/FrameRate");
#endif
            Components.Add(sceneManager);
        }

        /// <summary>
        ///     Updates the state of the game and all of its components
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        protected override void Update(GameTime gameTime)
        {
            if (IsActive) // TODO: Worth doing? Pauses game when the window is not in focus
            {
                base.Update(gameTime);
#if DEBUG
                elapsedTime += gameTime.ElapsedGameTime;

                if (elapsedTime > TimeSpan.FromSeconds(1))
                {
                    elapsedTime -= TimeSpan.FromSeconds(1);
                    frameRate = frameCounter;
                    frameCounter = 0;
                }
#endif
            }
        }

        /// <summary>
        ///     Draws the game and all of its components
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(bufferColor);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
#if DEBUG
            frameCounter++;

            string fps = string.Format("fps: {0}", frameRate);
#endif
            base.Draw(gameTime);
#if DEBUG           
            spriteBatch.DrawString(spriteFont, fps, new Vector2(33, 33), Color.Black);
            spriteBatch.DrawString(spriteFont, fps, new Vector2(32, 32), Color.White);
#endif
            spriteBatch.End();
        }
    }
}