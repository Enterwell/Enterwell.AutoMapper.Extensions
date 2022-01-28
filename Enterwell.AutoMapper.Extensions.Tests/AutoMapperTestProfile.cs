using AutoMapper;

namespace Enterwell.AutoMapper.Extensions.Tests
{
    /// <summary>
    /// Automapper tests profile.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class AutoMapperTestProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperTestProfile"/> class.
        /// </summary>
        public AutoMapperTestProfile()
        {

        }
    }

    /// <summary>
    /// Mock entity for source.
    /// </summary>
    public class MockEntitySource
    {
        public string Prop1 { get; set; }
    }

    /// <summary>
    /// Mock entity for destination.
    /// </summary>
    public class MockEntityDestination
    {
        public string Prop1 { get; set; }
    }
}