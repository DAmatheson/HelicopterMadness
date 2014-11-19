/* KeyboardInput.cs
 * Purpose: Helper methods for getting keyboard input
 * 
 * Revision History:
 *      Sean Coombes, 2014.11.14: Created
 */

using Microsoft.Xna.Framework.Input;

namespace HelicopterMadness.Scenes.HighScoreComponents
{
    /// <summary>
    ///     Helper methods for getting keyboard input
    /// </summary>
    public static class KeyboardInput
    {
        /// <summary>
        ///     Tries to get a single alphabetical character from keyboard input
        /// </summary>
        /// <param name="keyboardState">current state of the keyboard</param>
        /// <param name="oldState">the previous sate of the keyboard</param>
        /// <returns>A char if a new alpha key was pressed, null otherwise</returns>
        public static char? GetAlphaCharacterInput(KeyboardState keyboardState, KeyboardState oldState)
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
                    default:
                        // TODO: should probably make a ding noise or something like that
                        break;
                }
            }

            return null;
        }
    }
}
