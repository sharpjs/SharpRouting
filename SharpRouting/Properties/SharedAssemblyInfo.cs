using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

// General Information
[assembly: AssemblyProduct   ("SharpRouting")]
[assembly: AssemblyCompany   ("Jeff Sharp")]
[assembly: AssemblyCopyright ("Copyright © 2014 Jeff Sharp")]

// Version
[assembly: AssemblyVersion              ("1.0.*")]        // Assembly
[assembly: AssemblyInformationalVersion ("1.0.0-beta1")]  // NuGet Package

// Compliance
[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]

// Security
[assembly: SecurityRules(SecurityRuleSet.Level2)]
    
// Configuration
#if DEBUG
    [assembly: AssemblyConfiguration("Debug")]
#else
    [assembly: AssemblyConfiguration("Release")]
#endif
