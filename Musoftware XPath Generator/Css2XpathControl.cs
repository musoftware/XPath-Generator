using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Musoftware_XPath_Generator
{
    public partial class Css2XpathControl : UserControl 
    {
        public Browser Browse;
        public Css2XpathControl()
        {
            InitializeComponent();
        }

        private void Css2XpathControl_Load(object sender, EventArgs e)
        {

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            XPATHLib gene = new XPATHLib(Browse.driver);
            textBoxX3.Text = css2xpath.Converter.CSSToXPath(textBoxX2.Text, "//");
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            XPATHLib gene = new XPATHLib(Browse.driver);
            string shortx = gene.GetXPATHForChild(textBoxX4.Text, textBoxX1.Text);
            textBoxX6.Text = shortx;
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            XPATHLib gene = new XPATHLib(Browse.driver);
            string shortx = gene.GetShortXPATH(textBoxX3.Text, checkBoxX1.Checked);
            textBoxX1.Text = shortx;
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            XPATHLib gene = new XPATHLib(Browse.driver);
            textBoxX4.Text = css2xpath.Converter.CSSToXPath(textBoxX5.Text, "//");
        }
    }
}
