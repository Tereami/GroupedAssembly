﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GroupedAssembly {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class MyStrings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MyStrings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GroupedAssembly.MyStrings", typeof(MyStrings).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Elements are not included in the assembly, ids.
        /// </summary>
        public static string ErrorElementsNotInclude {
            get {
                return ResourceManager.GetString("ErrorElementsNotInclude", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No elements allowed to include in an assembly.
        /// </summary>
        public static string ErrorNoAllowedElements {
            get {
                return ResourceManager.GetString("ErrorNoAllowedElements", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No selected elements.
        /// </summary>
        public static string ErrorNoSelectedElements {
            get {
                return ResourceManager.GetString("ErrorNoSelectedElements", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Falied to create assembly.
        /// </summary>
        public static string FailedToCreateAssembly {
            get {
                return ResourceManager.GetString("FailedToCreateAssembly", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to set assembly name. Given name.
        /// </summary>
        public static string FailedToSetAssemblyName {
            get {
                return ResourceManager.GetString("FailedToSetAssemblyName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to set group name. Given name.
        /// </summary>
        public static string FailedToSetGroupName {
            get {
                return ResourceManager.GetString("FailedToSetGroupName", resourceCulture);
            }
        }
    }
}
