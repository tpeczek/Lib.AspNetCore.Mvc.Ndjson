using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lib.AspNetCore.Mvc.Ndjson
{
    /// <summary>
    /// An interface for NDJSON writer.
    /// </summary>
    public interface INdjsonWriter : IDisposable
    {
        /// <summary>
        /// Writes an object.
        /// </summary>
        /// <param name="value">An object to be written.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        Task WriteAsync(object value);

        /// <summary>
        /// Writes an object.
        /// </summary>
        /// <param name="value">An object to be written.</param>
        /// <param name="cancellationToken">A token that may be used to cancel the write operation.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        Task WriteAsync(object value, CancellationToken cancellationToken);
    }
}
