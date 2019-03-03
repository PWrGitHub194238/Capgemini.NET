using System;
using Xunit;

namespace _13.RangesAndIndices.Tests
{
    public class StartIndexTest
    {
        [Theory]
        [ClassData(typeof(StartIndexTestData))]
        public void GetFirstElementFromStringArrayBasedOnIndex(string[] input, Index indices, string expectedOutput)
        {
            // Arrange

            // Act
            string actualOutput = input[indices];

            // Assert
            Assert.Equal(expected: expectedOutput, actual: actualOutput);
        }
    }
}