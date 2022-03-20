namespace WebStore.Infrastucture.Middleware
{
    public class TestMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<TestMiddleware> logger;

        public TestMiddleware(RequestDelegate next, ILogger<TestMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            await next(context);
        }
    }
}
