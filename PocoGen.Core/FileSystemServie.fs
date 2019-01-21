module PocoGen.Core.Services.FileSystemService

open PocoGen.Core

open Models
open System.IO
open System

let IsValidPath (path:FileOutputPath) =
    try
        let dir = new DirectoryInfo(path)
        dir.Exists
    with
    | :? ArgumentException -> false
    | :? PathTooLongException ->false
    | :? NotSupportedException ->false
    | _ -> false

