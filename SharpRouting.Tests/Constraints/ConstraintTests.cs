using System;
using System.Web.Routing;
using NUnit.Framework;

namespace SharpRouting.Constraints
{
    public abstract class ConstraintTests
    {
        protected RouteDirection Direction { get; set; }

        protected abstract IRouteConstraint CreateConstraint();

        [SetUp]
        public void SetUp()
        {
            Direction = RouteDirection.IncomingRequest;
            RouteExtensions.IgnoreConstraintsWhenGeneratingUrls = false;
        }

        [TearDown]
        public void TearDown()
        {
            RouteExtensions.IgnoreConstraintsWhenGeneratingUrls = false;
        }

        protected bool Match(object values)
        {
            return CreateConstraint()
                .Match(null, null, "p", new RouteValueDictionary(values), Direction);
        }
    }
}
