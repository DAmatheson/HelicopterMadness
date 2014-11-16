/* HighScoreScene.cs
 * Purpose: Display high scores for the game
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.04: Created
 *      Sean Coombes, 2014.11.13: Logic added
 *      Sean Coombes, 2014.11.15: logic revision
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
        private const int MAX_NAME_CHARS = 3;

        private readonly Color highlightColor = Color.Red;
        private readonly Color normalColor = Color.Yellow;

        private HighScoreSceneStates state = HighScoreSceneStates.View;

        private readonly List<HighScoreEntry> highScoreEntries;
        private HighScoreEntry newScoreEntry;
        private readonly SpriteFont scoreFont;

        private TextDisplay headerDisplay;
        private TextDisplay[] scoreDisplays;

        public static int highestScore;
        private int lowestScore;

        private int inputIndex;

        private Vector2 titleDimensions;

        private KeyboardState oldKeyboardState;
        private KeyboardState keyboardState;
        private MouseState mouseState;
        private MouseState oldMouseState;
        private int newScoreIndex;

        public int HighestScore
        {
            get
            {
                return highestScore;
            }
        }

        public int LowestScore
        {
            get
            {
                return lowestScore;
            }
        }

        public HighScoreSceneStates State
        {
            get { return state; }
            set { state = value; }
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

            //newScoreIndex = -1;

            SetUpHighScoreEntries();

            //TEMP Testing text alignments and what not
            SpriteFont headerFont = game.Content.Load<SpriteFont>("Fonts/Regular");
            titleDimensions = headerFont.MeasureString("HIGHSCORES");
            Vector2 scorePos = new Vector2(SharedSettings.Stage.X / 2 - titleDimensions.X / 2, 0);
            headerDisplay = new TextDisplay(game, spriteBatch, headerFont, scorePos, highlightColor)
            {
                Message = "HIGHSCORES"
            };

            //display the actual scores
            scoreFont = game.Content.Load<SpriteFont>("Fonts/Highlight");
            Vector2 pos = new Vector2(0, 0);
            scoreDisplays = new TextDisplay[NUMBER_OF_SCORE_ENTRIES];

            for (int i = 0; i < NUMBER_OF_SCORE_ENTRIES; i++)
            {
                scoreDisplays[i] = new TextDisplay(game, spriteBatch, scoreFont, pos, normalColor);
            }

            UpdateScoreDisplays();

            Components.Add(headerDisplay);

            foreach (TextDisplay t in scoreDisplays)
            {
                Components.Add(t);
            }
        }

        /// <summary>
        ///     Loads up highscore file if one exists
        /// </summary>
        private void SetUpHighScoreEntries()
        {
            string scorepath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), SharedSettings.HIGHSCORE_FILE_NAME);

            File.Delete(scorepath);

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
        ///     Sets up a dummy score list
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
        ///     Updates the HighScoreScene's state
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            //early code to handle user name entry
            // TODO: check and replace spaces
            mouseState = Mouse.GetState();

            if (newScoreIndex > -1)
            {
                keyboardState = Keyboard.GetState();

                char key;

                if (KeyboardEntry.KeyboardInput(keyboardState, oldKeyboardState, out key) && inputIndex < MAX_NAME_CHARS)
                {
                    newScoreEntry.Name = newScoreEntry.Name.Remove(inputIndex, 1).Insert(inputIndex, key.ToString());

                    scoreDisplays[newScoreIndex].Message = string.Format("{0}. {1}", newScoreIndex + 1, newScoreEntry);

                    inputIndex = Math.Min(MAX_NAME_CHARS, ++inputIndex);
                }
                else if (keyboardState.NewKeyPress(oldKeyboardState, Keys.Back))
                {
                    inputIndex = Math.Max(0, --inputIndex);

                    newScoreEntry.RemoveCharFromName(inputIndex);

                    scoreDisplays[newScoreIndex].Message = string.Format("{0}. {1}", newScoreIndex + 1, newScoreEntry);
                }
                else if (keyboardState.IsKeyDown(Keys.Enter) && inputIndex == MAX_NAME_CHARS)
                {
                    SetNewScore();
                }

                oldKeyboardState = keyboardState;
            }

            if (mouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed 
                && state == HighScoreSceneStates.NewScore)
            {
                state = HighScoreSceneStates.Action;

            }
            oldMouseState = mouseState;
            base.Update(gameTime);
        }

        /// <summary>
        ///     Updates the Message, Position, and Color of the score and newscore TextDisplays
        /// </summary>
        private void UpdateScoreDisplays()
        {
            float yCoord = titleDimensions.Y * 3;
            float xCoord = 0;

            for (int i = 0; i < NUMBER_OF_SCORE_ENTRIES; i++)
            {
                TextDisplay currentScoreDisplay = scoreDisplays[i];

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

        /// <summary>
        /// Finds what score has been beat and flips a bool to allow update to be run
        /// </summary>
        /// <param name="score">the score the play got on the action scene</param>
        public void AddScoreEntry(int score)
        {
            //compare it against all highscores and place it where in the list it belongs
            for (int i = 0; i < NUMBER_OF_SCORE_ENTRIES; i++)
            {
                if (score > highScoreEntries[i].Score)
                {
                    inputIndex = 0;
                    newScoreIndex = i;

                    newScoreEntry = new HighScoreEntry(score);

                    highScoreEntries.Insert(i, newScoreEntry);
                    highScoreEntries.RemoveAt(highScoreEntries.Count - 1);

                    scoreDisplays[i].Color = highlightColor;

                    UpdateScoreDisplays();

                    break;
                }
            }
        }

        /// <summary>
        /// Sets the high and lows scores
        /// </summary>
        private void SetHighLowScores()
        {
            highestScore = highScoreEntries[0].Score;
            lowestScore = highScoreEntries.Last().Score;
        }

        /// <summary>
        /// Adds new highscore entry to list, preps display and sets up save
        /// </summary>
        private void SetNewScore()
        {
            scoreDisplays[newScoreIndex].Color = normalColor;

            newScoreIndex = -1; 
            state = HighScoreSceneStates.NewScore;

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
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), SharedSettings.HIGHSCORE_FILE_NAME);

            using (StreamWriter scoresWriter = new StreamWriter(scorepath, false))
            {
                foreach (HighScoreEntry itemEntry in highScoreEntries)
                {
                    scoresWriter.WriteLine(itemEntry.ToSaveString());
                }
            }
        }
    }
}