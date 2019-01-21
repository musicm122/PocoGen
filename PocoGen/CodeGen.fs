module CodeGen

open System
open Elmish
open Elmish.WPF
open Elmish.WPF.Utilities
open PocoGen
open System.Windows
open System.Security.Policy
open PocoGen.Core.Models

type Model =
  { OutputLocation: FileOutputPath
    Databases: Database list
    Languages: Language list
    Tables: Table list
    SelectedDatabase: Database option
    SelectedLanguage: Language
    SelectedTables:Table list option }

let init () =
  { OutputLocation = @"c:\"
    Databases = []
    Languages = [Language.CSharp; Language.VbNet]
    Tables=[]
    SelectedDatabase = None
    SelectedLanguage = Language.CSharp
    SelectedTables = None }

type Msg =
  | SetSelectedDatabase of Database option
  | SetSelectedLanguage of Language
  | BrowseForOutputFolder of FileOutputPath
  | PopulateAvailableDatabases of Database list
  | PopulateAvailableTables of Table list
  | GenerateCode

let update msg m =
  match msg with
  | SetSelectedDatabase db -> { m with SelectedDatabase = db }
  | SetSelectedLanguage l -> { m with SelectedLanguage = l }
  | BrowseForOutputFolder f -> { m with OutputLocation= f }
  | PopulateAvailableDatabases dbs -> { m with Databases = dbs }
  | PopulateAvailableTables tbls -> {m with Tables = tbls }
  | GenerateCode -> m


let hasADatabaseSelected(m:Model) =
    match Option.isSome m.SelectedDatabase with
    | true -> Ok
    | false -> Error

let hasAtLeastOneTableSelected(m:Model) =
    let resolved =
        match m.SelectedTables with
        | Some -> Option.get m.SelectedTables
        | None -> []

    let isEmpty = resolved |> List.isEmpty

    match Some m.SelectedTables |> List.isEmpty with
    | [] -> Error
    | _ -> Ok

let hasRequiredFields m:Model =
    match model with
    | m when


let bindings model dispatch =
  [
    "OutputLocation"|>Binding.twoWay
        (fun m -> m.OutputLocation )
        (fun v m -> v |> BrowseForOutputFolder)
    "Databases" |> Binding.twoWay
        (fun m -> m.Databases )
        (fun v m -> v |> PopulateAvailableDatabases)
    "Tables" |> Binding.twoWay
        (fun m -> m.Tables )
        (fun v m -> v |> PopulateAvailableTables)
    "SelectedDatabase" |> Binding.twoWay
        (fun m -> m.SelectedDatabase )
        (fun v m -> v |> SetSelectedDatabase)
    "SelectedLanguage" |> Binding.twoWay
        (fun m -> m.SelectedLanguage)
        (fun v m -> v |> SetSelectedLanguage)
    //"GenerateCode" |> Binding.cmdIfValid

  ]