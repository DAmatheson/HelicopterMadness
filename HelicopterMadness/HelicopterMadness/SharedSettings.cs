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
    // TODO: Comments
    public static class SharedSettings
    {
        public const float DEFAULT_STAGE_SPEED_X = 9f;

        public static Vector2 Stage;

        public static Vector2 StageCenter;

        public static Vector2 StageSpeed;

        public static readonly Random Random = new Random();
    }
}