module Page

open System
open Elmish
open Elmish.WPF
open System.Windows
open System.Windows.Controls
open System.Windows.Threading

type MainWindow = FsXaml.XAML<"MainWindow.xaml">
type AboutWindow = FsXaml.XAML<"AboutControl.xaml">
type CodeGenWindow = FsXaml.XAML<"CodeGenControl.xaml">
type ConnectionWindow = FsXaml.XAML<"ConnectionControl.xaml">

let initMainWindow =
    let taskManagerName = "TabManager"
    let mainStackPanelName = "MainStackPanel"
    let mainWindow = MainWindow()

    let getMainContentStackPanel =
        mainWindow.FindName mainStackPanelName :?> StackPanel

    let getTaskManager =
        let tabControl = new TabControl()
        tabControl.Name<-taskManagerName
        tabControl

    let addContentToStackPanel (stackPanel:StackPanel) (element:UIElement)  =
        stackPanel.Children.Add(element) |> ignore

    let addUserControlToTabManager (tabCtrl:TabControl) (name:string) (usrCtrl:UserControl) =
        let tabItem = new TabItem()
        tabItem.Header <- name
        tabItem.Content<-usrCtrl
        tabCtrl.Items.Add(tabItem) |> ignore

    let addTabManagerToMainWindow(window:Window)(tabCtrl:TabControl) =
        let mainStackPanel = getMainContentStackPanel
        mainStackPanel.Children.Add(tabCtrl)|> ignore

    let getLoadedTabManager =
        let tabManager = getTaskManager
        let about= AboutWindow()
        let codeGen = CodeGenWindow()
        let connection = ConnectionWindow()
        addUserControlToTabManager tabManager "Code Generation" codeGen
        addUserControlToTabManager tabManager "Db Connection" connection
        addUserControlToTabManager tabManager "About" about
        tabManager

    let mainStackPanel = getMainContentStackPanel
    let tabManager = getLoadedTabManager
    addContentToStackPanel mainStackPanel tabManager
    mainWindow
