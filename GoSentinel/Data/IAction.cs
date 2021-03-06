﻿using System.Threading.Tasks;
using GoSentinel.Bots;
using GoSentinel.Bots.Controllers;
using Telegram.Bot.Types;

namespace GoSentinel.Data
{
    public interface IAction
    {
        string Name { get; }
        Message Message { get; set; }
    }
}
