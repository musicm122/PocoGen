module PocoGen.Core.DataAccess

open FSharp.Core
open System
open System.Data.SqlClient
open Dapper
open PocoGen.Core.Models

type query =
    { dbName : string }

let testConnectionAsync (connString : ConnectionString) =
    async {
        try
            use connection = new SqlConnection(connString)
            do! connection.OpenAsync() |> Async.AwaitTask
            connection.Close()
            return Ok "Sql connection test successful"
        with :? SqlException as ex ->
            return Error (String.Format("Sql connection errror occured : {0} : {1}", ex.Message, ex.InnerException.Message))
    }

let testConnection (connString : ConnectionString) =
    try
        use connection = new SqlConnection(connString)
        connection.Open()
        connection.Close()
        Ok "Sql connection test successful"
    with
        | :? SqlException as ex -> String.Format("Sql connection error occured : {0} : {1}", ex.Message, ex.InnerException.Message) |> Error
        | :? System.InvalidOperationException -> String.Format("Sql connection error occured : Cannot open a connection without specifying a data source or server or the connection is already open.") |> Error
        | :? System.ArgumentException as ae -> "Sql connection error occured : Sql connection string in invalid format."|> Error

let getTableNamesFromMSSqlServerQueryAsync (database : Database)
    (conString : ConnectionString) =
    async {
        use connection = new SqlConnection(conString)
        do! connection.OpenAsync() |> Async.AwaitTask
        let sql = @"SELECT [t0].[TABLE_NAME]
        FROM [INFORMATION_SCHEMA].[TABLES] AS [t0]
        WHERE [t0].[TABLE_CATALOG] = @dbName"
        let! result = connection.QueryAsync<string>
                          (sql, { dbName = database.Name }) |> Async.AwaitTask
        return result |> Array.ofSeq
    }

/// **Description**
///
/// **Parameters**
///   * `database` - parameter of type `string`
///   * `conString` - parameter of type `ConnectionString`
///
/// **Output Type**
///   * `string []`
///
/// **Exceptions**
///
let getTableNamesFromMSSqlServerQuery (database : Database)
    (conString : ConnectionString) =
    use connection = new SqlConnection(conString)
    do connection.Open()
    let sql = @"SELECT [t0].[TABLE_NAME]
        FROM [INFORMATION_SCHEMA].[TABLES] AS [t0]
        WHERE [t0].[TABLE_CATALOG] = @dbName"
    let result = connection.Query<string>(sql, { dbName = database.Name })
    result |> Array.ofSeq

let getDatabaseNames (connString : ConnectionString) =
    use connection = new SqlConnection(connString)
    do connection.Open()
    let sql = @"select name from sys.databases"
    let result = connection.Query<string>(sql)
    connection.Close()
    result |> Array.ofSeq

let getDatabaseNamesAsync (connString : ConnectionString) =
    async {
        use connection = new SqlConnection(connString)
        do! connection.OpenAsync() |> Async.AwaitTask
        let sql = @"select name from sys.databases"
        let! result = connection.QueryAsync<string>(sql) |> Async.AwaitTask
        connection.Close()
        return result |> Array.ofSeq
    }
