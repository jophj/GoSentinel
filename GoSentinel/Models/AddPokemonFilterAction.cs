using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoSentinel.Bots;
using GoSentinel.Bots.Controllers;
using Telegram.Bot.Types;

namespace GoSentinel.Models
{
    public class AddPokemonFilterAction : IAiAction
    {
        public Message Message { get; set; }
        public string PokemonName { get; set; }
        public PokemonStat Stat { get; set; }
        public int ValueMin { get; set; }
        public int? ValueMax { get; set; }

        public void Accept(IAiActionHandler handler)
        {
            handler.HandleAsync(this);
        }
    }
}
