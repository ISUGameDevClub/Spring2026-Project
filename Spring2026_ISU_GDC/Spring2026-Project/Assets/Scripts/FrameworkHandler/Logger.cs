using System;
using System.Runtime.CompilerServices;
using Nomad.Core.Logger;

public static class Logger
{
	private static ILoggerService _logger;
	private static ILoggerService Instance
	{
		get
		{
			_logger ??= ServiceLocator.GetService<ILoggerService>() ?? throw new InvalidOperationException("LoggerService not created yet!");
			return _logger;
		}
	}
}