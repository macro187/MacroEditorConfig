using MacroGuards;

namespace MacroEditorConfig
{
    public class EditorConfigCommentLine : EditorConfigLine
    {

        public static EditorConfigCommentLine TryParse(int lineNumber, string line)
        {
            Guard.NotNull(line, nameof(line));
            var trimmedLine = line.Trim();
            if (!trimmedLine.StartsWith(";") && !trimmedLine.StartsWith("#")) return null;
            var comment = trimmedLine.Substring(1).Trim();
            return new EditorConfigCommentLine(lineNumber, line, comment);
        }


        EditorConfigCommentLine(int lineNumber, string text, string comment)
            : base(lineNumber, text)
        {
            Guard.NotNull(comment, nameof(comment));
            Comment = comment;
        }


        public string Comment { get; }

    }
}
