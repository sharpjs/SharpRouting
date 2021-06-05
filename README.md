#### `DEVELOPMENT HALTED`

This project has fulfilled its purpose.  It began in a time when ASP.NET Core
did not exist and when ASP.NET MVC provided only a rudimentary paradigm for
configuring routes.  Later, ASP.NET MVC 5 and ASP.NET Core introduced support
for [attribute routing](https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-5.0#attribute-routing-for-rest-apis),
which provides much of the capability of SharpRouting while being easier to
understand.  Attribute routing is built in and good enough.  Thus there is no
need for further development in this repository.

SharpRouting continues to be used in a few production scenarios.  Owing to 100%
code coverage by automated tests, no one ever reported a bug in this package,
nor do I expect any bug report in future.

# SharpRouting: Fluent Routing API for ASP.NET MVC

**SharpRouting** is a simple, lightweight API that makes routing easier in ASP.NET MVC projects.  It is available as a NuGet package for [MVC 4](https://www.nuget.org/packages/SharpRouting.Mvc4) or [MVC 5.x](https://www.nuget.org/packages/SharpRouting.Mvc5).

Benefits include:
* Create routes with less code.
* Routes are easier to understand, reading like human language.
* Fluent interface guides you to correct usage.
* Split complex routing into manageable chunks.
* [Don't Repeat Yourself](http://en.wikipedia.org/wiki/Don't_repeat_yourself): Avoid copy-pasting shared bits among routes.
* Fully compatible with [RESTful](http://en.wikipedia.org/wiki/Representational_state_transfer) and [resource-oriented](http://en.wikipedia.org/wiki/Resource-oriented_architecture) approaches.
* Extend the API with your own routing conventions.

**What about MVC 5?** MVC 5 introduced [attribute routing](http://blogs.msdn.com/b/webdev/archive/2013/10/17/attribute-routing-in-asp-net-mvc-5.aspx), which provides some of these benefits.  While MVC 5 attribute routing is quite good, SharpRouting retains advantages in versatility, extensibility, and avoidance of duplication.

SharpRouting has 100% code coverage by a suite of automated unit tests.  Every public method and type has IntelliSense documentation.

## Getting Started

First off, you need to install the appropriate NuGet package: [MVC 4](https://www.nuget.org/packages/SharpRouting.Mvc4) or [MVC 5.x](https://www.nuget.org/packages/SharpRouting.Mvc5)

Next, you should add a **using** directive at the top of each file that uses SharpRouting.

```cs
using SharpRouting;
```

## The Root Controller

Most projects have a **root controller**.  Often called **HomeController**, this controller handles requests to the app's base URL.  Let's create a route for it — fluently.

In **RouteConfig** (or wherever your routing configuration is), we need just one line to identify the root controller:
```cs
public static void RegisterRoutes(RouteCollection routes)
{
    // ...
    routes.MapController<HomeController>();
    // ...
}
```

In **HomeController**, we express the route:
```cs
public static void RegisterRoutes(IRouteBuilder root)
{
    root.Url("").Get().IsAction("Index");
}
```

Let's look at this in detail.

The **RegisterRoutes** method is special.  When you tell SharpRouting to map routes for a controller, SharpRouting finds this method in the controller and executes the routing instructions within it.  This enables you to express routing code in small chunks located located near the relevant action code, rather than in one huge, merge-conflict-prone RouteConfig.cs.  This practice doesn't appeal to everyone, so SharpRouting provides a few alternative ways to organize routes — more on that later.

`Url("")` begins mapping a route for a URL.  In this case, the URL is empty, meaning that the route will apply to requests for the application base URL itself.

`Get()` means that the route will apply only to GET requests.

`IsAction("Index")` indicates the name of the action name that will handle requests.  This method also creates the route and adds it to the route collection.

## Controllers

Once SharpRouting knows about the root controller, we are ready to create routes for the other controllers in the application.  Let's create routes for **ArticleController**, which represents a collection of articles on a hypothetical blog.

In **HomeController**, we point to our new controller.
```cs
public static void RegisterRoutes(IRouteBuilder root)
{
    root.Url("articles").IsController<ArticleController>();
}
```

In **ArticleController**, we create the routes:
```cs
public static void RegisterRoutes(IRouteBuilder articles)
{
    articles.Url("")   .Get()  .IsAction("Index");
    articles.Url("new").Get()  .IsAction("New");
    articles.Url("")   .Post() .IsAction("Create");
}
```

`root.Url("articles")` begins the mapping for a URL.  Because it appears in the root controller, it is relative to the application base URL.

`.IsController<ArticleController>()` maps the URL to **ArticleController**.  SharpRouting discovers the controller's **RegisterRoutes** method and invokes it.

`articles.Url("new")` begins the mapping for a URL.  Because it appears in **ArticleController**, it is relative *to the URL of **ArticleController***, which is `articles`.  Thus the entire URL being mapped is `articles/new`.

`.Get()` indicates that the route will apply only to GET requests.

`.IsAction("New");` indicates the name of the action name that will handle requests.  This method also creates the route and adds it to the route collection.

In SharpRouting, controllers are organized hierarchically.  Except for the root controller, each controller must be the child of some parent controller.  The parent can be the root controller itself, or the parent can be some other controller.  **Each controller inherits the routing information from its parent.**  This frees you from having to duplicate the same URLs, defaults, and constraints across many routes in your application.  In most cases with SharpRouting, you only need to specify these things once.

## Parameters

Typical example:
```cs
articles.Parameter("id").IsAction("Show");
```

Multiple parameters:
```cs
articles.Parameter("year").Parameter("id").IsAction("Show");
```

A parameter limited to a specific data type:
```cs
articles.Parameter("id").Guid().IsAction("Show");
```
Currenly, only `Guid` and `Int32` constraints are implemented.

A parameter that must match a regular expression pattern:
```cs
articles.Parameter("id").Pattern("^[a-z0-9]+$").IsAction("Show");
```

A parameter that must match a custom **IRouteConstraint**:
```cs
articles.Parameter("id").Constraint(myConstraint).IsAction("Show");
```

Optional parameters, with or without a default value:
```cs
articles.Parameter("id").Optional() .IsAction("Show");
articles.Parameter("id").Default(42).IsAction("Show");
```

## HTTP Methods (Verbs)

To limit a route to a specific HTTP method, include the matching directive in the route specification.

```cs
articles.Url("id").Get().IsAction("Show");
```

Multiple directives can be specified.  The route will match if the request uses any of the specified methods.

```cs
articles.Url("id").Put().Post().IsAction("Update");
```

All HTTP 1.1 methods are supported: `.Get()` `.Post()` `.Put()` `.Delete()` `.Head()` `.Patch()` `.Options()`

## Actions

Route specifications that end with `.IsAction(...)` create a route for a specific action.  The parameter is the action name, which must match the name of an action method or an action name specified by [ActionNameAttribute](http://msdn.microsoft.com/en-us/library/system.web.mvc.actionnameattribute.aspx).

For example, in **ArticlesController.cs**:
```cs
public static void RegisterRoutes(IRouteBuilder articles)
{
    articles.Url("id").Get().IsAction("Show");
}

public ActionResult Show()
{
    ....
}
```

#### Actions and Route Names

By default, the action name also determines the name of the created route.  The code above creates a route named **Articles.Show**, following a *controller*.*action* naming scheme.  It is possible to specify an alternate string for the second half of this name:

```cs
articles.Url("id").Get().IsAction("Show", route: "ShowFull");
```

This creates a route named **Articles.ShowFull**.

## Grouping

Use a parameter with multiple routes:

```csharp
articles.Parameter("id").Is(article =>
{
    article.Url("").Get().IsAction("Show");
    article.Url("").Put().IsAction("Update");
});
```

## Areas

Routing for an area is equally simple as routing for a controller.  Just tell SharpRouting about a controller that represents the root of the area:

```csharp
home.Url("admin").IsArea<Area.Admin.Controllers.AdminController>();
```

The controller can be empty (no action methods) if desired.  SharpRouting will invoke the controller's **RegisterRoutes** method to discover the routes for the area.

## Helper Methods

In a few advanced cases, it might be more straightforward to create a route directly, rather than via the fluent API.  SharpRouting provides several extension methods to help make custom routes more concise.  These methods all work on raw **Route** objects.

To limit a route to specific HTTP verbs:
```cs
route.ForGet();           // Route applies only to HTTP GET
route.ForPost();          // Route applies only to HTTP POST
route.ForPut();           // Route applies only to HTTP PUT
route.ForPutOrPost();     // Route applies only to HTTP PUT or POST
route.ForDelete();        // Route applies only to HTTP DELETE
route.ForDeleteOrPost();  // Route applies only to HTTP DELETE or POST
```

To set a route parameter as optional with no default value:
```cs
route.WithOptional(name);
```

To provide a default value for an optional route parameter:
```cs
route.WithDefault(key, value);
```

To add a constraint that must be met for the route:
```cs
route.WithConstraint(key, constraint);
```

To add a data token to provide information to the underlying routing system:
```cs
route.WithDataToken(key, value);
```
