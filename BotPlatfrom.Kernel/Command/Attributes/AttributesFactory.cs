using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Interfaces;

namespace BotPlatfrom.Kernel.Command.Attributes
{
	public static class AttributesFactory
	{
		public static Command<BotT, MessageT> GetCommand<BotT, MessageT>(this Callback<BotT, MessageT> signature) 
			where BotT: class, IBot
			where MessageT: class, IMessage
		{
			var decoratedCommand = new Command<BotT, MessageT>(signature);

			var attributesList = signature.Method.GetCustomAttributes<DecorateAttribute>();
			foreach (var attribute in attributesList)
			{
				decoratedCommand = attribute.Decorate(decoratedCommand);
			}
			return decoratedCommand;
		}
	}
}
