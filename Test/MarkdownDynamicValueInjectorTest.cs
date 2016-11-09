using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using MarkdownMvcLibrary;
using NUnit.Framework;

namespace Test {

    [TestFixture]
    public class MarkdownDynamicValueInjectorTest {

        private const string Delimeter = "||";
        private MarkdownDynamicValueInjector _classUnderTest;
        private Dictionary<string, string> _dynamicValues;
        private string _baseFilePath;

        [SetUp]
        public void FixtureSetup() {
            _classUnderTest = new MarkdownDynamicValueInjector();
            _dynamicValues = new Dictionary<string, string> {
                {"EmailAddressGeneral", "Hello@ThatConference.com"},
                {"FacebookUrl", "http://www.Facebook.com/ThatConference"},
                {"PublicSlackUrl", "http://thatslack.thatconference.com/"}
            };
            _baseFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\InputFiles";
        }

        [Test]
        public void InjectDynamicValues_When_Delimeter_Is_Null_Should_Return_Original_Markdown() {
            //Arrange
            var markdown = File.ReadAllText($"{_baseFilePath}\\test_markdown_static.md");
            //Act
            var result = _classUnderTest.InjectDynamicValues(markdown, _dynamicValues, null);
            //Assert
            result.Should().Be(markdown);
        }

        [Test]
        public void InjectDynamicValues_When_DynamicValues_Is_Empty_Should_Return_Original_Markdown() {
            //Arrange
            var markdown = File.ReadAllText($"{_baseFilePath}\\test_markdown_static.md");
            //Act
            var result = _classUnderTest.InjectDynamicValues(markdown, new Dictionary<string, string>(), Delimeter);
            //Assert
            result.Should().Be(markdown);
        }

        [Test]
        public void InjectDynamicValues_When_DynamicValues_Is_Null_Should_Return_Original_Markdown() {
            //Arrange
            var markdown = File.ReadAllText($"{_baseFilePath}\\test_markdown_static.md");
            //Act
            var result = _classUnderTest.InjectDynamicValues(markdown, null, Delimeter);
            //Assert
            result.Should().Be(markdown);
        }

        [Test]
        public void InjectDynamicValues_When_Static_Markdown_Should_Return_Original_Markdown() {
            //Arrange
            var markdown = File.ReadAllText($"{_baseFilePath}\\test_markdown_static.md");
            //Act
            var result = _classUnderTest.InjectDynamicValues(markdown, _dynamicValues, Delimeter);
            //Assert
            result.Should().Be(markdown);
        }

        [Test]
        public void InjectDynamicValues_When_Dynamic_Markdown_Should_Return_Markdown_With_Dynamic_Values_Replaced() {
            //Arrange
            var markdown = File.ReadAllText($"{_baseFilePath}\\test_markdown_dynamic.md");
            var expectedResult = File.ReadAllText($"{_baseFilePath}\\test_markdown_dynamic_values_replaced.md");
            //Act
            var result = _classUnderTest.InjectDynamicValues(markdown, _dynamicValues, Delimeter);
            //Assert
            result.Should().Be(expectedResult);
        }

    }

}