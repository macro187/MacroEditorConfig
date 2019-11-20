using System.Collections.Generic;
using System.Linq;
using MacroGuards;

namespace MacroEditorConfig
{
    public class EditorConfigFile
    {

        readonly List<string> lines;
        readonly List<EditorConfigSection> sections;


        public EditorConfigFile()
            : this(Enumerable.Empty<string>())
        {
        }


        public EditorConfigFile(IEnumerable<string> lines)
        {
            Guard.NotNull(lines, nameof(lines));
            this.lines = lines.ToList();
            Lines = this.lines;
            sections = new List<EditorConfigSection>();
            Sections = sections;
            Parse();
        }


        public IReadOnlyList<string> Lines { get; }
        public IReadOnlyList<EditorConfigSection> Sections { get; }


        void Parse()
        {
            sections.Clear();

            var lines =
                EditorConfigReader.Read(Lines)
                    .Concat(new EditorConfigLine[] { null });

            EditorConfigSectionHeaderLine currentHeader = null;
            var currentLines = new List<EditorConfigLine>();
            foreach (var line in lines)
            {
                EditorConfigSectionHeaderLine nextHeader;
                if (line is EditorConfigSectionHeaderLine header)
                {
                    nextHeader = header;
                }
                else if (line == null)
                {
                    nextHeader = null;
                }
                else
                {
                    currentLines.Add(line);
                    continue;
                }

                sections.Add(new EditorConfigSection(currentHeader, currentLines));
                currentHeader = nextHeader;
                currentLines.Clear();
            }
        }

    }
}
