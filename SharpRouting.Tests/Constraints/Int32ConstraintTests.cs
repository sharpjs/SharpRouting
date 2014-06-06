using System;
using System.Web.Routing;
using FluentAssertions;
using NUnit.Framework;

namespace SharpRouting.Constraints
{
    [TestFixture]
    public class Int32ConstraintTests
    {
        [Test]
        public void Match_Missing()
        {
            Match(new { }).Should().BeFalse();
        }

        [Test]
        public void Match_Unrecognized()
        {
            Match(new { p = true }).Should().BeFalse();
        }

        [Test]
        public void Match_Unparseable()
        {
            Match(new { p = "not an Int32" }).Should().BeFalse();
        }

        [Test]
        public void Match_Parseable()
        {
            Match(new { p = 42.ToString() }).Should().BeTrue();
        }

        [Test]
        public void Match_Int32()
        {
            Match(new { p = 42 }).Should().BeTrue();
        }

        [Test]
        public void ExtensionMethod()
        {
            var routes = new RouteCollection();
            
            routes.Map<RootController>(root =>
            {
                root.Parameter("p").Int32().IsAction("A");
            });

            routes["Root.A"].ShouldBeRoute("{p}", "Root", "A",
                constraints: new { p = Int32Constraint.Instance });
        }

        private static bool Match(object values)
        {
            return Int32Constraint.Instance
                .Match(null, null, "p", new RouteValueDictionary(values), RouteDirection.IncomingRequest);
        }
    }
}
