﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Testing.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.3.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Users\\sMali\\Documents\\MyTests\\")]
        public string defaulf_test_file_save_path {
            get {
                return ((string)(this["defaulf_test_file_save_path"]));
            }
            set {
                this["defaulf_test_file_save_path"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int number_of_starts {
            get {
                return ((int)(this["number_of_starts"]));
            }
            set {
                this["number_of_starts"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"if not exists (select * from sys.tables where name='group')
begin
create table [dbo].[group]
(
	[id] INT NOT NULL PRIMARY KEY Identity(1, 1),
	[name] nvarchar(10) not null
)
end
if not exists (select * from sys.tables where name='User')
begin
create table [dbo].[User]
(
	[id] INT NOT NULL PRIMARY KEY Identity(1, 1),
	[name] nvarchar(20) not null,
	[pass] nvarchar(20),
	[group] int not null
)
end

if not exists (select * from [group]) insert into [group]([name]) values (N'admin')

if not exists (select * from [User]) insert into [dbo].[User] ([name], [pass], [group]) VALUES (N'root', N'root', 1)
")]
        public string create_database {
            get {
                return ((string)(this["create_database"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Collections.Specialized.NameValueCollection list_of_tests {
            get {
                return ((global::System.Collections.Specialized.NameValueCollection)(this["list_of_tests"]));
            }
            set {
                this["list_of_tests"] = value;
            }
        }
    }
}
