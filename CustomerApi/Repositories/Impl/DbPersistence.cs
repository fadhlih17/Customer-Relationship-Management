namespace CustomerApi.Repositories.Impl;

public class DbPersistence : IPersistence
{
    private AppDbContext _context;

    public DbPersistence(AppDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}