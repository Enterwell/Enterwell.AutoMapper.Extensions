using System;
using System.Linq.Expressions;
using AutoMapper;

namespace Enterwell.AutoMapper.Extensions
{
    /// <summary>
    /// MapProperty definition extensions.
    /// </summary>
    public static class MapPropertyDefinitionExtensions
    {
        /// <summary>
        /// An IMappingExpression&lt;TSource,TDest&gt; extension method that maps property to source
        /// member.
        /// </summary>
        /// <typeparam name="TSource">Type of the source.</typeparam>
        /// <typeparam name="TDest">Type of the destination.</typeparam>
        /// <typeparam name="TMemberDest">Type of the member destination.</typeparam>
        /// <typeparam name="TMemberSource">Type of the member source.</typeparam>
        /// <param name="map">The map to act on.</param>
        /// <param name="dest">Destination for the.</param>
        /// <param name="source">Source for the.</param>
        /// <returns>
        /// An IMappingExpression&lt;TSource,TDest&gt;
        /// </returns>
        public static IMappingExpression<TSource, TDest> MapProperty<TSource, TDest, TMemberDest, TMemberSource>(
            this IMappingExpression<TSource, TDest> map,
            Expression<Func<TDest, TMemberDest>> dest,
            Expression<Func<TSource, TMemberSource>> source) =>
            map.ForMember(dest, opt => opt.MapFrom(source));

        /// <summary>
        /// An IMappingExpression&lt;TSource,TDest&gt; extension method that maps property to source
        /// member.
        /// </summary>
        /// <typeparam name="TSource">Type of the source.</typeparam>
        /// <typeparam name="TDest">Type of the destination.</typeparam>
        /// <typeparam name="TMemberDest">Type of the member destination.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="map">The map to act on.</param>
        /// <param name="dest">Destination for the.</param>
        /// <param name="sourceFunc">Source for the.</param>
        /// <returns>
        /// An IMappingExpression&lt;TSource,TDest&gt;
        /// </returns>
        public static IMappingExpression<TSource, TDest> MapPropertyFunc<TSource, TDest, TMemberDest, TResult>(
            this IMappingExpression<TSource, TDest> map,
            Expression<Func<TDest, TMemberDest>> dest,
            Func<TSource, TResult> sourceFunc) =>
            map.ForMember(dest, opt => opt.MapFrom((source, _) => sourceFunc(source)));

        /// <summary>
        /// An IMappingExpression&lt;TSource,TDest&gt; extension method that maps property to source
        /// member.
        /// </summary>
        /// <typeparam name="TSource">Type of the source.</typeparam>
        /// <typeparam name="TDest">Type of the destination.</typeparam>
        /// <typeparam name="TMemberDest">Type of the member destination.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="map">The map to act on.</param>
        /// <param name="dest">Destination for the.</param>
        /// <param name="sourceFunc">Source for the.</param>
        /// <returns>
        /// An IMappingExpression&lt;TSource,TDest&gt;
        /// </returns>
        public static IMappingExpression<TSource, TDest> MapPropertyFunc<TSource, TDest, TMemberDest, TResult>(
            this IMappingExpression<TSource, TDest> map,
            Expression<Func<TDest, TMemberDest>> dest,
            Func<TSource, IRuntimeMapper, TResult> sourceFunc) =>
            map.ForMember(dest, opt => opt.MapFrom((source, unused1, unused2, ctx) => sourceFunc(source, ctx.Mapper)));

        /// <summary>
        /// An IMappingExpression&lt;TSource,TDest&gt; extension method that maps property to source
        /// member.
        /// </summary>
        /// <typeparam name="TSource">Type of the source.</typeparam>
        /// <typeparam name="TDest">Type of the destination.</typeparam>
        /// <typeparam name="TMemberDest">Type of the member destination.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="map">The map to act on.</param>
        /// <param name="composition">Destination for the.</param>
        /// <param name="sourceFunc">Source for the.</param>
        /// <returns>
        /// An IMappingExpression&lt;TSource,TDest&gt;
        /// </returns>
        public static IMappingExpression<TSource, TDest> MapPropertyFunc<TSource, TDest, TMemberDest, TResult>(
            this IMappingExpression<TSource, TDest> map,
            Expression<Func<TDest, TMemberDest>> composition,
            Func<TSource, TDest, ResolutionContext, TResult> sourceFunc) =>
            map.ForMember(composition, opt => opt.MapFrom((source, dest, _, ctx) => sourceFunc(source, dest, ctx)));

        /// <summary>
        /// An IMappingExpression&lt;TSource,TDest&gt; extension method that maps property to source
        /// member.
        /// </summary>
        /// <typeparam name="TSource">Type of the source.</typeparam>
        /// <typeparam name="TDest">Type of the destination.</typeparam>
        /// <typeparam name="TMemberDest">Type of the member destination.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="map">The map to act on.</param>
        /// <param name="dest">Destination for the.</param>
        /// <param name="sourceFunc">Source for the.</param>
        /// <returns>
        /// An IMappingExpression&lt;TSource,TDest&gt;
        /// </returns>
        public static IMappingExpression<TSource, TDest> MapPropertyFunc<TSource, TDest, TMemberDest, TResult>(
            this IMappingExpression<TSource, TDest> map,
            Expression<Func<TDest, TMemberDest>> dest,
            Func<TSource, ResolutionContext, TResult> sourceFunc) =>
            map.ForMember(dest, opt => opt.MapFrom((source, _, __, ctx) => sourceFunc(source, ctx)));
    }
}
