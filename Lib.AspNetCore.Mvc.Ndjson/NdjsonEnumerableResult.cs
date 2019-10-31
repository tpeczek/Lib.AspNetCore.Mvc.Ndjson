using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;


namespace Lib.AspNetCore.Mvc.Ndjson
{
    /// <summary>
    /// An <see cref="ActionResult"/> for returning a NDJSON enumerable.
    /// </summary>
    public class NdjsonEnumerableResult : ActionResult, IStatusCodeActionResult
    {
        private readonly IAsyncEnumerable<Object> _values;

        /// <summary>
        /// Gets or sets the HTTP status code.
        /// </summary>
        public int? StatusCode { get; set; }

        /// <summary>
        /// Initializes new instance of <see cref="NdjsonEnumerableResult"/>.
        /// </summary>
        /// <param name="values">An <see cref="IAsyncEnumerable{Object}"/> providing values to be written.</param>
        public NdjsonEnumerableResult(IAsyncEnumerable<Object> values)
        {
            _values = values ?? throw new ArgumentNullException(nameof(values));
        }

        /// <summary>
        /// Executes the result operation of the action method synchronously. This method is called by MVC to process the result of an action method.
        /// </summary>
        /// <param name="context">The <see cref="ActionContext"/> in which the result is executed. The context information includes information about the action that was executed and request information.</param>
        public override void ExecuteResult(ActionContext context)
        {
            throw new NotSupportedException($"The {nameof(NdjsonEnumerableResult)} doesn't support synchronous execution.");
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
            using (INdjsonWriter ndjsonTextWriter = ndjsonTextWriterFactory.CreateWriter(context, this))
            {
                await foreach(object value in _values)
                {
                    await ndjsonTextWriter.WriteAsync(value);
                }
            }
        }
    }
}
