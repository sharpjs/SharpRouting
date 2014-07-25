using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

// General Information
[assembly: AssemblyTitle       ("SharpRouting.Mvc5")]
[assembly: AssemblyDescription ("Fluent Routing API for ASP.NET MVC 5")]

// Security
//
// All types and members are security-critical, except where
// being security-critical violates an inheritance rule.
// http://msdn.microsoft.com/en-us/library/dd233102.aspx
//
// (default behavior; no security attribute)
//
// NOTE: The above was forced, because System.Web.Mvc.dll
// changed in MVC 5 to be security-critical.

// Testing
#if DEBUG
    [assembly: InternalsVisibleTo("SharpRouting.Mvc5.Tests")]
#endif
