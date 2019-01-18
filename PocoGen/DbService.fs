module DbService

open Models

type DbTestConnection = ConnectionString -> bool
type DbGetTableNames = string -> string -> string[]
type DbGetDatabaseNames = string -> string -> string[]
