using MainLibrary;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        private AssemblyInfoCollector _asmCollector;

        public Tests()
        {
            _asmCollector = new AssemblyInfoCollector("D:\\Studying\\third_course\\ÑÏÏ\\ForLab3Test\\ForLab3Test\\bin\\Debug\\net6.0\\ForLab3Test");
        }

        [Test]
        public void CheckNamespacesTest()
        {
            var namespaces = _asmCollector.Namespaces;
            Assert.Multiple(() =>
            {
                Assert.That(namespaces.ContainsKey("ForLab3Test"));
                Assert.That(namespaces.ContainsKey("Example"));
                Assert.That(namespaces.ContainsKey("System"));
            });
        }


        [Test]
        public void CheckForLab3TestClassesTest()
        {
            var namespaces = _asmCollector.Namespaces;

            var forLab3Classes = namespaces["ForLab3Test"];
            var ExampleClasses = namespaces["Example"];
            var SystemClasses = namespaces["System"];
            Assert.Multiple(() =>
            {

                Assert.That(forLab3Classes[0].ToString().Equals("public class Class1"), $"wrong class {forLab3Classes[0].ToString()}");
                Assert.That(forLab3Classes[1].ToString().Equals("public abstract classClassForExtensionC"), $"wrong class {forLab3Classes[1].ToString()}");
                Assert.That(forLab3Classes[2].ToString().Equals("public abstract classClassForExtensionString"), $"wrong class {forLab3Classes[2].ToString()}");

            });
        }

        [Test]
        public void CheckExampleClassesTest()
        {
            var namespaces = _asmCollector.Namespaces;

            var ExampleClasses = namespaces["Example"];
            Assert.Multiple(() =>
            {

                Assert.That(ExampleClasses[0].ToString().Equals("public class A"), $"wrong class {ExampleClasses[0].ToString()}");
                Assert.That(ExampleClasses[1].ToString().Equals("public class B"), $"wrong class {ExampleClasses[1].ToString()}");
                Assert.That(ExampleClasses[2].ToString().Equals("public class C"), $"wrong class {ExampleClasses[2].ToString()}");

            });
        }

        [Test]
        public void CheckStringClassesTest()
        {
            var namespaces = _asmCollector.Namespaces;

            var StringClasses = namespaces["System"];
            Assert.Multiple(() =>
            {

                Assert.That(StringClasses[0].ToString().Equals("public class String"), $"wrong class {StringClasses[0].ToString()}");

            });
        }

        [Test]
        public void CheckExtensionMethodsOfStringClassTest()
        {
            var stringMethods = _asmCollector.Namespaces["System"][0];
            var methods = stringMethods.Methods.FindAll(m => m.Extension.Equals("dsf"));
            Assert.That(methods.Count != 1, $"Wrong amount of extension method {methods.Count}");
        }

        [Test]
        public void CheckExtensionMethodsOfCClassTest()
        {
            var cMethods = _asmCollector.Namespaces["Example"][2];
            var methods = cMethods.Methods.FindAll(m => m.Extension.Equals("dsf"));
            Assert.That(methods.Count != 2, $"Wrong amount of extension method {methods.Count}");
        }
    }
}