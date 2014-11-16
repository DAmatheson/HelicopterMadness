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
        private readonly int paddingWidth;

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
                name = value.Trim().Replace('_', ' ').PadRight(3).Substring(0, 3).Replace(' ', 'A').ToUpper();
            }
        }

        /// <summary>
        ///     The score for the entry
        /// </summary>
        public int Score { get; private set; }

        /// <summary>
        ///     Initializes a new instance of HighScoreEntry with the provided parameters and in a 
        ///     state for name entry
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
        ///     Replaces the character at the specified index with the replacement character if
        ///     the replacement character is between A and Z
        /// </summary>
        /// <param name="charIndex">The index of the character to replace</param>
        /// <param name="replacementChar">The replacement character</param>
        public void ReplaceChar(int charIndex, char replacementChar)
        {
            // 65 = A, 90 = Z
            // TODO: Magic numbers if we care
            if (charIndex < name.Length && replacementChar >= 65 && replacementChar <= 90)
            {
                name = name.Remove(charIndex, 1).Insert(charIndex, replacementChar.ToString());
            }
        }

        /// <summary>
        ///     Replaces the specified index from Name with an underscore
        /// </summary>
        /// <param name="charIndex">The index of the character to remove</param>
        public void RemoveCharFromName(int charIndex)
        {
            if (charIndex < name.Length)
            {
                name = name.Remove(charIndex, 1).Insert(charIndex, "_");
            }
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
            // Run the name through the Set method to ensure it is in a valid format
            Name = Name;

            return string.Format("{0} {1}", Name, Score);
        }

        /// <summary>
        ///     Sets the Name to three underscores for name input purposes
        /// </summary>
        private void SetNameForEntry()
        {
            name = "___";
        }
    }
}