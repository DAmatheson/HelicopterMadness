/* HighScoreEntry.cs
 * Purpose: Represents a high score entry 
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.15: Created
 */

namespace HelicopterMadness.Scenes.HighScoreComponents
{
    /// <summary>
    ///     A high score entry for the game
    /// </summary>
    public struct HighScoreEntry
    {
        private int paddingWidth;

        /// <summary>
        ///     The player name for the entry
        /// </summary>
        public string Name;

        /// <summary>
        ///     The score for the entry
        /// </summary>
        public int Score;

        /// <summary>
        ///     Initializes a new instance of HighScoreEntry with the provided parameters
        /// </summary>
        /// <param name="name">The player name for the entry</param>
        /// <param name="score">The score for the entry</param>
        /// <param name="paddingWidth">The padding width count for the entry. Defaults to 19</param>
        public HighScoreEntry(string name, int score, int paddingWidth = 19)
        {
            this.paddingWidth = paddingWidth;

            Name = name;
            Score = score;
        }

        /// <summary>
        ///     Returns the HighScoreEntry formatted for display
        /// </summary>
        /// <returns>Returns the HighScoreEntry formatted for display</returns>
        public override string ToString()
        {
            return string.Format("{0}{1}{2}", Name, new string('.', paddingWidth - Score.ToString().Length), Score);
        }
    }
}