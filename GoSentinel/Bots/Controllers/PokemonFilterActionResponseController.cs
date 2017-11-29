﻿using System;
using GoSentinel.Models;

namespace GoSentinel.Bots.Controllers
{
    public class PokemonFilterActionResponseController : IActionResponseController<PokemonFilterActionResponse>
    {
        public void Handle(IBot bot, IActionResponse actionResponseBase)
        {
            if (!(actionResponseBase is PokemonFilterActionResponse actionResponse))
            {
                throw new ArgumentNullException();
            }

            var action = actionResponse.Action;
            var msg = $"{action.GetType().Name} - {action.Message.From.Username} - {action.PokemonName} - {action.Stat} - {action.ValueMin } - {action.ValueMax}";
            bot.SendTextMessageAsync(actionResponse.Action.Message.Chat.Id, msg);
        }
    }
}