﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HubstaffReportGenerator {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("HubstaffReportGenerator.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!DOCTYPE html&gt;
        ///&lt;html&gt;
        ///
        ///&lt;head&gt;
        ///&lt;title&gt;Hubstaff Report&lt;/title&gt;
        ///	&lt;meta name=&quot;viewport&quot; content=&quot;width=device-width, initial-scale=1&quot;/&gt;
        ///&lt;link rel=&quot;stylesheet&quot; href=&quot;https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css&quot; integrity=&quot;sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u&quot; crossorigin=&quot;anonymous&quot;&gt;
        ///	&lt;style type=&quot;text/css&quot;&gt;
        ///		table {
        ///			table-layout: fixed;
        ///		}
        ///		
        ///		td {
        ///			/* css-3 */
        ///			white-space: -o-pre-wrap;
        ///			word-wrap: break-word;
        ///			white-sp [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string HtmlTemplate {
            get {
                return ResourceManager.GetString("HtmlTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;tr&gt;
        ///	&lt;td&gt;{{Date}}&lt;/td&gt;
        ///	&lt;td&gt;{{Member}}&lt;/td&gt;
        ///	&lt;td&gt;{{Project Hours}}&lt;/td&gt;
        ///	&lt;td&gt;{{Tasks}}&lt;/td&gt;
        ///	&lt;td&gt;{{Task Hours}}&lt;/td&gt;
        ///	&lt;td&gt;{{Activity}}&lt;/td&gt;
        ///	&lt;td&gt;{{Notes}}&lt;/td&gt;
        ///&lt;/tr&gt;.
        /// </summary>
        internal static string TBodyTemplate {
            get {
                return ResourceManager.GetString("TBodyTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;thead class=&quot;thead-inverse&quot;&gt;
        ///    &lt;tr&gt;
        ///        &lt;th style=&quot;width: 10%&quot;&gt;Date&lt;/th&gt;
        ///        &lt;th style=&quot;width: 10%&quot;&gt;Member&lt;/th&gt;
        ///        &lt;th style=&quot;width: 10%&quot;&gt;Project Hours&lt;/th&gt;
        ///        &lt;th style=&quot;width: 30%&quot;&gt;Tasks&lt;/th&gt;
        ///        &lt;th style=&quot;width: 10%&quot;&gt;Task Hours&lt;/th&gt;
        ///        &lt;th style=&quot;width: 10%&quot;&gt;Activity&lt;/th&gt;
        ///        &lt;th style=&quot;width: 20%&quot;&gt;Notes&lt;/th&gt;
        ///    &lt;/tr&gt;
        ///&lt;/thead&gt;.
        /// </summary>
        internal static string THeadTemplate {
            get {
                return ResourceManager.GetString("THeadTemplate", resourceCulture);
            }
        }
    }
}
