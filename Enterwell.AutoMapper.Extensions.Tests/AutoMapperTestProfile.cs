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
            this.CreateMap<MockSimpleEntitySource, MockSimpleEntityDestination>()
                .MapProperty(dst => dst.DestinationPropOne, src => src.SourcePropOne);
        }
    }
}