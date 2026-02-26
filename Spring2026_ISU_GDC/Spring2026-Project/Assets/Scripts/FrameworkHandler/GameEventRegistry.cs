using System;
using System.Runtime.CompilerServices;
using Nomad.Core.Events;

public static class GameEventRegistry
{
	private static IGameEventRegistryService _registry;
	private static IGameEventRegistryService Registry
	{
		get
		{
			_registry ??= ServiceLocator.GetService<IGameEventRegistryService>() ?? throw new InvalidOperationException("GameEventRegistry not created yet!");
			return _registry;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TArgs"></typeparam>
	/// <param name="name"></param>
	/// <param name="nameSpace"></param>
	/// <param name="flags"></param>
	/// <returns></returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IGameEvent<TArgs> GetEvent<TArgs>(string name, string nameSpace, EventFlags flags = EventFlags.Default) where TArgs : struct
		=> Registry.GetEvent<TArgs>(name, nameSpace, flags);
}