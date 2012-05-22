﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using tallyvu.Models;

namespace tallyvu.DataAccess
{
    public interface IRepository : IDisposable
    {
        void Add<TModel>(TModel instance) where TModel : class, IEntity;
        void Add<TModel>(IEnumerable<TModel> instances) where TModel : class, IEntity;

        IQueryable<TModel> All<TModel>(params string[] includePaths) where TModel : class, IEntity;

        void Delete<TModel>(string key) where TModel : class, IEntity;
        void Delete<TModel>(TModel instance) where TModel : class, IEntity;
        void Delete<TModel>(Expression<Func<TModel, bool>> predicate) where TModel : class, IEntity;

        TModel Single<TModel>(string key, params string[] includePaths) where TModel : class, IEntity;
        TModel Single<TModel>(Expression<Func<TModel, bool>> predicate, params string[] includePaths) where TModel : class, IEntity;

        IQueryable<TModel> Query<TModel>(Expression<Func<TModel, bool>> predicate, params string[] includePaths) where TModel : class, IEntity;
    }
}