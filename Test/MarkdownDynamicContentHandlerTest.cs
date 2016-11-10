using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using MarkdownMvcLibrary;
using NUnit.Framework;

namespace Test {

    [TestFixture]
    public class MarkdownDynamicContentHandlerTest {

        private MarkdownDynamicContentHandler _classUnderTest;
        private Dictionary<string, string> _dynamicValues;
        private string _baseFilePath;

        [SetUp]
        public void FixtureSetup() {
            _classUnderTest = new MarkdownDynamicContentHandler();
            _dynamicValues = new Dictionary<string, string> {
                {"EmailAddressGeneral", "Hello@ThatConference.com"},
                {"TwitterUrl", "http://www.Twitter.com/ThatConference"},
                {"FacebookUrl", "http://www.Facebook.com/ThatConference"},
                {"PublicSlackUrl", "http://thatslack.thatconference.com/"},
                {"ShouldSeeThis", "true" },
                {"ShouldNotSeeThis", "false" }
            };
            _baseFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\InputFiles";
        }

        [Test]
        public void ProcessMarkdown_When_DynamicValues_Is_Empty_Should_Return_Original_Markdown() {
            //Arrange
            var markdown = File.ReadAllText($"{_baseFilePath}\\test_markdown_static.md");
            //Act
            var result = _classUnderTest.ProcessMarkdown(markdown, new Dictionary<string, string>());
            //Assert
            result.Should().Be(markdown);
        }

        [Test]
        public void ProcessMarkdown_When_DynamicValues_Is_Null_Should_Return_Original_Markdown() {
            //Arrange
            var markdown = File.ReadAllText($"{_baseFilePath}\\test_markdown_static.md");
            //Act
            var result = _classUnderTest.ProcessMarkdown(markdown, null);
            //Assert
            result.Should().Be(markdown);
        }

        [Test]
        public void ProcessMarkdown_When_Static_Markdown_Should_Return_Original_Markdown() {
            //Arrange
            var markdown = File.ReadAllText($"{_baseFilePath}\\test_markdown_static.md");
            //Act
            var result = _classUnderTest.ProcessMarkdown(markdown, _dynamicValues);
            //Assert
            result.Should().Be(markdown);
        }

        [Test]
        public void ProcessMarkdown_When_Dynamic_Markdown_Should_Return_Markdown_With_Dynamic_Values_Replaced() {
            //Arrange
            var markdown = File.ReadAllText($"{_baseFilePath}\\test_markdown_dynamic.md");
            var expectedResult = File.ReadAllText($"{_baseFilePath}\\test_markdown_dynamic_processed.md");
            //Act
            var result = _classUnderTest.ProcessMarkdown(markdown, _dynamicValues);
            //Assert
            result.Should().Be(expectedResult);
        }

        [Test]
        public void ProcessMarkdown_When_Conditional_Markdown_Should_Return_With_Conditional_Content_Handled_Appropriately() {
            //Arrange
            var markdown = File.ReadAllText($"{_baseFilePath}\\test_markdown_conditional.md");
            var expectedResult = File.ReadAllText($"{_baseFilePath}\\test_markdown_conditional_processed.md");
            //Act
            var result = _classUnderTest.ProcessMarkdown(markdown, _dynamicValues);
            //Assert
            result.Should().Be(expectedResult);
        }

    }

}