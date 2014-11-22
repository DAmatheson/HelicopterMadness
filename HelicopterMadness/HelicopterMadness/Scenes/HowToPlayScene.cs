/* HelpScene.cs
 * Purpose: Help Scene that explains the controls for the game
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
    ///     How to play scene for the game which displays the controls for the game
    /// </summary>
    public class HowToPlayScene : GameScene
    {
        /// <summary>
        ///     Initializes a new instance of HowToPlayScene with the provided parameters
        /// </summary>
        /// <param name="game">The Game the HowToPlayScene belongs to</param>
        /// <param name="spriteBatch">The SpriteBatch the HowToPlayScene will draw its components with</param>
        public HowToPlayScene(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            Sprite howToPlaySprite = new Sprite(game, spriteBatch,
                game.Content.Load<Texture2D>("Images/HowToPlayScene"));

            Components.Add(howToPlaySprite);
        }
    }
}