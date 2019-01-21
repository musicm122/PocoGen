module Connection

open System
open Elmish
open Elmish.WPF
open Elmish.WPF.Utilities
open PocoGen
open System.Windows
open System.Security.Policy
open PocoGen.Core
open PocoGen.Core.Models

type Model =
  { ConnectionString: ConnectionString
    Output: string }

let init () =
  { ConnectionString = ""
    Output = ""}

type Msg =
  | SetConnectionString of ConnectionString
  | TestConnection of ConnectionString
  | SetOutput of string

let requiredNotEmpty s =
    if String.IsNullOrWhiteSpace s then Result.Error "Connection String is Required" else Result.Ok s

let runConnectionTest(model:Model):Model =
    let connectionTestResult = DataAccess.testConnection model.ConnectionString
    let testResult =
        match connectionTestResult with
        | Ok msg -> { model with Output = model.Output + "Test succesful" }
        | Error e -> { model with Output = model.Output + e }
    testResult

let update (msg:Msg) (m:Model) =
  match msg with
  | SetConnectionString conStringVal ->
        { m with ConnectionString = conStringVal }
  | SetOutput output -> {m with Output = output}
  | TestConnection con ->
        let testResult = runConnectionTest m
        let out = { m with Output = m.Output + String.Format("{0}\r\n{1}", m.Output, testResult) }
        out

let bindings model dispatch =
  [
    "ConnectionString" |> Binding.twoWay
        (fun m -> m.ConnectionString )
        (fun v m -> v |> SetConnectionString)
    "Output" |> Binding.twoWay
        (fun m -> m.Output )
        (fun v m -> v |> SetConnectionString)
    "TestConnection" |> Binding.cmdIfValid
        (fun m -> requiredNotEmpty m.ConnectionString |> Result.map TestConnection)
  ]