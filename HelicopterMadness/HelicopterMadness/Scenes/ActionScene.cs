/* ActionScene.cs
 * Purpose: The gameplay scene for the game
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.05: Created
 *      Sean Coombes , 2014.11.15: Revision
 */

using System;
using System.Collections.Generic;
using System.Linq;
using HelicopterMadness.Scenes.ActionComponents;
using HelicopterMadness.Scenes.BaseScene;
using HelicopterMadness.Scenes.CommonComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HelicopterMadness.Scenes
{
    /// <summary>
    ///     The action scene for the game
    /// </summary>
    public class ActionScene : GameScene
    {
        private const int NUMBER_OF_OBSTACLES = 7;
        private const string START_MESSAGE = "Click or Press Up to Start Play";

        private readonly float minObstacleXSpacing;
        private readonly float maxObstacleXSpacing;

        private readonly Helicopter helicopter;
        private readonly Border topBorder;
        private readonly Border bottomBorder;

        private readonly TextDisplay scoreDisplay;
        private readonly TextDisplay highScoreDisplay;
        private readonly FlashingTextDisplay midScreenMessage;

        private readonly SoundEffect beatHighestScoreSound;

        private readonly List<Obstacle> obstacles = new List<Obstacle>();

        private ActionSceneStates state = ActionSceneStates.PreStart;

        private MouseState oldMouseState;
        private KeyboardState oldKeyboardState;

        private int durationScore = 0;

        /// <summary>
        ///     Gets the current ActionSceneStates for the ActionScene
        /// </summary>
        public ActionSceneStates State
        {
            get { return state; }
        }

        /// <summary>
        ///     Gets the score for the current play of the ActionScene
        /// </summary>
        /// <returns>The score</returns>
        public int GetScore()
        {
            return durationScore / 100;
        }

        /// <summary>
        ///     Initializes a new instace of ActionScene with the provided parameters
        /// </summary>
        /// <param name="game">The Game the ActionScene belongs to</param>
        /// <param name="spriteBatch">The SpriteBatch the ActionScene will draw its components with</param>
        public ActionScene(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            Texture2D heliTexture = Game.Content.Load<Texture2D>("Images/HeliAnimated");
            Texture2D borderTexture = Game.Content.Load<Texture2D>("Images/StageBorder");
            Texture2D obstacleTexture = Game.Content.Load<Texture2D>("Images/Obstacle");

            SpriteFont highlightFont = Game.Content.Load<SpriteFont>("Fonts/Highlight");

            SoundEffect borderCollision = Game.Content.Load<SoundEffect>("Sounds/BorderCollision");
            SoundEffect helicopterSound = Game.Content.Load<SoundEffect>("Sounds/Helicopter");
            SoundEffect obstacleCollision = Game.Content.Load<SoundEffect>("Sounds/ObstacleCollision");

            beatHighestScoreSound = Game.Content.Load<SoundEffect>("Sounds/BeatHighestScore");

            Vector2 heliFrameDimensions = new Vector2(120, 61);

            minObstacleXSpacing = heliFrameDimensions.X * 1.15f + obstacleTexture.Width;
            maxObstacleXSpacing = minObstacleXSpacing * 2f;

            Vector2 heliPosition = new Vector2(SharedSettings.Stage.X / 4f - heliFrameDimensions.X / 2f,
                (SharedSettings.Stage.Y - heliFrameDimensions.Y) / 2f);

            Vector2 bottomBorderPosition = new Vector2(0, SharedSettings.Stage.Y - borderTexture.Height);

            Explosion explosion = new Explosion(game, spriteBatch,
                Game.Content.Load<Texture2D>("Images/explosion"), new Vector2(64, 64), 2);

            helicopter = new Helicopter(game, spriteBatch, heliTexture, heliPosition,
                heliFrameDimensions, helicopterSound, explosion)
            {
                Enabled = false
            };

            topBorder = new Border(game, spriteBatch, borderTexture, Vector2.Zero, borderCollision);
            bottomBorder = new Border(game, spriteBatch, borderTexture, bottomBorderPosition,
                borderCollision, 1f, true);

            GenerateObstacles(obstacleTexture, obstacleCollision);

            CollisionManager collisionManager = new CollisionManager(game,
                new [] { topBorder, bottomBorder }, helicopter);

            collisionManager.AddCollidableRange(obstacles);

            scoreDisplay = new TextDisplay(Game, spriteBatch, highlightFont,
                SharedSettings.NormalTextColor);

            string highestScore = HighScoreScene.HighestScore.ToString();

            Vector2 highScorePosition = new Vector2(SharedSettings.Stage.X - 
                highlightFont.MeasureString(highestScore).X, 0);

            highScoreDisplay = new TextDisplay(
                game, spriteBatch, highlightFont, highScorePosition,
                SharedSettings.HighestScoreColor, highestScore);

            midScreenMessage = new FlashingTextDisplay(Game, spriteBatch, highlightFont,
                SharedSettings.HelpTextColor, SharedSettings.BLINK_RATE)
            {
                Message = START_MESSAGE,
                Position = SharedSettings.StageCenter
            };

            UpdateScoreDisplays();

            Components.Add(midScreenMessage);
            Components.Add(scoreDisplay);
            Components.Add(highScoreDisplay);
            Components.Add(helicopter);
            Components.Add(explosion);
            Components.Add(topBorder);
            Components.Add(bottomBorder);
            Components.Add(collisionManager);
        }

        /// <summary>
        ///     Updates the ActionScene and all of its enabled components
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();

            if (State == ActionSceneStates.InPlay &&
                keyboardState.NewKeyPress(oldKeyboardState, Keys.Space))
            {
                PauseGame();
            }
            else if (State == ActionSceneStates.Paused &&
                keyboardState.NewKeyPress(oldKeyboardState, Keys.Space))
            {
                ResumeGame();
            }
            else if (State == ActionSceneStates.PreStart && 
                (mouseState.LeftMouseNewClick(oldMouseState) ||
                keyboardState.NewKeyPress(oldKeyboardState, Keys.Up)))
            {
                StartGame();
            }
            else if (helicopter.HasCrashed && State != ActionSceneStates.GameOver)
            {
                EndGame();
            }
            else if (State == ActionSceneStates.InPlay)
            {
                if (durationScore < int.MaxValue)
                {
                    UpdateScore(gameTime.ElapsedGameTime.Milliseconds);
                }

                SharedSettings.StageSpeed.X = Math.Min(SharedSettings.StageSpeed.X * 1.0005f,
                    SharedSettings.DEFAULT_STAGE_SPEED_X * 2f);

                UpdateObstaclePositions();
                UpdateScoreDisplays();
            }
            else if (State == ActionSceneStates.GameOver &&
                (mouseState.LeftMouseNewClick(oldMouseState) ||
                keyboardState.NewKeyPress(oldKeyboardState, Keys.Up)))
            {
                ResetToInitialState();
            }

            oldMouseState = mouseState;
            oldKeyboardState = keyboardState;


            base.Update(gameTime);
        }

        /// <summary>
        ///     Shows and enables the ActionScene
        /// </summary>
        public override void Show()
        {
            // Set oldMouseState to having left mouse button pressed so that if it is held down upon 
            // entering the scene it needs to be released and repressed to start the game
            oldMouseState = new MouseState(oldMouseState.X, oldMouseState.Y,
                oldMouseState.ScrollWheelValue, ButtonState.Pressed, oldMouseState.RightButton,
                oldMouseState.MiddleButton, oldMouseState.XButton1, oldMouseState.XButton2);

            // Same reasoning as the oldMouseState
            oldKeyboardState = new KeyboardState(Keys.Up);

            if (State == ActionSceneStates.GameOver)
            {
                ResetToInitialState();
            }

            midScreenMessage.Start();

            base.Show();
        }

        /// <summary>
        ///     Hides and disables the ActionScene
        /// </summary>
        public override void Hide()
        {
            if (!helicopter.HasCrashed && State == ActionSceneStates.InPlay)
            {
                PauseGame();
            }

            midScreenMessage.Stop();

            base.Hide();
        }

        /// <summary>
        ///     Creates and positions the obstacles required by the ActionScene
        /// </summary>
        /// <param name="obstacleTexture">The Texture2D for the obstacles</param>
        /// <param name="collisionSound">The sound the obstacle should play when in a collision</param>
        private void GenerateObstacles(Texture2D obstacleTexture, SoundEffect collisionSound)
        {
            Vector2 obstaclePosition = Vector2.Zero;
            float heliCenterY = helicopter.GetBounds().Center.Y;

            for (int i = 0; i <= NUMBER_OF_OBSTACLES; i++)
            {
                Obstacle obstacle;

                if (obstaclePosition == Vector2.Zero) // First iteration
                {
                    obstacle = new Obstacle(Game, spriteBatch, obstacleTexture, collisionSound);

                    obstacle.GenerateRandomPosition(SharedSettings.Stage.X, 0, 0, heliCenterY);
                }
                else
                {
                    obstacle = new Obstacle(Game, spriteBatch, obstacleTexture, collisionSound);

                    obstacle.GenerateRandomPosition(obstaclePosition.X, minObstacleXSpacing, maxObstacleXSpacing, heliCenterY);
                }

                obstacle.Enabled = false;

                Components.Add(obstacle);
                obstacles.Add(obstacle);

                obstaclePosition = obstacle.Position;
            }
        }

        /// <summary>
        ///     Resets the ActionScene to its initial state
        /// </summary>
        private void ResetToInitialState()
        {
            state = ActionSceneStates.PreStart;

            midScreenMessage.Message = START_MESSAGE;

            midScreenMessage.Start();

            SharedSettings.StageSpeed.X = SharedSettings.DEFAULT_STAGE_SPEED_X;

            durationScore = 0;
            scoreDisplay.Message = "0";
            highScoreDisplay.Color = SharedSettings.HighestScoreColor;

            helicopter.Reset();
            bottomBorder.Enabled = false;
            topBorder.Enabled = false;

            foreach (Obstacle obstacle in obstacles)
            {
                obstacle.Hide();
            }

            UpdateObstaclePositions(true);
        }

        /// <summary>
        ///     Updates the durationScore, capping it at int.MaxValue
        /// </summary>
        /// <param name="msSinceLastUpdate">Number of milliseconds since the last scene update</param>
        private void UpdateScore(int msSinceLastUpdate)
        {
            int scoreIncrease = msSinceLastUpdate *
                (int)SharedSettings.StageSpeedChange;

            // If the max value minus the increase is more than the duration score, it would overflow
            if (int.MaxValue - scoreIncrease < durationScore)
            {
                durationScore = int.MaxValue;
            }
            else if (GetScore() < HighScoreScene.HighestScore)
            {
                durationScore += scoreIncrease;

                if (GetScore() >= HighScoreScene.HighestScore)
                {
                    highScoreDisplay.Color = SharedSettings.NormalTextColor;

                    beatHighestScoreSound.Play();
                }
            }
            else
            {
                durationScore += scoreIncrease;
            }
        }

        /// <summary>
        ///     Loops the obstacles through the scene by placing them off the right of the screen
        ///     when they go off the left of the screen
        /// </summary>
        /// <param name="gameReset">True if the game is being reset</param>
        private void UpdateObstaclePositions(bool gameReset = false)
        {
            float minObstacleXSpace = minObstacleXSpacing;
            float maxObstacleXSpace = maxObstacleXSpacing * SharedSettings.StageSpeedChange;
            float heliCenterY = helicopter.GetBounds().Center.Y;

            for (int i = 0; i < obstacles.Count; i++)
            {
                Obstacle obstacle = obstacles[i];

                if (obstacle.Enabled)
                {
                    continue;
                }

                if (gameReset && i == 0)
                {
                    obstacle.GenerateRandomPosition(SharedSettings.Stage.X, 0, 0, heliCenterY);
                }
                else if (i == 0)
                {
                    obstacle.GenerateRandomPosition(obstacles.Last().Position.X,
                        minObstacleXSpace, maxObstacleXSpace, heliCenterY);
                }
                else
                {
                    obstacle.GenerateRandomPosition(obstacles[i - 1].Position.X,
                        minObstacleXSpace, maxObstacleXSpace, heliCenterY);
                }

                if (!gameReset)
                {
                    obstacle.Show();
                }
            }
        }

        /// <summary>
        ///     Updates the score and highscore displays
        /// </summary>
        private void UpdateScoreDisplays()
        {
            int currentScore = GetScore();

            scoreDisplay.Message = currentScore.ToString();

            if (currentScore > HighScoreScene.HighestScore)
            {
                highScoreDisplay.Message = currentScore.ToString();

                Vector2 newPosition = highScoreDisplay.Position;

                newPosition.X = SharedSettings.Stage.X -
                    highScoreDisplay.Font.MeasureString(highScoreDisplay.Message).X;

                highScoreDisplay.Position = newPosition;
            }
        }

        /// <summary>
        ///     Sets the ActionScene into an in play state
        /// </summary>
        private void StartGame()
        {
            state = ActionSceneStates.InPlay;

            midScreenMessage.Message = string.Empty;
            midScreenMessage.Stop();

            foreach (IGameComponent component in Components)
            {
                if (component == midScreenMessage || component.GetType() == typeof(Explosion))
                {
                    continue;
                }

                DrawableGameComponent drawableGameComponent = component as DrawableGameComponent;

                if (drawableGameComponent != null)
                {
                    drawableGameComponent.Enabled = true;
                    drawableGameComponent.Visible = true;

                    continue;
                }

                GameComponent gameComponent = component as GameComponent;

                if (gameComponent != null)
                {
                    gameComponent.Enabled = true;
                }
            }
        }

        /// <summary>
        ///     Sets the ActionScene into a game over state
        /// </summary>
        private void EndGame()
        {
            state = ActionSceneStates.GameOver;

            midScreenMessage.Message = string.Format("Game Over - Final Score: {0}", GetScore());

            DisableComponents();

            midScreenMessage.Start();
        }

        /// <summary>
        ///     Puts the ActionScene into a paused state
        /// </summary>
        private void PauseGame()
        {
            state = ActionSceneStates.Paused;

            midScreenMessage.Message = "Press Space to Unpause";

            DisableComponents();

            midScreenMessage.Start();
        }

        /// <summary>
        ///     Resumes the ActionScene from a paused state
        /// </summary>
        private void ResumeGame()
        {
            foreach (IGameComponent gameComponent in Components)
            {
                GameComponent component = gameComponent as GameComponent;

                if (component != null && component.GetType() != typeof(Explosion))
                {
                    component.Enabled = true;
                }
            }

            midScreenMessage.Message = string.Empty;
            midScreenMessage.Stop();

            state = ActionSceneStates.InPlay;
        }

        /// <summary>
        ///     Disables all of the GameComponents in Components
        /// </summary>
        private void DisableComponents()
        {
            foreach (IGameComponent component in Components)
            {
                GameComponent gameComponent = component as GameComponent;

                if (gameComponent != null && gameComponent.GetType() != typeof(Explosion))
                {
                    gameComponent.Enabled = false;
                }
            }   
        }
    }
}