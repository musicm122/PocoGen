module CompositionRoot

open System
open Models


let logError=
    Logger.log LogMessage.Error


type DbTestConnection = ConnectionString -> bool
type DbGetTableNames = string -> string -> string[]
type DbGetDatabaseNames = string -> string -> string[]

