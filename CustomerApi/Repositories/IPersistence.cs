namespace CustomerApi.Repositories;

public interface IPersistence
{
    Task SaveChangesAsync();
}