/* MenuScene.cs
 * Purpose: Main menu scene
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.05: Created
 */

using System;
using System.Collections.Generic;
using HelicopterMadness.Scenes.BaseScene;
using HelicopterMadness.Scenes.MenuComponents;
using Microsoft.Xna.Framework;
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

            menu = new MenuComponent(game, spriteBatch, regularFont, highlightFont, menuItems);

            Components.Add(menu);
        }

        /// <summary>
        ///     Notifies the SceneManager of scene changes and updates the MenuScene's components
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            int menuSelectedIndex = menu.SelectedIndex;

            if (keyState.IsKeyDown(Keys.Enter))
            {
                sceneManager.OnMenuSelection(
                    (MenuItems) Enum.ToObject(typeof (MenuItems), menuSelectedIndex));
            }

            base.Update(gameTime);
        }
    }
}