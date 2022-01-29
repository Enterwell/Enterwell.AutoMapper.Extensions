using AutoMapper;

namespace Enterwell.AutoMapper.Extensions.Tests
{
    /// <summary>
    /// Automapper test fixture.
    /// </summary>
    public class AutoMapperFixture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperFixture"/> class.
        /// </summary>
        public AutoMapperFixture()
        {
            this.Mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperTestProfile>();
            }).CreateMapper();
        }

        /// <summary>
        /// Gets or sets the mapper.
        /// </summary>
        /// <value>
        /// The mapper.
        /// </value>
        public IMapper Mapper { get; set; }
    }
}
