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
    // TODO: Comments
    public class Explosion : AnimatedSprite
    {
        private readonly Vector2 origin;
        private readonly int delay;

        private int delayCounter;

        public Explosion(Game game, SpriteBatch spriteBatch, Texture2D texture,
            Vector2 position, Vector2 frameDimension, int delay)
            : base(game, spriteBatch, texture, position, frameDimension)
        {
            this.delay = delay;

            origin = new Vector2(frameDimension.X / 2f, frameDimension.Y / 2f);

            StopAnimation();
        }

        public Vector2 Position
        {
            set { position = value; }
        }

        /// <summary>
        /// Allows the game component to update itself.
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

        public override void Draw(GameTime gameTime)
        {
            if (frameIndex >= 0 && frameIndex < frames.Count)
            {
                spriteBatch.Draw(texture, position, frames[frameIndex], Color.White, 0f,
                    origin, Vector2.One, SpriteEffects.None, 0f);
            }
        }

        public void StopAnimation()
        {
            Enabled = false;
            Visible = false;

            frameIndex = -1;
        }

        public void StartAnimation()
        {
            Enabled = true;
            Visible = true;
        }
    }
}