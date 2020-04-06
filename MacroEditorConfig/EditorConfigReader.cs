using System.Collections.Generic;
using MacroExceptions;
using MacroGuards;

namespace MacroEditorConfig
{
    public static class EditorConfigReader
    {

        public static IEnumerable<EditorConfigSection> Read(IEnumerable<string> linesOfText)
        {
            Guard.NotNull(linesOfText, nameof(linesOfText));
            return ReadImpl(linesOfText);
        }


        static IEnumerable<EditorConfigSection> ReadImpl(IEnumerable<string> linesOfText)
        {
            var currentLines = new List<EditorConfigLine>();
            foreach (var line in ReadLines(linesOfText))
            {
                EditorConfigSectionHeaderLine nextHeader;
                if (line == null)
                {
                    nextHeader = null;
                }
                else if (line is EditorConfigSectionHeaderLine header)
                {
                    nextHeader = header;
                }
                else
                {
                    currentLines.Add(line);
                    continue;
                }

                yield return new EditorConfigSection(currentLines);
                currentLines.Clear();
                if (nextHeader != null)
                {
                    currentLines.Add(nextHeader);
                }
            }
        }


        static IEnumerable<EditorConfigLine> ReadLines(IEnumerable<string> linesOfText)
        {
            var lineNumber = 1;
            foreach (var lineOfText in linesOfText)
            {
                yield return
                    EditorConfigBlankLine.TryParse(lineNumber, lineOfText) ??
                    EditorConfigCommentLine.TryParse(lineNumber, lineOfText) ??
                    EditorConfigSectionHeaderLine.TryParse(lineNumber, lineOfText) ??
                    EditorConfigDeclarationLine.TryParse(lineNumber, lineOfText) ??
                    (EditorConfigLine)null ??
                    throw new TextFileParseException("Unrecognised line", lineNumber, lineOfText);

                lineNumber++;
            }

            yield return null;
        }

    }
}
