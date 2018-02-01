using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoSentinel.Models;

namespace GoSentinel.Services
{
    public interface IGymByNameService
    {
        Gym GetGym(string gymName);
    }
}
