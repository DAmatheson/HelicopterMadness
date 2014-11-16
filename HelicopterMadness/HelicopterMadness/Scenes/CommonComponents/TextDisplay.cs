/* TextDisplay.cs
 * Purpose: Drawable component for displaying text on screen
 * 
 * Revision History:
 *      Drew Matheson, 2014.10.30: Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelicopterMadness.Scenes.CommonComponents
{
    /// <summary>
    ///     Displays text on screen
    /// </summary>
    public class TextDisplay : DrawableGameComponent
    {
        protected readonly SpriteBatch spriteBatch;
        protected  Color color;

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

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
            Color = color;

            Enabled = false; // TODO: Relook over this. Nothing happens in update currently.

            Message = string.Empty;
        }

        /// <summary>
        ///     Initializes a new instace of TextDisplay with the provided parameters
        /// </summary>
        /// <param name="game">The game the TextDisplay belongs to</param>
        /// <param name="spriteBatch">The SpriteBatch the TextDisplay will draw itself with</param>
        /// <param name="font">The font used for the TextDisplay</param>
        /// <param name="color">The color of the TextDisplay's text</param>
        public TextDisplay(Game game, SpriteBatch spriteBatch, SpriteFont font, Color color)
            : this(game, spriteBatch, font, Vector2.Zero, color)
        { }

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