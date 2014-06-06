using System;
using FluentAssertions;
using NUnit.Framework;

namespace SharpRouting
{
    [TestFixture]
    public class TypeExtensionsTests
    {
        [Test]
        public void IsController_True()
        {
            typeof(XController).IsController().Should().BeTrue();
        }

        [Test]
        public void IsController_Null()
        {
            (null as Type).IsController().Should().BeFalse();
        }

        [Test]
        public void IsController_NotPublic()
        {
            typeof(InternalController).IsController().Should().BeFalse();
        }

        [Test]
        public void IsController_Abstract()
        {
            typeof(AbstractController).IsController().Should().BeFalse();
        }

        [Test]
        public void IsController_WronglyNamed()
        {
            typeof(ControllerWronglyNamed).IsController().Should().BeFalse();
        }

        [Test]
        public void IsController_NoNamespace()
        {
            typeof(NoNamespaceController).IsController().Should().BeFalse();
        }

        [Test]
        public void IsController_NotAnIController()
        {
            typeof(NotAnIController).IsController().Should().BeFalse();
        }

        [Test]
        public void RequireControllerType_Null()
        {
            (null as Type)
                .Invoking(t => t.RequireControllerType())
                .ShouldThrow<ArgumentNullException>();
        }
    }
}
