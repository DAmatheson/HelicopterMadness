/* CreditScene.cs
 * Purpose: Displays the games credits
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.05: Created
 */

using HelicopterMadness.Scenes.BaseScene;
using HelicopterMadness.Scenes.CommonComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelicopterMadness.Scenes
{
    /// <summary>
    ///     Credits scene for the game
    /// </summary>
    public class CreditScene : GameScene
    {
        /// <summary>
        ///     Initializes a new instance of CreditScene with the provided parameters
        /// </summary>
        /// <param name="game">The Game the CreditScene belongs to</param>
        /// <param name="spriteBatch">The SpriteBatch the CreditScene will draw its components with</param>
        public CreditScene(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            Sprite creditsSprite = new Sprite(game, spriteBatch,
                game.Content.Load<Texture2D>("Images/CreditsScene"));

            Components.Add(creditsSprite);
        }
    }
}