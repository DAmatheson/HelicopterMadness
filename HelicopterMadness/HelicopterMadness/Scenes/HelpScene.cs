/* HelpScene.cs
 * Purpose: Help Scene that explains the gameplay of the game
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
    ///     Help scene for the game which displays a description of the gameplay
    /// </summary>
    public class HelpScene : GameScene
    {
        /// <summary>
        ///     Initializes a new instance of HelpScene with the provided parameters
        /// </summary>
        /// <param name="game">The Game the HelpScene belongs to</param>
        /// <param name="spriteBatch">The SpriteBatch the HelpScene will draw its components with</param>
        public HelpScene(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            Sprite helpSprite = new Sprite(game, spriteBatch,
                game.Content.Load<Texture2D>("Images/HelpScene"));

            Components.Add(helpSprite);
        }
    }
}