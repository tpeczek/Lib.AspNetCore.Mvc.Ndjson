using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Lib.AspNetCore.Mvc.Ndjson;
using Lib.AspNetCore.Mvc.Ndjson.NewtonsoftJson;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions methods for configuring MVC via an <see cref="IMvcBuilder"/>.
    /// </summary>
    public static class NewtonsoftNdjsonStreamMvcBuilderExtensions
    {
        /// <summary>
        /// Configures NDJSON specific action results.
        /// </summary>
        /// <param name="builder">The <see cref="IMvcBuilder"/>.</param>
        /// <returns>The <see cref="IMvcBuilder"/>.</returns>
        public static IMvcBuilder AddNewtonsoftNdjsonResults(this IMvcBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services.TryAddSingleton<INdjsonWriterFactory, NewtonsoftNdjsonWriterFactory>();

            return builder;
        }
    }
}
