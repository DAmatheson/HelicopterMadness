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
    /// <summary>
    ///     Displays the menu entries and allows the user to move between them
    /// </summary>
    public class MenuComponent : DrawableGameComponent
    {
        private readonly SpriteBatch spriteBatch;
        private readonly Color regularColor = Color.Black;
        private readonly SpriteFont regularFont;
        private readonly SpriteFont highlightFont;
        private readonly IList<string> menuItems;
        private readonly Vector2 position;

        private int selectedIndex = 0;
        private KeyboardState oldState;

        /// <summary>
        ///     Initializes a new instance of MenuComponent with the provided parameters
        /// </summary>
        /// <param name="game">The Game the MenuComponent belongs to</param>
        /// <param name="spriteBatch">The SpriteBatch the MenuComponent will draw itself with</param>
        /// <param name="regularFont">The regular font for the unselected menu entries</param>
        /// <param name="highlightFont">The highlight font for the selected menu entry</param>
        /// <param name="menuEntries">The list of strings for the menu entries</param>
        public MenuComponent(Game game, SpriteBatch spriteBatch, SpriteFont regularFont,
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

            Vector2 longestMenuItem = highlightFont.MeasureString(longestEntry);

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
        /// <param name="gameTime">Provides a snapshot of timing values</param>
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
            }

            oldState = keyState;
        }

        /// <summary>
        ///     Draws the MenuComponent's menu entries
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPosition = position;

            for (int i = 0; i < menuItems.Count; i++)
            {
                if (selectedIndex == i)
                {
                    spriteBatch.DrawString(highlightFont, menuItems[i], tempPosition,
                        SharedSettings.HighlightTextColor);

                    tempPosition.Y += highlightFont.LineSpacing;
                }
                else
                {
                    spriteBatch.DrawString(regularFont, menuItems[i], tempPosition, regularColor);

                    tempPosition.Y += regularFont.LineSpacing;
                }
            }
        }
    }
}