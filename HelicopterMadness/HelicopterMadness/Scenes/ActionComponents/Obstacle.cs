/* Obstacle.cs
 * Purpose: Obstacle to be avoided by the player
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.06: Created
 */

using HelicopterMadness.Scenes.CommonComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace HelicopterMadness.Scenes.ActionComponents
{
    /// <summary>
    ///     A simple collidable obstacle
    /// </summary>
    public class Obstacle : Sprite, ICollidable
    {
        private SoundEffect collisionSound;

        /// <summary>
        ///     Initializes a new instace of Obstacle with the provided parameters
        /// </summary>
        /// <param name="game">The Game the Obstacle belongs to</param>
        /// <param name="spriteBatch">The SpriteBatch the Obstacle will draw itself with</param>
        /// <param name="texture">The texture for the Obstacle</param>
        /// <param name="collisionSound">The sound effect to play for a collision with the obstacle</param>
        public Obstacle(Game game, SpriteBatch spriteBatch, Texture2D texture, SoundEffect collisionSound)
            : base(game, spriteBatch, texture)
        {
            this.collisionSound = collisionSound;
        }

        /// <summary>
        ///     Gets the current position of the obstacle
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
        }

        /// <summary>
        ///     Updates the Obstacle's state
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            if (position.X + texture.Width < 0)
            {
                Enabled = false;
                Visible = false;
            }

            position.X -= SharedSettings.StageSpeed.X * (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }

        /// <summary>
        ///     Draws the Obstacle
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, SharedSettings.OBSTACLE_LAYER + 0.01f);
        }

        /// <summary>
        ///     Gets the bounds of the Obstacle
        /// </summary>
        /// <returns>The <see cref="Rectangle"/> bounds of the Obstacle</returns>
        public Rectangle GetBounds()
        {
            return new Rectangle((int) position.X, (int) position.Y, texture.Width, texture.Height);
        }

        /// <summary>
        ///     Notifies the Obstacle that it has been in a collision
        /// </summary>
        /// <param name="otherCollidable">The ICollidable the Obstacle collided with</param>
        public void OnCollision(ICollidable otherCollidable)
        {
            collisionSound.Play(0.60f, 0f, 0f);

            Enabled = false;
        }

        /// <summary>
        ///     Enables and shows the Obstacle
        /// </summary>
        public void Show()
        {
            Enabled = true;
            Visible = true;
        }

        /// <summary>
        ///     Disables and hides the Obstacle
        /// </summary>
        public void Hide()
        {
            Enabled = false;
            Visible = false;
        }

        /// <summary>
        ///     Generates a random position for the Obstacle to start at
        /// </summary>
        /// <param name="closestXPosition">The X position to offset the Obstacle from</param>
        /// <param name="minXSpacing">The minimum X spacing between this and the closest ICollidable</param>
        /// <param name="maxXSpacing">The maximum X spacing between this and the closest ICollidable</param>
        public void GenerateRandomPosition(float closestXPosition, float minXSpacing, float maxXSpacing)
        {
            float paddingXSpace = (float) SharedSettings.Random.NextDouble() *
                (maxXSpacing - minXSpacing) + minXSpacing;

            float positionY = (float) SharedSettings.Random.NextDouble() *
                (SharedSettings.Stage.Y - texture.Height);

            position = new Vector2(closestXPosition + paddingXSpace, positionY);
        }
    }
}