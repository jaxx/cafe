using System.Linq;
using System.Reflection;
using Cafe.App.Framework;
using Cafe.Util.Settings;
using DryIoc;

namespace Cafe.App.DryIoc
{
    public class Bootstrapper
    {
        private readonly IContainer container = new Container();

        private Bootstrapper()
        {
            container.Register<Program>(Reuse.Singleton);

            RegisterSettings();

            container.RegisterDelegate(x => EventFlowBuilder.Build(x.Resolve<EventFlowSettings>()));

            //RegisterTypesWithSuffix(typeof(FileDao).Assembly, "Dao");
        }

        private IContainer GetContainer()
        {
            return container;
        }

        public static IContainer Bootstrap()
        {
            return new Bootstrapper().GetContainer();
        }

        private void RegisterSettings()
        {
            container.Register<ISettingsProvider, SettingsProvider>(Reuse.Singleton);
            container.RegisterSettings<EventFlowSettings>();
        }

        private void RegisterTypesWithSuffix(Assembly assembly, string suffix)
        {
            var implementingClasses = assembly.GetTypes()
                .Where(type => type.Name.EndsWith(suffix) && type.IsPublic && !type.IsAbstract && type.GetInterfaces().Length != 0);

            foreach (var implementingClass in implementingClasses)
            {
                var iface = implementingClass.GetInterfaces().SingleOrDefault(x => x.Name.EndsWith(implementingClass.Name));
                if (iface != null)
                    container.Register(iface, implementingClass, Reuse.Singleton, PropertiesAndFields.Auto, ifAlreadyRegistered: IfAlreadyRegistered.Keep);
            }
        }
    }
}