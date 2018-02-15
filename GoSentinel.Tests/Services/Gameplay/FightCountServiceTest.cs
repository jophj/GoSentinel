using System;
using GoSentinel.Services.Gameplay;
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

        [Fact]
        public void Count_WithHigherDecayedCp_ShoultThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _service.Count(10, 20));
        }

        [Theory]
        [InlineData(1000, 466)]
        [InlineData(10, 4)]
        [InlineData(2864, 10)]
        [InlineData(2864, 1336)]
        [InlineData(2864, 1234)]
        public void Count_WithLowerThan4666Cp_ShouldReturnOne(int maxCp, int decayedCp)
        {
            Assert.Equal(1, _service.Count(maxCp, decayedCp));
        }

        [Theory]
        [InlineData(1000, 733)]
        [InlineData(10, 7)]
        [InlineData(2864, 2100)]
        [InlineData(2864, 2012)]
        public void Count_WithLowerThan7333Cp_ShouldReturnTwo(int maxCp, int decayedCp)
        {
            Assert.Equal(2, _service.Count(maxCp, decayedCp));
        }

        [Theory]
        [InlineData(1000, 734)]
        [InlineData(1000, 1000)]
        [InlineData(10, 8)]
        [InlineData(2864, 2101)]
        [InlineData(2864, 2864)]
        public void Count_WithHigherThan7333Cp_ShouldReturnThree(int maxCp, int decayedCp)
        {
            Assert.Equal(3, _service.Count(maxCp, decayedCp));
        }
    }
}
