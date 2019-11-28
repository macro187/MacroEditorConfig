using System;
using MacroGuards;

namespace MacroEditorConfig
{
    public abstract class EditorConfigLine
    {

        protected EditorConfigLine(int lineNumber, string text)
        {
            Guard.NotNull(text, nameof(text));
            if (lineNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(lineNumber), $"Invalid line number {lineNumber} specified");
            }

            Index = lineNumber - 1;
            LineNumber = lineNumber;
            Text = text;
        }


        /// <summary>
        /// Zero-based ordinal position of the line within the file
        /// </summary>
        ///
        public int Index { get; }


        /// <summary>
        /// One-based ordinal position of the line within the file
        /// </summary>
        public int LineNumber { get; }


        /// <summary>
        /// The full text of the line
        /// </summary>
        ///
        public string Text { get; }

    }
}
