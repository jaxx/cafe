using Cafe.Util.Settings;
using DryIoc;

namespace Cafe.App.DryIoc
{
    public static class RegistrationExtensions
    {
        public static IRegistrator RegisterSettings<TSettingsType>(this IRegistrator registrator)
        {
            var type = typeof(TSettingsType);
            registrator.RegisterDelegate(type, x => x.Resolve<ISettingsProvider>().PopulateSettings(type), Reuse.Singleton);
            return registrator;
        }
    }
}