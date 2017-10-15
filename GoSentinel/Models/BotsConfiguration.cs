using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoSentinel.Models
{
    public class BotsConfiguration
    {
        public TelegramBotConfiguration Telegram { get; set; }
    }

    public class TelegramBotConfiguration
    {
        public string Token { get; set; }
    }
}
