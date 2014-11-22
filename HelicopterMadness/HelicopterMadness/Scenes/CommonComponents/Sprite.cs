/* Sprite.cs
 * Purpose: Basic class to minimize work required when creating new drawable components
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.04: Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelicopterMadness.Scenes.CommonComponents
{
    /// <summary>
    ///     Basic class for a drawable sprite
    /// </summary>
    public class Sprite : DrawableGameComponent
    {
        protected readonly SpriteBatch spriteBatch;
        protected Texture2D texture;
        protected Vector2 position;

        /// <summary>
        ///     Initializes a sprite with the provided parameters
        /// </summary>
        /// <param name="game">The Game the Sprite belongs to</param>
        /// <param name="spriteBatch">The SpriteBatch the Sprite will draw itself with</param>
        /// <param name="texture">The Texture2D for the Sprite</param>
        /// <param name="position">The position of the Sprite</param>
        public Sprite(Game game, SpriteBatch spriteBatch, Texture2D texture, Vector2 position)
            : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.position = position;
        }

        /// <summary>
        ///     Initializes a sprite with the provided parameters and a position of Vector2.Zero
        /// </summary>
        /// <param name="game">The Game the Sprite belongs to</param>
        /// <param name="spriteBatch">The SpriteBatch the Sprite will draw itself with</param>
        /// <param name="texture">The texture for the Sprite</param>
        public Sprite(Game game, SpriteBatch spriteBatch, Texture2D texture)
            : this(game, spriteBatch, texture, Vector2.Zero)
        { }

        /// <summary>
        ///     Draws the Sprite with the texture at the provided position
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}