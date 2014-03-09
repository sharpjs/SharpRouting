# Fluent Routing API for ASP.NET MVC

**Fluent Routing** is a simple, lightweight API that makes routing easier in ASP.NET MVC projects.  It is available as a NuGet package for [MVC 4](https://www.nuget.org/packages/FluentRouting.Mvc4).  A package for MVC 5 is coming soon.

Benefits include:
* Create routes with less code.
* Routes are easier to understand, reading like human language.
* Fluent interface guides you to correct usage.
* Split complex routing into manageable chunks.
* [Don't Repeat Yourself](http://en.wikipedia.org/wiki/Don't_repeat_yourself): Avoid copy-pasting shared bits among routes.
* Fully compatible with [RESTful](http://en.wikipedia.org/wiki/Representational_state_transfer) and [resource-oriented](http://en.wikipedia.org/wiki/Resource-oriented_architecture), approaches.
* Extend the API with your own routing conventions.

**What about MVC 5?** MVC 5 introduced [attribute routing](http://blogs.msdn.com/b/webdev/archive/2013/10/17/attribute-routing-in-asp-net-mvc-5.aspx), which provides some of these benefits.  While MVC 5 attribute routing is quite good, Fluent Routing retains advantages in versatility, extensibility, and avoidance of duplication.

**WARNING:** Fluent Routing is in limited beta testing.  It might not work at all.  If you run into trouble, send a tweet to @sharpjs.

## Getting Started

First off, you need to install the appropriate NuGet package: [MVC 4](https://www.nuget.org/packages/FluentRouting.Mvc4).  MVC 5 support is coming soon.

Next, you should add a **using** directive at the top of each file that uses Fluent Routing.

```cs
using FluentRouting;
```

### Mapping the Root Controller

Most projects have a **root controller**.  Often called **HomeController**, this controller handles requests to the app's base URL.  Let's create a route for it - fluently.

In **RouteConfig**, we need just one line to identify the root controller.

```cs
routes.MapController<HomeController>();
```

In **HomeController**, we map the route:

```cs
public static void RegisterRoutes(IRouteBuilder root)
{
    root.Url("").Get().IsAction("Index");
}
```

Let's look at this in detail.

The **RegisterRoutes** method is special.  When you tell Fluent Routing to map routes for a controller, Fluent Routing finds this method in the controller and executes the routing instructions within it.  This allows you to put routing code near the relevant action code, avoiding a huge RouteConfig.cs.  This doesn't appeal to everyone, so Fluent Routing provides a few alternative ways to organize -- more on that later.

`Url("")` begins mapping a route for a URL.  In this case, the URL is empty, meaning that the route will apply to requests for the application base URL itself.

`Get()` means that the route will apply only to GET requests.

`IsAction("Index")` indicates the action name that will handle the requests.  This method also creates the route and adds it to the route collection.

### Mapping Controllers

In progress

### Parameters

In progress

### Subscopes (Name?)

In progress

### Areas

In progress

## Helper Methods

Incomplete

In a few advanced cases, it might be more straightforward to create a route directly, rather than via the fluent API.  Fluent Routing provides several extension methods to help make custom routes more concise.

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
