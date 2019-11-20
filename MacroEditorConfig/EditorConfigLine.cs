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

            LineNumber = lineNumber;
            Text = text;
        }


        public int LineNumber { get; }
        public string Text { get; }

    }
}
