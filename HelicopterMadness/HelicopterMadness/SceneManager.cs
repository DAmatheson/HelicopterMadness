/* SceneManager.cs
 * Purpose: Manage the state of GameScenes
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.05: Created
 */

using System.Collections.Generic;
using HelicopterMadness.Scenes;
using HelicopterMadness.Scenes.ActionComponents;
using HelicopterMadness.Scenes.HighScoreComponents;
using HelicopterMadness.Scenes.BaseScene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HelicopterMadness
{
    /// <summary>
    ///     Manages which GameScene is active
    /// </summary>
    public class SceneManager : DrawableGameComponent
    {
        private readonly Dictionary<MenuItems, GameScene> scenes;

        private readonly MenuScene menuScene;
        private readonly ActionScene actionScene;
        private readonly HighScoreScene highScoreScene;

        private GameScene enabledScene;

        /// <summary>
        ///     Initializes a new instance of SceneManager
        /// </summary>
        /// <param name="game">The Game the SceneManager belongs to</param>
        /// <param name="spriteBatch">The SpriteBatch the SceneManager uses to draw its components</param>
        public SceneManager(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            // These must match up with the order and values of the enum MenuItems
            List<string> menuEntries = new List<string>
            {
                "Start Game", "How To Play", "Help", "High Score", "Credits", "Quit"
            };

            menuScene = new MenuScene(game, spriteBatch, this, menuEntries);

            highScoreScene = new HighScoreScene(game, spriteBatch);
            actionScene = new ActionScene(game, spriteBatch);
            HowToPlayScene howToPlayScene = new HowToPlayScene(game, spriteBatch);
            HelpScene helpScene = new HelpScene(game, spriteBatch);
            CreditScene creditScene = new CreditScene(game, spriteBatch);

            scenes = new Dictionary<MenuItems, GameScene>
            {
                { MenuItems.StartGame, actionScene },
                { MenuItems.HowToPlay, howToPlayScene },
                { MenuItems.Help, helpScene },
                { MenuItems.HighScore, highScoreScene },
                { MenuItems.Credit, creditScene }
            };

            HideAllScenes();

            menuScene.Show();

            enabledScene = menuScene;
        }

        /// <summary>
        ///     Updates the enabled scene and potentially returns to the main menu
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Escape) && !menuScene.Enabled && !menuScene.Visible)
            {
                HideAllScenes();

                menuScene.Show();

                enabledScene = menuScene;
            }

            if (enabledScene == actionScene && actionScene.State == ActionSceneStates.GameOver &&
                highScoreScene.State == HighScoreSceneStates.View)
            {
                int gameScore = actionScene.GetScore();

                if (gameScore > highScoreScene.LowestScore)
                {
                    enabledScene.Hide();

                    enabledScene = highScoreScene;

                    highScoreScene.Show();

                    highScoreScene.AddScoreEntry(actionScene.GetScore());
                }
            }
            
            if (enabledScene == highScoreScene && highScoreScene.State == HighScoreSceneStates.Action)
            {
                enabledScene.Hide();

                enabledScene = actionScene;

                actionScene.Show();
            }

            enabledScene.Update(gameTime);
        }

        /// <summary>
        ///     Draws the enabled scene
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            enabledScene.Draw(gameTime);
        }

        /// <summary>
        ///     Hides and disables all managed scenes
        /// </summary>
        private void HideAllScenes()
        {
            foreach (GameScene gameScene in scenes.Values)
            {
                gameScene.Hide();
            }
        }

        /// <summary>
        ///     Switches the scene when a menu item is selected
        /// </summary>
        /// <param name="selectedItem">The scene to switch to</param>
        public void OnMenuSelection(MenuItems selectedItem)
        {
            menuScene.Hide();

            if (selectedItem == MenuItems.Quit)
            {
                Game.Exit();
            }
            else
            {
                enabledScene = scenes[selectedItem];

                enabledScene.Show();
            }
        }
    }
}