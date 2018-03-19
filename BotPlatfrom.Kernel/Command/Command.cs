using System;
using System.Collections.Specialized;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using BotPlatfrom.Kernel.Interfaces;

namespace BotPlatfrom.Kernel.Command
{
	public delegate void Callback(IMessage message, IBot bot, object arg = null);
    public class Command
    {
	    public readonly Callback Callback;
	    public readonly Type BotType;

	    public Command(Callback callback, Type executorType = null)
	    {
		    Callback = callback;
		    BotType = executorType ?? typeof(IBot);
	    }

	    protected void BeforeExecute(IMessage message, IBot bot, object arg = null) { }

	    public virtual Task<bool> ExecuteAsync(IMessage message, IBot bot, object arg = null)
	    {
		    return Task.Run(() =>
		    {
			    try
			    {
				    BeforeExecute(message, bot, arg);
				    Execute(message, bot, arg);
				    AfterExecute(message, bot, arg);

				    return true;
			    }
			    catch (Exception ex)
			    {
				    return false;
			    }
		    });
	    }
	    public virtual bool Execute(IMessage message, IBot bot, object arg = null)
	    {
		    try
		    {
			    BeforeExecute(message, bot, arg);
			    Execute(message, bot, arg);
			    AfterExecute(message, bot, arg);

			    return true;
		    }
		    catch (Exception ex)
		    {
			    return false;
		    }
	    }
	    protected void AfterExecute(IMessage message, IBot bot, object arg = null) { }

	}

	public abstract class AttributedCommand : Command
	{
		protected Command BaseCommand;
		protected AttributedCommand(Command baseCommand) : base(baseCommand.Callback)
		{
			BaseCommand = baseCommand;
		}
	}
}
