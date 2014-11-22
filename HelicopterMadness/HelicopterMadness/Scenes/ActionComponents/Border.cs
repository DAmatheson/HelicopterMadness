/* Border.cs
 * Purpose: Collidable screen boarder for the game
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.05: Created
 */

using HelicopterMadness.Scenes.CommonComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace HelicopterMadness.Scenes.ActionComponents
{
    /// <summary>
    ///     A collidable border that loops forever
    /// </summary>
    public class Border : ScreenLoopSprite, ICollidable
    {
        private SoundEffect collisionSound;

        /// <summary>
        ///     Initializes a new instance of Border with the provided parameters
        /// </summary>
        /// <param name="game">The Game the Border belongs to</param>
        /// <param name="spriteBatch">The SpriteBatch the Border will draw itself with</param>
        /// <param name="texture">The texture for the border</param>
        /// <param name="position">The position for the border</param>
        /// <param name="collisionSound">The sound effect to play for a collision with the border</param>
        /// <param name="speedFactor">
        ///     The speed factor as compared to SharedSettings.StageSpeed with 1 being equal to StageSpeed
        /// </param>
        /// <param name="flipped">If true, the border texture will be flipped</param>
        public Border(Game game, SpriteBatch spriteBatch, Texture2D texture, Vector2 position,
            SoundEffect collisionSound, float speedFactor = 1f, bool flipped = false)
            : base(game, spriteBatch, texture, position, speedFactor, SharedSettings.OBSTACLE_LAYER, flipped)
        {
            Enabled = false;

            this.collisionSound = collisionSound;
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
            collisionSound.Play();
        }
    }
}