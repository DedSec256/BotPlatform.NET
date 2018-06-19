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
		public static Command<TBot, TMessage> GetCommand<TBot, TMessage>(this Callback<TBot, TMessage> signature) 
		{
			var decoratedCommand = new Command<TBot, TMessage>(signature);
			var attributesList = signature.Method.GetCustomAttributes<DecorateAttribute>();

			return attributesList.Aggregate(decoratedCommand, (current, attribute) => attribute.Decorate(current));
		}
	}
}
