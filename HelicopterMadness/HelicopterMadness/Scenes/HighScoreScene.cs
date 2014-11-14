/* HighScoreScene.cs
 * Purpose: Display high scores for the game
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.04: Created
 */

using System;
using System.IO;
using System.Linq;
using HelicopterMadness.Scenes.BaseScene;
using HelicopterMadness.Scenes.CommonComponents;
using HelicopterMadness.Scenes.HighScoreComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace HelicopterMadness.Scenes
{
    // TODO: Comments
    public class HighScoreScene : GameScene
    {
        private const int NUMBER_OF_SCORE_ENTRIES = 5;
        private const int TOP_DUMMY_SCORE = 100;
        private const string DUMMY_NAME = "ZZZ";

        private readonly Color highlightColor = Color.Red;
        private readonly Color normalColor = Color.Yellow;

        private readonly List<HighScoreEntry> highScoreEntries;
        private readonly SpriteFont scoreFont;

        private TextDisplay headerDisplay;
        private TextDisplay[] scoreDisplays;
        private TextDisplay newHighScoreDisplay; // Never used

        private int highestScore;
        private int lowestScore;
        private int newHighScoreIndex = -1;

        private Vector2 titleDimensions;

        public HighScoreScene(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            // TODO: Continue building this out
            highScoreEntries = new List<HighScoreEntry>();

            SetUpHighScoreEntries();

            //TEMP Testing text alignments and what not
            SpriteFont headerFont = game.Content.Load<SpriteFont>("Fonts/Regular");
            titleDimensions = headerFont.MeasureString("HIGHSCORES");
            Vector2 scorePos = new Vector2(SharedSettings.Stage.X / 2 - titleDimensions.X / 2, 0);
            headerDisplay = new TextDisplay(game, spriteBatch, headerFont, scorePos, Color.Red)
            {
                Message = "HIGHSCORES"
            };

            //display the actual scores
            scoreFont = game.Content.Load<SpriteFont>("Fonts/Highlight");
            Vector2 pos = new Vector2(0, 0);
            scoreDisplays = new TextDisplay[NUMBER_OF_SCORE_ENTRIES];
            
            for (int i = 0; i < NUMBER_OF_SCORE_ENTRIES; i++)
            {
                scoreDisplays[i] = new TextDisplay(game, spriteBatch, scoreFont, pos, Color.Yellow);
            }

            UpdateScoreDisplays();
            //latests 

            Components.Add(headerDisplay);

            for (int i = 0; i < scoreDisplays.Length; i++)
            {
                Components.Add(scoreDisplays[i]);
            }
        }

        private void SetUpHighScoreEntries()
        {
            string scorepath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Highscore.txt");

            if (File.Exists(scorepath))
            {
                try
                {
                    //open file and load into list 
                    using (StreamReader scores = File.OpenText(scorepath))
                    {
                        int count = 0;

                        while (!scores.EndOfStream && count < NUMBER_OF_SCORE_ENTRIES)
                        {
                            string[] scoreString = scores.ReadLine().Split(' ');
                            int score;

                            //skips line if the score string array doesnt have both parts and if the score is not an int
                            if (scoreString.Length == 2 && int.TryParse(scoreString[1], out score))
                            {
                                //edits the name so it fits the decided on patter of 3 chars in upcase with no spaces
                                scoreString[0] =
                                    scoreString[0].Trim().PadRight(3).Substring(0, 3).Replace(' ', 'A').ToUpper();

                                highScoreEntries.Add(new HighScoreEntry(scoreString[0], score));
                                count++;
                            }
                        }
                    }

                    //add dummy scores to the list if any scores from the above while loop were invalid
                    while (highScoreEntries.Count < 5 && highScoreEntries.Count > 0)
                    {
                        highScoreEntries.Add(new HighScoreEntry(DUMMY_NAME, 0));
                    }

                    if (highScoreEntries.Count == 0)
                    {
                        CreateDummyScoresList();
                    }

                    SetHighLowScores();
                }
                catch (Exception)
                {
                    //todo:handleexception
                    throw;
                }
            }
            else
            {
                CreateDummyScoresList();
                SetHighLowScores();
            }
        }

        private void CreateDummyScoresList()
        {        
            //create list of dummy data
            for (int i = 0; i < NUMBER_OF_SCORE_ENTRIES; i++)
            {
                int score = TOP_DUMMY_SCORE;
                //todo:magicnumber
                score = score - i * 10;
                highScoreEntries.Add(new HighScoreEntry(DUMMY_NAME, score));
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
            // TODO: Remove or add update logic

            base.Update(gameTime);
        }

        public override void Hide()
        {
            if (newHighScoreIndex >= 0 && newHighScoreIndex < scoreDisplays.Length)
            {
                scoreDisplays[newHighScoreIndex].Color = normalColor;
            }

            base.Hide();
        }

        /// <summary>
        ///     Updates the Message, Position, and Color of the score TextDisplays
        /// </summary>
        private void UpdateScoreDisplays()
        {
            float yCoord = titleDimensions.Y * 3;
            float xCoord = 0;

            for (int i = 0; i < NUMBER_OF_SCORE_ENTRIES; i++)
            {
                TextDisplay currentScoreDisplay = scoreDisplays[i];

                if (i == newHighScoreIndex)
                {
                    currentScoreDisplay.Color = highlightColor;
                }
                else
                {
                    currentScoreDisplay.Color = normalColor;
                }

                currentScoreDisplay.Message = string.Format("{0}. {1}", i + 1, highScoreEntries[i]);

                Vector2 scoreDisplaySize = currentScoreDisplay.Font.MeasureString(currentScoreDisplay.Message);

                if (i == 0)
                {
                    xCoord = scoreDisplaySize.X / 2;
                }
                else
                {
                    yCoord += scoreDisplaySize.Y;
                }

                currentScoreDisplay.Position = new Vector2(SharedSettings.Stage.X / 2 - xCoord, yCoord);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            // TODO: Remove this or implement custom drawing

            base.Draw(gameTime);
        }

        public void AddScoreEntry(int score)
        {
            //compare it against all highscores and place it where in the list it belongs
            for (int i = 0; i < NUMBER_OF_SCORE_ENTRIES; i++)
            {
                //some how bring this scene forward and hide away the action scene until score is added
                if (score > highScoreEntries[i].Score)
                {
                    highScoreEntries.Insert(i, new HighScoreEntry(GetName(), score));
                    highScoreEntries.RemoveAt(highScoreEntries.Count - 1);

                    newHighScoreIndex = i;

                    SetHighLowScores();
                    UpdateScoreDisplays();

                    break;
                }
            }
        }

        private void SetHighLowScores()
        {
            highestScore = highScoreEntries[0].Score;
            lowestScore = highScoreEntries.Last().Score;
        }

        private string GetName()
        {
            //todo:getusername
            return "yay";
        }
    }
}