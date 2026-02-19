using UnityEngine;
using Nomad.Core;
using Nomad.Core.ServiceRegistry.Interfaces;
using Nomad.Core.ServiceRegistry.Services;
using Nomad.Core.Events;
using Nomad.Core.Logger;
using Nomad.Logger;
using Nomad.Events;
using Nomad.CVars;
using Nomad.CVars.Interfaces;
using Nomad.EngineUtils;
using Nomad.Core.EngineUtils;
using Nomad.FileSystem;

public class UnityBootstrapper : MonoBehaviour
{
	private static NomadFrameworkBootstrapper _bootstrapper;
	private static IServiceRegistry _serviceRegistry;

	private static ServiceLocator _serviceLocator;

#if NOMAD_ENABLE_LOGGER
	private static ILoggerService _logger;
#endif

#if NOMAD_ENABLE_EVENTS
	private static IGameEventRegistryService _eventRegistry;
#endif

#if NOMAD_ENABLE_CVARS
	private static ICVarSystemService _cvarSystem;
#endif

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
	private static void Initialize()
	{
		Debug.Log("Initializing NomadFramework...");

		var collection = new ServiceCollection();
		var locator = new Nomad.Core.ServiceRegistry.Services.ServiceLocator(collection);
		_serviceRegistry = collection;

		_serviceRegistry.RegisterSingleton<IEngineService>(new UnityEngineService());

		_serviceLocator = new ServiceLocator(locator);
		_bootstrapper = new NomadFrameworkBootstrapper(_serviceRegistry, locator)
#if NOMAD_ENABLE_LOGGER
			.AddBootstrapper(new LoggerBootstrapper())
#endif
			.AddBootstrapper(new FileSystemBootstrapper())

#if NOMAD_ENABLE_EVENTS
			.AddBootstrapper(new EventBootstrapper())
#endif

#if NOMAD_ENABLE_CVARS
			.AddBootstrapper(new CVarBootstrapper())
#endif
		;
		_bootstrapper.Bootstrap();
	}
}