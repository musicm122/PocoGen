module About

open System
open Elmish
open Elmish.WPF
open Elmish.WPF.Utilities
open PocoGen
open System.Windows
open System.Security.Policy

    //open Types
    type Msg =
        | Increment
        | Decrement
        | CauseCrash

    type Model = { Count: int }

    let causeCrash() = 13 / 0 |> ignore

    let init() = { Count = 0 }

    let update (msg: Msg) (model: Model) =
        match msg with
        | Increment -> { model with Count = model.Count + 1 }
        | Decrement -> { model with Count = model.Count - 1 }
        | CauseCrash ->
            // causeCrash() // Uncomment this call if you want an exception here.
            model

    let view _ _ =
        [ "Increment" |> Binding.cmd (fun _ m -> Increment)
          "Decrement" |> Binding.cmd (fun _ m -> Decrement)
          "CauseCrash" |> Binding.cmd (fun _ m ->
            // causeCrash() // Uncomment this call if you want an exception here.
            CauseCrash)
          "Count" |> Binding.oneWay (fun m -> m.Count) ]