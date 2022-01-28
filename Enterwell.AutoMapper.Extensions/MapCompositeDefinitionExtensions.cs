using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;

namespace Enterwell.AutoMapper.Extensions
{
    /// <summary>
    /// MapComposite definition extensions.
    /// </summary>
    public static class MapCompositeDefinitionExtensions
    {
        /// <summary>
        /// Gets the composite property.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="index">The index.</param>
        /// <returns>Returns the composite property at given index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Missing parameter at index {index} of type {typeof(TSource)}</exception>
        public static TSource GetCompositeProperty<TSource>(this ResolutionContext context, int index)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (context.Items.Values.Count <= index)
                return default;

            return context.Items.Values.ElementAt(index) is TSource
                ? (TSource)context.Items.Values.ElementAt(index)
                : default;
        }

        /// <summary>
        /// Gets the composite property required.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="index">The index.</param>
        /// <returns>Returns the required composite property at given index.</returns>
        /// <exception cref="InvalidCastException">Failed to cast composite property at index {index} to type {typeof(TSource)}</exception>
        public static TSource GetCompositePropertyRequired<TSource>(this ResolutionContext context, int index)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (context.Items.Values.Count <= index)
                throw new ArgumentOutOfRangeException(
                    $"Missing parameter at index {index} of type {typeof(TSource).FullName}");

            return context.Items.Values.ElementAt(index) is TSource
                ? (TSource)context.Items.Values.ElementAt(index)
                : throw new InvalidCastException(
                    $"Failed to cast composite property at index {index} to type {typeof(TSource).FullName}");
        }

        /// <summary>
        /// Maps the composite property.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TDest">The type of the dest.</typeparam>
        /// <typeparam name="TMemberDest">The type of the member dest.</typeparam>
        /// <param name="map">The map.</param>
        /// <param name="dest">The dest.</param>
        /// <param name="additionalPropertyIndex">Index of the additional property.</param>
        /// <returns>Returns the IMappingExpression.</returns>
        public static IMappingExpression<TSource, TDest> MapCompositeProperty<TSource, TDest, TMemberDest>(
            this IMappingExpression<TSource, TDest> map,
            Expression<Func<TDest, TMemberDest>> dest,
            int additionalPropertyIndex = 0) =>
            map.MapPropertyFunc(dest, (source, ctx) => ctx.GetCompositeProperty<TMemberDest>(additionalPropertyIndex));


        /// <summary>
        /// Maps the composite property required.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TDest">The type of the dest.</typeparam>
        /// <typeparam name="TMemberDest">The type of the member dest.</typeparam>
        /// <param name="map">The map.</param>
        /// <param name="dest">The dest.</param>
        /// <param name="additionalPropertyIndex">Index of the additional property.</param>
        /// <returns>Returns the IMappingExpression.</returns>
        public static IMappingExpression<TSource, TDest> MapCompositePropertyRequired<TSource, TDest, TMemberDest>(
            this IMappingExpression<TSource, TDest> map,
            Expression<Func<TDest, TMemberDest>> dest,
            int additionalPropertyIndex = 0) =>
            map.MapPropertyFunc(dest, (source, ctx) => ctx.GetCompositePropertyRequired<TMemberDest>(additionalPropertyIndex));
    }
}