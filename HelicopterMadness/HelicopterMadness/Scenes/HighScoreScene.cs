/* HighScoreScene.cs
 * Purpose: Display high scores for the game
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.04: Created
 */

using System.Diagnostics;
using HelicopterMadness.Scenes.BaseScene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HelicopterMadness.Scenes
{
    // TODO: Comments
    public class HighScoreScene : GameScene
    {
        public HighScoreScene(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            // TODO: Actually build this out
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Remove this or implement updates

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // TODO: Remove this or implement custom drawing

            base.Draw(gameTime);
        }

        public void AddScore(int score)
        {
            // TODO: Check if score is higher etc.
            Debug.WriteLine("AddScore was called with a score of: " + score);

            List<KeyValuePair<int, string>> highScoreList = new List<KeyValuePair<int, string>>();

            highScoreList.Add(new KeyValuePair<int, string>(12, "name"));

            highScoreList.Sort((value1, value2) => value1.Key.CompareTo(value2.Key));
        }
    }
}