namespace Infrastructure.Services.Interfaces;

public interface IDbContextWrapper<T>
     where T : Microsoft.EntityFrameworkCore.DbContext
{
     T DbContext { get; }
     Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
}