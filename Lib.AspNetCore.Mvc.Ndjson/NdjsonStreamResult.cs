using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Lib.AspNetCore.Mvc.Ndjson
{
    /// <summary>
    /// An <see cref="ActionResult"/> for returning a NDJSON stream.
    /// </summary>
    public class NdjsonStreamResult : ActionResult, IStatusCodeActionResult
    {
        private INdjsonWriter _ndjsonTextWriter;
        private readonly TaskCompletionSource<bool> _readyTaskCompletionSource = new TaskCompletionSource<bool>();
        private readonly TaskCompletionSource<bool> _completeTaskCompletionSource = new TaskCompletionSource<bool>();

        /// <summary>
        /// Gets or sets the HTTP status code.
        /// </summary>
        public int? StatusCode { get; set; }

        /// <summary>
        /// Executes the result operation of the action method synchronously. This method is called by MVC to process the result of an action method.
        /// </summary>
        /// <param name="context">The <see cref="ActionContext"/> in which the result is executed. The context information includes information about the action that was executed and request information.</param>
        public override void ExecuteResult(ActionContext context)
        {
            throw new NotSupportedException($"The {nameof(NdjsonStreamResult)} doesn't support synchronous execution.");
        }

        /// <summary>
        /// Executes the result operation of the action method asynchronously. This method is called by MVC to process the result of an action method.
        /// </summary>
        /// <param name="context">The <see cref="ActionContext"/> in which the result is executed. The context information includes information about the action that was executed and request information.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous execute operation.</returns>
        public override async Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            INdjsonWriterFactory ndjsonTextWriterFactory = context.HttpContext.RequestServices.GetRequiredService<INdjsonWriterFactory>();
            using (_ndjsonTextWriter = ndjsonTextWriterFactory.CreateWriter(context, this))
            {
                _readyTaskCompletionSource.SetResult(true);

                await _completeTaskCompletionSource.Task;
            }
        }

        /// <summary>
        /// Asynchronously writes the specified value to the stream.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous write operation.</returns>
        public async Task WriteAsync(object value)
        {
            if (!_readyTaskCompletionSource.Task.IsCompletedSuccessfully)
            {
                await _readyTaskCompletionSource.Task;
            }

            await _ndjsonTextWriter.WriteAsync(value);
        }

        /// <summary>
        /// Marks the stream as being complete, meaning no more values will be written to it.
        /// </summary>
        public void Complete()
        {
            _completeTaskCompletionSource.SetResult(true);
        }
    }
}
