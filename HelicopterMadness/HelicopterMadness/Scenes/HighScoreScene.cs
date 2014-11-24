/* HighScoreScene.cs
 * Purpose: Display high scores for the game
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.04: Created
 *      Sean Coombes, 2014.11.13: Logic added
 *      Sean Coombes, 2014.11.15: logic revision
 */

using System;
using System.IO;
using System.Linq;
using HelicopterMadness.Scenes.BaseScene;
using HelicopterMadness.Scenes.CommonComponents;
using HelicopterMadness.Scenes.HighScoreComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace HelicopterMadness.Scenes
{
    /// <summary>
    ///     High score scene for the game
    /// </summary>
    public class HighScoreScene : GameScene
    {
        private const int NUMBER_OF_SCORE_ENTRIES = 5;
        private const int TOP_DUMMY_SCORE = 100;
        private const int DUMMY_SCORE_DIFFERENCE = 10;
        private const int BLINKRATE = 100;

        private const string CONTINUE = "Click the left mouse button to play another game";
        private const string WINNER_MESSAGE =
            "Congratulations you have gotten a new highscore enter a 3 character name and press enter";

        private static int highestScore;

        private readonly string scorepath;
        private readonly List<HighScoreEntry> highScoreEntries;
        private readonly TextDisplay[] scoreDisplays;
        private readonly FlashingTextDisplay helpMessage;

        private readonly SoundEffectInstance invalidKeySound;
        private readonly SoundEffectInstance newHighScoreSound;

        private HighScoreSceneStates state = HighScoreSceneStates.View;

        private HighScoreEntry newScoreEntry;

        private int lowestScore;

        private int newScoreIndex;
        private int inputIndex;

        private Vector2 titleDimensions;

        private KeyboardState oldKeyboardState;
        private MouseState oldMouseState;

        /// <summary>
        ///     Gets the highest saved score
        /// </summary>
        public static int HighestScore
        {
            get { return highestScore; }
        }

        /// <summary>
        ///     Gets the lowest saved score
        /// </summary>
        public int LowestScore
        {
            get { return lowestScore; }
        }

        /// <summary>
        ///     Gets the current HighScoreSceneStates of the HighScoreScene
        /// </summary>
        public HighScoreSceneStates State
        {
            get { return state; }
        }

        /// <summary>
        ///     Initializes a new instace of HighScoreScene with the provided parameters
        /// </summary>
        /// <param name="game">The Game the HighScoreScene belongs to</param>
        /// <param name="spriteBatch">The SpriteBatch the HighScoreScene uses to draw its components</param>
        public HighScoreScene(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            highScoreEntries = new List<HighScoreEntry>();

            scorepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                SharedSettings.HIGHSCORE_FILE_NAME);

            SetUpHighScoreEntries();

            SpriteFont headerFont = game.Content.Load<SpriteFont>("Fonts/HighScoreHeader");
            SpriteFont winnerFont = game.Content.Load<SpriteFont>("Fonts/HighScoreHelp");
            SpriteFont scoreFont = game.Content.Load<SpriteFont>("Fonts/HighScoreRegular");

            invalidKeySound = Game.Content.Load<SoundEffect>("Sounds/InvalidKeyPress").CreateInstance();
            invalidKeySound.Volume = 0.3f;

            newHighScoreSound = Game.Content.Load<SoundEffect>("Sounds/NewHighScore").CreateInstance();
            newHighScoreSound.Volume = 0.6f;
            
            titleDimensions = headerFont.MeasureString("HIGHSCORES");
            Vector2 scorePos = new Vector2((SharedSettings.Stage.X - titleDimensions.X) / 2, 0);

            TextDisplay headerDisplay = new TextDisplay(game, spriteBatch, headerFont, scorePos,
                SharedSettings.HighlightTextColor)
            {
                Message = "HIGHSCORES"
            };

            //displays blinking string alerting the player they got a new highscore 
            helpMessage = new FlashingTextDisplay(game, spriteBatch, winnerFont,
                SharedSettings.WinnerTextColor, BLINKRATE);
            
            //display the actual scores
            scoreDisplays = new TextDisplay[NUMBER_OF_SCORE_ENTRIES];

            for (int i = 0; i < NUMBER_OF_SCORE_ENTRIES; i++)
            {
                scoreDisplays[i] = new TextDisplay(game, spriteBatch, scoreFont,
                    SharedSettings.NormalTextColor);
            }

            UpdateScoreDisplays();

            Components.Add(headerDisplay);
            Components.Add(helpMessage);

            foreach (TextDisplay scores in scoreDisplays)
            {
                Components.Add(scores);
            }
        }

        /// <summary>
        ///     Hides the HighScoreScene
        /// </summary>
        public override void Hide()
        {
            // Scene is being hidden but name entry wasn't finished
            if (state == HighScoreSceneStates.NewScoreEntry)
            {
                SetNewScore();
            }

            state = HighScoreSceneStates.View;
            helpMessage.Stop();

            base.Hide();
        }

        /// <summary>
        ///     Updates the HighScoreScene's state
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            if (state == HighScoreSceneStates.NewScoreEntry)
            {
                KeyboardState keyboardState = Keyboard.GetState();

                char? key = keyboardState.GetAlphaCharacterInput(oldKeyboardState);

                if (key != null && inputIndex < SharedSettings.MAX_NAME_CHARS)
                {
                    newScoreEntry.ReplaceChar(inputIndex, (char)key);

                    scoreDisplays[newScoreIndex].Message = string.Format("{0}. {1}",
                        newScoreIndex + 1, newScoreEntry);

                    inputIndex = Math.Min(SharedSettings.MAX_NAME_CHARS, ++inputIndex);
                }
                else if (keyboardState.NewKeyPress(oldKeyboardState, Keys.Back))
                {
                    inputIndex = Math.Max(0, --inputIndex);

                    newScoreEntry.RemoveCharFromName(inputIndex);

                    scoreDisplays[newScoreIndex].Message = string.Format("{0}. {1}",
                        newScoreIndex + 1, newScoreEntry);
                }
                else if (keyboardState.IsKeyDown(Keys.Enter) &&
                    inputIndex == SharedSettings.MAX_NAME_CHARS)
                {
                    SetNewScore();
                }
                else if (keyboardState.GetAlphaCharacterInput() == null &&
                    !keyboardState.IsKeyDown(Keys.Back) && keyboardState.GetPressedKeys().Length > 0 &&
                    invalidKeySound.State != SoundState.Playing)
                {
                    // Plays the sound if an invalid key, anything but A-Z and backspace, is pressed and
                    // the new high score jingle isn't playing
                    if (newHighScoreSound.State != SoundState.Playing)
                    {
                        invalidKeySound.Play();
                    }
                }

                oldKeyboardState = keyboardState;
            }

            if (mouseState.LeftMouseNewClick(oldMouseState, Game) &&
                state == HighScoreSceneStates.NewScoreAdded)
            {
                state = HighScoreSceneStates.Action;
            }

            oldMouseState = mouseState;

            base.Update(gameTime);
        }

        /// <summary>
        ///     Finds what score has been beat and flips a bool to allow update to be run
        /// </summary>
        /// <param name="score">the score the play got on the action scene</param>
        public void AddScoreEntry(int score)
        {
            newHighScoreSound.Play();

            // Compare it against all highscores and place it where in the list it belongs
            for (int i = 0; i < NUMBER_OF_SCORE_ENTRIES; i++)
            {
                if (score > highScoreEntries[i].Score)
                {
                    state = HighScoreSceneStates.NewScoreEntry;

                    helpMessage.Message = WINNER_MESSAGE;
                    RepositionHelpMessage();
                    helpMessage.Start();

                    inputIndex = 0;
                    newScoreIndex = i;

                    newScoreEntry = new HighScoreEntry(score);

                    highScoreEntries.Insert(i, newScoreEntry);
                    highScoreEntries.RemoveAt(highScoreEntries.Count - 1);

                    scoreDisplays[i].Color = SharedSettings.HighlightTextColor;

                    UpdateScoreDisplays();

                    break;
                }
            }
        }

        /// <summary>
        ///     Loads up highscore file if one exists, otherwise creates dummy data
        /// </summary>
        private void SetUpHighScoreEntries()
        {           
            // TODO: this needs to be removed before handing in
#if DEBUG
            File.Delete(scorepath);
#endif

            if (File.Exists(scorepath))
            {
                try
                {
                    using (StreamReader scores = File.OpenText(scorepath))
                    {
                        while (!scores.EndOfStream && highScoreEntries.Count < NUMBER_OF_SCORE_ENTRIES)
                        {
                            string[] scoreString = scores.ReadLine().Split(' ');

                            int score;

                            // Skips if the score string array doesn't have both parts and if the score is not an int
                            if (scoreString.Length == 2 && int.TryParse(scoreString[1], out score))
                            {
                                highScoreEntries.Add(new HighScoreEntry(scoreString[0], score));
                            }
                        }

                        // -1 * is there so the sort order is descending instead of ascending
                        highScoreEntries.Sort(
                            (entry1, entry2) => -1 * entry1.Score.CompareTo(entry2.Score));
                    }
                }
                catch (Exception ex)
                {
                    if (ex is OutOfMemoryException)
                    {
                        throw;
                    }
                }
            }

            if (highScoreEntries.Count < NUMBER_OF_SCORE_ENTRIES)
            {
                FillScoresList();
            }

            SetHighLowScores();
        }

        /// <summary>
        ///     Fill up the score list with dummy data if it isn't full
        /// </summary>
        private void FillScoresList()
        {
            HighScoreEntry lowestEntry = highScoreEntries.LastOrDefault();

            int score = lowestEntry != null
                ? Math.Max(lowestEntry.Score - DUMMY_SCORE_DIFFERENCE, 0)
                : TOP_DUMMY_SCORE;

            while (highScoreEntries.Count < NUMBER_OF_SCORE_ENTRIES)
            {
                highScoreEntries.Add(new HighScoreEntry("ZZZ", score));

                score = Math.Max(score - DUMMY_SCORE_DIFFERENCE, 0);
            }
        }

        /// <summary>
        ///     Updates the Message, Position, and Color of the score and newscore TextDisplays
        /// </summary>
        private void UpdateScoreDisplays()
        {
            float yCoord = titleDimensions.Y * 2;
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
        ///     Sets the high and low scores
        /// </summary>
        private void SetHighLowScores()
        {
            highestScore = highScoreEntries[0].Score;
            lowestScore = highScoreEntries.Last().Score;
        }

        /// <summary>
        ///     Adds new highscore entry to list, preps display, and saves
        /// </summary>
        private void SetNewScore()
        {
            scoreDisplays[newScoreIndex].Color = SharedSettings.NormalTextColor;

            state = HighScoreSceneStates.NewScoreAdded;

            helpMessage.Message = CONTINUE;
            RepositionHelpMessage();

            newScoreIndex = -1; 

            // Ensure the name is in a valid format by running it through the Set method of Name
            newScoreEntry.Name = newScoreEntry.Name;

            SetHighLowScores();
            UpdateScoreDisplays();
            SaveScores();
        }

        /// <summary>
        ///     Saves the highscore list to a file
        /// </summary>
        private void SaveScores()
        {
            try
            {
                using (StreamWriter scoresWriter = new StreamWriter(scorepath))
                {
                    foreach (HighScoreEntry itemEntry in highScoreEntries)
                    {
                        scoresWriter.WriteLine(itemEntry.ToSaveString());
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO: Decide whether we display a message on screen or not
            }
        }

        /// <summary>
        ///     Sets the position for the new highscore message
        /// </summary>
        /// <returns>the position for the string</returns>
        private void RepositionHelpMessage()
        {
            Vector2 helpDimensions = helpMessage.Font.MeasureString(helpMessage.Message);

            helpMessage.Position = new Vector2(SharedSettings.StageCenter.X - helpDimensions.X / 2,
                SharedSettings.Stage.Y - SharedSettings.Stage.Y / 4);
        }
    }
}