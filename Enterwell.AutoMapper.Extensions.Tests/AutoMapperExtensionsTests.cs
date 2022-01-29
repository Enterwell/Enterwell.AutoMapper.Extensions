using System;
using Xunit;

namespace Enterwell.AutoMapper.Extensions.Tests
{
    /// <summary>
    /// Tests for AutoMapper extensions.
    /// </summary>
    /// <seealso cref="Xunit.IClassFixture&lt;Enterwell.AutoMapper.Extensions.Tests.AutoMapperFixture&gt;" />
    public class AutoMapperExtensionsTests: IClassFixture<AutoMapperFixture>
    {
        private readonly AutoMapperFixture fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperExtensionsTests"/> class.
        /// </summary>
        /// <param name="fixture">The fixture.</param>
        /// <exception cref="System.ArgumentNullException">fixture</exception>
        public AutoMapperExtensionsTests(AutoMapperFixture fixture)
        {
            this.fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        }

        /// <summary>
        /// Tests mapping validation for test fixture.
        /// </summary>
        [Fact]
        public void AutoMapperExtensionsTests_MappingValidation()
        {
            var exception = Record.Exception(() => this.fixture.Mapper.ConfigurationProvider.AssertConfigurationIsValid());
            Assert.Null(exception);
        }
    }
}
