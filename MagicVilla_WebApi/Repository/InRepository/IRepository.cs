﻿using MagicVilla_WebApi.Models;
using System.Linq.Expressions;

namespace MagicVilla_WebApi.Repository.InRepository
{
    public interface IRepository<T> where T : class

    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);

        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true);
        Task CreateAsync(T entity);


        Task RemoveAsync(T entity);

        Task SaveAsync();
    }
}

