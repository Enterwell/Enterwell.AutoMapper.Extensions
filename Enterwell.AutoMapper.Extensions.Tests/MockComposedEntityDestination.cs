using System;

namespace Enterwell.AutoMapper.Extensions.Tests
{
    /// <summary>
    /// Mock composed entity destination.
    /// </summary>
    /// <seealso cref="MockSimpleEntityDestination" />
    public class MockComposedEntityDestination : MockSimpleEntityDestination
    {
        /// <summary>
        /// Gets or sets the additional property.
        /// </summary>
        /// <value>
        /// The additional property.
        /// </value>
        public DateTime AdditionalRequiredProperty { get; set; }

        /// <summary>
        /// Gets or sets the second additional property.
        /// </summary>
        /// <value>
        /// The second additional property.
        /// </value>
        public string  SecondAdditionalOptionalProperty { get; set; }
    }
}