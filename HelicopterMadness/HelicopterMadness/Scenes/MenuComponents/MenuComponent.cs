/* MenuComponent.cs
 * Purpose: Displays the menu items and allows the user to select one
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.05: Created
 */

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HelicopterMadness.Scenes.MenuComponents
{
    // TODO: Comments
    public class MenuComponent : DrawableGameComponent
    {
        private readonly SpriteBatch spriteBatch;
        private readonly Color regularColor = Color.Black;
        private readonly Color highlightColor = Color.Red;
        private readonly SpriteFont regularFont;
        private readonly SpriteFont highlightFont;
        private readonly IList<string> menuItems;

        private Vector2 longestMenuItem;

        private bool noKeyInput = true;

        private int selectedIndex = 0;
        private Vector2 position;
        private KeyboardState oldState;

        public MenuComponent(
            Game game, SpriteBatch spriteBatch, SpriteFont regularFont,
            SpriteFont highlightFont, IList<string> menuEntries)
            : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.regularFont = regularFont;
            this.highlightFont = highlightFont;

            menuItems = menuEntries;

            int length = 0;
            string longestEntry = string.Empty;

            foreach (string entry in menuEntries)
            {
                if (entry.Length > length)
                {
                    length = entry.Length;
                    longestEntry = entry;
                }
            }

            longestMenuItem = highlightFont.MeasureString(longestEntry);

            position = new Vector2((SharedSettings.Stage.X - longestMenuItem.X) / 2f,
                (SharedSettings.Stage.Y - longestMenuItem.Y * menuEntries.Count) / 2f);
        }

        /// <summary>
        ///     Gets the menu's selected index
        /// </summary>
        public int SelectedIndex
        {
            get { return selectedIndex; }
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                if (selectedIndex <= 0)
                {
                    selectedIndex = menuItems.Count - 1;
                }
                else
                {
                    selectedIndex -= 1;
                }

                noKeyInput = false;
            }

            if (keyState.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                if (selectedIndex >= menuItems.Count - 1)
                {
                    selectedIndex = 0;
                }
                else
                {
                    selectedIndex++;
                }

                noKeyInput = false;
            }

            oldState = keyState;
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPosition = position;

            for (int i = 0; i < menuItems.Count; i++)
            {
                if (selectedIndex == i)
                {
                    spriteBatch.DrawString(highlightFont, menuItems[i], tempPosition, highlightColor);

                    tempPosition.Y += highlightFont.LineSpacing;
                }
                else
                {
                    spriteBatch.DrawString(regularFont, menuItems[i], tempPosition, regularColor);

                    tempPosition.Y += regularFont.LineSpacing;
                }
            }

            if (noKeyInput) // TODO: Temp solution
            {
                string helpMessage = "Use Arrow Keys To Navigate And Enter To Select";

                Vector2 helpPosition =
                    new Vector2((SharedSettings.Stage.X - highlightFont.MeasureString(helpMessage).X) / 2, 10);

                spriteBatch.DrawString(highlightFont, helpMessage, helpPosition, regularColor);
            }
        }
    }
}