﻿using CodeSphere.Domain.Abstractions;
using CodeSphere.Domain.Abstractions.Repositores;
using CodeSphere.Domain.Premitives;
using CodeSphere.Infrastructure.Context;
using CodeSphere.Infrastructure.Implementation.Repositories;
using System.Collections;

namespace CodeSphere.Infrastructure.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        private Hashtable _repositories;
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            _repositories = new Hashtable();

        }
        public Task<int> CompleteAsync()
            => context.SaveChangesAsync();

        public ValueTask DisposeAsync()
            => context.DisposeAsync();

        // create repository per request  
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            // if repository<order> => key = order
            var key = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(key))
            {
                var repo = new GenericRepository<TEntity>(context);

                _repositories.Add(key, repo);
            }

            return _repositories[key] as IGenericRepository<TEntity>;
        }
    }
}
