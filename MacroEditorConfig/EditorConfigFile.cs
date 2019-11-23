using System;
using System.Collections.Generic;
using System.Linq;
using MacroExceptions;
using MacroGuards;

namespace MacroEditorConfig
{
    public class EditorConfigFile
    {

        readonly List<string> lines;


        /// <summary>
        /// Initialise a new empty editorconfig file
        /// </summary>
        ///
        public EditorConfigFile()
            : this(Enumerable.Empty<string>())
        {
        }


        /// <summary>
        /// Initialise a new editorconfig file containing the specified lines of text
        /// </summary>
        ///
        /// <exception cref="TextFileParseException">
        /// The specified <paramref name="lines"/> of text were not in valid editorconfig format
        /// </exception>
        ///
        public EditorConfigFile(IEnumerable<string> lines)
        {
            Guard.NotNull(lines, nameof(lines));
            this.lines = lines.ToList();
            Lines = this.lines;
            Parse();
        }


        public IReadOnlyList<string> Lines { get; }
        public IReadOnlyList<EditorConfigSection> Sections { get; private set; }
        public EditorConfigSection Preamble => Sections.Single(s => s.IsPreamble);


        /// <summary>
        /// Edit the lines of text in the editorconfig file
        /// </summary>
        ///
        /// <param name="editAction">
        /// Action that modifies the lines of text in the file
        /// </param>
        ///
        /// <remarks>
        /// After <paramref name="editAction"/> is performed, the file is re-parsed and state updated
        /// </remarks>
        ///
        /// <exception cref="TextFileParseException">
        /// After <paramref name="editAction"/> was performed, the <paramref name="lines"/> of text were no longer in
        /// valid editorconfig format
        /// </exception>
        ///
        public void Edit(Action<IList<string>> editAction)
        {
            Guard.NotNull(editAction, nameof(editAction));
            editAction(lines);
            Parse();
        }


        void Parse()
        {
            Sections = EditorConfigReader.Read(Lines).ToList();
        }

    }
}
