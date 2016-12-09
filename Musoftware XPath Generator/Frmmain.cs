using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Musoftware_XPath_Generator
{
    public partial class Frmmain : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);


        public static Browser browse = new Browser();


        public Frmmain()
        {
            InitializeComponent();
            Program.BrowserParentHandle = superTabControlPanel2.Handle;
            if (!browse.Initialize())
            {
                MessageBox.Show("Sorry Your Firefox is not updated");
                Environment.Exit(0);
                return;
            }
            css2XpathControl1.Browse = browse;
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Frmmain_Load(object sender, EventArgs e)
        {

        }

        private void superTabControl1_SelectedTabChanged(object sender, DevComponents.DotNetBar.SuperTabStripSelectedTabChangedEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void superTabControlPanel2_SizeChanged(object sender, EventArgs e)
        {
            Program.BrowserParentWidth = superTabControlPanel2.Width;
            Program.BrowserParentHeight = superTabControlPanel2.Height;
        }

        private void itemPanel1_ItemClick(object sender, EventArgs e)
        {

        }

        private void superTabItem2_Click(object sender, EventArgs e)
        {
            browse.RefreshSize();
        }

        private void Frmmain_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void superTabControlPanel2_Resize(object sender, EventArgs e)
        {
        }

        private void Frmmain_ResizeEnd(object sender, EventArgs e)
        {
            browse.RefreshSize();
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            browse.Navigate(textBoxItem1.Text);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
      


        }

        private void css2XpathControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
