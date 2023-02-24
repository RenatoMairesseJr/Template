using Domain.DbModels;
using Domain.DbModels.Common;
using Infrastructure.Repository.DatabaseConfig;
using Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;


namespace Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository <T> where T : EntityBase
    {
        public GenericRepository(DatabaseContext dbContex)
        {
            _dbContext = dbContex;
            //_userName = userService.GetUser();
        }

        protected readonly DatabaseContext _dbContext;
        //private readonly string _userName;


        public async Task<List<T>> GetAll() =>  await _dbContext.Set<T>().AsNoTracking().ToListAsync();

        public async Task<T> GetById(int id) => await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);


        public async Task<T> Create(T entity, bool save = true, bool log = false)
        {
            var local = _dbContext.Set<T>().Local.FirstOrDefault();

            if (local != null)
            {
                _dbContext.Entry(local).State = EntityState.Detached;
            }

            await _dbContext.AddAsync(entity);

            if (save) 
                await Save(log);

            return entity;
        }

        public async Task<List<T>> CreateRange(List<T> entity, bool save = true, bool log = false)
        {
            await _dbContext.AddRangeAsync(entity);
            if (save) 
                await Save(log);

            return entity;
        }

        public async Task<T> Update(T entity, bool save = true, bool log = false) 
        {
            var local = _dbContext.Set<T>().Local.FirstOrDefault();

            if (local != null)
            {
                _dbContext.Entry(local).State = EntityState.Detached;
            }

            _dbContext.Entry(entity).State = EntityState.Modified;
            if (save) 
                await Save(log);

            return entity;
        }

        public async Task<List<T>> UpdateRange(List<T> entity, bool save = true, bool log = false)
        {
            _dbContext.UpdateRange(entity);
            if (save) 
                await Save(log);

            return entity;
        }

        public async Task<T> Remove(T entity, bool save = true, bool log = false)
        {
            _dbContext.Remove(entity);
            if (save) 
                await Save(log);

            return entity;
        }

        public async Task<List<T>> RemoveRange(List<T> entity, bool save = true, bool log = false)
        {
            _dbContext.Set<T>().RemoveRange(entity);
            if (save) 
                await Save(log);

            return entity;
        }

        private void AddChangesToEntities(List<EntityEntry> changedEntities, EntityState type)
        {
            var auditEntries = new List<AuditingHistory>();

            foreach (var entity in changedEntities)
            {
                var id = entity.Properties.FirstOrDefault(a => a.Metadata.IsPrimaryKey()).CurrentValue.ToString();
                var auditingHistory = new AuditingHistory
                {
                    AuditingHistoryGuid = Guid.NewGuid(),
                    Referrer = string.Empty,
                    Date = DateTime.Now,
                    UserName = string.Empty,
                    ChangeType = type.ToString(),
                    RecordPK = id ?? string.Empty,
                };

                foreach (var property in entity.Properties.Where(a => a.Metadata.IsPrimaryKey() == false))
                {
                    var to = property.CurrentValue?.ToString();
                    var from = "";

                    if (type == EntityState.Modified)
                        from = entity.GetDatabaseValues().GetValue<object>(property.Metadata.Name)?.ToString() ?? "";

                    if (to != from || type == EntityState.Added)
                    {
                        auditingHistory.Changes.Add(new AuditingHistoryChange
                        {
                            From = type == EntityState.Added ? "" : from,
                            To = to ?? string.Empty,
                            PropertyName = property.Metadata.Name,
                            TableName = entity.Entity.GetType().Name,
                        });
                    }
                }

                if (auditingHistory.Changes.Any())
                {
                    auditEntries.Add(auditingHistory);
                }
            }

            _dbContext.Set<AuditingHistory>().UpdateRange(auditEntries);
        }

        public async Task Save(bool log)
        {
            //Log changes to Audit tables
            if (log)
            {
                //identify changes by type
                var added = _dbContext.ChangeTracker.Entries().Where(e => e.State == EntityState.Added).ToList();
                var changed = _dbContext.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified).ToList();

                //Create Audit tables entries before save. so, we won't lose original value
                AddChangesToEntities(changed, EntityState.Modified);

                await Save();
                //Create Audit tables entries after save to get the PK value
                AddChangesToEntities(added, EntityState.Added);
            }
            
            await Save();            
        }

        private async Task Save()
        {
            foreach (var entity in _dbContext.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).Select(e => e.Entity))
            {
                Validator.ValidateObject(entity, new ValidationContext(entity));
            }

            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}

