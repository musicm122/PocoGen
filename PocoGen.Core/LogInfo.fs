module PocoGen.Core.Logger

open PocoGen.Core.Models

type ILogInfo = string -> unit
type ILogDebug = string -> unit
type ILogFailure = string -> unit

let log logMessage =
    match logMessage with
    | Debug msg -> printfn "DEBUG %s" msg
    | Info msg -> printfn "INFO %s" msg
    | Failure msg -> printfn "ERROR %s" msg
