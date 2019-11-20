using System.Collections.Generic;
using System.Linq;
using MacroGuards;

namespace MacroEditorConfig
{
    public class EditorConfigSection
    {

        public EditorConfigSection(
            EditorConfigSectionHeaderLine headerLine,
            IEnumerable<EditorConfigLine> lines
        )
        {
            Guard.NotNull(lines, nameof(lines));
            HeaderLine = headerLine;
            Lines = lines.ToList();
        }


        public EditorConfigSectionHeaderLine HeaderLine { get; }
        public IReadOnlyList<EditorConfigLine> Lines { get; }
        public bool IsPreamble => HeaderLine == null;

    }
}
