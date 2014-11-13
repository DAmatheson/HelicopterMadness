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
        PreStart,
        InPlay,
        Paused,
        GameOver
    }
}