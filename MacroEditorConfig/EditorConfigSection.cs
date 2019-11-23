using System.Collections.Generic;
using System.Linq;
using MacroGuards;

namespace MacroEditorConfig
{
    public class EditorConfigSection
    {

        public EditorConfigSection(IEnumerable<EditorConfigLine> lines)
        {
            Guard.NotNull(lines, nameof(lines));
            Lines = lines.ToList();
        }


        public IReadOnlyList<EditorConfigLine> Lines { get; }


        public EditorConfigSectionHeaderLine Header =>
            Lines.OfType<EditorConfigSectionHeaderLine>().SingleOrDefault();


        public IEnumerable<EditorConfigDeclarationLine> Declarations =>
            Lines.OfType<EditorConfigDeclarationLine>();


        public bool IsPreamble => Header == null;


    }
}
