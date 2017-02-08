using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheoPlayer
{
    class conf_ini
    {
        public string [] sciezka = new string[5];
        //0 sciezka_muzyka_podklad        //1 sciezka_muzyka_wokal        //2 sciezka_filmy        //3 sciezka_karaoke        //4 karaoke czy wlaczone

        private bool Karaoke;
        private TabControl tabcontrol;

        private Panel panel = new Panel();
        private Button muzyka_podklad_fortepian, muzyka_podklad_orkiestra, muzyka_wokal, filmy, teksty, karaoke;
        private TextBox tb_muzyka_podklad_fortepian, tb_muzyka_podklad_orkiestra, tb_muzyka_wokal, tb_filmy, tb_teksty, tb_karaoke;
        private int tab_index;
        public bool change = false;
        public conf_ini(TabControl x, int _tab_index)
        {
            tabcontrol = x;
            tab_index= _tab_index;
            f_exist();

            f_show();
        }
        private  void f_exist()
        {
            if (!System.IO.File.Exists("conf.ini"))//nie istnieje - tworzymy plik z ustawieniami
            {

                sciezka[0] = @"Z:\Muzyka\2017";
                if (!System.IO.Directory.Exists(sciezka[0])) System.IO.Directory.CreateDirectory(sciezka[0]);

                sciezka[1] = @"Z:\Muzyka\2017";
                if (!System.IO.Directory.Exists(sciezka[1])) System.IO.Directory.CreateDirectory(sciezka[1]);

                sciezka[2] = @"Z:\Muzyka\2017";
                if (!System.IO.Directory.Exists(sciezka[2])) System.IO.Directory.CreateDirectory(sciezka[2]);

                sciezka[3] = @"Z:\Muzyka\2017";
                if (!System.IO.Directory.Exists(sciezka[3])) System.IO.Directory.CreateDirectory(sciezka[3]);

                sciezka[4] = @"Z:\Muzyka\2017";
                if (!System.IO.Directory.Exists(sciezka[4])) System.IO.Directory.CreateDirectory(sciezka[4]);

                System.IO.File.AppendAllText("conf.ini", sciezka[0] + ";" + sciezka[1] + ";" + sciezka[2] + ";" + sciezka[3] + ";"+ sciezka[4] + ";"+ "false");
                Karaoke = false;

            }
            else//istnieje
            {

                System.IO.StreamReader file = new System.IO.StreamReader("conf.ini");
                if (System.IO.File.Exists("conf.ini"))
                {
                    sciezka = file.ReadLine().Split(';');
                    file.Close();
                }
                Karaoke = (sciezka[5] == "true") ? true : false;
                if (!System.IO.Directory.Exists(sciezka[0]))
                {
                    sciezka[0] = @"Z:\Muzyka\2017";
                    System.IO.Directory.CreateDirectory(sciezka[0]);

                }
                if (!System.IO.Directory.Exists(sciezka[1]))
                {
                    sciezka[1] = @"Z:\Muzyka\2017";
                    System.IO.Directory.CreateDirectory(sciezka[1]);
                }

                if (!System.IO.Directory.Exists(sciezka[2]))
                {
                    sciezka[2] = @"Z:\Muzyka\2017";
                    System.IO.Directory.CreateDirectory(sciezka[2]);
                }
                if (!System.IO.Directory.Exists(sciezka[3]))
                {
                    sciezka[3] = @"Z:\Muzyka\2017";
                    System.IO.Directory.CreateDirectory(sciezka[3]);
                }
                if (!System.IO.Directory.Exists(sciezka[4]))
                {
                    sciezka[4] = @"Z:\Muzyka\2017";
                    System.IO.Directory.CreateDirectory(sciezka[4]);
                }
            }

            tabcontrol.TabPages[tab_index].Controls.Add(panel);
        }

        private void f_show()
        {
            //textbox + button konfiguracyjny
            #region muzyka fortepian


            tb_muzyka_podklad_fortepian = new TextBox();
            tb_muzyka_podklad_fortepian.Multiline = true;
            tb_muzyka_podklad_fortepian.ScrollBars = ScrollBars.Vertical;
            tb_muzyka_podklad_fortepian.Font = new Font("Arial", 15);
            tb_muzyka_podklad_fortepian.Text = "Ścieżka do fortepianowego podkładu muzycznego" + Environment.NewLine + "Obecna ścieżka: " + Environment.NewLine;
            tb_muzyka_podklad_fortepian.Size = new Size(tb_muzyka_podklad_fortepian.PreferredSize.Width, tb_muzyka_podklad_fortepian.PreferredSize.Height);
            tb_muzyka_podklad_fortepian.Text += sciezka[0]; 
            tb_muzyka_podklad_fortepian.BorderStyle= BorderStyle.None;
            tb_muzyka_podklad_fortepian.ReadOnly= true;
            tb_muzyka_podklad_fortepian.Location = new Point(0, 0);
            panel.Controls.Add(tb_muzyka_podklad_fortepian);


            muzyka_podklad_fortepian = new Button();
            muzyka_podklad_fortepian.Name = "muzyka_podklad_fortepian";
            muzyka_podklad_fortepian.Font = new Font("Arial", 15);
            muzyka_podklad_fortepian.Click += button_Click;
            muzyka_podklad_fortepian.Text = "zmień";
            muzyka_podklad_fortepian.Size = new Size((int)muzyka_podklad_fortepian.Font.Size * (muzyka_podklad_fortepian.Text.Length + 3), tb_muzyka_podklad_fortepian.Height);
            muzyka_podklad_fortepian.Location = new Point(tb_muzyka_podklad_fortepian.Location.X + tb_muzyka_podklad_fortepian.Width, tb_muzyka_podklad_fortepian.Location.Y);
            panel.Controls.Add(muzyka_podklad_fortepian);

            #endregion

            #region muzyka orkiestra


            tb_muzyka_podklad_orkiestra = new TextBox();
            tb_muzyka_podklad_orkiestra.Multiline = true;
            tb_muzyka_podklad_orkiestra.ScrollBars = ScrollBars.Vertical;
            tb_muzyka_podklad_orkiestra.Font = new Font("Arial", 15);
            tb_muzyka_podklad_orkiestra.Text = "Ścieżka do orkiestralnego podkładu muzycznego " + Environment.NewLine + "Obecna ścieżka: " + Environment.NewLine + sciezka[1];
            tb_muzyka_podklad_orkiestra.Size = tb_muzyka_podklad_fortepian.Size;
            tb_muzyka_podklad_orkiestra.BorderStyle = BorderStyle.None;
            tb_muzyka_podklad_orkiestra.ReadOnly = true;
            tb_muzyka_podklad_orkiestra.Location = new Point(0, tb_muzyka_podklad_fortepian.Height * 2);
            panel.Controls.Add(tb_muzyka_podklad_orkiestra);


            muzyka_podklad_orkiestra = new Button();
            muzyka_podklad_orkiestra.Name = "muzyka_podklad_orkiestra";
            muzyka_podklad_orkiestra.Font = new Font("Arial", 15);
            muzyka_podklad_orkiestra.Click += button_Click;
            muzyka_podklad_orkiestra.Text = "zmień";
            muzyka_podklad_orkiestra.Size = muzyka_podklad_fortepian.Size;
            muzyka_podklad_orkiestra.Location = new Point(tb_muzyka_podklad_orkiestra.Location.X + tb_muzyka_podklad_orkiestra.Width, tb_muzyka_podklad_orkiestra.Location.Y);
            panel.Controls.Add(muzyka_podklad_orkiestra);


            #endregion

            #region muzyka wokal


            tb_muzyka_wokal = new TextBox();
            tb_muzyka_wokal.Multiline = true;
            tb_muzyka_wokal.ScrollBars = ScrollBars.Vertical;
            tb_muzyka_wokal.Font = new Font("Arial", 15);
            tb_muzyka_wokal.Text = "Ścieżka do nagrań wokalnych" + Environment.NewLine + "Obecna ścieżka: " + Environment.NewLine + sciezka[2];
            tb_muzyka_wokal.Size = tb_muzyka_podklad_fortepian.Size;
            tb_muzyka_wokal.BorderStyle= BorderStyle.None;
            tb_muzyka_wokal.ReadOnly= true;
            tb_muzyka_wokal.Location = new Point(0, tb_muzyka_podklad_fortepian.Height * 4);
            panel.Controls.Add(tb_muzyka_wokal);


            muzyka_wokal = new Button();
            muzyka_wokal.Name = "muzyka_wokal";
            muzyka_wokal.Font = new Font("Arial", 15);
            muzyka_wokal.Click += button_Click;
            muzyka_wokal.Text = "zmień";
            muzyka_wokal.Size = muzyka_podklad_fortepian.Size;
            muzyka_wokal.Location = new Point(tb_muzyka_podklad_fortepian.Location.X + tb_muzyka_wokal.Width, tb_muzyka_wokal.Location.Y);
            panel.Controls.Add(muzyka_wokal);


            #endregion

            #region filmy


            tb_filmy = new TextBox();
            tb_filmy.Multiline = true;
            tb_filmy.ScrollBars = ScrollBars.Vertical;
            tb_filmy.Font = new Font("Arial", 15);
            tb_filmy.Text = "Ścieżka do filmów" + Environment.NewLine + "Obecna ścieżka: " + Environment.NewLine + sciezka[3];
            tb_filmy.Size = tb_muzyka_podklad_fortepian.Size;
            tb_filmy.BorderStyle= BorderStyle.None;
            tb_filmy.ReadOnly= true;
            tb_filmy.Location = new Point(0, tb_muzyka_podklad_fortepian.Height * 6);
            panel.Controls.Add(tb_filmy);


            filmy = new Button();
            filmy.Name = "filmy";
            filmy.Font = new Font("Arial", 15);
            filmy.Click += button_Click;
            filmy.Text = "zmień";
            filmy.Size = muzyka_podklad_fortepian.Size;
            filmy.Location = new Point(tb_muzyka_podklad_fortepian.Location.X + tb_filmy.Width, tb_filmy.Location.Y);
            panel.Controls.Add(filmy);


            #endregion

            #region Karaoke


            tb_karaoke = new TextBox();
            tb_karaoke.Multiline = true;
            tb_karaoke.ScrollBars = ScrollBars.Vertical;
            tb_karaoke.Font = new Font("Arial", 15);
            tb_karaoke.Text = "Czy wyświetlać tekst pieśni w trakcie odtwarzania?";
            tb_karaoke.Size = tb_muzyka_podklad_fortepian.Size;
            tb_karaoke.BorderStyle = BorderStyle.None;
            tb_karaoke.ReadOnly = true;
            tb_karaoke.Location = new Point(0, tb_muzyka_podklad_fortepian.Height * 8);
            panel.Controls.Add(tb_karaoke);


            karaoke = new Button();
            karaoke.Name = "karaoke";
            karaoke.Font = new Font("Arial", 15);
            karaoke.Click += karaoke_Click;
            karaoke.Text = (Karaoke==false)?"Włącz":"Wyłącz";
            karaoke.Size = muzyka_podklad_fortepian.Size;
            karaoke.Location = new Point(tb_karaoke.Location.X + tb_karaoke.Width, tb_karaoke.Location.Y);
            panel.Controls.Add(karaoke);


            #endregion

            #region teksty


            tb_teksty = new TextBox();
            tb_teksty.Visible = (Karaoke == true) ? true : false;
            tb_teksty.Multiline = true;
            tb_teksty.ScrollBars = ScrollBars.Vertical;
            tb_teksty.Font = new Font("Arial", 15);
            tb_teksty.Text = "Ścieżka do tekstów pieśni" + Environment.NewLine + "Obecna ścieżka: " + Environment.NewLine + sciezka[4];
            tb_teksty.Size = tb_muzyka_podklad_fortepian.Size;
            tb_teksty.BorderStyle = BorderStyle.None;
            tb_teksty.ReadOnly = true;
            tb_teksty.Location = new Point(0, tb_muzyka_podklad_fortepian.Height * 10);
            panel.Controls.Add(tb_teksty);


            teksty = new Button();
            teksty.Visible = (Karaoke == true) ? true : false;
            teksty.Name = "teksty";
            teksty.Font = new Font("Arial", 15);
            teksty.Click += button_Click;
            teksty.Text = "zmień";
            teksty.Size = muzyka_podklad_fortepian.Size;
            teksty.Location = new Point(tb_muzyka_podklad_fortepian.Location.X + tb_teksty.Width, tb_teksty.Location.Y);
            panel.Controls.Add(teksty);


            #endregion
            panel.Width = panel.PreferredSize.Width;
            panel.Height = panel.PreferredSize.Height;
            panel.Location = new Point(tabcontrol.ClientSize.Width / 2 - panel.Width / 2, tabcontrol.ClientSize.Height / 3 - panel.Height / 3);
        }

        void button_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
            if (openFolderDialog.ShowDialog() == DialogResult.OK)
            {
            }
            if (openFolderDialog.SelectedPath != "")
            {
                if ((sender as Button).Name == "muzyka_podklad_fortepian")
                {
                    sciezka[0] = openFolderDialog.SelectedPath;
                    tb_muzyka_podklad_fortepian.Text = "Ścieżka do fortepianowego podkładu muzycznego " + Environment.NewLine + "Obecna ścieżka: " + Environment.NewLine + sciezka[0];
                }
                else if ((sender as Button).Name == "muzyka_podklad_orkiestra")
                {
                    sciezka[1] = openFolderDialog.SelectedPath;
                    tb_muzyka_podklad_orkiestra.Text = "Ścieżka do orkiestralnego podkładu muzycznego" + Environment.NewLine + "Obecna ścieżka: " + Environment.NewLine + sciezka[1];
                }
                else if ((sender as Button).Name == "muzyka_wokal")
                {
                    sciezka[2] = openFolderDialog.SelectedPath;
                    tb_muzyka_wokal.Text = "Ścieżka do nagrań wokalnych" + Environment.NewLine + "Obecna ścieżka: " + Environment.NewLine + sciezka[2];
                }
                else if ((sender as Button).Name == "filmy")
                {
                    sciezka[3] = openFolderDialog.SelectedPath;
                    tb_filmy.Text = "Ścieżka do filmów" + Environment.NewLine + "Obecna ścieżka: " + Environment.NewLine + sciezka[3];
                }
                else if ((sender as Button).Name == "teksty")
                {
                    sciezka[4] = openFolderDialog.SelectedPath;
                    tb_teksty.Text = "Ścieżka do tekstów pieśni" + Environment.NewLine + "Obecna ścieżka: " + Environment.NewLine + sciezka[4];
                }
            }
            modyfikuj_conf();
            openFolderDialog.Dispose();
            change = true;
        }

        void karaoke_Click(object sender, EventArgs e)
        {

            if (Karaoke) karaoke.Text = "Włącz";
            else karaoke.Text = "Wyłącz";

            Karaoke = !Karaoke;
            teksty.Visible = Karaoke;
            tb_teksty.Visible = Karaoke;
            panel.Height = panel.PreferredSize.Height;
            modyfikuj_conf();

        }
        void modyfikuj_conf()
        {
            if (System.IO.File.Exists("conf.ini"))
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter("conf.ini");
                file.WriteLine(sciezka[0] + ';' + sciezka[1] + ';' + sciezka[2] + ';' + sciezka[3] + ';' + sciezka[4] + ';' + Karaoke);
                file.Close();
            }
            else f_exist();
        }
    }
}
