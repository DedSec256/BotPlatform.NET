﻿using BotPlatform.Core.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BotPlatform.Tests.CommandCenterTests
{
	[TestClass]
	public class CommandCenterTests
	{
		/// <summary>
		/// Проверяем, выполняется ли проверка ботов на возможность
		/// выполнения команды
		/// </summary>
		[TestMethod]
		public void CanBeExecutedByBotTest()
		{
			CommandCenter.Instance.AddModule(new TestCommandModule());

			var result = CommandCenter.Instance.Execute("bot", "help", m => m);
			Assert.AreEqual(true, result);
		}
		[TestMethod]
		public void CanBeExecutedByBotTest1()
		{
			CommandCenter.Instance.AddModule(new TestCommandModule());

			var result = CommandCenter.Instance.Execute(1, 1, m => "help");
			Assert.AreEqual(false, result);
		}

	}
}
