/* HelicopterGame.cs
 * Purpose: Main game class
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.05: Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelicopterMadness
{
    /// <summary>
    ///     The main class for the Helicopter game
    /// </summary>
    public class HelicopterGame : Game
    {
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

            Components.Add(sceneManager);
        }

        /// <summary>
        ///     Updates the state of the game and all of its components
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        protected override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                base.Update(gameTime);
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

            base.Draw(gameTime);

            spriteBatch.End();
        }
    }
}