using System;
using AutoMapper;

namespace Enterwell.AutoMapper.Extensions
{
    /// <summary>
    /// Extensions for executing mappings.
    /// </summary>
    public static class MappingToExtensions
    {
        /// <summary>
        /// An object extension method that maps one object to specified object using provided mapper.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="obj">The obj to act on.</param>
        /// <param name="mapper">The mapper.</param>
        /// <returns>
        /// A the mapped object.
        /// </returns>
        public static T MapTo<T>(this object obj, IMapper mapper) => mapper.Map<T>(obj);

        /// <summary>
        /// An object extension method that maps one object to specified object with composite properties using provided mapper.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="obj">The obj to act on.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="compositeProperties">The composite properties.</param>
        /// <returns>
        /// A the mapped object.
        /// </returns>
        public static T MapComposeTo<T>(this object obj, IMapper mapper, params object[] compositeProperties) =>
            mapper.MapComposeTo<T>(obj, compositeProperties);

        /// <summary>
        /// Maps the specified source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mapper">The mapper.</param>
        /// <param name="source">The source.</param>
        /// <param name="compositeProperties">The composite properties.</param>
        /// <returns>Returns the mapped object.</returns>
        public static T MapComposeTo<T>(this IMapper mapper, object source, params object[] compositeProperties)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            if (source == null) throw new ArgumentNullException(nameof(source));

            var destination = mapper.Map<T>(source, opts =>
            {
                for (var i = 0; i < compositeProperties.Length; i++)
                    opts.Items.Add(i.ToString(), compositeProperties[i]);
            });

            return destination;
        }
    }
}