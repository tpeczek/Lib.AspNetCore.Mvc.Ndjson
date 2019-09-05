using System;
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
    }
}
