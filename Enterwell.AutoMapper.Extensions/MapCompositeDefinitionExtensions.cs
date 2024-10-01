using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.Internal;

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

        /// <summary>
        /// An IMappingExpression&lt;TSrc,TDest&gt; extension method that map composite object properties to source object.
        /// </summary>
        /// <typeparam name="TSource"> Type of the source.</typeparam>
        /// <typeparam name="TDest">Type of the destination.</typeparam>
        /// <param name="map">          The map to act on.</param>
        /// <param name="compositeType">Type of the composite.</param>
        /// <param name="index">        (Optional) The index.</param>
        public static void MapCompositeAll<TSource, TDest>(
            this IMappingExpression<TSource, TDest> map,
            Type compositeType,
            int index = 0) =>
            Array.ForEach(compositeType.GetProperties(),
                compositeProperty =>
                    map.ForMember(compositeProperty.Name,
                        opt => opt.MapFrom((src, dest, _, ctx) =>
                        compositeProperty.GetValue(ctx.GetCompositePropertyRequired(compositeType, index)))));

        /// <summary>
        /// Gets the composite property required.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="type">The type.</param>
        /// <param name="index">The index.</param>
        /// <returns>Returns the required composite property at given index.</returns>
        /// <exception cref="InvalidCastException">Failed to cast composite property at index {index} to type {typeof(TSource)}</exception>
        public static object GetCompositePropertyRequired(this ResolutionContext context, Type type, int index)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (context.Items.Values.Count <= index)
                throw new ArgumentOutOfRangeException($"Missing parameter at index {index} of type {type.FullName}");

            return context.Items.Values.ElementAt(index) != type.GetDefaultValue()
                ? context.Items.Values.ElementAt(index)
                : throw new InvalidCastException(
                    $"Failed to cast composite property at index {index} to type {type.FullName}");
        }

        /// <summary>
        /// Gets the default value of given type.
        /// </summary>
        /// <param name="t">The type.</param>
        /// <returns>Returns null for classes and default value for value types.</returns>
        private static object GetDefaultValue(this Type t)
        {
            if (t.IsValueType && Nullable.GetUnderlyingType(t) == null)
                return Activator.CreateInstance(t);
            return null;
        }
    }
}