/* SharedSettings.cs
 * Purpose: Common Settings and values used throughout the game
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.04: Created
 */

using System;
using Microsoft.Xna.Framework;

namespace HelicopterMadness
{
    /// <summary>
    ///     Contains shared settings used throughout the game
    /// </summary>
    public static class SharedSettings
    {
        /// <summary>
        ///     The layer depth value for obstacles in the game
        /// </summary>
        public const float OBSTACLE_LAYER = 0.75f;

        /// <summary>
        ///     The default stage X axis movement speed
        /// </summary>
        public const float DEFAULT_STAGE_SPEED_X = 540f;

        /// <summary>
        ///     The filename for the highscore save file
        /// </summary>
        public const string HIGHSCORE_FILE_NAME = "Highscore.txt";

        /// <summary>
        ///     The Color for highlight text
        /// </summary>
        public static readonly Color HighlightTextColor = Color.DarkRed;

        /// <summary>
        ///     The Color for normal text
        /// </summary>
        public static readonly Color NormalTextColor = Color.Yellow;

        /// <summary>
        ///     The Color for normal text
        /// </summary>
        public static readonly Color WinnerTextColor = Color.Azure;

        /// <summary>
        ///     Gets the speed change from the starting speed to the current speed
        /// </summary>
        public static float StageSpeedChange
        {
            get
            {
                return StageSpeed.X / DEFAULT_STAGE_SPEED_X;
            }
        }

        /// <summary>
        ///     A Vector2 containing the greatest X and Y for the stage
        /// </summary>
        public static Vector2 Stage;

        /// <summary>
        ///     A Vector2 containing the X and Y for the center of the stage
        /// </summary>
        public static Vector2 StageCenter;

        /// <summary>
        ///     A Vector2 containing the X and Y speed for objects moving on the stage
        /// </summary>
        public static Vector2 StageSpeed = new Vector2(DEFAULT_STAGE_SPEED_X, 0);

        /// <summary>
        ///     A static random number generator
        /// </summary>
        public static readonly Random Random = new Random();
    }
}