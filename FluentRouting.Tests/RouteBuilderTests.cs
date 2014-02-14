using System;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using NUnit.Framework;

namespace FluentRouting
{
    [TestFixture]
    public class RouteBuilderTests : RoutingTests
    {
        #region Nop Tests

        [Test]
        public void Root_Nop()
        {
            Routes.Map<RootController>(root =>
            {
                // Nothing
            });

            Routes.Should().BeEmpty();
        }

        [Test]
        public void Root_Url_Controller_Nop()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("x").IsController<XController>(x =>
                {
                    // Nothing
                });
            });

            Routes.Should().BeEmpty();
        }

        #endregion

        #region Root_Empty_* Tests

        [Test]
        public void Root_Empty_Action()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("").IsAction("A");
            });

            Routes.Should().HaveCount(1);
            Routes["Root.A"].ShouldBeRoute("", "Root", "A");
        }

        [Test]
        public void Root_Empty_Controller_Url_Action()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("").IsController<XController>(x =>
                {
                    x.Url("a").IsAction("A");
                });
            });

            Routes.Should().HaveCount(1);
            Routes["X.A"].ShouldBeRoute("a", "X", "A");
        }

        [Test]
        public void Root_Empty_Controller_Parameter_Action()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("").IsController<XController>(x =>
                {
                    x.Parameter("a").IsAction("A");
                });
            });

            Routes.Should().HaveCount(1);
            Routes["X.A"].ShouldBeRoute("{a}", "X", "A");
        }

        #endregion

        #region Root_Url_* Tests

        [Test]
        public void Root_Url_Action()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("a").IsAction("A");
            });

            Routes.Should().HaveCount(1);
            Routes["Root.A"].ShouldBeRoute("a", "Root", "A");
        }

        [Test]
        public void Root_Url_Controller_Empty_Action()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("x").IsController<XController>(x =>
                {
                    x.Url("").IsAction("A");
                });
            });

            Routes.Should().HaveCount(1);
            Routes["X.A"].ShouldBeRoute("x", "X", "A");
        }

        [Test]
        public void Root_Url_Controller_Url_Action()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("x").IsController<XController>(x =>
                {
                    x.Url("a").IsAction("A");
                });
            });

            Routes.Should().HaveCount(1);
            Routes["X.A"].ShouldBeRoute("x/a", "X", "A");
        }

        [Test]
        public void Root_Url_Controller_Parameter_Action()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("x").IsController<XController>(x =>
                {
                    x.Parameter("a").IsAction("A");
                });
            });

            Routes.Should().HaveCount(1);
            Routes["X.A"].ShouldBeRoute("x/{a}", "X", "A");
        }

        #endregion

        #region Root_Urls_* Tests

        [Test]
        public void Root_Urls_Action()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("a").Url("aa").IsAction("A");
            });

            Routes.Should().HaveCount(1);
            Routes["Root.A"].ShouldBeRoute("a/aa", "Root", "A");
        }

        [Test]
        public void Root_Urls_Controller_Empty_Action()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("x").Url("xx").IsController<XController>(x =>
                {
                    x.Url("").IsAction("A");
                });
            });

            Routes.Should().HaveCount(1);
            Routes["X.A"].ShouldBeRoute("x/xx", "X", "A");
        }

        [Test]
        public void Root_Urls_Controller_Url_Action()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("x").Url("xx").IsController<XController>(x =>
                {
                    x.Url("a").IsAction("A");
                });
            });

            Routes.Should().HaveCount(1);
            Routes["X.A"].ShouldBeRoute("x/xx/a", "X", "A");
        }

        [Test]
        public void Root_Urls_Controller_Parameter_Action()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("x").Url("xx").IsController<XController>(x =>
                {
                    x.Parameter("a").IsAction("A");
                });
            });

            Routes.Should().HaveCount(1);
            Routes["X.A"].ShouldBeRoute("x/xx/{a}", "X", "A");
        }

        #endregion

        #region Root_Parameter_* Tests

        [Test]
        public void Root_Parameter_Action()
        {
            Routes.Map<RootController>(root =>
            {
                root.Parameter("a").IsAction("A");
            });

            Routes.Should().HaveCount(1);
            Routes["Root.A"].ShouldBeRoute("{a}", "Root", "A");
        }

        [Test]
        public void Root_Parameter_Controller_Empty_Action()
        {
            Routes.Map<RootController>(root =>
            {
                root.Parameter("x").IsController<XController>(x =>
                {
                    x.Url("").IsAction("A");
                });
            });

            Routes.Should().HaveCount(1);
            Routes["X.A"].ShouldBeRoute("{x}", "X", "A");
        }

        [Test]
        public void Root_Parameter_Controller_Url_Action()
        {
            Routes.Map<RootController>(root =>
            {
                root.Parameter("x").IsController<XController>(x =>
                {
                    x.Url("a").IsAction("A");
                });
            });

            Routes.Should().HaveCount(1);
            Routes["X.A"].ShouldBeRoute("{x}/a", "X", "A");
        }

        [Test]
        public void Root_Parameter_Controller_Parameter_Action()
        {
            Routes.Map<RootController>(root =>
            {
                root.Parameter("x").IsController<XController>(x =>
                {
                    x.Parameter("a").IsAction("A");
                });
            });

            Routes.Should().HaveCount(1);
            Routes["X.A"].ShouldBeRoute("{x}/{a}", "X", "A");
        }

        #endregion

        #region Root_Parameters_* Tests

        [Test]
        public void Root_Parameters_Action()
        {
            Routes.Map<RootController>(root =>
            {
                root.Parameter("a").Parameter("aa").IsAction("A");
            });

            Routes.Should().HaveCount(1);
            Routes["Root.A"].ShouldBeRoute("{a}/{aa}", "Root", "A");
        }

        [Test]
        public void Root_Parameters_Controller_Empty_Action()
        {
            Routes.Map<RootController>(root =>
            {
                root.Parameter("x").Parameter("xx").IsController<XController>(x =>
                {
                    x.Url("").IsAction("A");
                });
            });

            Routes.Should().HaveCount(1);
            Routes["X.A"].ShouldBeRoute("{x}/{xx}", "X", "A");
        }

        [Test]
        public void Root_Parameters_Controller_Url_Action()
        {
            Routes.Map<RootController>(root =>
            {
                root.Parameter("x").Parameter("xx").IsController<XController>(x =>
                {
                    x.Url("a").IsAction("A");
                });
            });

            Routes.Should().HaveCount(1);
            Routes["X.A"].ShouldBeRoute("{x}/{xx}/a", "X", "A");
        }

        [Test]
        public void Root_Parameters_Controller_Parameter_Action()
        {
            Routes.Map<RootController>(root =>
            {
                root.Parameter("x").Parameter("xx").IsController<XController>(x =>
                {
                    x.Parameter("a").IsAction("A");
                });
            });

            Routes.Should().HaveCount(1);
            Routes["X.A"].ShouldBeRoute("{x}/{xx}/{a}", "X", "A");
        }

        #endregion

        #region Parameter Options Tests

        [Test]
        public void Parameters_Optional()
        {
            Routes.Map<RootController>(root =>
            {
                root.Parameter("p0").Optional()
                    .Parameter("p1").Optional().IsController<XController>(x =>
                {
                    x.Parameter("p2").Optional().IsAction("A");
                });
            });

            Routes.Should().HaveCount(1);
            Routes["X.A"].ShouldBeRoute("{p0}/{p1}/{p2}", "X", "A", defaults: new
            {
                p0 = UrlParameter.Optional,
                p1 = UrlParameter.Optional,
                p2 = UrlParameter.Optional
            });
        }

        [Test]
        public void Parameters_Defaults()
        {
            Routes.Map<RootController>(root =>
            {
                root.Parameter("p0").WithDefault("d0")
                    .Parameter("p1").WithDefault("d1").IsController<XController>(x =>
                {
                    x.Parameter("p2").WithDefault("d2").IsAction("A");
                });
            });

            Routes.Should().HaveCount(1);
            Routes["X.A"].ShouldBeRoute("{p0}/{p1}/{p2}", "X", "A", defaults: new
            {
                p0 = "d0",
                p1 = "d1",
                p2 = "d2"
            });
        }

        [Test]
        public void Parameters_Constraints()
        {
            var c0 = new FakeConstraint();
            var c1 = new FakeConstraint();
            var c2 = new FakeConstraint();

            Routes.Map<RootController>(root =>
            {
                root.Parameter("p0").WithConstraint(c0)
                    .Parameter("p1").WithConstraint(c1).IsController<XController>(x =>
                {
                    x.Parameter("p2").WithConstraint(c2).IsAction("A");
                });
            });

            Routes.Should().HaveCount(1);
            Routes["X.A"].ShouldBeRoute("{p0}/{p1}/{p2}", "X", "A", constraints: new
            {
                p0 = c0,
                p1 = c1,
                p2 = c2
            });
        }

        [Test]
        public void Parameters_Patterns()
        {
            Routes.Map<RootController>(root =>
            {
                root.Parameter("p0").WithPattern("c0")
                    .Parameter("p1").WithPattern("c1").IsController<XController>(x =>
                {
                    x.Parameter("p2").WithPattern("c2").IsAction("A");
                });
            });

            Routes.Should().HaveCount(1);
            Routes["X.A"].ShouldBeRoute("{p0}/{p1}/{p2}", "X", "A", constraints: new
            {
                p0 = "c0",
                p1 = "c1",
                p2 = "c2"
            });
        }

        #endregion

        #region HTTP Verb Tests

        [Test]
        public void Root_Url_Action_Get()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("a").Get().IsAction("A");
            });

            Routes.Should().HaveCount(1);
            Routes["Root.A"].ShouldBeRoute("a", "Root", "A", constraints: new
            {
                httpMethod = new HttpMethodConstraint("GET")
            });
        }

        [Test]
        public void Root_Url_Action_Post()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("a").Post().IsAction("A");
            });

            Routes.Should().HaveCount(1);
            Routes["Root.A"].ShouldBeRoute("a", "Root", "A", constraints: new
            {
                httpMethod = new HttpMethodConstraint("POST")
            });
        }

        [Test]
        public void Root_Url_Action_Put()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("a").Put().IsAction("A");
            });

            Routes.Should().HaveCount(1);
            Routes["Root.A"].ShouldBeRoute("a", "Root", "A", constraints: new
            {
                httpMethod = new HttpMethodConstraint("PUT")
            });
        }

        [Test]
        public void Root_Url_Action_Delete()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("a").Delete().IsAction("A");
            });

            Routes.Should().HaveCount(1);
            Routes["Root.A"].ShouldBeRoute("a", "Root", "A", constraints: new
            {
                httpMethod = new HttpMethodConstraint("DELETE")
            });
        }

        [Test]
        public void Root_Url_Action_Head()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("a").Head().IsAction("A");
            });

            Routes.Should().HaveCount(1);
            Routes["Root.A"].ShouldBeRoute("a", "Root", "A", constraints: new
            {
                httpMethod = new HttpMethodConstraint("HEAD")
            });
        }

        [Test]
        public void Root_Url_Action_Patch()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("a").Patch().IsAction("A");
            });

            Routes.Should().HaveCount(1);
            Routes["Root.A"].ShouldBeRoute("a", "Root", "A", constraints: new
            {
                httpMethod = new HttpMethodConstraint("PATCH")
            });
        }

        [Test]
        public void Root_Url_Action_Options()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("a").Options().IsAction("A");
            });

            Routes.Should().HaveCount(1);
            Routes["Root.A"].ShouldBeRoute("a", "Root", "A", constraints: new
            {
                httpMethod = new HttpMethodConstraint("OPTIONS")
            });
        }

        [Test]
        public void Root_Url_Action_MultipleVerbs()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("a").Get().Post().Put().Delete().Head().Patch().Options().IsAction("A");
            });

            Routes.Should().HaveCount(1);
            Routes["Root.A"].ShouldBeRoute("a", "Root", "A", constraints: new
            {
                httpMethod = new HttpMethodConstraint("GET", "POST", "PUT", "DELETE", "HEAD", "PATCH", "OPTIONS")
            });
        }

        // TODO: More verbs, please?

        #endregion

        #region Area Tests

        [Test]
        public void Root_Url_Area_Url_Action()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("x").IsArea<XController>(x =>
                {
                    x.Url("a").IsAction("A");
                });
            });

            Routes.Should().HaveCount(1);
            Routes["X.A"].ShouldBeRoute("x/a", "X", "A", dataTokens: new
            {
                area = "X",
                Namespaces = new[] { "FluentRouting.*" },
                UseNamespaceFallback = false
            });
        }

        [Test]
        public void Root_Url_Area_Url_Controller_Url_Action()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("x").IsArea<XController>(x =>
                {
                    x.Url("y").IsController<YController>(y =>
                    {
                        y.Url("a").IsAction("A");
                    });
                });
            });

            Routes.Should().HaveCount(1);
            Routes["Y.A"].ShouldBeRoute("x/y/a", "Y", "A", dataTokens: new
            {
                area = "X",
                Namespaces = new[] { "FluentRouting.*" },
                UseNamespaceFallback = false
            });
        }

        [Test]
        public void Root_Url_Area_Url_Area_Url_Action()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("x").IsArea<XController>(x =>
                {
                    x.Url("y").IsArea<YController>(y =>
                    {
                        y.Url("a").IsAction("A");
                    });
                });
            });

            Routes.Should().HaveCount(1);
            Routes["Y.A"].ShouldBeRoute("x/y/a", "Y", "A", dataTokens: new
            {
                area = "Y",
                Namespaces = new[] { "FluentRouting.*" },
                UseNamespaceFallback = false
            });
        }

        #endregion

        #region Method Equivalence Tests

        [Test]
        public void Map_ByName()
        {
            var expected = new RouteCollection();
            expected.Map<RootController>(r =>
            {
                r.Url("a").IsAction("A");
            });

            Routes.MapController("Root", r =>
            {
                r.Url("a").IsAction("A");
            });

            Routes.Should().HaveCount(1);
            Routes["Root.A"].ShouldBeRoute((Route) expected["Root.A"]);
        }

        [Test]
        public void Map_ByType_Implicit()
        {
            var expected = new RouteCollection();
            expected.MapController("SelfRegistering", r =>
            {
                r.Url("a").IsAction("A");
            });

            Routes.Map<SelfRegisteringController>();

            Routes.Should().HaveCount(1);
            Routes["SelfRegistering.A"].ShouldBeRoute((Route) expected["SelfRegistering.A"]);
        }

        [Test]
        public void Map_Same()
        {
            var expected = new RouteCollection();
            expected.Map<RootController>(r =>
            {
                r.Url("x").IsController<XController>(x =>
                {
                    x.Url("xx").IsController<XController>(xx =>
                    {
                        xx.Url("a").IsAction("A");
                    });
                });
            });

            Routes.Map<RootController>(r =>
            {
                r.Url("x").IsController<XController>(x =>
                {
                    x.Url("xx").Is(xx =>
                    {
                        xx.Url("a").IsAction("A");
                    });
                });
            });

            Routes.Should().HaveCount(1);
            Routes["X.A"].ShouldBeRoute((Route) expected["X.A"]);
        }

        [Test]
        public void Map_Url_Controller_ByName()
        {
            var expected = new RouteCollection();
            expected.Map<RootController>(r =>
            {
                r.Url("x").IsController<XController>(x =>
                {
                    x.Url("a").IsAction("A");
                });
            });

            Routes.Map<RootController>(r =>
            {
                r.Url("x").IsController("X", x =>
                {
                    x.Url("a").IsAction("A");
                });
            });

            Routes.Should().HaveCount(1);
            Routes["X.A"].ShouldBeRoute((Route) expected["X.A"]);
        }

        [Test]
        public void Map_Url_Controller_ByType_Implicit()
        {
            var expected = new RouteCollection();
            expected.Map<RootController>(r =>
            {
                r.Url("x").IsController("SelfRegistering", x =>
                {
                    x.Url("a").IsAction("A");
                });
            });

            Routes.Map<RootController>(r =>
            {
                r.Url("x").IsController<SelfRegisteringController>();
            });

            Routes.Should().HaveCount(1);
            Routes["SelfRegistering.A"].ShouldBeRoute((Route) expected["SelfRegistering.A"]);
        }

        [Test]
        public void Map_Url_Area_ByName()
        {
            var expected = new RouteCollection();
            expected.Map<RootController>(r =>
            {
                r.Url("x").IsArea<XController>(x =>
                {
                    x.Url("a").IsAction("A");
                });
            });

            Routes.Map<RootController>(r =>
            {
                r.Url("x").IsArea("X", "FluentRouting", x =>
                {
                    x.Url("a").IsAction("A");
                });
            });

            Routes.Should().HaveCount(1);
            Routes["X.A"].ShouldBeRoute((Route) expected["X.A"]);
        }

        [Test]
        public void Map_Url_Area_ByType_Implicit()
        {
            var expected = new RouteCollection();
            expected.Map<RootController>(r =>
            {
                r.Url("x").IsArea("SelfRegistering", "FluentRouting", x =>
                {
                    x.Url("a").IsAction("A");
                });
            });

            Routes.Map<RootController>(r =>
            {
                r.Url("x").IsArea<SelfRegisteringController>();
            });

            Routes.Should().HaveCount(1);
            Routes["SelfRegistering.A"].ShouldBeRoute((Route) expected["SelfRegistering.A"]);
        }

        #endregion

        #region Explicit Route Name Tests

        [Test]
        public void Root_Url_Action_ExplicitRouteName()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("a").IsAction("A", route: "Q");
            });

            Routes.Should().HaveCount(1);
            Routes["Root.Q"].ShouldBeRoute("a", "Root", "A");
        }

        [Test]
        public void Root_Url_Controller_Url_Action_ExplicitRouteName()
        {
            Routes.Map<RootController>(root =>
            {
                root.Url("x").IsController<XController>(x =>
                {
                    x.Url("a").IsAction("A", route: "Q");
                });
            });

            Routes.Should().HaveCount(1);
            Routes["X.Q"].ShouldBeRoute("x/a", "X", "A");
        }

        #endregion

        #region Exception Tests

        [Test]
        public void Map_ByName_NullRoutes()
        {
            (null as RouteCollection).Invoking(routes =>
            {
                routes.MapController("Root", root => { });
            })
            .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Map_ByName_NullControllerName()
        {
            Routes.Invoking(routes =>
            {
                routes.MapController(null, root => { });
            })
            .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Map_ByName_EmptyControllerName()
        {
            Routes.Invoking(routes =>
            {
                routes.MapController("", root => { });
            })
            .ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Map_ByName_NullSpecification()
        {
            Routes.Invoking(routes =>
            {
                routes.MapController("Root", null);
            })
            .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Map_ByType_Explicit_NullRoutes()
        {
            (null as RouteCollection).Invoking(routes =>
            {
                routes.Map<RootController>(root => { });
            })
            .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Map_ByType_Explicit_InvalidControllerType()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<ControllerWronglyNamed>(root => { });
            })
            .ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Map_ByType_Explicit_NullSpecification()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(null);
            })
            .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Map_ByType_Implicit_NullRoutes()
        {
            (null as RouteCollection).Invoking(routes =>
            {
                routes.Map<SelfRegisteringController>();    
            })
            .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Map_ByType_Implicit_InvalidType()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<ControllerWronglyNamed>();
            })
            .ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Map_ByType_Implicit_NoSpecification()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>();
            })
            .ShouldThrow<MissingMethodException>();
        }

        [Test]
        public void Map_Root_Url_NullUrl()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Url(null);
                });
            })
            .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Map_Root_Url_LeadingSlash()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Url("/x");
                });
            })
            .ShouldThrow<FormatException>();
        }

        [Test]
        public void Map_Root_Url_TrailingSlash()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Url("x/");
                });
            })
            .ShouldThrow<FormatException>();
        }

        [Test]
        public void Map_Root_Url_Action_NullName()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Url("x").IsAction(null);
                });
            })
            .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Map_Root_Url_Action_EmptyName()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Url("x").IsAction("");
                });
            })
            .ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Map_Root_Url_Same_NullSpecification()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Url("x").Is(null);
                });
            })
            .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Map_Root_Url_Controller_ByName_NullName()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Url("x").IsController(null, x => { });
                });
            })
            .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Map_Root_Url_Controller_ByName_EmptyName()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Url("x").IsController("", x => { });
                });
            })
            .ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Map_Root_Url_Controller_ByName_NullSpecification()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Url("x").IsController("X", null);
                });
            })
            .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Map_Root_Url_Controller_ByType_Explicit_InvalidType()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Url("x").IsController<ControllerWronglyNamed>(x => { });
                });
            })
            .ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Map_Root_Url_Controller_ByType_Explicit_NullSpecification()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Url("x").IsController<XController>(null);
                });
            })
            .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Map_Root_Url_Controller_ByType_Implicit_InvalidType()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Url("x").IsController<ControllerWronglyNamed>();
                });
            })
            .ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Map_Root_Url_Controller_ByType_Implicit_NoSpecification()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Url("x").IsController<XController>();
                });
            })
            .ShouldThrow<MissingMethodException>();
        }

        [Test]
        public void Map_Root_Url_Area_ByName_NullName()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Url("a").IsArea(null, "ns", a => { });
                });
            })
            .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Map_Root_Url_Area_ByName_EmptyName()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Url("a").IsArea("", "ns", a => { });
                });
            })
            .ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Map_Root_Url_Area_ByName_NullNamespace()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Url("a").IsArea("A", null, a => { });
                });
            })
            .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Map_Root_Url_Area_ByName_EmptyNamespace()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Url("a").IsArea("A", "", a => { });
                });
            })
            .ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Map_Root_Url_Area_ByName_NullSpecification()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Url("a").IsArea("A", "ns", null);
                });
            })
            .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Map_Root_Url_Area_ByType_Explicit_InvalidType()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Url("a").IsArea<ControllerWronglyNamed>(a => { });
                });
            })
            .ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Map_Root_Url_Area_ByType_Explicit_NullSpecification()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Url("a").IsArea<XController>(null);
                });
            })
            .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Map_Root_Url_Area_ByType_Implicit_InvalidType()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Url("a").IsArea<ControllerWronglyNamed>();
                });
            })
            .ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Map_Root_Url_Area_ByType_Implicit_NoSpecification()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Url("a").IsArea<XController>();
                });
            })
            .ShouldThrow<MissingMethodException>();
        }

        [Test]
        public void Map_Root_Parameter_NullName()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Parameter(null);
                });
            })
            .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Map_Root_Parameter_EmptyName()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Parameter("");
                });
            })
            .ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Root_Parameter_Default_NullValue()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Parameter("p").WithDefault(null);
                });
            })
            .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Root_Parameter_Pattern_NullPattern()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Parameter("p").WithPattern(null);
                });
            })
            .ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Root_Parameter_Pattern_EmptyPattern()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Parameter("p").WithPattern("");
                });
            })
            .ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Root_Parameter_Constraint_NullConstraint()
        {
            Routes.Invoking(routes =>
            {
                routes.Map<RootController>(root =>
                {
                    root.Parameter("p").WithConstraint(null);
                });
            })
            .ShouldThrow<ArgumentNullException>();
        }

        #endregion
    }
}
