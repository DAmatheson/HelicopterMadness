/* Border.cs
 * Purpose: Collidable screen boarder for the game
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.05: Created
 */

using HelicopterMadness.Scenes.CommonComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelicopterMadness.Scenes.ActionComponents
{
    /// <summary>
    ///     A collidable border that loops forever
    /// </summary>
    public class Border : Sprite, ICollidable
    {
        private Vector2 secondPosition;

        private readonly SpriteEffects spriteEffect;

        /// <summary>
        ///     Initializes a new instance of Border with the provided parameters
        /// </summary>
        /// <param name="game">The Game the Border belongs to</param>
        /// <param name="spriteBatch">The SpriteBatch the Border will draw itself with</param>
        /// <param name="texture">The texture for the border</param>
        /// <param name="position">The position for the border</param>
        /// <param name="flipped">If true, the border texture will be flipped</param>
        public Border(
            Game game, SpriteBatch spriteBatch, Texture2D texture, Vector2 position, bool flipped = false)
            : base(game, spriteBatch, texture, position)
        {
            spriteEffect = flipped
                ? SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically
                : SpriteEffects.None;

            secondPosition = position;
            secondPosition.X = texture.Width;

            Enabled = false;
        }

        /// <summary>
        ///     Updates the Borders position
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            position.X -= SharedSettings.StageSpeed.X;
            secondPosition.X -= SharedSettings.StageSpeed.X;

            if (position.X + texture.Width < 0) // First border is offscreen
            {
                position.X = secondPosition.X + texture.Width;
            }
            else if (secondPosition.X + texture.Width < 0) // Second border is offscreen
            {
                secondPosition.X = position.X + texture.Width;
            }
        }

        /// <summary>
        ///     Draws the Border
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1f, spriteEffect, 1f);
            spriteBatch.Draw(texture, secondPosition, null, Color.White, 0f, Vector2.Zero, 1f, spriteEffect, 1f);
        }

        /// <summary>
        ///     Gets the bounds of the Border
        /// </summary>
        /// <returns>The <see cref="Rectangle"/> bounds of the Border</returns>
        public Rectangle GetBounds()
        {
            return new Rectangle(0, (int) position.Y, texture.Width, texture.Height);
        }

        /// <summary>
        ///     Notifies the Border that it has been in a collision
        /// </summary>
        /// <param name="otherCollidable">The ICollidable the Border collided with</param>
        public void OnCollision(ICollidable otherCollidable)
        {
            // TODO: Collision sound
        }
    }
}