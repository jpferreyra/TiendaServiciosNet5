﻿using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Libro.XUnitTestProject
{
    public class AsyncQueryProvider<T> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _queryProvider;

        public AsyncQueryProvider(IQueryProvider queryProvider)
        {
            _queryProvider = queryProvider;
        }
        public IQueryable CreateQuery(Expression expression)
        {
            return new AsyncEnumerable<T>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new AsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _queryProvider.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _queryProvider.Execute<TResult>(expression);
        }

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
        {
            var resultType = typeof(TResult).GetGenericArguments()[0];
            var resultExecute = typeof(IQueryProvider)
                            .GetMethod(name: nameof(IQueryProvider.Execute),
                            genericParameterCount: 1,
                            types: new[] { typeof(Expression) }
                            )
                            .MakeGenericMethod(resultType)
                            .Invoke(this, new[] { expression });


            return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))?
                .MakeGenericMethod(resultType).Invoke(null, new[] { resultExecute });
        }
    }
}
