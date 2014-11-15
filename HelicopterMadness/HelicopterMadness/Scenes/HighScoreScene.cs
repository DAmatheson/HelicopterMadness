/* HighScoreScene.cs
 * Purpose: Display high scores for the game
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.04: Created
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using HelicopterMadness.Scenes.BaseScene;
using HelicopterMadness.Scenes.CommonComponents;
using HelicopterMadness.Scenes.HighScoreComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace HelicopterMadness.Scenes
{
    // TODO: Comments
    /// <summary>
    /// 
    /// </summary>
    public class HighScoreScene : GameScene
    {
        private const int NUMBER_OF_SCORE_ENTRIES = 5;
        private const int TOP_DUMMY_SCORE = 100;
        private const string DUMMY_NAME = "ZZZ";

        private readonly Color highlightColor = Color.Red;
        private readonly Color normalColor = Color.Yellow;

        private readonly List<HighScoreEntry> highScoreEntries;
        private HighScoreEntry newScoreEntry;
        private readonly SpriteFont scoreFont;

        private TextDisplay headerDisplay;
        private TextDisplay[] scoreDisplays;
        private TextDisplay newHighScoreDisplay; // Never used(yea had plans for it not sure if i will use it)

        private int highestScore;
        private int lowestScore;
        private int newHighScoreIndex = -1;

        private Vector2 titleDimensions;
        private string newName;
        private int newScore;


        private bool isNewScore;
        private KeyboardState oldState;
        private KeyboardState keyboardState;

        public int HighestScore
        {
            get { return highestScore; }
        }

        public int LowestScore
        {
            get { return lowestScore; }
        }
        /// <summary>
        /// constructsor sets up needed components 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
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

            foreach (TextDisplay t in scoreDisplays)
            {
                Components.Add(t);
            }

            //just for testing purposes
            newHighScoreDisplay = new TextDisplay(game,spriteBatch,scoreFont,Vector2.Zero,Color.Black);
            Components.Add(newHighScoreDisplay);
        }

        /// <summary>
        /// loads up highscore file if one exists
        /// </summary>
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

        /// <summary>
        /// sets up a dummy score list
        /// </summary>
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

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            //early code to handle user name entry
            // TODO: check and replace spaces

            if (isNewScore)
            {
                keyboardState = Keyboard.GetState();
                char key;
                newScoreEntry = new HighScoreEntry(newName, newScore);
                UpdateScoreDisplays();
                if (KeyboardEntry.KeyboardInput(keyboardState, oldState, out key)&& newName.Length < 3)
                {
                    //todo: this needs to replace whitespace
                    newName += key.ToString();
                    newScoreEntry.Name = newName;   
                }
                else if (oldState.IsKeyDown(Keys.Back) && keyboardState.IsKeyUp(Keys.Back) && newScoreEntry.Name.Length > 0)
                {
                    //todo: this needs to remove the previous letter and add a white
                    newName = newName.Remove((newName.Length - 1), 1);
                    newScoreEntry.Name = newName;
                }
                else if (keyboardState.IsKeyDown(Keys.Enter) && newName.Length == 3)
                {
                    isNewScore = false;
                    
                    SetNewScore();             
                }
                oldState = keyboardState;
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Hide()
        {
            //todo: do we need this?
            if (newHighScoreIndex >= 0 && newHighScoreIndex < scoreDisplays.Length)
            {
                scoreDisplays[newHighScoreIndex].Color = normalColor;
            }

            base.Hide();
        }

        /// <summary>
        ///     Updates the Message, Position, and Color of the score and newscore TextDisplays
        /// </summary>
        private void UpdateScoreDisplays()
        {
            float yCoord = titleDimensions.Y * 3;
            float xCoord = 0;
            int index = 0;
            bool isSet = false;

            for (int i = 0; i < NUMBER_OF_SCORE_ENTRIES; i++)
            {
                TextDisplay currentScoreDisplay = scoreDisplays[i];           

                currentScoreDisplay.Message = string.Format("{0}. {1}", i + 1, highScoreEntries[index]);

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

                if (i == newHighScoreIndex && !isSet)
                {
                    currentScoreDisplay.Visible = false;
                    isSet = true;
                    index--;

                    newHighScoreDisplay.Visible = true;
                    newHighScoreDisplay.Color = highlightColor;
                    newHighScoreDisplay.Message = string.Format("{0}. {1}", i + 1, newScoreEntry);
                    newHighScoreDisplay.Position = currentScoreDisplay.Position;
                }
                else
                {
                    currentScoreDisplay.Color = normalColor;
                    currentScoreDisplay.Visible = true;
                }

                index++;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            // TODO: Remove this or implement custom drawing

            base.Draw(gameTime);
        }
        /// <summary>
        /// finds what score has been beat and flips a bool to allow update to be run
        /// </summary>
        /// <param name="score">the score the play got on the action scene</param>
        public void AddScoreEntry(int score)
        {
            //compare it against all highscores and place it where in the list it belongs
            for (int i = 0; i < NUMBER_OF_SCORE_ENTRIES; i++)
            {
                
                if (score > highScoreEntries[i].Score)
                {
                    newHighScoreIndex = i;
                    newScore = score;
                    newName = "   ";
                    isNewScore = true;   
                    break;
                }
            }
        }
        /// <summary>
        /// sets the high and lows scores
        /// </summary>
        private void SetHighLowScores()
        {
            highestScore = highScoreEntries[0].Score;
            lowestScore = highScoreEntries.Last().Score;
        }
        /// <summary>
        /// adds new highscore entry to list, preps display and sets up save
        /// </summary>
        private void SetNewScore()
        {
            highScoreEntries.Insert(newHighScoreIndex, new HighScoreEntry(newName, newScore));
            highScoreEntries.RemoveAt(highScoreEntries.Count - 1);
            newHighScoreDisplay.Visible = false;

            newHighScoreIndex = -1;
            SetHighLowScores();
            UpdateScoreDisplays();
            SaveScores();
        }
        /// <summary>
        /// Saves the highscore list to a file
        /// </summary>
        private void SaveScores()
        {
            string scorepath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Highscore.txt");

            using (StreamWriter scoresWriter = new StreamWriter(scorepath, false))
            {
                foreach (HighScoreEntry itemEntry in highScoreEntries)
                {
                    scoresWriter.WriteLine(itemEntry.Name + " " + itemEntry.Score);
                }
            }
        }
    }
}