using System;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace Cafe.Util.Settings
{
    public interface ISettingsProvider
    {
        object PopulateSettings(Type settingsType);
    }

    public class SettingsProvider : ISettingsProvider
    {
        public object PopulateSettings(Type settingsType)
        {
            ConfigurationManager.RefreshSection("appSettings");

            var constructor = settingsType.GetConstructors().Single();

            return Activator.CreateInstance(
                settingsType,
                constructor.GetParameters()
                    .Select(p => GetConfigurationValue($"{settingsType.Name}.{p.Name}", p))
                    .ToArray());
        }

        private static object GetConfigurationValue(string configurationKey, ParameterInfo parameterInfo)
        {
            var value = ConfigurationManager.AppSettings[configurationKey];

            if (string.IsNullOrWhiteSpace(value))
            {
                if (parameterInfo.IsOptional)
                    return parameterInfo.DefaultValue;
                throw new ConfigurationErrorsException(
                    $"Väärtus `{configurationKey}` on kohustuslik, kuid hetkel puudub rakenduse konfiguratsioonifailist.");
            }

            var converter = TypeDescriptor.GetConverter(parameterInfo.ParameterType);
            if (!converter.CanConvertFrom(typeof(string)))
                throw new ConfigurationErrorsException($"Ei oska `System.String` andmetüüpi teisendada `{parameterInfo.ParameterType.FullName}` andmetüübiks.");

            return converter.ConvertFrom(value);
        }
    }
}