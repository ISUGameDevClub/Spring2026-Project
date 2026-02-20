using System;
using Nomad.Core.ServiceRegistry.Interfaces;

/*
===================================================================================

ServiceLocator

===================================================================================
*/
/// <summary>
/// 
/// </summary>

public sealed class ServiceLocator
{
	private static IServiceLocator _locator;

	/*
	===============
	ServiceLocator
	===============
	*/
	/// <summary>
	/// 
	/// </summary>
	/// <param name="locator"></param>
	public ServiceLocator(IServiceLocator locator)
	{
		_locator = locator;
	}

	/*
	===============
	GetService
	===============
	*/
	/// <summary>
	/// Fetches a service based on the type provided by <typeparamref name="TService"/>.
	/// </summary>
	/// <typeparam name="TService"></typeparam>
	/// <returns></returns>
	public static TService GetService<TService>() where TService : class
		=> _locator.GetService<TService>();
}