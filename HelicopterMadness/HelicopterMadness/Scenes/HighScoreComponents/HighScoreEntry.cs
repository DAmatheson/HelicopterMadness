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
    public class HighScoreEntry
    {
        private int paddingWidth;

        /// <summary>
        ///     The player name for the entry
        /// </summary>
        private string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                // TODO: Consider REGEX and regex on initial set so characters are limited to valid ones
                // Force the name into our defined highscore naming scheme
                name = value.Trim().PadRight(3).Substring(0, 3).Replace(' ', 'A').ToUpper();
            }
        }

        /// <summary>
        ///     The score for the entry
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        ///     Initializes a new instance of HighScoreEntry with the provided parameters
        /// </summary>
        /// <param name="score">The score for the entry</param>
        /// <param name="paddingWidth">The padding width count for the entry. Defaults to 19</param>
        public HighScoreEntry(int score, int paddingWidth = 19)
        {
            this.paddingWidth = paddingWidth;

            Score = score;

            SetNameForEntry();
        }

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
        ///     Removes a specific index's character from Name
        /// </summary>
        public void RemoveCharFromName(int charIndex)
        {
            name = name.Remove(charIndex, 1).Insert(charIndex, "_");
        }

        public void SetNameForEntry()
        {
            name = "___";
        }
      
        /// <summary>
        ///     Returns the HighScoreEntry formatted for display
        /// </summary>
        /// <returns>Returns the HighScoreEntry formatted for display</returns>
        public override string ToString()
        {
            return string.Format("{0}{1}{2}", Name, new string('.', paddingWidth - Score.ToString().Length), Score);
        }

        /// <summary>
        ///     Returns the HighScoreEntry formatted for saving
        /// </summary>
        /// <returns>Returns the HighScoreEntry formatted for saving</returns>
        public string ToSaveString()
        {
            return string.Format("{0} {1}", Name, Score);
        }
    }
}