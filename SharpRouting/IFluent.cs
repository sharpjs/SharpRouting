using System;
using System.ComponentModel;

namespace SharpRouting
{
    /// <summary>
    ///   Base interface for fluent programming interfaces.
    ///   Hides members of <c>System.Object</c> from IntelliSense,
    ///   providing a cleaner experience for consumers.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///     Note that hiding does not occur in Visual Studio when this interface
    ///     is referenced via a project reference.
    ///   </para>
    ///   <para>
    ///     Credit to Daniel Cazzulino.  See http://bit.ly/ifluentinterface for more information.
    ///   </para>
    /// </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IFluent
    {
        /// <summary>
        ///   Gets the type of this instance.
        /// </summary>
        /// <returns>
        ///   The type of this instance.
        /// </returns>
        /// <remarks>
        ///   This redeclaration hides the <c>System.Object.GetType()</c> method from IntelliSense,
        ///   providing a cleaner fluent interface for consumers.
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Never)]
        Type GetType();

        /// <summary>
        ///   Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///   A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        /// <remarks>
        ///   This redeclaration hides the <c>System.Object.GetHashCode()</c> method from IntelliSense,
        ///   providing a cleaner fluent interface for consumers.
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();

        /// <summary>
        ///   Returns a <c>"System.String"</c> that represents this instance.
        /// </summary>
        /// <returns>
        ///   A <c>System.String</c> that represents this instance.
        /// </returns>
        /// <remarks>
        ///   This redeclaration hides the <c>System.Object.ToString()</c> method from IntelliSense,
        ///   providing a cleaner fluent interface for consumers.
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Never)]
        string ToString();

        /// <summary>
        ///   Determines whether the specified <c>System.Object</c> is equal to this instance.
        /// </summary>
        /// <param name="other">The <c>System.Object</c> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <c>System.Object</c> is equal to this instance;
        ///   otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        ///   This redeclaration hides the <c>System.Object.Equals(object)</c> method from IntelliSense,
        ///   providing a cleaner fluent interface for consumers.
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object other);
    }
}
