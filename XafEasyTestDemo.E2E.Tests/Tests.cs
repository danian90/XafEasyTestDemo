using DevExpress.EasyTest.Framework;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

// To run functional tests for ASP.NET Web Forms and ASP.NET Core Blazor XAF Applications,
// install browser drivers: https://www.selenium.dev/documentation/getting_started/installing_browser_drivers/.
//
// -For Google Chrome: download "chromedriver.exe" from https://chromedriver.chromium.org/downloads.
// -For Microsoft Edge: download "msedgedriver.exe" from https://developer.microsoft.com/en-us/microsoft-edge/tools/webdriver/.
//
// Selenium requires a path to the downloaded driver. Add a folder with the driver to the system's PATH variable.
//
// Refer to the following article for more information: https://docs.devexpress.com/eXpressAppFramework/403852/

namespace XafEasyTestDemo.Module.E2E.Tests;

public class XafEasyTestDemoTests : IDisposable {
    const string BlazorAppName = "XafEasyTestDemoBlazor";
    const string WinAppName = "XafEasyTestDemoWin";
    const string AppDBName = "XafEasyTestDemo";
    EasyTestFixtureContext FixtureContext { get; } = new EasyTestFixtureContext();

    public XafEasyTestDemoTests() {
        FixtureContext.RegisterApplications(
            new BlazorApplicationOptions(BlazorAppName, string.Format(@"{0}\..\..\..\..\XafEasyTestDemo.Blazor.Server", Environment.CurrentDirectory)),
            new WinApplicationOptions(WinAppName, string.Format(@"{0}\..\..\..\..\XafEasyTestDemo.Win\bin\EasyTest\net8.0-windows\XafEasyTestDemo.Win.exe", Environment.CurrentDirectory))
        );
        FixtureContext.RegisterDatabases(new DatabaseOptions(AppDBName, "XafEasyTestDemoEasyTest", server: @"(localdb)\mssqllocaldb"));
    }
    public void Dispose() {
        FixtureContext.CloseRunningApplications();
    }

    [Theory]
    [InlineData(BlazorAppName)]
    public void TestBlazorApp(string applicationName) {
        // Preparar la base de datos
        FixtureContext.DropDB("XafEasyTestDemoEasyTest");

        // Crear el contexto de aplicación
        var appContext = FixtureContext.CreateApplicationContext(applicationName);
        appContext.RunApplication();

        // Navegar a la vista "Student"
        appContext.Navigate("Student");

        // Crear un nuevo "Student"
        appContext.GetAction("New").Execute();
        appContext.GetForm().FillForm(("First Name", "John"), ("Last Name", "Smith"));
        appContext.GetAction("Save").Execute();

        // Verificar que los valores se han guardado correctamente
        appContext.Navigate("Student");
        var table = appContext.GetGrid();
        var rowCount = table.GetRowCount();
        Assert.Equal(1, rowCount);

        var row = table.GetRow(0, "First Name", "Last Name");
        Assert.Equal("John", row.GetValue(0));
        Assert.Equal("Smith", row.GetValue(1));
    }

    //[Theory]
    //[InlineData(WinAppName)]
    //public void TestWinApp(string applicationName) {
    //    FixtureContext.DropDB(AppDBName);
    //    var appContext = FixtureContext.CreateApplicationContext(applicationName);
    //    appContext.RunApplication();
    //}
}
