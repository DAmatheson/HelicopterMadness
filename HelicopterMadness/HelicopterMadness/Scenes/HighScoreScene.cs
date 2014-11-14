/* HighScoreScene.cs
 * Purpose: Display high scores for the game
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.04: Created
 */

using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Threading;
using System.Xml;
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
        private TextDisplay headerDisplay;
        private TextDisplay scoreDisplay;

        private int highestScore;
        private int lowestScore;

        private SpriteFont scoreFont;
        private Vector2 scoreDim;

        private const int TOP_SCORES = 5;
        private const int DUMMY_SCORE = 100;
        private const string DUMMY_NAME = "ZZZ";
        public HighScoreScene(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            // TODO: Continue building this out
            highScoreEntries = new List<HighScoreEntry>();
            string scorepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Highscore.txt");


                if (File.Exists(scorepath))
                {
                    try
                    {
                        //open file and load into list 
                        using (StreamReader scores = File.OpenText(scorepath))
                        {
                            int count = 0;

                            while (!scores.EndOfStream && count < TOP_SCORES)
                            {

                                string[] scoreString = scores.ReadLine().Split(' ');
                                int score;

                                //skips line if the score string array doesnt have both parts and if the score is not an int
                                if (scoreString.Length != 2 || !int.TryParse(scoreString[1], out score))
                                {
                                    continue;
                                }

                                //edits the name so it fits the decided on patter of 3 chars in uppcase with no spaces
                                scoreString[0] = scoreString[0].Trim().PadRight(3).Replace(' ', 'A').ToUpper();

                                //highScoreEntries[count] = new HighScoreEntry(scoreString[0], score);
                                highScoreEntries.Add(new HighScoreEntry(scoreString[0], score));
                                count++;
                            }
                        }

                        //add dummmy scores to the list if any scores from the above while loop were invalid
                        while (highScoreEntries.Count < 5 && highScoreEntries.Count > 0)
                        {
                            //highScoreEntries[highScoreEntries.Count] = new HighScoreEntry(DUMMY_NAME, 0);
                            highScoreEntries.Add(new HighScoreEntry(DUMMY_NAME,0));
                        }

                        if(highScoreEntries.Count == 0)
                        {
                            prepDummyList();
                        }

                        highestScore = highScoreEntries[0].Score;
                        lowestScore = highScoreEntries.Last().Score;
                        
                    }
                    catch (Exception)
                    {
                        //todo:handleexception
                        throw;
                    }
                }
                else
                {
                    prepDummyList();
                }

            //TEMP Testing text alignments and what not
            SpriteFont headerFont = game.Content.Load<SpriteFont>("Fonts/Regular");
            scoreDim = headerFont.MeasureString("HIGHSCORES");
            Vector2 scorePos = new Vector2(SharedSettings.Stage.X/2 - scoreDim.X/2, 0);
            headerDisplay = new TextDisplay(game,spriteBatch,headerFont,scorePos,Color.Red);
            headerDisplay.Message = "HIGHSCORES";

            //display the actual scores
            scoreFont = game.Content.Load<SpriteFont>("Fonts/Highlight");
            Vector2 pos = new Vector2(0,0);        
            scoreDisplay = new TextDisplay(game,spriteBatch,scoreFont,pos, Color.Yellow);

            Components.Add(headerDisplay);
            Components.Add(scoreDisplay);
        }

        private void prepDummyList()
        {        
            //create list of dummy data
            for (int i = 0; i < TOP_SCORES; i++)
            {
                int score = DUMMY_SCORE;
                //todo:magicnumber
                score = score - i * 10;
                highScoreEntries.Add(new HighScoreEntry(DUMMY_NAME, score));
            }
            highestScore = highScoreEntries[0].Score;
            lowestScore = highScoreEntries.Last().Score;
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
            // TODO: fix this so it only updates once
            scoreDisplay.Message = "";
            for (int i = 0; i < TOP_SCORES; i++)
            {
                scoreDisplay.Message += i + 1 + ". " + highScoreEntries[i].Name + "..................." + highScoreEntries[i].Score + "\n";
            }

            Vector2 testDim = scoreFont.MeasureString(scoreDisplay.Message);
            scoreDisplay.Position = new Vector2(SharedSettings.Stage.X / 2 - testDim.X / 2, scoreDim.Y * 3);

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

                //compare it against all highscores and place it where in the list it belongs
                for (int i = 0; i < TOP_SCORES; i++)
                {
                    //some how bring this scene forward and hide away the action scene until score is added
                    if (score <= highScoreEntries[i].Score) continue;
                    highScoreEntries.Insert(i, new HighScoreEntry(getName(), score));
                    break;
                }
        }

        private string getName()
        {
            //todo:getusername
            return "yay";
        }
    }
}