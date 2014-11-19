/* Explosion.cs
 * Purpose: Animated explosion
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
    ///     An class for displaying an animated explosion
    /// </summary>
    public class Explosion : AnimatedSprite
    {
        private readonly Vector2 origin;
        private readonly int delay;

        private int delayCounter;

        /// <summary>
        ///     Initializes a new instance of Explosion with the provided parameters
        /// </summary>
        /// <param name="game">The Game the explosion belongs to</param>
        /// <param name="spriteBatch">The SpriteBatch the explosion will draw itself with</param>
        /// <param name="texture">The sprite sheet for the explosion</param>
        /// <param name="frameDimension">The dimensions of a single frame from the sprite sheet</param>
        /// <param name="delay">The amount of time between frame changes</param>
        public Explosion(Game game, SpriteBatch spriteBatch, Texture2D texture,
            Vector2 frameDimension, int delay)
            : base(game, spriteBatch, texture, Vector2.Zero, frameDimension)
        {
            this.delay = delay;

            origin = new Vector2(frameDimension.X / 2f, frameDimension.Y / 2f);

            StopAnimation();
        }

        /// <summary>
        ///     Sets the position of the explosion
        /// </summary>
        public Vector2 Position
        {
            set { position = value; }
        }

        /// <summary>
        ///     Keeps the explosion running for a single loop of the sprite sheet
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            delayCounter++;

            if (delayCounter > delay)
            {
                frameIndex++;

                if (frameIndex == frames.Count)
                {
                    StopAnimation();
                }

                delayCounter = 0;
            }
        }

        /// <summary>
        ///     Draws the animated explosion
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            if (frameIndex >= 0 && frameIndex < frames.Count)
            {
                spriteBatch.Draw(texture, position, frames[frameIndex], Color.White, 0f,
                    origin, Vector2.One, SpriteEffects.None, 0f);
            }
        }

        /// <summary>
        ///     Hides, disables, and reset the explosion
        /// </summary>
        public void StopAnimation()
        {
            Enabled = false;
            Visible = false;

            frameIndex = -1;
        }

        /// <summary>
        ///     Makes the explosion enabled and visible.
        ///     Set the position of the explosion before calling this
        /// </summary>
        public void StartAnimation()
        {
            Enabled = true;
            Visible = true;
        }
    }
}