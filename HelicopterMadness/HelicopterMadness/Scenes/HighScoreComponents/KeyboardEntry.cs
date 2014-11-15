using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelicopterMadness.Scenes.HighScoreComponents
{
    /// <summary>
    /// 
    /// </summary>
    public static class KeyboardEntry
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyboardState">current state of the keyboard</param>
        /// <param name="oldState">the previous sate of the keyboard</param>
        /// <param name="key">the character that was presed</param>
        /// <returns>true if keys pressed returns a value</returns>
        public static bool KeyboardInput(KeyboardState keyboardState, KeyboardState oldState, out char key)
        {
            Keys[] keys = keyboardState.GetPressedKeys();


            if (keys.Length > 0 && !oldState.IsKeyDown(keys[0]))
            {
                switch (keys[0])
                {
                    //inline or change for school standards
                    //Alphabet keys
                    case Keys.A: { key = 'A'; } return true;
                    case Keys.B: { key = 'B'; } return true;
                    case Keys.C: { key = 'C'; } return true;
                    case Keys.D: { key = 'D'; } return true;
                    case Keys.E: { key = 'E'; } return true;
                    case Keys.F: { key = 'F'; } return true;
                    case Keys.G: { key = 'G'; } return true;
                    case Keys.H: { key = 'H'; } return true;
                    case Keys.I: { key = 'I'; } return true;
                    case Keys.J: { key = 'J'; } return true;
                    case Keys.K: { key = 'K'; } return true;
                    case Keys.L: { key = 'L'; } return true;
                    case Keys.M: { key = 'M'; } return true;
                    case Keys.N: { key = 'N'; } return true;
                    case Keys.O: { key = 'O'; } return true;
                    case Keys.P: { key = 'P'; } return true;
                    case Keys.Q: { key = 'Q'; } return true;
                    case Keys.R: { key = 'R'; } return true;
                    case Keys.S: { key = 'S'; } return true;
                    case Keys.T: { key = 'T'; } return true;
                    case Keys.U: { key = 'U'; } return true;
                    case Keys.V: { key = 'V'; } return true;
                    case Keys.W: { key = 'W'; } return true;
                    case Keys.X: { key = 'X'; } return true;
                    case Keys.Y: { key = 'Y'; } return true;
                    case Keys.Z: { key = 'Z'; } return true;
                    default:
                        //should probably make a ding noise or something like that
                        break;

                }
            }

            key = (char)0;
            return false;
        }
    }
}
