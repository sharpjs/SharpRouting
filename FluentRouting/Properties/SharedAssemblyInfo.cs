using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

// General Information
[assembly: AssemblyProduct   ("Fluent Routing")]
[assembly: AssemblyCompany   ("Jeff Sharp")]
[assembly: AssemblyCopyright ("Copyright © 2014 Jeff Sharp")]
[assembly: AssemblyVersion   ("1.0.*")]

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
