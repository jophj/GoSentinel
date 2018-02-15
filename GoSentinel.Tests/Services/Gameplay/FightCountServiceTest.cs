using System;
using GoSentinel.Services.Gameplays;
using Xunit;

namespace GoSentinel.Tests.Services.Gameplay
{
    public class FightCountServiceTest
    {
        private readonly FightCountService _service;

        public FightCountServiceTest()
        {
            _service = new FightCountService();
        }

        [Fact]
        public void Count_WithZeroCp_ShouldThrowArumentException()
        {
            Assert.Throws<ArgumentException>(() => _service.Count(0, 1));
        }

        [Fact]
        public void Count_WithZeroDecayedCp_ShouldThrowArumentException()
        {
            Assert.Throws<ArgumentException>(() => _service.Count(1, 0));
        }
    }
}
