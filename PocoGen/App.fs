[<AutoOpen>]
module App

open System
open Elmish
open Elmish.WPF
open System.Windows
open System.Windows.Threading
open System.Windows.Controls

let elmishErrorHandler (messageToShow: string, ex: exn) =
    Windows.MessageBox.Show(ex.Message) |> ignore
    ()

let dispatcherUnhandledException (e: DispatcherUnhandledExceptionEventArgs) =
    e.Handled <- true
    Windows.MessageBox.Show(e.Exception.Message) |> ignore
    ()

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
