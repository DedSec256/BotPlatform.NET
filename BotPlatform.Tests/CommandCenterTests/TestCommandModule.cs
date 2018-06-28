using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.System;

namespace BotPlatform.Tests.CommandCenterTests
{
	class TestCommandModule : CommandsModule<string, string>
	{
		public override IDictionary<string, Callback<string, string>> Initialize()
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
