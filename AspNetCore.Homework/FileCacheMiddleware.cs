using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace AspNetCore.Homework
{
    public class FileCacheMiddleware
    {
        private readonly RequestDelegate next;

        private FileLRUCache cache;

        public FileCacheMiddleware(RequestDelegate next, string storagePath, int capacity)
        {
            this.next = next;
            this.cache = new FileLRUCache(capacity, storagePath);
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path;
            if (path.Value.Contains("GetCategoryImageById/")|| path.Value.Contains("images/"))
            {
                var id = int.Parse(path.Value.Split('/').Last());
                var image = cache.Get(id);

                if (image != null)
                    await context.Response.Body.WriteAsync(image);
                else
                {
                    using (var buffer = new MemoryStream())
                    {
                        //replace the context response with our buffer
                        var stream = context.Response.Body;
                        context.Response.Body = buffer;

                        await next.Invoke(context);

                        //reset to start of stream
                        buffer.Seek(0, SeekOrigin.Begin);

                        if (context.Response.ContentType.Contains("image"))
                        {
                            cache.Add(id, buffer.ToArray());
                        }

                        //copy our content to the original stream and put it back
                        await buffer.CopyToAsync(stream);
                        context.Response.Body = stream;
                    }
                }
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
}
