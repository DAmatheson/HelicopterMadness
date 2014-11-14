/* ActionSceneStates.cs
 * Purpose: Enum representing the states of the action scene
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.08: Created
 */ 

namespace HelicopterMadness.Scenes.ActionComponents
{
    /// <summary>
    ///     Identifies a particular state of an ActionScene
    /// </summary>
    public enum ActionSceneStates
    {
        /// <summary>
        ///     Represents a Pre Play state for the action scene
        /// </summary>
        PreStart,

        /// <summary>
        ///     Represents a In Play state for the action scene
        /// </summary>
        InPlay,

        /// <summary>
        ///     Represents a Paused state for the action scene
        /// </summary>
        Paused,

        /// <summary>
        ///     Represents a Game Over state for the action scene
        /// </summary>
        GameOver
    }
}