/* InputHelper.cs
 * Purpose: Helper methods for checking input states
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.12: Created
 *      Sean Coombes, 2014.11.14: Added GetAlphaCharacterInput
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HelicopterMadness
{
    /// <summary>
    ///     Extension methods for mouse and keyboard state
    /// </summary>
    public static class InputHelper
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

        /// <summary>
        ///     Tries to get a single alphabetical character from keyboard input
        /// </summary>
        /// <param name="keyboardState">Current state of the keyboard</param>
        /// <param name="oldState">The previous state of the keyboard, optional</param>
        /// <returns>char if a new alpha key was pressed, null otherwise</returns>
        public static char? GetAlphaCharacterInput(this KeyboardState keyboardState, KeyboardState oldState = default(KeyboardState))
        {
            Keys[] keys = keyboardState.GetPressedKeys();

            if (keys.Length > 0 && !oldState.IsKeyDown(keys[0]))
            {
                switch (keys[0])
                {
                    //Alphabet keys
                    case Keys.A: return 'A';
                    case Keys.B: return 'B';
                    case Keys.C: return 'C';
                    case Keys.D: return 'D';
                    case Keys.E: return 'E'; 
                    case Keys.F: return 'F'; 
                    case Keys.G: return 'G'; 
                    case Keys.H: return 'H'; 
                    case Keys.I: return 'I'; 
                    case Keys.J: return 'J'; 
                    case Keys.K: return 'K'; 
                    case Keys.L: return 'L'; 
                    case Keys.M: return 'M'; 
                    case Keys.N: return 'N'; 
                    case Keys.O: return 'O'; 
                    case Keys.P: return 'P'; 
                    case Keys.Q: return 'Q'; 
                    case Keys.R: return 'R'; 
                    case Keys.S: return 'S'; 
                    case Keys.T: return 'T'; 
                    case Keys.U: return 'U'; 
                    case Keys.V: return 'V'; 
                    case Keys.W: return 'W'; 
                    case Keys.X: return 'X'; 
                    case Keys.Y: return 'Y'; 
                    case Keys.Z: return 'Z';
                }
            }

            return null;
        }
    }
}