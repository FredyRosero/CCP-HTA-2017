﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CCP_HTA_2017.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("registro de errores.csv")]
        public string archivoErrores {
            get {
                return ((string)(this["archivoErrores"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("registro de actividad.csv")]
        public string archivoActividad {
            get {
                return ((string)(this["archivoActividad"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public string ultimoNinPacienteBuscado {
            get {
                return ((string)(this["ultimoNinPacienteBuscado"]));
            }
            set {
                this["ultimoNinPacienteBuscado"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>pas,60,#00FF00,120,#FF0000</string>
  <string>col,60,#00FF00,120,#FF0000</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection NumberToColorSetting {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["NumberToColorSetting"]));
            }
            set {
                this["NumberToColorSetting"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("data source=\"C:\\Users\\Fredy\\Source\\Repos\\CCP-HTA-2017\\CCP HTA 2017\\App_Data\\sqlit" +
            "e.db\";datetimeformat=JulianDay;synchronous=Full;datetime format=JulianDay;fail i" +
            "f missing=True")]
        public string sqliteConnectionString {
            get {
                return ((string)(this["sqliteConnectionString"]));
            }
        }
    }
}
