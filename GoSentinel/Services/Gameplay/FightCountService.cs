using System;
using System.Linq;

namespace GoSentinel.Services.Gameplay
{
    public class FightCountService
    {
        private readonly float[] _cpThresholds = new float[]
        {
            0,
            .4666f,
            .7333f
        };

        public int Count(int maxCp, int decayedCp)
        {
            if (maxCp == 0 || decayedCp == 0)
            {
                throw new ArgumentException();
            }

            if (maxCp < decayedCp)
            {
                throw new ArgumentException("Max CP should not be less than decayed CP");
            }

            int runs = _cpThresholds.Count(cpt => ((double)maxCp / (double)decayedCp) > cpt);
            return runs;
        }
    }
}
