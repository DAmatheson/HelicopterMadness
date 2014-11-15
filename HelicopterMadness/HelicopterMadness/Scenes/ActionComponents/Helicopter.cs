/* Helicopter.cs
 * Purpose: Player controller helicopter for the game
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.04: Created
 */

using HelicopterMadness.Scenes.CommonComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HelicopterMadness.Scenes.ActionComponents
{
    /// <summary>
    ///     The Helicopter that the player controls
    /// </summary>
    public class Helicopter : AnimatedSprite, ICollidable
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
        /// <param name="frameDimensions">The length and width of a single frame from the texture</param>
        /// <param name="explosion">The Explosion the helicopter will enable when crashing</param>
        public Helicopter(Game game, SpriteBatch spriteBatch, Texture2D texture,
            Vector2 position, Vector2 frameDimensions, Explosion explosion)
            : base(game, spriteBatch, texture, position, frameDimensions)
        {
            speed = new Vector2(0, 0);

            this.explosion = explosion;

            initialPosition = position;
        }

        /// <summary>
        ///     Updates the Helicopter's state
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (!HasCrashed)
            {
                float accelerate;

                float stageSpeedChange = SharedSettings.StageSpeedChange;

                if (keyboardState.IsKeyDown(Keys.Up) || mouseState.LeftMouseClicked())
                {
                    // Avg time between updates = ~0.0166667
                    // Desired base up movement speed = -0.42 
                    // 0.42 * (VERTICAL_SPEED / 0.0166667) = 3.60
                    accelerate = -(VERTICAL_SPEED * 3.60f * (float)gameTime.ElapsedGameTime.TotalSeconds *
                        stageSpeedChange);
                }
                else
                {
                    // Avg time between updates = ~0.0166667
                    // Desired base down movement speed = 0.7 
                    // 0.7 * (VERTICAL_SPEED / 0.0166667) = ~6
                    accelerate = VERTICAL_SPEED * 6f * (float)gameTime.ElapsedGameTime.TotalSeconds * 
                        stageSpeedChange;
                }

                speed.Y = MathHelper.Clamp(speed.Y + accelerate, -VERTICAL_SPEED * stageSpeedChange, VERTICAL_SPEED * stageSpeedChange);

                position.Y += speed.Y;

                base.Update(gameTime);
            }
        }

        /// <summary>
        ///     Gets the bounds of the Helicopter
        /// </summary>
        /// <returns>The <see cref="Rectangle"/> bounds of the Helicopter</returns>
        public Rectangle GetBounds()
        {
            return new Rectangle((int) position.X, (int) position.Y, (int)frameDimensions.X, (int)frameDimensions.Y);
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
                position.X + frameDimensions.X / 2f,
                position.Y + frameDimensions.Y / 2f);

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