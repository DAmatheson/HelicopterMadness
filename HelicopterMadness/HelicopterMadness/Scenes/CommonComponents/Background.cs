// Background.cs
// Purpose: Class for drawing a looping background
// 
// Revision History:
//      Drew Matheson, 2014.11.19: Created

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelicopterMadness.Scenes.CommonComponents
{
    /// <summary>
    ///     Background sprite for the game
    /// </summary>
    internal class Background : Sprite
    {
        private Vector2 secondPosition;

        /// <summary>
        ///     Initializes a background with the provided parameters
        /// </summary>
        /// <param name="game">The Game the Background belongs to</param>
        /// <param name="spriteBatch">The SpriteBatch the Background will draw itself with</param>
        /// <param name="texture">The texture for the Background</param>
        public Background(Game game, SpriteBatch spriteBatch, Texture2D texture)
            : base(game, spriteBatch, texture)
        {
            secondPosition = position;
            secondPosition.X = texture.Width;
        }

        /// <summary>
        ///     Updates the backgrounds position to keep it looping
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            float movement = SharedSettings.StageSpeed.X * (float) gameTime.ElapsedGameTime.TotalSeconds *
                0.65f;

            position.X -= movement;
            secondPosition.X -= movement;

            if (position.X + texture.Width < 0) // First background is offscreen
            {
                position.X = secondPosition.X + texture.Width;
            }
            else if (secondPosition.X + texture.Width < 0) // Second background is offscreen
            {
                secondPosition.X = position.X + texture.Width;
            }
        }

        /// <summary>
        ///     Draws the background
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(
                texture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            spriteBatch.Draw(
                texture, secondPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }
    }
}