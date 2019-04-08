[<AutoOpen>]
module App

open System
open Elmish
open Elmish.WPF
open System.Windows
open System.Windows.Threading
open System.Windows.Controls

type Model =
    { CodeGenModel: CodeGen.Model
      ConnectionModel: Connection.Model }

let init() =
    { CodeGenModel = CodeGen.init()
      ConnectionModel = Connection.init() }

type Msg =
    | LoadCodeGen
    | LoadConnection
    | CodeGenMsg of CodeGen.Msg
    | ConnectionMsg of Connection.Msg

let LoadCodeGenWindow() =
    Application.Current.Dispatcher.Invoke(fun () ->
      let win = Page.CodeGenWindow()
      win.DataContext <- Application.Current.MainWindow.DataContext
    )

let LoadConnectionWindow() =
    Application.Current.Dispatcher.Invoke(fun () ->
      let win = Page.ConnectionWindow()
      win.DataContext <- Application.Current.MainWindow.DataContext
    )

let elmishErrorHandler (messageToShow: string, ex: exn) =
    Windows.MessageBox.Show(ex.Message) |> ignore
    ()

let dispatcherUnhandledException (e: DispatcherUnhandledExceptionEventArgs) =
    e.Handled <- true
    Windows.MessageBox.Show(e.Exception.Message) |> ignore
    ()


let update msg m =
    match msg with
    | LoadCodeGen -> m, Cmd.attemptFunc LoadCodeGenWindow () raise
    | LoadConnection -> m, Cmd.attemptFunc LoadCodeGenWindow () raise
    | CodeGenMsg msg' ->{ m with CodeGenModel = CodeGen.update msg' m.CodeGenModel  }, Cmd.none
    | ConnectionMsg msg' ->{ m with ConnectionModel = Connection.update msg' m.ConnectionModel }, Cmd.none


  //let bindings model dispatch =
  //  [
  //    "LoadCodeGen" |> Binding.cmd (fun m -> LoadCodeGenWindow)
  //    "LoadConnection" |> Binding.cmd (fun m -> LoadConnectionWindow)
  //    "CodeGenModel" |> Binding.subModel
  //      (fun m -> m.CodeGenModel )
  //      CodeGen.bindings
  //      Win1Msg
  //    "Win2" |> Binding.subModel
  //      (fun m -> m.Win2)
  //      Win2.bindings
  //      Win2Msg
  //  ]
//let loadResources=
//    [
//        new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml");
//        new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml");
//        new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.DeepPurple.xaml");
//        new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Lime.xaml");
//    ]
//    |> List.iter (fun uri ->
//    let dict= new ResourceDictionary()
//    dict.Source <- uri
//    Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()))

let initWindow =
    Page.initMainWindow

let windowLoaded _ =
    //loadResources
    Windows.Application.Current.DispatcherUnhandledException.Add dispatcherUnhandledException
    Windows.Application.Current.ShutdownMode <- ShutdownMode.OnMainWindowClose
    ()

    //https://github.com/elmish/Elmish.WPF/blob/master/src/Samples/NewWindow/Program.fs
