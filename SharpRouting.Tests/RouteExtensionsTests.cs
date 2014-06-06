using System;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using NUnit.Framework;

namespace SharpRouting
{
    [TestFixture]
    public class RouteExtensionsTests : RoutingTests
    {
        [Test]
        public void WithDefault_NullRoute()
        {
            (null as Route)
                .Invoking(r => r.WithDefault("key", "value"))
                .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WithDefault_NullKey()
        {
            Route("u")
                .Invoking(r => r.WithDefault(null, "value"))
                .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WithDefault_NullDefaults()
        {
            var route = new Route("u", null);
            route.Defaults.Should().BeNull();

            var actual = route.WithDefault("key", "value");

            actual.Should().BeSameAs(route);
            actual.ShouldBeEquivalentTo(new Route("u", null)
            {
                Defaults = new RouteValueDictionary { { "key", "value" } }
            });
        }

        [Test]
        public void WithDefault_Typical()
        {
            var route = Route("u");
            route.Defaults.Should().NotBeNull();

            var actual = route.WithDefault("key", "value");

            actual.Should().BeSameAs(route);
            actual.ShouldBeEquivalentTo(Route("u", defaults: new { key = "value" }));
        }

        [Test]
        public void WithConstraint_Pattern_NullRoute()
        {
            (null as Route)
                .Invoking(r => r.WithConstraint("key", "pattern"))
                .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WithConstraint_Pattern_NullKey()
        {
            Route("u")
                .Invoking(r => r.WithConstraint(null, "pattern"))
                .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WithConstraint_Pattern_NullPattern()
        {
            Route("u")
                .Invoking(r => r.WithConstraint("key", null as string))
                .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WithConstraint_Pattern_EmptyPattern()
        {
            Route("u")
                .Invoking(r => r.WithConstraint("key", ""))
                .ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Test]
        public void WithConstraint_Pattern_NullConstraints()
        {
            var route = new Route("u", null);
            route.Constraints.Should().BeNull();

            var actual = route.WithConstraint("key", "pattern");

            actual.Should().BeSameAs(route);
            actual.ShouldBeEquivalentTo(new Route("u", null)
            {
                Constraints = new RouteValueDictionary { { "key", "pattern" } }
            });
        }

        [Test]
        public void WithConstraint_Pattern_Typical()
        {
            var route = Route("u");
            route.Constraints.Should().NotBeNull();

            var actual = route.WithConstraint("key", "pattern");

            actual.Should().BeSameAs(route);
            actual.ShouldBeEquivalentTo(Route("u", constraints: new { key = "pattern" }));
        }

        [Test]
        public void WithConstraint_Custom_NullRoute()
        {
            (null as Route)
                .Invoking(r => r.WithConstraint("key", new FakeConstraint()))
                .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WithConstraint_Custom_NullKey()
        {
            Route("u")
                .Invoking(r => r.WithConstraint(null,  new FakeConstraint()))
                .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WithConstraint_Custom_NullPattern()
        {
            Route("u")
                .Invoking(r => r.WithConstraint("key", null as IRouteConstraint))
                .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WithConstraint_Custom_NullConstraints()
        {
            var route = new Route("u", null);
            route.Constraints.Should().BeNull();
            var constraint = new FakeConstraint();

            var actual = route.WithConstraint("key", constraint);

            actual.Should().BeSameAs(route);
            actual.ShouldBeEquivalentTo(new Route("u", null)
            {
                Constraints = new RouteValueDictionary { { "key", constraint } }
            });
        }

        [Test]
        public void WithConstraint_Custom_Typical()
        {
            var route = Route("u");
            route.Constraints.Should().NotBeNull();
            var constraint = new FakeConstraint();

            var actual = route.WithConstraint("key", constraint);

            actual.Should().BeSameAs(route);
            actual.ShouldBeEquivalentTo(Route("u", constraints: new { key = constraint }));
        }

        [Test]
        public void WithDataToken_NullRoute()
        {
            (null as Route)
                .Invoking(r => r.WithDataToken("key", "value"))
                .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WithDataToken_NullKey()
        {
            Route("u")
                .Invoking(r => r.WithDataToken(null, "value"))
                .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WithDataToken_NullDataTokens()
        {
            var route = new Route("u", null);
            route.DataTokens.Should().BeNull();

            var actual = route.WithDataToken("key", "value");

            actual.Should().BeSameAs(route);
            actual.ShouldBeEquivalentTo(new Route("u", null)
            {
                DataTokens = new RouteValueDictionary { { "key", "value" } }
            });
        }

        [Test]
        public void WithDataToken_Typical()
        {
            var route = Route("u");
            route.DataTokens.Should().NotBeNull();

            var actual = route.WithDataToken("key", "value");

            actual.Should().BeSameAs(route);
            actual.ShouldBeEquivalentTo(Route("u", dataTokens: new { key = "value" }));
        }

        [Test]
        public void ToAction()
        {
            var route = Route("u").ToAction("C", "A");

            route.ShouldBeRoute("u", "C", "A");
        }

        [Test]
        public void ToAction_NullController()
        {
            Route("u")
                .Invoking(r => r.ToAction(null, "A"))
                .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void ToAction_EmptyController()
        {
            Route("u")
                .Invoking(r => r.ToAction("", "A"))
                .ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Test]
        public void ToAction_NullAction()
        {
            Route("u")
                .Invoking(r => r.ToAction("C", null))
                .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void ToAction_EmptyAction()
        {
            Route("u")
                .Invoking(r => r.ToAction("C", ""))
                .ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Test]
        public void WithOptional()
        {
            var route = Route("u").WithOptional("key");

            route.ShouldBeRoute("u",
                defaults: new { key = UrlParameter.Optional });
        }

        [Test]
        public void ForGet()
        {
            var route = Route("u", "C", "A").ForGet();

            route.ShouldBeRoute("u", "C", "A",
                constraints: new { httpMethod = new HttpMethodConstraint("GET") });
        }

        [Test]
        public void ForPost()
        {
            var route = Route("u", "C", "A").ForPost();

            route.ShouldBeRoute("u", "C", "A",
                constraints: new { httpMethod = new HttpMethodConstraint("POST") });
        }

        [Test]
        public void ForPut()
        {
            var route = Route("u", "C", "A").ForPut();

            route.ShouldBeRoute("u", "C", "A",
                constraints: new { httpMethod = new HttpMethodConstraint("PUT") });
        }

        [Test]
        public void ForPutOrPost()
        {
            var route = Route("u", "C", "A").ForPutOrPost();

            route.ShouldBeRoute("u", "C", "A",
                constraints: new { httpMethod = new HttpMethodConstraint("PUT", "POST") });
        }

        [Test]
        public void ForDelete()
        {
            var route = Route("u", "C", "A").ForDelete();

            route.ShouldBeRoute("u", "C", "A",
                constraints: new { httpMethod = new HttpMethodConstraint("DELETE") });
        }

        [Test]
        public void ForDeleteOrPost()
        {
            var route = Route("u", "C", "A").ForDeleteOrPost();

            route.ShouldBeRoute("u", "C", "A",
                constraints: new { httpMethod = new HttpMethodConstraint("DELETE", "POST") });
        }

        [Test]
        public void InArea()
        {
            var route = Route("u").InArea("A");

            route.ShouldBeRoute("u",
                dataTokens: new { area = "A" });
        }

        [Test]
        public void InArea_NullArea()
        {
            Route("u")
                .Invoking(r => r.InArea(null))
                .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void InArea_EmptyArea()
        {
            Route("u")
                .Invoking(r => r.InArea(""))
                .ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Test]
        public void InNamespaces()
        {
            var route = Route("u").InNamespaces("NA", "NB");

            route.ShouldBeRoute("u",
                dataTokens: new { Namespaces = new[] { "NA", "NB" }, UseNamespaceFallback = false });
        }

        [Test]
        public void InNamespaces_NullNamespaces()
        {
            Route("u")
                .Invoking(r => r.InNamespaces(null))
                .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void InNamespaces_EmptyNamespaces()
        {
            Route("u")
                .Invoking(r => r.InNamespaces())
                .ShouldThrow<ArgumentOutOfRangeException>();
        }
    }
}
