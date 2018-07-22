using System.Linq;
using System.Reflection;
using BotPlatform.Core.System.Commands;

namespace BotPlatform.Core.System.Attributes
{
	public static class AttributesFactory
	{
		public static Command<TBot, TMessage> GetDecoratedCommand<TBot, TMessage>(this Callback<TBot, TMessage> signature) 
		{
			var decoratedCommand = new Command<TBot, TMessage>(signature);
			var attributesList = signature.Method.GetCustomAttributes<DecorateAttribute>();

			return attributesList.Aggregate(decoratedCommand, (current, attribute) => attribute.Decorate(current));
		}
	}
}
