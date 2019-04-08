module PocoGen.Core.DbService

open PocoGen.Core.Models

type DbTestConnection = ConnectionString -> bool
type DbGetTableNames = string -> string -> string[]
type DbGetDatabaseNames = string -> string -> string[]
