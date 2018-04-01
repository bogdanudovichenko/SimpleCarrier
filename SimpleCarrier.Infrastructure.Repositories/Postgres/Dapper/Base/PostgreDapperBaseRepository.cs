﻿using Npgsql;
using System;
using System.Data;

namespace SimpleCarrier.Infrastructure.Repositories.Postgres.Dapper.Base
{
    public abstract class PostgreDapperBaseRepository
    {
        protected readonly string _connection;

        public PostgreDapperBaseRepository(string connection)
        {
            if (string.IsNullOrEmpty(connection)) throw new ArgumentNullException(nameof(connection));
            _connection = connection;
        }

        public IDbConnection OpenedConnection
        {
            get
            {
                IDbConnection db = new NpgsqlConnection(_connection);
                db.Open();
                return db;
            }
        }
    }
}
