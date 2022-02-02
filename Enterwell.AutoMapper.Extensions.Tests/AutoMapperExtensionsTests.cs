using System;
using AutoMapper;
using Xunit;

namespace Enterwell.AutoMapper.Extensions.Tests
{
    /// <summary>
    /// Tests for AutoMapper extensions.
    /// </summary>
    public class AutoMapperExtensionsTests
    {
        /// <summary>
        /// Tests MapTo extension.
        /// </summary>
        [Fact]
        public void AutoMapperExtensionsTests_MapTo()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MockSimpleEntitySource, MockSimpleEntityDestination>()
                    .ForMember(dst => dst.DestinationPropOne,
                        opt => opt.MapFrom(src => src.SourcePropOne));
            }).CreateMapper();

            var source = new MockSimpleEntitySource
            {
                SourcePropOne = 5,
                CommonPropOne = Guid.NewGuid().ToString()
            };

            var destination = source.MapTo<MockSimpleEntityDestination>(mapper);

            Assert.Equal(source.SourcePropOne, destination.DestinationPropOne);
            Assert.Equal(source.CommonPropOne, destination.CommonPropOne);
        }

        /// <summary>
        /// Tests MapProperty extension.
        /// </summary>
        [Fact]
        public void AutoMapperExtensionsTests_MapProperty()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MockSimpleEntitySource, MockSimpleEntityDestination>()
                    .MapProperty(dst => dst.DestinationPropOne, src => src.SourcePropOne);
            }).CreateMapper();
            
            var commonProp = Guid.NewGuid().ToString();
            var srcProp = 5;
            var source = new MockSimpleEntitySource { CommonPropOne = commonProp, SourcePropOne = srcProp };
            var dst = mapper.Map<MockSimpleEntityDestination>(source);

            Assert.Equal(source.CommonPropOne, dst.CommonPropOne);
            Assert.Equal(source.SourcePropOne, dst.DestinationPropOne);
        }

        /// <summary>
        /// Tests MapPropertyFunc extension.
        /// </summary>
        [Fact]
        public void AutoMapperExtensionsTests_MapPropertyFunc()
        {
            const int expectedDestProp = 3;
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MockSimpleEntitySource, MockSimpleEntityDestination>()
                    .MapPropertyFunc(dst => dst.DestinationPropOne, src => Math.Min(src.SourcePropOne, expectedDestProp));
            }).CreateMapper();

            var commonProp = Guid.NewGuid().ToString();
            const int srcProp = 5;
            var source = new MockSimpleEntitySource { CommonPropOne = commonProp, SourcePropOne = srcProp };
            var dst = mapper.Map<MockSimpleEntityDestination>(source);

            Assert.Equal(source.CommonPropOne, dst.CommonPropOne);
            Assert.Equal(dst.DestinationPropOne, expectedDestProp);
        }

        /// <summary>
        /// Tests map with additional required property that is valid.
        /// </summary>
        [Fact]
        public void AutoMapperExtensionsTests_MapComposeTo_RequiredCompositePropertyValid()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MockSimpleEntitySource, MockComposedEntityDestination>()
                    .MapProperty(dst => dst.DestinationPropOne, src => src.SourcePropOne)
                    .MapCompositePropertyRequired(dst => dst.AdditionalRequiredProperty);
            }).CreateMapper();

            var commonProp = Guid.NewGuid().ToString();
            int srcProp = 5;
            var additionalProperty = DateTime.Now;

            var source = new MockSimpleEntitySource
            {
                CommonPropOne = commonProp,
                SourcePropOne = srcProp
            };

            var destination = source.MapComposeTo<MockComposedEntityDestination>(mapper, additionalProperty);

            Assert.Equal(source.CommonPropOne, destination.CommonPropOne);
            Assert.Equal(source.SourcePropOne, destination.DestinationPropOne);
            Assert.Equal(destination.AdditionalRequiredProperty, additionalProperty);
        }

        /// <summary>
        /// Tests MapCompose with additional required property that is missing.
        /// </summary>
        [Fact]
        public void AutoMapperExtensionsTests_MapComposeTo_RequiredCompositePropertyMissing()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MockSimpleEntitySource, MockComposedEntityDestination>()
                    .MapProperty(dst => dst.DestinationPropOne, src => src.SourcePropOne)
                    .MapCompositePropertyRequired(dst => dst.AdditionalRequiredProperty);
            }).CreateMapper();

            var source = new MockSimpleEntitySource();

            var exception = Record.Exception(() => source.MapComposeTo<MockComposedEntityDestination>(mapper));
            
            Assert.NotNull(exception);
        }

        /// <summary>
        /// Tests MapCompose with additional required property provided wrong type.
        /// </summary>
        [Fact]
        public void AutoMapperExtensionsTests_MapComposeTo_CompositePropertyWrongType()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MockSimpleEntitySource, MockComposedEntityDestination>()
                    .MapProperty(dst => dst.DestinationPropOne, src => src.SourcePropOne)
                    .MapCompositePropertyRequired(dst => dst.AdditionalRequiredProperty);
            }).CreateMapper();

            const string additionalPropertyWrongType = "String instead of DateTime";

            var source = new MockSimpleEntitySource();

            var exception = Record.Exception(() => source.MapComposeTo<MockComposedEntityDestination>(mapper, additionalPropertyWrongType));

            Assert.NotNull(exception);
        }

        /// <summary>
        /// Tests map with MapCompositeAll.
        /// </summary>
        [Fact]
        public void AutoMapperExtensionsTests_MapCompositeAll()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MockSimpleEntitySource, MockComposedEntityDestination>()
                    .MapProperty(dst => dst.DestinationPropOne, src => src.SourcePropOne)
                    .MapCompositeAll(typeof(MockComposedEntitySource));
            }).CreateMapper();

            var source = new MockSimpleEntitySource
            {
                SourcePropOne = 1,
                CommonPropOne = Guid.NewGuid().ToString()
            };

            var composedSource = new MockComposedEntitySource
            {
                AdditionalRequiredProperty = DateTime.Now,
                SecondAdditionalOptionalProperty = Guid.NewGuid().ToString()
            };

            var destination = source.MapComposeTo<MockComposedEntityDestination>(mapper, composedSource);

            Assert.Equal(destination.CommonPropOne, source.CommonPropOne);
            Assert.Equal(destination.DestinationPropOne, source.SourcePropOne);
            Assert.Equal(destination.AdditionalRequiredProperty, composedSource.AdditionalRequiredProperty);
            Assert.Equal(destination.SecondAdditionalOptionalProperty, composedSource.SecondAdditionalOptionalProperty);
        }
    }
}
