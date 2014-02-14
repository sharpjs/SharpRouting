using System;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using NUnit.Framework;

namespace FluentRouting
{
    // NOTE: Tests only as required for coverage.  Many paths are exercised elsewhere.

    [TestFixture]
    public class RouteScopeTests : RoutingTests
    {
        private TestableRouteScope RootScope;
        private TestableRouteScope ChildScope;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            RootScope  = new TestableRouteScope(Routes,    "Root",  "root" );
            ChildScope = new TestableRouteScope(RootScope, "Child", "child");
        }

        [Test]
        public void Constructor_Root_NullUrl()
        {
            true.Invoking(_ => new TestableRouteScope(Routes, "C", null))
                .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Constructor_Child_NullParent()
        {
            true.Invoking(_ => new TestableRouteScope(null as RouteScope, "C", "u"))
                .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Routes_Root()
        {
            RootScope.Routes.As<object>().Should().BeSameAs(Routes);
        }

        [Test]
        public void Routes_Child()
        {
            ChildScope.Routes.As<object>().Should().BeSameAs(Routes);
        }

        [Test]
        public void Parent_Root()
        {
            RootScope.Parent.Should().BeNull();
        }

        [Test]
        public void Parent_Child()
        {
            ChildScope.Parent.Should().BeSameAs(RootScope);
        }

        [Test]
        public void ControllerName()
        {
            RootScope.ControllerName.Should().Be("Root");
        }

        [Test]
        public void Url_Root()
        {
            RootScope.Url.Should().Be("root");
        }

        [Test]
        public void Url_Child()
        {
            ChildScope.Url.Should().Be("root/child");
        }

        [Test]
        public void SetOptional_NullKey()
        {
            RootScope
                .Invoking(s => s.SetOptional(null))
                .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void SetDefault_NullKey()
        {
            RootScope
                .Invoking(s => s.SetDefault(null, "value"))
                .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void SetPattern_NullKey()
        {
            RootScope
                .Invoking(s => s.SetPattern(null, "pattern"))
                .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void SetConstraint_NullKey()
        {
            RootScope
                .Invoking(s => s.SetConstraint(null, new FakeConstraint()))
                .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void SetDataToken_NullKey()
        {
            RootScope
                .Invoking(s => s.SetDataToken(null, "value"))
                .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void SetDataToken()
        {
            RootScope.SetDataToken("k", "v");
            RootScope
                .MapAction("A")
                .ShouldBeRoute("root", "Root", "A", dataTokens: new { k = "v" });
        }

        [Test]
        public void Configure_NullModifier()
        {
            RootScope
                .Invoking(s => s.Configure(null))
                .ShouldThrow<ArgumentNullException>();
        }
    }
}
