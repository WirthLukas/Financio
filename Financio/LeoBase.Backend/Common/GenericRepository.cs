using LeoBase.Backend.Contracts.Entities;
using LeoBase.Backend.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LeoBase.Backend.Common;

/// <summary>
/// Generische Zugriffsmethoden für eine Entität
/// Werden spezielle Zugriffsmethoden benötigt, wird eine spezielle
/// abgeleitete Klasse erstellt.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class GenericRepository<TEntity, TId, TVersion> : IGenericRepository<TEntity, TId, TVersion>
    where TEntity : class, IIdentifiable<TId>, IVersionable<TVersion>, new()
{
    protected DbSet<TEntity> DbSet { get; } // Set der entsprechenden Entität im Context
    protected DbContext Context { get; }

    public GenericRepository(DbContext dbContext, DbSet<TEntity>? dbSet = null)
    {
        Context = dbContext;
        DbSet = dbSet ?? dbContext.Set<TEntity>();
    }

    /// <inheritdoc />
    public async Task<TEntity[]> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params string[] includeProperties)
    {
        IQueryable<TEntity> query = DbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        // alle gewünschten abhängigen Entitäten mitladen
        foreach (string includeProperty in includeProperties)
        {
            query = query.Include(includeProperty.Trim());
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToArrayAsync();
        }

        return await query.ToArrayAsync().ConfigureAwait(false);
    }

    /// <inheritdoc />
    public ValueTask<TEntity?> GetByIdAsync(TId id) => DbSet.FindAsync(id);

    /// <inheritdoc />
    public Task<TEntity?> GetByIdAsync(TId id, params string[] includeProperties)
    {
        IQueryable<TEntity> query = DbSet;

        foreach (var property in includeProperties)
        {
            query = query.Include(property.Trim());
        }

        return query.SingleOrDefaultAsync(e => EqualityComparer<TId>.Default.Equals(e.Id, id));
    }

    public Task<bool> ExistsAsync(TId id) => DbSet.AnyAsync(e => EqualityComparer<TId>.Default.Equals(e.Id, id));

    /// <inheritdoc />
    public async Task AddAsync(TEntity entity) => await DbSet.AddAsync(entity);

    /// <inheritdoc />
    public Task AddRangeAsync(IEnumerable<TEntity> entities) => DbSet.AddRangeAsync(entities);

    /// <inheritdoc />
    public bool Remove(TId id)
    {
        TEntity? entityToDelete = DbSet.Find(id);

        if (entityToDelete != null)
        {
            Remove(entityToDelete);
            return true;
        }

        return false;
    }

    /// <inheritdoc />
    public void Remove(TEntity entityToRemove)
    {
        if (Context.Entry(entityToRemove).State == EntityState.Detached)
        {
            DbSet.Attach(entityToRemove);
        }
        DbSet.Remove(entityToRemove);
    }

    ///// <summary>
    /////     Entität aktualisieren
    ///// </summary>
    ///// <param name="entityToUpdate"></param>
    //public void Update(TEntity entityToUpdate)
    //{
    //    //Prüfen ob Entität bereits im aktuellen Context vorhanden (falls ja, muss vorher Detach auf Entität durchgeführt werden,
    //    //um Attach ausführen zu können)
    //    TEntity localEntity = DbSet.Local.FirstOrDefault(x => x.Id == entityToUpdate.Id);
    //    if (localEntity != null)
    //    {
    //        if (Context.Entry(entityToUpdate).State == EntityState.Added)
    //        {
    //            throw new DbUpdateException("Update performed on inserted but not commited dataset", default(Exception));
    //        }
    //        Context.Entry(localEntity).State = EntityState.Added;
    //        DbSet.Local.Remove(localEntity);
    //    }
    //    DbSet.Attach(entityToUpdate);
    //    Context.Entry(entityToUpdate).State = EntityState.Modified;
    //    //Context.Update(entityToUpdate);
    //}

    public async Task Modify(TId id, TEntity incoming)
    {
        TEntity? entity = await DbSet.FindAsync(id);

        if (entity is not null)
        {
            Context.Entry(entity).CurrentValues.SetValues(incoming);
            Context.Entry(entity).Property(e => e.RowVersion).OriginalValue = incoming.RowVersion;
        }       
    }

    /// <inheritdoc />
    public Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        IQueryable<TEntity> query = DbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return query.CountAsync();
    }
}
