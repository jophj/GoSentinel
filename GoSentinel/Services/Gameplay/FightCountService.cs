using System.Linq;

namespace GoSentinel.Services.Gameplays
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
            int runs = _cpThresholds.Count(cpt => ((double)maxCp / (double)decayedCp) > cpt);
            return runs;
        }
    }
}
