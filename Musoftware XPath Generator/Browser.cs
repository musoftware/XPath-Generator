using Musoftware_XPath_Generator;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
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
using System.Windows.Forms;

public class Browser
{
    public FirefoxDriver driver;
    private List<int> newChromePids;
    private List<IntPtr> newWindowPids = new List<IntPtr>();

    [DllImport("user32.dll")]
    static extern IntPtr SetParent(int hWndChild, int hWndNewParent);
    [DllImport("User32")]
    public static extern int ShowWindow(int hwnd, int nCmdShow);
    [DllImport("user32")]
    public static extern int RemoveMenu(IntPtr systemMenu, int itemPosition, int flag);
    [DllImport("user32")]
    public static extern IntPtr GetSystemMenu(IntPtr systemMenu, int revert);
    [DllImport("user32")]
    public static extern int GetMenuItemCount(IntPtr systemMenu);
    [DllImport("user32")]
    public static extern int DrawMenuBar(IntPtr currentWindow);

    public const int MF_BYCOMMAND = 0;
    public const int MF_DISABLED = 2;
    public const int SC_CLOSE = 0xF060;

   


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

            IEnumerable<int> pidsBefore = Enumerable.Select<Process, int>((IEnumerable<Process>)Process.GetProcessesByName("firefox"), (Func<Process, int>)(p => p.Id));
            FirefoxDriverService chromeDriverService = FirefoxDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;

            driver = new FirefoxDriver(chromeDriverService);

            IEnumerable<int> pidsAfter = Enumerable.Select<Process, int>((IEnumerable<Process>)Process.GetProcessesByName("firefox"), (Func<Process, int>)(p => p.Id));
            newChromePids = Enumerable.Except<int>(pidsAfter, pidsBefore).ToList();
            if (newChromePids != null)
                foreach (int processId in newChromePids)
                {
                    var handleInt = Process.GetProcessById(processId).MainWindowHandle;
                    if (handleInt.ToInt32() != 0)
                    {
                        ShowWindow(handleInt.ToInt32(), 3);
                        newWindowPids.Add(handleInt);
                      
                            SetParent(handleInt.ToInt32(), Program.BrowserParentHandle.ToInt32());
                            WindowsWorks.WindowsClass.MakeExternalWindowBorderless(handleInt, Program.BrowserParentWidth, Program.BrowserParentHeight);
                       
                    }
                }
            return isInitialize = true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public void Finalize()
    {
        if (driver != null)
            driver.Quit();
    }

    internal void RefreshSize()
    {
        if (newChromePids == null) return;

        foreach (var processId in newChromePids)
        {
            var localHandle = (Process.GetProcessById(processId).MainWindowHandle);
            ShowWindow(localHandle.ToInt32(), 3);
        }

        foreach (var localHandle in newWindowPids)
        {
            ShowWindow(localHandle.ToInt32(), 1);
            ShowWindow(localHandle.ToInt32(), 3);

            WindowsWorks.WindowsClass.MakeExternalWindowBorderless(localHandle, Program.BrowserParentWidth, Program.BrowserParentHeight);

        }
    }
}
