using System;

namespace Enterwell.AutoMapper.Extensions.Tests
{
    /// <summary>
    /// Mock entity for source.
    /// </summary>
    public class MockSimpleEntitySource
    {
        /// <summary>
        /// Gets or sets the prop1.
        /// </summary>
        /// <value>
        /// The prop1.
        /// </value>
        public string CommonPropOne { get; set; }

        /// <summary>
        /// Gets or sets the property source one.
        /// </summary>
        /// <value>
        /// The property source one.
        /// </value>
        public int SourcePropOne { get; set; }
    }

    public class MockAdditionalEntitySource
    {
        public DateTime AdditionalSourceProperty { get; set; }
    }
}