﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Calendar_Converter.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string BreakDataFile {
            get {
                return ((string)(this["BreakDataFile"]));
            }
            set {
                this["BreakDataFile"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2015-01-01")]
        public global::System.DateTime OldStart {
            get {
                return ((global::System.DateTime)(this["OldStart"]));
            }
            set {
                this["OldStart"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2015-01-01")]
        public global::System.DateTime NewStart {
            get {
                return ((global::System.DateTime)(this["NewStart"]));
            }
            set {
                this["NewStart"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool IncludeBreaks {
            get {
                return ((bool)(this["IncludeBreaks"]));
            }
            set {
                this["IncludeBreaks"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool CalculateBreaks {
            get {
                return ((bool)(this["CalculateBreaks"]));
            }
            set {
                this["CalculateBreaks"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("16")]
        public uint NumberOfWeeks {
            get {
                return ((uint)(this["NumberOfWeeks"]));
            }
            set {
                this["NumberOfWeeks"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int CurrentWeek {
            get {
                return ((int)(this["CurrentWeek"]));
            }
            set {
                this["CurrentWeek"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2")]
        public int DateFormatString {
            get {
                return ((int)(this["DateFormatString"]));
            }
            set {
                this["DateFormatString"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("#FFDC143C")]
        public global::System.Windows.Media.SolidColorBrush OldSemesterColor {
            get {
                return ((global::System.Windows.Media.SolidColorBrush)(this["OldSemesterColor"]));
            }
            set {
                this["OldSemesterColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("#FF808080")]
        public global::System.Windows.Media.SolidColorBrush NewSemesterColor {
            get {
                return ((global::System.Windows.Media.SolidColorBrush)(this["NewSemesterColor"]));
            }
            set {
                this["NewSemesterColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public int SpringBreakWeekNum {
            get {
                return ((int)(this["SpringBreakWeekNum"]));
            }
            set {
                this["SpringBreakWeekNum"] = value;
            }
        }
    }
}
