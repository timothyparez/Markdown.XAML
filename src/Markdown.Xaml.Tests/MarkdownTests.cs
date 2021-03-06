﻿using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml;

namespace Markdown.Xaml.Tests
{
    [TestFixture]
    [UseReporter(typeof(DiffReporter))]
    public class MarkdownTests
    {
        [Test]
        public void Transform_givenTest1_generatesExpectedResult()
        {
            var text = LoadText("Test1.md");
            var markdown = new Markdown();
            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        [Test]
        [RequiresSTA]
        public void Transform_givenLists_generatesExpectedResult()
        {
            var text = LoadText("Lists.md");
            var markdown = new Markdown();
            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        [Test]
        [RequiresSTA]
        public void Transform_givenHorizontalRules_generatesExpectedResult()
        {
            var text = LoadText("HorizontalRules.md");
            var markdown = new Markdown();
            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        [Test]
        [RequiresSTA]
        public void Transform_givenLinksInline_generatesExpectedResult()
        {
            var text = LoadText("Links_inline_style.md");
            var markdown = new Markdown();
            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        [Test]
        [RequiresSTA]
        public void Transform_givenTextStyles_generatesExpectedResult()
        {
            var text = LoadText("Text_style.md");
            var markdown = new Markdown();
            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        private string LoadText(string name)
        {
            using (Stream stream = Assembly.GetExecutingAssembly()
                               .GetManifestResourceStream("Markdown.Xaml.Tests." + name))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private string AsXaml(object instance)
        {
            using (var writer = new StringWriter())
            {
                var settings = new XmlWriterSettings { Indent = true };
                using (var xmlWriter = XmlWriter.Create(writer, settings))
                {
                    XamlWriter.Save(instance, xmlWriter);
                }

                writer.WriteLine();
                return writer.ToString();
            }
        }
    }
}
