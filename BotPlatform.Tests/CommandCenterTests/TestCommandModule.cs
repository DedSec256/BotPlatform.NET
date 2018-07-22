using System.Collections.Generic;
using BotPlatform.Core.System;

namespace BotPlatform.Tests.CommandCenterTests
{
	class TestCommandModule : ICommandsModule<string, string>
	{
		public IDictionary<string, Callback<string, string>> Initialize()
		{
			return new Dictionary<string, Callback<string, string>>()
			{
				{"help", HelpCallback}
			};
		}

		private void HelpCallback(string bot, string message, object arg)
		{
		}
	}
}
