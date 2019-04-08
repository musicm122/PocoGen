module Logger

open Models

type ILogInfo = string -> unit
type ILogDebug = string -> unit
type ILogError = string -> unit

let log logMessage =
    match logMessage with
    | Debug msg -> printfn "DEBUG %s" msg
    | Info msg -> printfn "INFO %s" msg
    | Error msg -> printfn "ERROR %s" msg
