using System;
using GoSentinel.Bots.Controllers;
using GoSentinel.Data;
using Telegram.Bot.Types;
using Xunit;

namespace GoSentinel.Tests.Bots.Controllers.BotAction
{
    public abstract class ControllerTests<T> where T : IAction
    {
        protected IActionController<T> Controller { get; }

        protected ControllerTests(IActionController<T> controller)
        {
            Controller = controller;
        }

        [Fact]
        public void Handle_WithNullArgument_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Controller.Handle(null));
        }

        [Fact]
        public void Handle_WithWrongTypeAction_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Controller.Handle(new WrongTypeAction()));
        }

        public class WrongTypeAction : IAction
        {
            public string Name => "WrongTypeAction";
            public Message Message { get; set; }
        }

        protected abstract T MakeAction();
    }
}