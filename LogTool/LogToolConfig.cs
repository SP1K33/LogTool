using System;

namespace SP1K3.LogTool
{
	[Flags] public enum LogRules { UtcNow = 1, MethodInfo, LogConsole }

	public readonly struct LogToolConfig
	{
		public LogToolConfig(LogRules logRules, string filePath)
		{
			Rules = logRules;
			FilePath = filePath;
		}
		public LogRules Rules { get; }
		public string FilePath { get; }
	}
}