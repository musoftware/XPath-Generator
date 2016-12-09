using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public class XPATHLib
    {
        private FirefoxDriver driver;

        public XPATHLib(FirefoxDriver driver)
        {
            this.driver = driver;
        }


        public string GetXPATHForChild(string fullXPATH, string XPATHOfParent)
        {
            // //div[@class="col-xs-9"][1]/div[@class="content-widget"][1]/.//a[@class="companyName"]

            string shortXPATH = GetShortXPATH(fullXPATH);

            if (!shortXPATH.StartsWith("/"))
                shortXPATH = "/" + shortXPATH;


            if (!string.IsNullOrEmpty(shortXPATH) && shortXPATH.StartsWith("("))
            {
                shortXPATH = shortXPATH.Substring(2);
                shortXPATH = shortXPATH.Remove(shortXPATH.LastIndexOf(")"), 1);
            }


            if (!string.IsNullOrEmpty(XPATHOfParent))
            {
                XPATHOfParent = XPATHOfParent.Substring(2);
                XPATHOfParent = XPATHOfParent.Remove(XPATHOfParent.LastIndexOf(")"), 1);
            }
            if (!XPATHOfParent.StartsWith("//") && XPATHOfParent.StartsWith("/"))
            {
                XPATHOfParent = "/" + XPATHOfParent;
            }

            return XPATHOfParent + "/." + System.Text.RegularExpressions.Regex.Replace(shortXPATH, @"\[\d\]", "");
        }


        public string GetXPATHForLookLike(string fullXPATH)
        {
            var XPATH = GetShortXPATH(fullXPATH);

            var element = driver.FindElementByXPath(XPATH);

            string editedXPATH = XPATH;
            if (XPATH.EndsWith("]"))
            {
                editedXPATH = XPATH.Substring(1, XPATH.LastIndexOf("[") - 2);
            }

            var parent = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript(
                                    "return arguments[0].parentNode;", element);

            string parentXPATH = getXPATHFromElement(parent, null, false);

            if (!string.IsNullOrEmpty(parentXPATH))
            {
                parentXPATH = parentXPATH.Substring(3);
                parentXPATH = parentXPATH.Remove(parentXPATH.LastIndexOf(")"), 1);
            }

            editedXPATH = editedXPATH.Insert(2, parentXPATH + "/");

            return editedXPATH;
        }

        public string GetShortXPATH(string fullXPATH, bool skipId = false)
        {
            var elements = driver.FindElementsByXPath(fullXPATH);

            if (elements.Count == 0)
                throw new NoElementFoundException();

            if (elements.Count > 1)
                throw new MoreElementsFoundException();

            var element = elements[0];


            return getXPATHFromElement(element, fullXPATH, skipId);
        }

        public string getXPATHFromElement(IWebElement element, string fullXPATH = null, bool skipId = false)
        {

            var javascriptDriver = (IJavaScriptExecutor)driver;

            Dictionary<string, object> attributes = javascriptDriver.ExecuteScript("var items = {}; for (index = 0; index < arguments[0].attributes.length; ++index) { items[arguments[0].attributes[index].name] = arguments[0].attributes[index].value }; return items;", element) as Dictionary<string, object>;

            foreach (var attribute in attributes)
            {
                if (attribute.Key == "href") continue;
                if (skipId && attribute.Key == "id") continue;

                string XPATH = "//" + element.TagName + "[@" + attribute.Key + "=\"" + attribute.Value + "\"]";

                var likedElements = driver.FindElementsByXPath(XPATH);

                if (likedElements.Count == 1)
                {
                    return XPATH;
                }
                else if (likedElements.Count > 1)
                {
                    int index = 1;
                    foreach (var elementIn in likedElements)
                    {
                        if (elementIn.Location == element.Location && elementIn.Size == element.Size)
                        {
                            return "(" + XPATH + ")" + "[" + index + "]";
                        }
                        index++;
                    }
                }
            }

            if (attributes.Count == 0)
            {
                IWebElement parent = element.FindElement(By.XPath(".."));

                string parentXPaath = getXPATHFromElement(parent);

                if (!string.IsNullOrEmpty(parentXPaath) && parentXPaath.StartsWith("("))
                {
                    //parentXPaath = parentXPaath.Substring(1);
                    //parentXPaath = parentXPaath.Remove(parentXPaath.LastIndexOf(")"), 1);
                }
                parentXPaath = parentXPaath + "/" + element.TagName;
                if (element.FindElements(By.XPath(parentXPaath)).Count == 1)
                {
                    return parentXPaath;
                }
            }





            return fullXPATH ?? element.TagName;
        }




    }
