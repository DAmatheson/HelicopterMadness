/* Explosion.cs
 * Purpose: Animated explosion
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.05: Created
 */ 

using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelicopterMadness.Scenes.ActionComponents
{
    // TODO: Comments
    public class Explosion : DrawableGameComponent
    {
        private readonly SpriteBatch spriteBatch;
        private readonly Texture2D texture;
        private readonly Vector2 origin;
        private readonly int delay;

        private Vector2 position;
        private Vector2 dimensions;
        private List<Rectangle> frames;

        private int frameIndex = -1;
        private int delayCounter;

        public Explosion(Game game, SpriteBatch spriteBatch, Texture2D texture,
            Vector2 position, Vector2 frameDimension, int delay)
            : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.position = position;
            this.texture = texture;
            this.delay = delay;

            origin = new Vector2(frameDimension.X / 2f, frameDimension.Y / 2f);

            dimensions = frameDimension;

            CreateFrames();
            StopAnimation();
        }

        public Vector2 Position
        {
            set { position = value; }
        }

        private void CreateFrames()
        {
            frames = new List<Rectangle>();

            for (int height = 0; height < texture.Height; height += (int)dimensions.Y)
            {
                for (int width = 0; width < texture.Width; width += (int)dimensions.X)
                {
                    Rectangle frame = new Rectangle(width, height, (int)dimensions.X, (int)dimensions.Y);

                    frames.Add(frame);
                }
            }
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

                if (frameIndex == frames.Count())
                {
                    StopAnimation();
                }

                delayCounter = 0;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (frameIndex >= 0 && frameIndex < frames.Count())
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