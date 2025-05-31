
public class DatabaseInitializerMiddleware
{
    private readonly RequestDelegate _next;

    public DatabaseInitializerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, AppDbContext dbContext)
    {
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();

        SeedData.Initialize(dbContext);

        await _next(context); // الانتقال للـ Middleware التالي
    }
}


