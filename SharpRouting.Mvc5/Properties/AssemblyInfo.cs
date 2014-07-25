using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

// General Information
[assembly: AssemblyTitle       ("SharpRouting.Mvc5")]
[assembly: AssemblyDescription ("Fluent Routing API for ASP.NET MVC 5")]

// Security
//
// All code is transparent; the entire assembly will not do anything privileged or unsafe.
// http://msdn.microsoft.com/en-us/library/dd233102.aspx
//
[assembly: SecurityTransparent]

// Testing
#if DEBUG
    [assembly: InternalsVisibleTo("SharpRouting.Tests")]
#endif
