using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MacroEditorConfig.Tests
{
    [TestClass]
    public class EditorConfigFileTests
    {

        static IEnumerable<string> lines =
            new[]
            {
                "; comment_preamble1",
                "",
                "# comment_preamble2",
                "key_preamble1 = value_preamble1",
                "",
                "[a]",
                "key_a1 = value_a1",
                "key_a2 = value_a2",
                "",
                "[b]",
                "key_b1 = value_b1",
            };


        [TestMethod]
        public void File_parses_correctly()
        {
            EditorConfigSection section;
            EditorConfigLine line;
            EditorConfigSectionHeaderLine headerLine;
            EditorConfigCommentLine commentLine;
            EditorConfigBlankLine blankLine;
            EditorConfigDeclarationLine declarationLine;

            var file = new EditorConfigFile(lines);
            Assert.AreEqual(3, file.Sections.Count);

            section = file.Sections[0];
            Assert.IsTrue(section.IsPreamble);
            Assert.AreEqual(5, section.Lines.Count);

            line = section.Lines[0];
            Assert.AreEqual(1, line.LineNumber);
            Assert.IsNotNull(commentLine = line as EditorConfigCommentLine);
            Assert.AreEqual("comment_preamble1", commentLine.Comment);

            line = section.Lines[1];
            Assert.AreEqual(2, line.LineNumber);
            Assert.IsNotNull(blankLine = line as EditorConfigBlankLine);

            line = section.Lines[2];
            Assert.AreEqual(3, line.LineNumber);
            Assert.IsNotNull(commentLine = line as EditorConfigCommentLine);
            Assert.AreEqual("comment_preamble2", commentLine.Comment);

            line = section.Lines[3];
            Assert.AreEqual(4, line.LineNumber);
            Assert.IsNotNull(declarationLine = line as EditorConfigDeclarationLine);
            Assert.AreEqual("key_preamble1", declarationLine.Key);
            Assert.AreEqual("value_preamble1", declarationLine.Value);

            line = section.Lines[4];
            Assert.AreEqual(5, line.LineNumber);
            Assert.IsNotNull(blankLine = line as EditorConfigBlankLine);

            section = file.Sections[1];
            Assert.IsFalse(section.IsPreamble);
            Assert.AreEqual(3, section.Lines.Count);

            Assert.IsNotNull(headerLine = section.HeaderLine);
            Assert.AreEqual(6, headerLine.LineNumber);
            Assert.AreEqual("a", headerLine.Name);

            line = section.Lines[0];
            Assert.AreEqual(7, line.LineNumber);
            Assert.IsNotNull(declarationLine = line as EditorConfigDeclarationLine);
            Assert.AreEqual("key_a1", declarationLine.Key);
            Assert.AreEqual("value_a1", declarationLine.Value);

            line = section.Lines[1];
            Assert.AreEqual(8, line.LineNumber);
            Assert.IsNotNull(declarationLine = line as EditorConfigDeclarationLine);
            Assert.AreEqual("key_a2", declarationLine.Key);
            Assert.AreEqual("value_a2", declarationLine.Value);

            line = section.Lines[2];
            Assert.AreEqual(9, line.LineNumber);
            Assert.IsNotNull(blankLine = line as EditorConfigBlankLine);

            section = file.Sections[2];
            Assert.IsFalse(section.IsPreamble);
            Assert.AreEqual(1, section.Lines.Count);

            Assert.IsNotNull(headerLine = section.HeaderLine);
            Assert.AreEqual(10, headerLine.LineNumber);
            Assert.AreEqual("b", headerLine.Name);

            line = section.Lines[0];
            Assert.AreEqual(11, line.LineNumber);
            Assert.IsNotNull(declarationLine = line as EditorConfigDeclarationLine);
            Assert.AreEqual("key_b1", declarationLine.Key);
            Assert.AreEqual("value_b1", declarationLine.Value);
        }

    }
}
