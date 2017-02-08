using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace TheoPlayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        TabControl page = new TabControl();
        conf_ini conf;
        piesni piesni;
        void add_page()
        {
            this.Controls.Add(page);
            page.Size = new Size(ClientSize.Width,ClientSize.Height);
            page.TabPages.Add("Pieśni");
            page.TabPages.Add("Filmy");
            page.TabPages.Add("Opcje");
            page.TabPages.Add("Tworca");
            page.Appearance = TabAppearance.Normal;
            page.Click += page_TabIndexChanged;
            //page.TabPages["Pieśni"].Controls.Add()
        }

        private void page_TabIndexChanged(object sender, EventArgs e)
        {
            if (conf.change == true)
            {
                if (piesni != null)
                {
                    conf.change = false;
                    piesni.refresh(conf.sciezka[0], conf.sciezka[1],conf.sciezka[2]);
                }
                //piesni.panel=null;
               // MessageBox.Show("2");
                //piesni = new piesni(page, 0, conf.sciezka[0], conf.sciezka[1]);
            }
            //MessageBox.Show("asd");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            add_page();
            conf = new conf_ini(page,2);

            piesni = new piesni(page, 0, conf.sciezka[0],conf.sciezka[1], conf.sciezka[2]);


        }


        
    }
}
//  string x = "asd;sda";
//   string [] tab=new string[1];
//  tab=x.Split(';');
//  MessageBox.Show(tab[0]);