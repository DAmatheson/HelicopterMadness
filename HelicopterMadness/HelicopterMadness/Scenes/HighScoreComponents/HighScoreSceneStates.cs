/* ActionSceneStates.cs
 * Purpose: Enum representing the states of the action scene
 * 
 * Revision History:
 *      Sean Coombes, 2014.11.15: Created
 */ 
namespace HelicopterMadness.Scenes.HighScoreComponents
{
    /// <summary>
    ///     Identifies a particular state of the HighscoreScene
    /// </summary>
    public enum HighScoreSceneStates
    {
        /// <summary>
        ///     Represents a view state where entered from menu
        /// </summary>
        View,

        /// <summary>
        ///     Represents a view where a new score is being entered
        /// </summary>
        NewScoreEntry,

        /// <summary>
        ///     Represents a view where a new score was just entered
        /// </summary>
        NewScoreAdded,

        /// <summary>
        ///     Represents a state where the scene is ready to change
        /// </summary>
        Action,
    }
}
