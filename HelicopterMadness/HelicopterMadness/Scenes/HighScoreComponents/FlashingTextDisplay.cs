/* FlashingTextDisplay.cs
 * Purpose: Component for drawing flashing text on screen
 * 
 * Revision History:
 *      Sean Coombes, 2014.11.20: Created
 */

using HelicopterMadness.Scenes.CommonComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelicopterMadness.Scenes.HighScoreComponents
{
    /// <summary>
    ///     Displays flashing text on screen
    /// </summary>
    public class FlashingTextDisplay : TextDisplay
    {
        private readonly int delay;
        private int delayCounter;
        private bool toDraw;

        /// <summary>
        ///     Initializes a new instance of FlashingTextDisplay with the provided parameters
        /// </summary>
        /// <param name="game">The Game the FlashingTextDisplay belongs to</param>
        /// <param name="spriteBatch">The SpriteBatch FlashingTextDisplay will draw itself with</param>
        /// <param name="font">The font used for FlashingTextDisplay</param>
        /// <param name="color">The color used for FlashingTextDisplay</param>
        /// <param name="delay">The delay for switching between visible and hidden</param>
        public FlashingTextDisplay(Game game, SpriteBatch spriteBatch, SpriteFont font,
            Color color, int delay)
            : base(game, spriteBatch, font, color)
        {
            this.delay = delay;

            Stop();
        }

        /// <summary>
        ///     Updates the FlashingTextDisplay to switch between visible and hidden
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            delayCounter++;

            if (delayCounter > delay)
            {
                delayCounter = 0;
                toDraw = !toDraw;
            }
        }

        /// <summary>
        ///     Draws the FlashingTextDisplay if it is currently cycled on
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            if (toDraw)
            {
                spriteBatch.DrawString(Font, Message, Position, color);
            }
        }

        /// <summary>
        ///     Enables and shows the FlashingTextDisplay
        /// </summary>
        public void Start()
        {
            Enabled = true;
            Visible = true;
        }

        /// <summary>
        ///     Disables and hides the FlashingTextDisplay
        /// </summary>
        public void Stop()
        {
            Enabled = false;
            Visible = false;
        }
    }
}