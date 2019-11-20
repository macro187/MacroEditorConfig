using MacroGuards;

namespace MacroEditorConfig
{
    public class EditorConfigSectionHeaderLine : EditorConfigLine
    {

        public static EditorConfigSectionHeaderLine TryParse(int lineNumber, string line)
        {
            Guard.NotNull(line, nameof(line));
            var trimmed = line.Trim();
            if (!trimmed.StartsWith("[") && !trimmed.EndsWith("]")) return null;
            var name = trimmed.Substring(1, trimmed.Length - 2);
            return new EditorConfigSectionHeaderLine(lineNumber, line, name);
        }


        EditorConfigSectionHeaderLine(int lineNumber, string text, string name)
            : base(lineNumber, text)
        {
            Guard.NotNull(name, nameof(name));
            Name = name;
        }


        public string Name { get; }

    }
}
