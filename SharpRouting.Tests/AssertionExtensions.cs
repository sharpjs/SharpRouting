using System.Web.Routing;
using FluentAssertions;

namespace SharpRouting
{
    internal static class AssertionExtensions
    {
        internal static Route ShouldBeRoute(this RouteBase subject, Route expected)
        {
            subject.Should().NotBeNull().And.BeAssignableTo<Route>();
            var route = subject as Route;
            route.ShouldBeEquivalentTo(expected, o => o.Excluding(r => r.RouteHandler));
            return route;
        }

        internal static Route ShouldBeRoute(
            this RouteBase subject,
            string url,
            string controller  = null,
            string action      = null,
            object defaults    = null,
            object constraints = null,
            object dataTokens  = null)
        {
            var expected = RoutingTests.Route(url, controller, action, defaults, constraints, dataTokens);
            return subject.ShouldBeRoute(expected);
        }
    }
}
