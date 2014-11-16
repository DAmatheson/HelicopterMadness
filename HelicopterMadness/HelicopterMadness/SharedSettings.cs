﻿/* SharedSettings.cs
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
        ///     The default stage X axis movement speed
        /// </summary>
        public const float DEFAULT_STAGE_SPEED_X = 540f;

        public const string HIGHSCORE_FILE_NAME = "Highscore.txt";

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