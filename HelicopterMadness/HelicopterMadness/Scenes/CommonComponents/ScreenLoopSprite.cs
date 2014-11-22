/* ScreenLoopSprite.cs
 * Purpose: Sprite that loops across the screen continuously
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.22: Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelicopterMadness.Scenes.CommonComponents
{
    /// <summary>
    ///     A sprite which continuously loops the screen
    /// </summary>
    public class ScreenLoopSprite : Sprite
    {
        private readonly float speedFactor;
        private readonly float layerDepth;
        private readonly SpriteEffects spriteEffect;

        private Vector2 secondPosition;

        /// <summary>
        ///     Initializes a screen looping sprite with the provided parameters
        /// </summary>
        /// <param name="game">The Game the ScreenLoopSprite belongs to</param>
        /// <param name="spriteBatch">The SpriteBatch the ScreenLoopSprite will draw itself with</param>
        /// <param name="texture">The texture for the ScreenLoopSprite</param>
        /// <param name="position">The position for the ScreenLoopSprite</param>
        /// <param name="speedFactor">
        ///     The speed factor as compared to SharedSettings.StageSpeed with 1 being equal to StageSpeed
        /// </param>
        /// <param name="layerDepth">The depth of the layer that ScreenLoopSprite draws itself at</param>
        /// <param name="flipped">If true, the border texture will be flipped</param>
        public ScreenLoopSprite(Game game, SpriteBatch spriteBatch, Texture2D texture, Vector2 position,
            float speedFactor, float layerDepth, bool flipped = false)
            : base(game, spriteBatch, texture, position)
        {
            this.speedFactor = speedFactor;
            this.layerDepth = layerDepth;

            secondPosition = position;
            secondPosition.X = texture.Width;

            spriteEffect = flipped
                ? SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically
                : SpriteEffects.None;
        }

        /// <summary>
        ///     Initializes a screen looping sprite with the provided parameters
        ///     <para>Position is Vector2.Zero, layerDepth is 1f, and flipped is false</para>
        /// </summary>
        /// <param name="game">The Game the ScreenLoopSprite belongs to</param>
        /// <param name="spriteBatch">The SpriteBatch the ScreenLoopSprite will draw itself with</param>
        /// <param name="texture">The texture for the ScreenLoopSprite</param>
        /// <param name="speedFactor">
        ///     The speed factor as compared to SharedSettings.StageSpeed with 1 being equal to StageSpeed
        /// </param>
        public ScreenLoopSprite(Game game, SpriteBatch spriteBatch, Texture2D texture, float speedFactor)
            : this(game, spriteBatch, texture, Vector2.Zero, speedFactor, 1f) { }

        /// <summary>
        ///     Updates the sprite's position to keep it looping
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            float movement = SharedSettings.StageSpeed.X * (float) gameTime.ElapsedGameTime.TotalSeconds *
                speedFactor;

            position.X -= movement;
            secondPosition.X -= movement;

            if (position.X + texture.Width < 0) // First loop is offscreen
            {
                position.X = secondPosition.X + texture.Width;
            }
            else if (secondPosition.X + texture.Width < 0) // Second loop is offscreen
            {
                secondPosition.X = position.X + texture.Width;
            }
        }

        /// <summary>
        ///     Draws the screen looping sprite
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(
                texture, position, null, Color.White, 0f, Vector2.Zero, 1f, spriteEffect, layerDepth);

            spriteBatch.Draw(
                texture, secondPosition, null, Color.White, 0f, Vector2.Zero, 1f, spriteEffect, layerDepth);
        }
    }
}