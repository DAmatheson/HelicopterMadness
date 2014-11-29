/* MenuScene.cs
 * Purpose: Main menu scene
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.05: Created
 */

using System;
using System.Collections.Generic;
using HelicopterMadness.Scenes.BaseScene;
using HelicopterMadness.Scenes.CommonComponents;
using HelicopterMadness.Scenes.MenuComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HelicopterMadness.Scenes
{
    /// <summary>
    ///     Main Menu Game Scene
    /// </summary>
    public class MenuScene : GameScene
    {
        private readonly MenuComponent menu;
        private readonly SceneManager sceneManager;
        private readonly FlashingTextDisplay helpDisplay;

        /// <summary>
        ///     Initializes a new instance of MenuScene with the provided parameters
        /// </summary>
        /// <param name="game">The Game the MenuScene belongs to</param>
        /// <param name="spriteBatch">The SpriteBatch the MenuScene uses to draw its components</param>
        /// <param name="sceneManager">The SceneManager managing the MenuScene</param>
        /// <param name="menuItems">String list containing the menu items</param>
        public MenuScene(Game game, SpriteBatch spriteBatch, SceneManager sceneManager, IList<string> menuItems)
            : base(game, spriteBatch)
        {
            this.sceneManager = sceneManager;

            SpriteFont regularFont = Game.Content.Load<SpriteFont>("Fonts/Regular");
            SpriteFont highlightFont = Game.Content.Load<SpriteFont>("Fonts/Highlight");
            SpriteFont headerFont = Game.Content.Load<SpriteFont>("Fonts/Header");
            SoundEffect selectionChangeSound = Game.Content.Load<SoundEffect>("Sounds/MenuSelectionChange");

            menu = new MenuComponent(game, spriteBatch, regularFont, highlightFont, menuItems,
                selectionChangeSound);

            string titleMessage = "Helicopter Madness";

            Vector2 titlePosition = new Vector2((SharedSettings.Stage.X - headerFont.MeasureString(titleMessage).X) / 2f, 50f);

            TextDisplay title = new TextDisplay(
                game, spriteBatch, headerFont, titlePosition, SharedSettings.TitleTextColor, titleMessage);

            string helpMessage = "Use Arrow Keys to Navigate and Enter to Select";

            Vector2 helpPosition = new Vector2((SharedSettings.Stage.X - highlightFont.MeasureString(helpMessage).X) / 2f,
                    (SharedSettings.Stage.Y - SharedSettings.Stage.Y / 4f));

            helpDisplay = new FlashingTextDisplay(game, spriteBatch, highlightFont, Color.Black, SharedSettings.BLINK_RATE)
            {
                Message = helpMessage,
                Position = helpPosition
            };

            helpDisplay.Start();

            Components.Add(menu);
            Components.Add(title);
            Components.Add(helpDisplay);
        }

        /// <summary>
        ///     Notifies the SceneManager of scene changes and updates the MenuScene's components
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Enter))
            {
                if (helpDisplay.Visible)
                {
                    RemoveHelpMessage();
                }
                
                sceneManager.OnMenuSelection(
                    (MenuItems) Enum.ToObject(typeof (MenuItems), menu.SelectedIndex));
            }
            else if (menu.SelectedIndex != 0 && helpDisplay.Visible)
            {
                RemoveHelpMessage();
            }

            base.Update(gameTime);
        }

        private void RemoveHelpMessage()
        {
            helpDisplay.Stop();

            Components.Remove(helpDisplay);
        }
    }
}