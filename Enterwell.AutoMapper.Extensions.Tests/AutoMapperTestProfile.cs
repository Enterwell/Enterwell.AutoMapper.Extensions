using AutoMapper;

namespace Enterwell.AutoMapper.Extensions.Tests
{
    public class AutoMapperTestProfile : Profile
    {
        public AutoMapperTestProfile()
        {

        }
    }

    public class MockEntitySource
    {
        public string Prop1 { get; set; }
    }

    public class MockEntityDestination
    {
        public string Prop1 { get; set; }
    }
}