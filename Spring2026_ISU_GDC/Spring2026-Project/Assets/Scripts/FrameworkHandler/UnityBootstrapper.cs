using UnityEngine;
using Nomad.Core;
using Nomad.Core.Events;
using Nomad.Core.Logger;
using Nomad.Logger;
using Nomad.Events;
using Nomad.CVars;
using Nomad.EngineUtils;
using Nomad.FileSystem;
using Nomad.Core.CVars;
using Nomad.Core.ServiceRegistry.Globals;

public class UnityBootstrapper : MonoBehaviour
{
	private static NomadFrameworkBootstrapper _bootstrapper;

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

		var serviceRegistry = ServiceRegistry.Instance;
		var serviceLocator = ServiceLocator.Instance;

		_bootstrapper = new NomadFrameworkBootstrapper(serviceRegistry, serviceLocator)
			.AddBootstrapper(new LoggerBootstrapper())
			.AddBootstrapper(new EventBootstrapper())
			.AddBootstrapper(new CVarBootstrapper())
			.AddBootstrapper(new EngineBootstrapper())
			.AddBootstrapper(new FileSystemBootstrapper())
		;
		_bootstrapper.Bootstrap();
	}
}