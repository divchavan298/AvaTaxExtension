using System.Collections.Generic;
using VirtoCommerce.Platform.Core.Settings;

namespace NB.AvaTaxExtension.Core
{
    public static class ModuleConstants
    {
        public static class Security
        {
            public static class Permissions
            {
                public const string Access = "AvaTaxExtension:access";
                public const string Create = "AvaTaxExtension:create";
                public const string Read = "AvaTaxExtension:read";
                public const string Update = "AvaTaxExtension:update";
                public const string Delete = "AvaTaxExtension:delete";

                public static string[] AllPermissions { get; } =
                {
                Access,
                Create,
                Read,
                Update,
                Delete,
            };
            }
        }

        public static class Settings
        {
            public static class General
            {
                //public static SettingDescriptor AvaTaxExtensionEnabled { get; } = new SettingDescriptor
                //{
                //    Name = "AvaTaxExtension.AvaTaxExtensionEnabled",
                //    GroupName = "AvaTaxExtension|General",
                //    ValueType = SettingValueType.Boolean,
                //    DefaultValue = false,
                //};

                //public static SettingDescriptor AvaTaxExtensionPassword { get; } = new SettingDescriptor
                //{
                //    Name = "AvaTaxExtension.AvaTaxExtensionPassword",
                //    GroupName = "AvaTaxExtension|Advanced",
                //    ValueType = SettingValueType.SecureString,
                //    DefaultValue = "qwerty",
                //};

                //public static IEnumerable<SettingDescriptor> AllGeneralSettings
                //{
                //    get
                //    {
                //        yield return AvaTaxExtensionEnabled;
                //        yield return AvaTaxExtensionPassword;
                //    }
                //}

                public static SettingDescriptor SynchronizationIsActive { get; } = new SettingDescriptor
                {
                    Name = "AvaTaxExtension.IsActive",
                    GroupName = "Tax|General",
                    ValueType = SettingValueType.Boolean
                };
                public static SettingDescriptor SynchronizationIsCommited { get; } = new SettingDescriptor
                {
                    Name = "AvaTaxExtension.IsCommit",
                    GroupName = "Tax|General",
                    ValueType = SettingValueType.Boolean
                };

                public static IEnumerable<SettingDescriptor> AllGeneralSettings
                {
                    get
                    {
                        return new List<SettingDescriptor>
                        {
                            SynchronizationIsActive,
                            SynchronizationIsCommited
                        };
                    }
                }
            }

            public static IEnumerable<SettingDescriptor> AllSettings
            {
                get
                {
                    return General.AllGeneralSettings;
                }
            }
        }
    }
}
