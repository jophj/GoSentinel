﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GoSentinel.Data;
using GoSentinel.Models;

namespace GoSentinel.Bots.Controllers.BotAction
{
    public class GymStateActionController : IActionController<GymStateAction>
    {
        public IActionResponse Handle(IAction baseAction)
        {
            if (baseAction == null)
            {
                throw new ArgumentNullException();
            }

            if (!(baseAction is GymStateAction action) || string.IsNullOrEmpty(action.GymName))
            {
                throw new ArgumentException();
            }

            return new GymStateActionResponse()
            {
                Action = action,
                Gym = new Gym()
                {
                    Id = "gymId",
                    Name = action.GymName,
                    Pokemons = new Collection<DefenderPokemon>()
                }
            };
        }
    }
}
