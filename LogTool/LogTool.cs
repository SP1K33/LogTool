using System;
using System.Diagnostics;
using System.IO;

namespace SP1K3.LogTool
{
	public static class LogTool
	{
		private static LogToolConfig _currentConfig;

		public static void Configure(LogToolConfig config)
		{
			_currentConfig = config;
		}

		private static bool TrySeparate(ref string source)
		{
			if (string.IsNullOrEmpty(source))
			{
				return false;
			}

			var result = source[source.Length - 1] != ' ';
			if (result)
			{
				source += " ";
			}
			return result;
		}

		public static void Log(string log, LogToolConfig? overrideLog = null)
		{
			var config = overrideLog ?? _currentConfig;
			string result = string.Empty;

			if (FlagsHelper.IsSet(config.Rules, LogRules.UtcNow))
			{
				result += $"[{DateTime.UtcNow}]";
			}

			TrySeparate(ref result);

			if (FlagsHelper.IsSet(config.Rules, LogRules.MethodInfo))
			{
				var method = new StackTrace().GetFrame(1).GetMethod();
				var className = method.ReflectedType.Name;
				var methodName = method.Name;
				result += $"[{className}.{methodName}]";
			}

			TrySeparate(ref result);

			result += log + '\n';

			File.AppendAllText(config.FilePath, result);

			if (FlagsHelper.IsSet(config.Rules, LogRules.LogConsole))
			{
				Console.Write(result);
			}
		}
	}
}
