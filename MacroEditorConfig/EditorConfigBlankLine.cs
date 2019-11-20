using MacroGuards;

namespace MacroEditorConfig
{
    public class EditorConfigBlankLine : EditorConfigLine
    {

        public static EditorConfigBlankLine TryParse(int lineNumber, string line)
        {
            Guard.NotNull(line, nameof(line));
            var trimmedLine = line.Trim();
            if (trimmedLine != "") return null;
            return new EditorConfigBlankLine(lineNumber, line);
        }


        EditorConfigBlankLine(int lineNumber, string text)
            : base(lineNumber, text)
        {
        }

    }
}
