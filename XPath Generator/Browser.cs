using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

public class Browser
{
    public ChromeDriver driver;

    [DllImport("User32")]
    private static extern int ShowWindow(int hwnd, int nCmdShow);

    public bool isInitialize { get; set; }




    public bool Click(string element_name)
    {
        try
        {
            var elementsByName = driver.FindElementsByName(element_name);
            if (elementsByName.Count <= 0)
                return false;
            elementsByName[0].Click();
            Thread.Sleep(1000);
            return true;
        }
        catch
        {
            return false;
        }
    }


    public void RunScript(string script)
    {
        driver.ExecuteScript(script);
    }

    public void Navigate(string url)
    {
        driver.Url = url;
    }


    public bool Initialize()
    {
        try
        {
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;

            var directory = System.IO.Path.GetDirectoryName(path);


            IEnumerable<int> pidsBefore = Enumerable.Select<Process, int>((IEnumerable<Process>)Process.GetProcessesByName("chrome"), (Func<Process, int>)(p => p.Id));
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService(directory + "\\App");
            chromeDriverService.HideCommandPromptWindow = false;


            ChromeOptions chromeDriverOptions = new ChromeOptions();
            chromeDriverOptions.AddArguments("--disable-notifications");

           
            chromeDriverOptions.AddExtension(directory + "\\App\\block.crx");

            driver = new ChromeDriver(chromeDriverService, chromeDriverOptions);

            IEnumerable<int> pidsAfter = Enumerable.Select<Process, int>((IEnumerable<Process>)Process.GetProcessesByName("chrome"), (Func<Process, int>)(p => p.Id));
            var newChromePids = Enumerable.Except<int>(pidsAfter, pidsBefore).ToList();
            //if (newChromePids != null)
            //    foreach (int processId in newChromePids)
            //        ShowWindow(Process.GetProcessById(processId).MainWindowHandle.ToInt32(), 0);

            return isInitialize = true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public void Finalize()
    {
        if (isInitialize)
        {
            driver.Quit();
        }
    }
}
