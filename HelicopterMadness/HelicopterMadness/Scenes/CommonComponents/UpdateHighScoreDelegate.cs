/* UpdateHighScoreDelegate.cs
 * Purpose: Delegate definition for handling updating high scores
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.12: Created
 */ 

namespace HelicopterMadness.Scenes.CommonComponents
{
    /// <summary>
    ///     Represents the method that will handle updating high scores
    /// </summary>
    /// <param name="score">The high score to act upon</param>
    public delegate void UpdateHighScoreDelegate(int score);
}