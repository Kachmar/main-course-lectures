using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundamentals
{
    using Microsoft.AspNetCore.Http;

    public class MyRequestHandler
    {
        private readonly RequestDelegate next;

        public MyRequestHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync(" Hello from MyRequestHandler ");
            await next.Invoke(context);
        }
    }
}
