using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Lib.AspNetCore.Mvc.Ndjson;
using Lib.AspNetCore.Mvc.Ndjson.NewtonsoftJson;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for adding Newtonsoft.Json to <see cref="IMvcCoreBuilder"/>.
    /// </summary>
    public static class NdjsonStreamMvcCoreBuilderExtensions
    {
        /// <summary>
        /// Configures NDJSON specific action result.
        /// </summary>
        /// <param name="builder">The <see cref="IMvcBuilder"/>.</param>
        /// <returns>The <see cref="IMvcBuilder"/>.</returns>
        public static IMvcCoreBuilder AddNdjsonStreamResult(this IMvcCoreBuilder builder)
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
