using MacroGuards;

namespace MacroEditorConfig
{
    public class EditorConfigDeclarationLine : EditorConfigLine
    {

        public static EditorConfigDeclarationLine TryParse(int lineNumber, string line)
        {
            Guard.NotNull(line, nameof(line));
            var equalsPosition = line.IndexOf('=');
            if (equalsPosition < 0) return null;
            var key = line.Substring(0, equalsPosition).Trim();
            var value = line.Substring(equalsPosition + 1).Trim();
            return new EditorConfigDeclarationLine(lineNumber, line, key, value);
        }


        EditorConfigDeclarationLine(int lineNumber, string text, string key, string value)
            : base(lineNumber, text)
        {
            Guard.NotNull(key, nameof(key));
            Guard.NotNull(value, nameof(value));
            Key = key;
            Value = value;
        }


        public string Key { get; }
        public string Value { get; }

    }
}
