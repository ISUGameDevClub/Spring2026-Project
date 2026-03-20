using UnityEngine;
using Nomad.Core;
using Nomad.Core.ServiceRegistry.Interfaces;
using Nomad.Core.ServiceRegistry.Services;
using Nomad.Core.Events;
using Nomad.Core.Logger;
using Nomad.Events;
using Nomad.CVars;
using Nomad.FileSystem;
using Nomad.Core.CVars;
using Nomad.EngineUtils;
using Nomad.Logger;
using Nomad.Save;

public sealed class UnityBootstrapper : MonoBehaviour
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
		_serviceLocator = new ServiceLocator(collection);
		_serviceRegistry = collection;

		_bootstrapper = new NomadFrameworkBootstrapper(_serviceRegistry, _serviceLocator)
			.AddBootstrapper( new LoggerBootstrapper() )
			.AddBootstrapper( new EventBootstrapper() )
			.AddBootstrapper( new CVarBootstrapper() )
			.AddBootstrapper( new EngineBootstrapper() )
			.AddBootstrapper( new FileSystemBootstrapper() )
			.AddBootstrapper( new SaveBootstrapper() )
		;
		_bootstrapper.Bootstrap();
	}
}