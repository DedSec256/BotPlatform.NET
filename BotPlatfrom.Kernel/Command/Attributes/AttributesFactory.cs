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

			var attributesList = signature.Method.CustomAttributes
				.Where(a => typeof(DecorateAttribute).IsAssignableFrom(a.AttributeType));

			foreach (var attribute in attributesList)
			{
				var attributeInstance = Activator.CreateInstance(attribute.AttributeType) as DecorateAttribute;
				decoratedCommand = attributeInstance.Decorate(decoratedCommand);
			}

			return decoratedCommand;
		}
	}
}
