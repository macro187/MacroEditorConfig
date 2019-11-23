using System.Collections.Generic;
using MacroExceptions;
using MacroGuards;

namespace MacroEditorConfig
{
    public static class EditorConfigReader
    {

        public static IEnumerable<EditorConfigSection> Read(IEnumerable<string> lines)
        {
            Guard.NotNull(lines, nameof(lines));
            return ReadImpl(lines);
        }


        static IEnumerable<EditorConfigSection> ReadImpl(IEnumerable<string> lines)
        {
            EditorConfigSectionHeaderLine currentHeader = null;
            var currentLines = new List<EditorConfigLine>();
            foreach (var line in ReadLines(lines))
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

                yield return new EditorConfigSection(currentHeader, currentLines);
                currentHeader = nextHeader;
                currentLines.Clear();
            }
        }


        static IEnumerable<EditorConfigLine> ReadLines(IEnumerable<string> lines)
        {
            var lineNumber = 1;
            foreach (var line in lines)
            {
                yield return
                    EditorConfigBlankLine.TryParse(lineNumber, line) ??
                    EditorConfigCommentLine.TryParse(lineNumber, line) ??
                    EditorConfigSectionHeaderLine.TryParse(lineNumber, line) ??
                    EditorConfigDeclarationLine.TryParse(lineNumber, line) ??
                    (EditorConfigLine)null ??
                    throw new TextFileParseException("Unrecognised line", lineNumber, line);

                lineNumber++;
            }

            yield return null;
        }

    }
}
