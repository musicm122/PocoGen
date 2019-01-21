module PocoGen.Core

open System
open Elmish
open Elmish.WPF
open Elmish.WPF.Utilities
open PocoGen
open System.Windows
open System.Security.Policy
open App
open System.Windows

[<EntryPoint; STAThread>]
let main argv =
    let window = App.initWindow
    window.Loaded.Add windowLoaded
    Program.mkSimple Connection.init Connection.update Connection.bindings
    |> fun p -> { p with onError = elmishErrorHandler }
    |> Program.withConsoleTrace
    |> Program.runWindowWithConfig
        { ElmConfig.Default with LogConsole = true }
        (window)