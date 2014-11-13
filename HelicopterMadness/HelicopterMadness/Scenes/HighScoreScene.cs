/* HighScoreScene.cs
 * Purpose: Display high scores for the game
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.04: Created
 */

using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using HelicopterMadness.Scenes.BaseScene;
using HelicopterMadness.Scenes.HighScoreComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace HelicopterMadness.Scenes
{
    // TODO: Comments
    public class HighScoreScene : GameScene
    {
        private readonly List<HighScoreEntry> highScoreEntries;

        private int highestScore;
        private int lowestScore;


        private const int TOP_SCORES = 5;
        public HighScoreScene(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            // TODO: Actually build this out
            highScoreEntries = new List<HighScoreEntry>();
            string scorepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Highscore.txt");


            try
            {
                if (File.Exists(scorepath))
                {
                    //open file and load into list 
                    using (StreamReader scores = File.OpenText(scorepath))
                    {
                        int count = 0;

                        while (!scores.EndOfStream && count < TOP_SCORES)
                        {
                            //would this crash if there is no space on the current line?
                            string[] scoreString = scores.ReadLine().Split(' ');
                            int score;

                            //skips line if the score string array doesnt have both parts and if the score is not an int
                            if (scoreString.Length != 2 || !int.TryParse(scoreString[1], out score))
                            {
                                continue;
                            }

                            //edits the name so it fits the decided on patter of 3 chars in uppcase with no spaces
                            scoreString[0] = scoreString[0].Trim().PadRight(3).Replace(' ', 'A').ToUpper();

                            highScoreEntries[count] = new HighScoreEntry(scoreString[0], score);
                            count++;

                            
                        }
                    }
                }
                else
                {
                    //create list of dummy data
                    for (int i = 0; i < TOP_SCORES; i++)
                    {

                        //highScoreEntries[i] = new HighScoreEntry(scoreString[0], score);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            
            

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