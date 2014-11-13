/* HighScoreScene.cs
 * Purpose: Display high scores for the game
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.04: Created
 */

using HelicopterMadness.Scenes.BaseScene;
using HelicopterMadness.Scenes.HighScoreComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HelicopterMadness.Scenes
{
    // TODO: Comments
    public class HighScoreScene : GameScene
    {
        private readonly List<HighScoreEntry> highScoreEntries;

        private int highestScore;
        private int lowestScore;

        public HighScoreScene(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            // TODO: Actually build this out
            highScoreEntries = new List<HighScoreEntry>();
        }

        public int HighestScore
        {
            get { return highestScore; }
        }

        public int LowestScore
        {
            get { return lowestScore; }
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
            // TODO:
            // Update Lowest Score
            // Update Highest Score
            // Sort?
        }
    }
}