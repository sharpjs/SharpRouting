using System;
using System.Web.Routing;
using FluentAssertions;
using NUnit.Framework;

namespace FluentRouting.Constraints
{
    [TestFixture]
    public class GuidConstraintTests
    {
        [Test]
        public void Match_Missing()
        {
            Match(new { }).Should().BeFalse();
        }

        [Test]
        public void Match_Unrecognized()
        {
            Match(new { p = 0 }).Should().BeFalse();
        }

        [Test]
        public void Match_Unparseable()
        {
            Match(new { p = "not a guid" }).Should().BeFalse();
        }

        [Test]
        public void Match_Parseable()
        {
            Match(new { p = Guid.NewGuid().ToString() }).Should().BeTrue();
        }

        [Test]
        public void Match_Guid()
        {
            Match(new { p = Guid.NewGuid() }).Should().BeTrue();
        }

        [Test]
        public void ExtensionMethod()
        {
            var routes = new RouteCollection();
            
            routes.Map<RootController>(root =>
            {
                root.Parameter("p").Guid().IsAction("A");
            });

            routes["Root.A"].ShouldBeRoute("{p}", "Root", "A",
                constraints: new { p = GuidConstraint.Instance });
        }

        private static bool Match(object values)
        {
            return GuidConstraint.Instance
                .Match(null, null, "p", new RouteValueDictionary(values), RouteDirection.IncomingRequest);
        }
    }
}
