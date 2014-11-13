/* CreditScene.cs
 * Purpose: Displays the games credits
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
    public class CreditScene : GameScene
    {
        private readonly Texture2D texture;

        public CreditScene(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            texture = game.Content.Load<Texture2D>("Images/CreditsScene");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);

            base.Draw(gameTime);
        }
    }
}