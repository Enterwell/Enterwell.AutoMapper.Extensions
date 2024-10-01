using System;

namespace Enterwell.AutoMapper.Extensions.Tests
{
    /// <summary>
    /// Mock simple entity source
    /// </summary>
    public class MockSimpleEntitySource
    {
        /// <summary>
        /// Gets or sets the common property one.
        /// </summary>
        /// <value>
        /// The common property one.
        /// </value>
        public string CommonPropOne { get; set; }

        /// <summary>
        /// Gets or sets the source property one.
        /// </summary>
        /// <value>
        /// The source property one.
        /// </value>
        public int SourcePropOne { get; set; }
    }

    /// <summary>
    /// Mock additional entity source.
    /// </summary>
    public class MockAdditionalEntitySource
    {
        /// <summary>
        /// Gets or sets the additional source property.
        /// </summary>
        /// <value>
        /// The additional source property.
        /// </value>
        public DateTime AdditionalSourceProperty { get; set; }
    }
}