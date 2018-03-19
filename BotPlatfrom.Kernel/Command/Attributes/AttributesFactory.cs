using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BotPlatfrom.Kernel.Command.Attributes
{
	public static class AttributesFactory
	{

		public static Command GetCallback(this Callback signature, Type executorType)
		{
			var decoratedCommand = new Command(signature, executorType);

			var attributesList = signature.Method.GetCustomAttributes<DecorateAttribute>();
			foreach (var attribute in attributesList)
			{
				decoratedCommand = attribute.Decorate(decoratedCommand);
			}
			return decoratedCommand;
		}
	}
}
