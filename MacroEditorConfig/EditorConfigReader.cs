using System.Collections.Generic;
using MacroExceptions;
using MacroGuards;

namespace MacroEditorConfig
{
    public static class EditorConfigReader
    {

        public static IEnumerable<EditorConfigLine> Read(IEnumerable<string> lines)
        {
            Guard.NotNull(lines, nameof(lines));
            return ReadImpl(lines);
        }


        static IEnumerable<EditorConfigLine> ReadImpl(IEnumerable<string> lines)
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
        }

    }
}
