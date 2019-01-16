module DataAccess

open System
open System.Data.SqlClient
open Dapper

type query = {
    dbName:string;
}

let testConnection connString = async {
    try
        use connection = new SqlConnection(connString)
        do! connection.OpenAsync() |> Async.AwaitTask
        connection.Close()
        return (true, "Sql connection test successful")
    with
        | :? SqlException as ex ->
            return (false, "Sql connection errror occured :" + ex.Message + " : " + ex.InnerException.Message)
    }

let getTableNamesFromMSSqlServerQuery(database:string, conString:string)= async {
    use connection = new SqlConnection(conString)
    do! connection.OpenAsync() |> Async.AwaitTask
    let sql = @"SELECT [t0].[TABLE_NAME]
        FROM [INFORMATION_SCHEMA].[TABLES] AS [t0]
        WHERE [t0].[TABLE_CATALOG] = @dbName"
    let! result =
        connection.QueryAsync<string>(sql, {dbName = database;})
        |> Async.AwaitTask
    return result
        |> Array.ofSeq
}

let getDatabaseNames(schema, connString) = async {
    use connection = new SqlConnection(connString)
    do! connection.OpenAsync() |> Async.AwaitTask
    let sql = @"select name from sys.databases"
    let! result =
        connection.QueryAsync<string>(sql)
        |> Async.AwaitTask
    connection.Close()
    return result
        |> Array.ofSeq
}

