/* TextDisplay.cs
 * Purpose: Drawable component for displaying text on screen
 * 
 * Revision History:
 *      Drew Matheson, 2014.10.30: Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelicopterMadness
{
    /// <summary>
    ///     Displays text on screen
    /// </summary>
    public class TextDisplay : DrawableGameComponent
    {
        private readonly SpriteBatch spriteBatch;
        private readonly Color color;

        /// <summary>
        ///     Initializes a new instace of TextDisplay with the provided parameters
        /// </summary>
        /// <param name="game">The game the TextDisplay belongs to</param>
        /// <param name="spriteBatch">The SpriteBatch the TextDisplay will draw itself with</param>
        /// <param name="font">The font used for the TextDisplay</param>
        /// <param name="position">The position of the TextDisplay on screen</param>
        /// <param name="color">The color of the TextDisplay's text</param>
        public TextDisplay(Game game, SpriteBatch spriteBatch, SpriteFont font,
            Vector2 position, Color color)
            : base(game)
        {
            this.spriteBatch = spriteBatch;

            Font = font;
            Position = position;
            this.color = color;

            Message = string.Empty;
        }

        /// <summary>
        ///     Gets the TextDisplay's Font
        /// </summary>
        public SpriteFont Font { get; private set; }

        /// <summary>
        ///     Gets or Sets the TextDisplay's Position
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        ///     Gets or Sets the TextDisplay's Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Draws the TextDisplay
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.DrawString(Font, Message, Position, color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}