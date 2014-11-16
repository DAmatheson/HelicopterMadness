/* InputExtensionMethods.cs
 * Purpose: Helper methods for checking input states
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.12: Created
 */ 

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HelicopterMadness
{
    /// <summary>
    ///     Extension methods for mouse and keyboard state
    /// </summary>
    public static class InputExtensionMethods
    {
        /// <summary>
        ///     Checks that the left mouse button was released before being pressed and the game is active
        /// </summary>
        /// <param name="ms">Current MouseState</param>
        /// <param name="previousState">Previous MouseState</param>
        /// <param name="game">Game to check if active</param>
        /// <returns></returns>
        public static bool LeftMouseNewClick(this MouseState ms, MouseState previousState, Game game)
        {
            return game.IsActive &&
                ms.LeftButton == ButtonState.Pressed &&
                previousState.LeftButton == ButtonState.Released;
        }

        /// <summary>
        ///     Checks that the left mouse button was pressed before being released and the game is active
        /// </summary>
        /// <param name="ms">Current MouseState</param>
        /// <param name="previousState">Previous MouseState</param>
        /// <param name="game">Game to check if active</param>
        /// <returns></returns>
        public static bool LeftMouseNewRelease(this MouseState ms, MouseState previousState, Game game)
        {
            return game.IsActive &&
                ms.LeftButton == ButtonState.Released &&
                previousState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        ///     Checks if the left mouse button was clicked
        /// </summary>
        /// <param name="mouseState">MouseState to check</param>
        /// <returns>True if clicked, false otherwise</returns>
        public static bool LeftMouseClicked(this MouseState mouseState)
        {
            return mouseState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        ///     Checks that the key was up before being pressed
        /// </summary>
        /// <param name="ks">Current KeyboardState</param>
        /// <param name="previousState">Previous KeyboardState</param>
        /// <param name="key">Key to check for press</param>
        /// <returns></returns>
        public static bool NewKeyPress(this KeyboardState ks, KeyboardState previousState, Keys key)
        {
            return ks.IsKeyDown(key) && previousState.IsKeyUp(key);
        }
    }
}