/* HelpScene.cs
 * Purpose: Help Scene that explains the gameplay of the game
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.05: Created
 */

using HelicopterMadness.Scenes.BaseScene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelicopterMadness.Scenes
{
    // TODO: Comments
    public class HowToPlayScene : GameScene
    {
        private readonly Texture2D texture;

        public HowToPlayScene(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            texture = game.Content.Load<Texture2D>("Images/HowToPlayScene");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
        }
    }
}