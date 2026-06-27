using Xunit;
using System;
using System.Linq;
using task05;

namespace task05tests
{
    public class TestClass
    {
        public int PublicField;
        private string _privateField;
        public int Property { get; set; }

        public void Method() { }
        public int MethodWithParams(int a, string b) => a;
        private void PrivateMethod() { }
    }

    [Serializable]
    public class AttributedClass { }

    public class ClassAnalyzerTests
    {
        [Fact]
        public void GetPublicMethods_ReturnsCorrectMethods()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var methods = analyzer.GetPublicMethods().ToList();

            Assert.Contains("Method", methods);
            Assert.Contains("MethodWithParams", methods);
            Assert.DoesNotContain("PrivateMethod", methods);
        }

        [Fact]
        public void GetAllFields_IncludesPrivateFields()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var fields = analyzer.GetAllFields().ToList();

            Assert.Contains("PublicField", fields);
            Assert.Contains("_privateField", fields);
        }

        [Fact]
        public void GetProperties_ReturnsCorrectProperties()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var properties = analyzer.GetProperties().ToList();

            Assert.Contains("Property", properties);
        }

        [Fact]
        public void GetMethodParams_ReturnsCorrectParameters()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var paramsInfo = analyzer.GetMethodParams("MethodWithParams").ToList();

            Assert.Equal(3, paramsInfo.Count);
            Assert.Contains("Return: Int32", paramsInfo);
            Assert.Contains("a: Int32", paramsInfo);
            Assert.Contains("b: String", paramsInfo);
        }

        [Fact]
        public void GetMethodParams_InvalidMethod_ReturnsEmpty()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var paramsInfo = analyzer.GetMethodParams("NonExistentMethod");
            Assert.Empty(paramsInfo);
        }

        [Fact]
        public void HasAttribute_WithAttribute_ReturnsTrue()
        {
            var analyzer = new ClassAnalyzer(typeof(AttributedClass));
            var result = analyzer.HasAttribute<SerializableAttribute>();
            Assert.True(result);
        }

        [Fact]
        public void HasAttribute_WithoutAttribute_ReturnsFalse()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var result = analyzer.HasAttribute<SerializableAttribute>();
            Assert.False(result);
        }
    }
}