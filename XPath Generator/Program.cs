using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XPath_Generatpr
{
    class Program
    {
        static void Main(string[] args)
        {
            Browser browser = new Browser();

            browser.Initialize();

            browser.Navigate("https://www.yellowpages.com.eg/en/search/data");

            XPATHLib gene = new XPATHLib(browser.driver);

            string container = gene.GetXPATHForLookLike("/html/body/div[1]/div/div[2]/div[1]/div[3]");
            Console.WriteLine(container);

            string child = gene.GetXPATHForChild("/html/body/div[1]/div/div[2]/div[1]/div[3]/div[1]/div[2]/div[1]/div[1]/div[1]/div/h4/a", container);

            Console.WriteLine(child);

            browser.Finalize();

            Console.ReadKey();


        }

    
    }
}
