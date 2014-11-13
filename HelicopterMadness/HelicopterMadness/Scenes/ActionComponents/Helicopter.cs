/* Helicopter.cs
 * Purpose: Player controller helicopter for the game
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.04: Created
 */

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HelicopterMadness.Scenes.ActionComponents
{
    /// <summary>
    ///     The Helicopter that the player controls
    /// </summary>
    public class Helicopter : Sprite, ICollidable
    {
        private const float VERTICAL_SPEED = 7f;

        private readonly Vector2 initialPosition;

        private readonly Explosion explosion;

        private Vector2 speed;

        public bool HasCrashed { get; private set; }

        /// <summary>
        ///     Initializes a new instance of Helicopter with the provided parameters
        /// </summary>
        /// <param name="game">The Game the Helicopter belongs to</param>
        /// <param name="spriteBatch">The SpriteBatch the Helicopter will draw itself with</param>
        /// <param name="texture">The texture for the Helicopter</param>
        /// <param name="position">The initial position of the Helicopter</param>
        /// <param name="explosion">The Explosion the helicopter will enable when crashing</param>
        public Helicopter(
            Game game, SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Explosion explosion)
            : base(game, spriteBatch, texture, position)
        {
            speed = new Vector2(0, 0);

            this.explosion = explosion;

            initialPosition = new Vector2(
                SharedSettings.Stage.X / 4f - texture.Width / 2f,
                (SharedSettings.Stage.Y - texture.Height) / 2f);

            this.position = initialPosition;
        }

        /// <summary>
        ///     Updates the Helicopter's state
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (!HasCrashed)
            {
                float accelerate;

                if (speed.Y < -0.6f) // Increase downward pull when 
                {
                    accelerate = Math.Abs(speed.Y) * 0.6f * 1.2f;
                }
                else
                {
                    accelerate = Math.Max(speed.Y * 0.1f, 0.09f) * 1.2f;
                }

                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    accelerate -= Math.Max(Math.Abs(speed.Y) / 1.4f, 0.15f) + 0.1f * 1.2f;

                }

                speed.Y = MathHelper.Clamp(speed.Y + accelerate, -VERTICAL_SPEED, VERTICAL_SPEED) * 1.2f;

                position.Y += speed.Y;
            }
        }

        /// <summary>
        ///     Gets the bounds of the Helicopter
        /// </summary>
        /// <returns>The <see cref="Rectangle"/> bounds of the Helicopter</returns>
        public Rectangle GetBounds()
        {
            return new Rectangle((int) position.X, (int) position.Y, texture.Width, texture.Height);
        }

        /// <summary>
        ///     Notifies the Helicopter that it has been in a collision
        /// </summary>
        /// <param name="otherCollidable">The ICollidable the Helicopter collided with</param>
        public void OnCollision(ICollidable otherCollidable)
        {
            HasCrashed = true;

            Enabled = false;

            explosion.Position = new Vector2(
                position.X + texture.Width / 2f,
                position.Y + texture.Height / 2f);

            explosion.StartAnimation();
        }

        /// <summary>
        ///     Resets the Helicopter to its start of game state
        /// </summary>
        public void Reset()
        {
            position = initialPosition;

            Enabled = false;
            Visible = true;
            HasCrashed = false;

            explosion.StopAnimation();

            speed = Vector2.Zero;
        }
    }
}