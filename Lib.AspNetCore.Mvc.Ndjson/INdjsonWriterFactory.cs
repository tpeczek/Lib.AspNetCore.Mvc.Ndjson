using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Lib.AspNetCore.Mvc.Ndjson
{
    /// <summary>
    /// An interface for <see cref="INdjsonWriter"/> factory.
    /// </summary>
    public interface INdjsonWriterFactory
    {
        /// <summary>
        /// Creates a new <see cref="INdjsonWriter"/>.
        /// </summary>
        /// <param name="context">The <see cref="ActionContext"/>.</param>
        /// <param name="result">The <see cref="NdjsonStreamResult"/>.</param>
        /// <returns>The new <see cref="INdjsonWriter"/>.</returns>
        INdjsonWriter CreateWriter(ActionContext context, IStatusCodeActionResult result);
    }
}
