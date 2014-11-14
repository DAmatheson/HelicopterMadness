/* AnimatedSprite.cs
 * Purpose: A base class for an animated sprite
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.13: Created
 */

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelicopterMadness.Scenes.CommonComponents
{
    /// <summary>
    ///     Base class for a drawable animated sprite
    /// </summary>
    public abstract class AnimatedSprite : Sprite
    {
        /// <summary>
        ///     The length and width of a single frame
        /// </summary>
        protected readonly Vector2 frameDimensions;

        protected List<Rectangle> frames;
        protected int frameIndex = 0;

        /// <summary>
        ///     Initializes a new instance of AnimatedSprite with the provided parameters
        /// </summary>
        /// <param name="game">The Game the AnimatedSprite belongs to</param>
        /// <param name="spriteBatch">The SpriteBatch the AnimatedSprite will draw itself with</param>
        /// <param name="texture">The texture for the AnimatedSprite</param>
        /// <param name="position">The initial position of the AnimatedSprite</param>
        /// <param name="frameDimensions">The length and width of a single frame from the texture</param>
        protected AnimatedSprite(
            Game game, SpriteBatch spriteBatch, Texture2D texture, Vector2 position,
            Vector2 frameDimensions)
            : base(game, spriteBatch, texture, position)
        {
            this.frameDimensions = frameDimensions;

            GenerateFrames(frameDimensions);
        }

        /// <summary>
        ///     Loops through the animation frames
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            frameIndex++;

            if (frameIndex == frames.Count)
            {
                frameIndex = 0;
            }
        }

        /// <summary>
        ///     Draws the Helicopter
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            if (!Enabled)
            {
                spriteBatch.Draw(texture, position, frames[0], Color.White);
            }
            else if (frameIndex >= 0 && frameIndex < frames.Count)
            {
                spriteBatch.Draw(texture, position, frames[frameIndex], Color.White);
            }
        }

        /// <summary>
        ///     Generates the frames from the texture
        /// </summary>
        /// <param name="dimensions"></param>
        private void GenerateFrames(Vector2 dimensions)
        {
            frames = new List<Rectangle>();

            for (int height = 0; height < texture.Height; height += (int) dimensions.Y)
            {
                for (int width = 0; width < texture.Width; width += (int) dimensions.X)
                {
                    Rectangle frame = new Rectangle(width, height, (int) dimensions.X, (int) dimensions.Y);

                    frames.Add(frame);
                }
            }
        }
    }
}