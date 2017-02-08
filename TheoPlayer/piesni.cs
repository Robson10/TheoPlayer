using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using WMPLib;

//-----------------------------------------------mp3/aac
namespace TheoPlayer
{
    sealed class piesni
    {
        private TabControl tabcontrol;
        public Panel panel = new Panel(); 
        public Panel boczny_p = new Panel();
        public Panel fortepian, orkiestra, wokal;
        private int page;
        private string sciezka_podklad_fortepian,sciezka_podklad_orkiestra, sciezka_wokal;

        private List<string> pliki_fortepian=new List<string>();
        private List<string> pliki_fortepian_Title = new List<string>();

        private List<string> pliki_orkiestra = new List<string>();
        private List<string> pliki_orkiestra_Title = new List<string>();

        private List<string> pliki_wokal = new List<string>();
        private List<string> pliki_wokal_Title = new List<string>();


        private WMPLib.WindowsMediaPlayer odtwarzacz = new WMPLib.WindowsMediaPlayer();
        private Color bg_tabcontrol = Color.FromArgb(100, 100, 100);
        private Color bg_picturebox = Color.FromArgb(130, 130, 130);
        private Color bg_picturebox_checked = Color.FromArgb(180,180,180);
        private Color bg_picturebox_played = Color.FromArgb(250,100,100);
        
        private object prev, prev_open;
        private Button L1, L2, L3;
        Timer czas = new Timer();
        Random rnd = new Random();
        bool bg_music_on=false;

        WMPLib.WindowsMediaPlayer x = new WMPLib.WindowsMediaPlayer();
        #region zbedne teraz
        public piesni(TabControl tb_control, int _page, string _podklad,string _podklad_ork,string _wokal)
        {
            tabcontrol = tb_control;
            page = _page;
            sciezka_podklad_fortepian = _podklad;
            sciezka_podklad_orkiestra = _podklad_ork;
            sciezka_wokal = _wokal;


            wczytaj_pliki(sciezka_podklad_fortepian, pliki_fortepian,pliki_fortepian_Title);
            wczytaj_pliki(sciezka_podklad_orkiestra, pliki_orkiestra,pliki_orkiestra_Title);
            wczytaj_pliki(sciezka_wokal, pliki_wokal,pliki_wokal_Title);
            wyglad();

            czas.Tick += czas_Tick;

        }

        void czas_Tick(object sender, EventArgs e)
        {
            czas.Enabled = false;
            if (bg_music_on==true)
            f_bg_music();
        }


        private void wczytaj_pliki(string sciezka_folder, List<string> tab_sciezki, List<string> title)
        {
            string  name, ext;

            string[] temp_pliki = System.IO.Directory.GetFiles(sciezka_folder);

            for (int i = 0; i < temp_pliki.Count(); i++)
            {
                ext = temp_pliki[i].Remove(0, temp_pliki[i].IndexOf('.')).ToString();
                //mimeType = System.Web.MimeMapping.GetMimeMapping(temp_pliki[i]);
                if (ext == ".mp3" || ext == ".m4a")
                {
                    tab_sciezki.Add(temp_pliki[i]);
                    try
                    {
                        name = x.newMedia(temp_pliki[i]).getItemInfo("Title").Substring(0, 3);
                        if (Convert.ToInt32(name) >= 0 && Convert.ToInt32(name) <= Int32.MaxValue) title.Add(name);
                        else title.Add(temp_pliki[i].Remove(0, sciezka_folder.Count() + 1).Substring(0, temp_pliki[i].Count() - sciezka_folder.Count() - 1 - ext.Count()));
                    }
                    catch
                    {
                        title.Add(temp_pliki[i].Remove(0, sciezka_folder.Count() + 1).Substring(0, temp_pliki[i].Count() - sciezka_folder.Count() - 1 - ext.Count()));
                    }
                }
            }
            sortowanie(tab_sciezki,title);
        }
        private void sortowanie(List<string> tab_sciezki, List<string> title)
        {
            int pmin;
            string temp;
            //int koniec = title.Count - 1;


            //sortowanie
            for (int j = 0; j < title.Count - 1; j++)
            {
                pmin = j;
                for (int i = j + 1; i < title.Count; i++)
                {
                    //if (i < koniec)
                    {
                        try
                        {
                            if (Convert.ToInt32(title[i].ToString()) < Convert.ToInt32(title[pmin].ToString())) pmin = i;
                        }
                        catch
                        {
                            //ma przerzucic utwor na koniec
                            /*if (j < koniec)
                            {
                                temp = title[j];
                                title[j] = title[koniec];
                                title[koniec] = temp;

                                temp = tab_sciezki[j];
                                tab_sciezki[j] = tab_sciezki[koniec];
                                tab_sciezki[koniec] = temp;
                                koniec--;
                                j--;
                                break;
                            }*/
                        }
                    }
                }
                    temp = title[pmin];
                    title[pmin] = title[j];
                    title[j] = temp;

                    temp = tab_sciezki[pmin];
                    tab_sciezki[pmin] = tab_sciezki[j];
                    tab_sciezki[j] = temp;
                    
            }
        }
        
        public void odtworz(string tab)
        {
            odtwarzacz.controls.stop();
            odtwarzacz.URL = tab;
            odtwarzacz.controls.play();
            if (bg_music_on == true)
            {
                czas.Enabled = true;
                czas.Interval = Convert.ToInt32(odtwarzacz.newMedia(tab).duration) * 1000;
            }
        }
        public void refresh(string _podklad_fortepian,string _podklad_orkiestra ,string _wokal)
        {
            sciezka_podklad_fortepian = _podklad_fortepian;
            sciezka_podklad_orkiestra = _podklad_orkiestra;
            sciezka_wokal = _wokal;

            pliki_fortepian.Clear();
            pliki_orkiestra.Clear();
            pliki_wokal.Clear();

            pliki_fortepian_Title.Clear();
            pliki_orkiestra_Title.Clear();
            pliki_wokal_Title.Clear();

            panel.Controls.Clear();

            wczytaj_pliki(sciezka_podklad_fortepian, pliki_fortepian,pliki_fortepian_Title);
            wczytaj_pliki(sciezka_podklad_orkiestra, pliki_orkiestra,pliki_orkiestra_Title);
            wczytaj_pliki(sciezka_wokal, pliki_wokal,pliki_wokal_Title);
            wyglad();
        }

        private void wyglad()
        {
            panel.BackColor = bg_tabcontrol;

            panel.Size = new Size(tabcontrol.ClientSize.Width-5, tabcontrol.ClientSize.Height*98/100);
            tabcontrol.Controls[page].Controls.Add(panel);
            panel.AutoScroll =true;
            panel.VerticalScroll.Enabled = true;

            panel_fortepian(true);
            panel_orkiestra(false);
            panel_wokal(false);
            panel_boczny(true);

        }
        private void panel_boczny(bool widok)
        {
            panel.Controls.Add(boczny_p);
            boczny_p.Size = new Size(panel.Width * 2 / 10, panel.Height);

            boczny_p.Location = new Point(panel.Width * 7 / 10, 0);


            Button play = new Button();
            play.Text = "Odtwórz";
            play.Size = new Size(boczny_p.Width / 2, boczny_p.Width / 4);
            play.Font = new Font("Arial", play.Height / 5);
            play.Location = new Point(0, 0);
            play.FlatStyle = FlatStyle.Flat;
            play.BackColor = bg_picturebox;
            boczny_p.Controls.Add(play);
            play.Click += play_Click;

            Button stop = new Button();
            stop.Text = "Zatrzymaj";
            stop.Size = new Size(boczny_p.Width / 2, boczny_p.Width / 4);
            stop.Font = play.Font;
            stop.Location = new Point(play.Location.X + play.Width, 0);
            stop.FlatStyle = FlatStyle.Flat;
            stop.BackColor = bg_picturebox;
            boczny_p.Controls.Add(stop);
            stop.Click += stop_Click;
            

            Button bg_music = new Button();
            bg_music.Name = "bg_music";
            bg_music.Text = "Uruchom"+Environment.NewLine+"Muzykę w tle";
            bg_music.Size = new Size(boczny_p.Width , boczny_p.Width/4);
            bg_music.Font = play.Font;
            bg_music.Location = new Point(play.Location.X, play.Location.Y+play.Height);
            bg_music.FlatStyle = FlatStyle.Flat;
            bg_music.BackColor = bg_picturebox;
            boczny_p.Controls.Add(bg_music);
            bg_music.Click+=bg_music_Click;

            Label volume_text = new Label();
            boczny_p.Controls.Add(volume_text);
            volume_text.Text = "Głośność";
            volume_text.Location = new Point(bg_music.Location.X, bg_music.Location.Y + bg_music.Height);
            volume_text.Font = new Font("arial", 15); 
            volume_text.Size = new Size(boczny_p.Width, volume_text.PreferredHeight);
            volume_text.TextAlign = ContentAlignment.TopCenter;

            TrackBar volume = new TrackBar();
            boczny_p.Controls.Add(volume);
            volume.Size = new Size(boczny_p.Width, play.Height / 2);
            volume.Location = new Point(0, volume_text.Location.Y+volume_text.Height);
            volume.Maximum = 10;
            volume.Minimum = 0;
            volume.Value = 10;
            volume.ValueChanged += volume_Scroll;
            

            L1 = new Button();
            L1.Text = "Podkład" + Environment.NewLine + "Fortepianowy";
            L1.Size = new Size(boczny_p.Width , boczny_p.Width / 4);
            L1.Font = play.Font;
            L1.Location = new Point(0, play.Height*4);
            L1.FlatStyle = FlatStyle.Flat;
            L1.BackColor = bg_picturebox;
            boczny_p.Controls.Add(L1);
            L1.Click += L1_Click;

            L2 = new Button();
            L2.Text = "Podkład" + Environment.NewLine + "Orkiestrowy";
            L2.Size = new Size(boczny_p.Width, boczny_p.Width / 4);
            L2.Font = play.Font;
            L2.Location = new Point(0, L1.Location.Y + L1.Height);
            L2.FlatStyle = FlatStyle.Flat;
            L2.BackColor = bg_picturebox;
            boczny_p.Controls.Add(L2);
            L2.Click+=L2_Click;

            L3 = new Button();
            L3.Text = "Utwory" + Environment.NewLine + "Wokalne";
            L3.Size = new Size(boczny_p.Width, boczny_p.Width / 4);
            L3.Font = play.Font;
            L3.Location = new Point(0, L2.Location.Y + L2.Height);
            L3.FlatStyle = FlatStyle.Flat;
            L3.BackColor = bg_picturebox;
            boczny_p.Controls.Add(L3);
            L3.Click += L3_Click;
        
        
        }

        void volume_Scroll(object sender, EventArgs e)
        {
            odtwarzacz.settings.volume=((sender as TrackBar).Value)*10;
        }

        void stop_Click(object sender, EventArgs e)
        {
            try
            {
                odtwarzacz.close();
                bg_music_on = false;
                (prev_open as PictureBox).BackColor = bg_picturebox;
            }
            catch{}
        }
        #endregion

        private void L3_Click(object sender, EventArgs e)
        {
            wokal.Visible = true;
            orkiestra.Visible = false;
            fortepian.Visible = false;

            L3.BackColor = bg_picturebox_checked;
            L2.BackColor = bg_picturebox;
            L1.BackColor = bg_picturebox;

        }

        private void L2_Click(object sender, EventArgs e)
        {
            wokal.Visible = false;
            orkiestra.Visible = true;
            fortepian.Visible = false;

            L3.BackColor = bg_picturebox;
            L2.BackColor = bg_picturebox_checked;
            L1.BackColor = bg_picturebox;
        }

        private void L1_Click(object sender, EventArgs e)
        {
            wokal.Visible = false;
            orkiestra.Visible = false;
            fortepian.Visible = true;

            L3.BackColor = bg_picturebox;
            L2.BackColor = bg_picturebox;
            L1.BackColor = bg_picturebox_checked;
        }


        private void bg_music_Click(object sender, EventArgs e)
        {
            try { (prev_open as PictureBox).BackColor = bg_picturebox; }
            catch { }
            f_bg_music();
        }
        private void f_bg_music()
        {

            bg_music_on = true;
            int utwor;
            int x = rnd.Next(0, 3);
            bool powtarza=true;
            while (powtarza == true)
            {
                powtarza = false;
                try
                {
                    if (x == 0)
                    {
                        utwor = rnd.Next(0, pliki_fortepian.Count());
                        odtworz(pliki_fortepian[utwor]);
                        prev_open = (fortepian.Controls[utwor] as PictureBox);
                    }
                    else if (x == 1)
                    {
                        utwor = rnd.Next(0, pliki_orkiestra.Count());
                        odtworz(pliki_orkiestra[utwor]);
                        prev_open = (orkiestra.Controls[utwor] as PictureBox);
                    }
                    else if (x == 2)
                    {
                        utwor = rnd.Next(0, pliki_wokal.Count());
                        odtworz(pliki_wokal[utwor]);
                        prev_open = (wokal.Controls[utwor] as PictureBox);
                    }
                }
                catch { powtarza = true; x = rnd.Next(0, 3); }
            }
        }

        void play_Click(object sender, EventArgs e)
        {
            bg_music_on = false;
            try { odtwarzacz.close(); }
            catch { }
            try
            {
                if (fortepian.Visible == true)
                {
                    odtworz(pliki_fortepian[Convert.ToInt32((prev as PictureBox).Name.ToString())]);
                    (prev as PictureBox).BackColor = bg_picturebox_played;
                    try { (prev_open as PictureBox).BackColor = bg_picturebox; }
                    catch { }
                    prev_open = prev;
                }
                else if (orkiestra.Visible == true)
                {
                    odtworz(pliki_orkiestra[Convert.ToInt32((prev as PictureBox).Name.Remove(0, 1).ToString())]);
                    (prev as PictureBox).BackColor = bg_picturebox_played;
                    try { (prev_open as PictureBox).BackColor = bg_picturebox; }
                    catch { }
                    prev_open = prev;
                }
                else if (wokal.Visible == true)
                {
                    odtworz(pliki_wokal[Convert.ToInt32((prev as PictureBox).Name.Remove(0, 1).ToString())]);
                    (prev as PictureBox).BackColor = bg_picturebox_played;
                    try { (prev_open as PictureBox).BackColor = bg_picturebox; }
                    catch { }
                    prev_open = prev;
                }
            }
            catch { }

        }
        private void panel_fortepian(bool widok)
        {
            fortepian = new Panel();
            fortepian.Size = new Size(panel.Width * 7 / 10, panel.Height);
            panel.Controls.Add(fortepian);
            fortepian.Visible = widok;

            int kolumny_max = 10;
            Size pb_size = new Size((int)(fortepian.Width / kolumny_max * 0.9), (fortepian.Width / kolumny_max / 2));
            Point xy = new Point((int)(pb_size.Width * 1.1), (int)(pb_size.Height * 1.1));

            for (int i = 0; i < pliki_fortepian.Count; i++)
            {

                PictureBox p_podklad = new PictureBox();
                p_podklad.Name = i.ToString();
                fortepian.Controls.Add(p_podklad);
                p_podklad.Size = pb_size;
                p_podklad.Location = new Point((i % kolumny_max) * xy.X, i / kolumny_max * xy.Y);

                p_podklad.BackColor = bg_picturebox;

                p_podklad.Click += p_podklad_F_Click;
                p_podklad.DoubleClick += p_podklad_F_DoubleClick;
                p_podklad.Paint += p_podklad_F_Paint;
            }
        }
        private void panel_orkiestra(bool widok)
        {
            orkiestra = new Panel();
            orkiestra.Size = new Size(panel.Width * 7 / 10, panel.Height);
            panel.Controls.Add(orkiestra);
            orkiestra.Visible = widok;

            int kolumny_max = 10;
            Size pb_size = new Size((int)(orkiestra.Width / kolumny_max * 0.9), (orkiestra.Width / kolumny_max / 2));
            Point xy = new Point((int)(pb_size.Width * 1.1), (int)(pb_size.Height * 1.1));

            for (int i = 0; i < pliki_orkiestra.Count; i++)
            {

                PictureBox p_podklad = new PictureBox();
                p_podklad.Name = "O" + i.ToString();
                orkiestra.Controls.Add(p_podklad);
                p_podklad.Size = pb_size;
                p_podklad.Location = new Point((i % kolumny_max) * xy.X, i / kolumny_max * xy.Y);

                p_podklad.BackColor = bg_picturebox;

                p_podklad.Click += p_podklad_O_Click;
                p_podklad.DoubleClick += p_podklad_O_DoubleClick;
                p_podklad.Paint += p_podklad_O_Paint;
            }
        }
        private void panel_wokal(bool widok)
        {
            wokal = new Panel();
            wokal.Size = new Size(panel.Width * 7 / 10, panel.Height);
            panel.Controls.Add(wokal);
            wokal.Visible = widok;

            int kolumny_max = 10;
            Size pb_size = new Size((int)(wokal.Width / kolumny_max * 0.9), (wokal.Width / kolumny_max / 2));
            Point xy = new Point((int)(pb_size.Width * 1.1), (int)(pb_size.Height * 1.1));

            for (int i = 0; i <pliki_wokal.Count; i++)
            {

                PictureBox p_podklad = new PictureBox();
                p_podklad.Name = "W" + i.ToString();
                wokal.Controls.Add(p_podklad);
                p_podklad.Size = pb_size;
                p_podklad.Location = new Point((i % kolumny_max) * xy.X, i / kolumny_max * xy.Y);

                p_podklad.BackColor = bg_picturebox;

                p_podklad.Click += p_podklad_W_Click;
                p_podklad.DoubleClick += p_podklad_W_DoubleClick;
                p_podklad.Paint += p_podklad_W_Paint;
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------
        #region event fortepian

        void p_podklad_F_Paint(object sender, PaintEventArgs e)
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;

            int rozmiar = (sender as PictureBox).Height * 2 / 10;
            int i = Convert.ToInt32((sender as PictureBox).Name);
            //string name = pliki_fortepian[i].Substring(sciezka_podklad_fortepian.Count() + 1, pliki_fortepian[i].LastIndexOf('.') - sciezka_podklad_fortepian.Count() - 1);
            string name = pliki_fortepian_Title[i];
            using (Font myFont = new Font("Arial", rozmiar))
            {
                e.Graphics.DrawString(name, myFont, Brushes.White, 0, (sender as PictureBox).Height / 2 - rozmiar / 2, stringFormat);
            }
        }

        void p_podklad_F_Click(object sender, EventArgs e)
        {
            
                if ((sender as PictureBox).BackColor != bg_picturebox_played)
                {
                    if ((sender as PictureBox).BackColor == bg_picturebox)
                    {
                        if (prev != prev_open)
                        {
                            try{(prev as PictureBox).BackColor = bg_picturebox;}
                            catch { }
                        }
                        (sender as PictureBox).BackColor = bg_picturebox_checked;
                    }
                    else (sender as PictureBox).BackColor = bg_picturebox;
                    
                    prev = sender;
                }
            
        }

        private void p_podklad_F_DoubleClick(object sender, EventArgs e)
        {
            bg_music_on = false;
            odtworz(pliki_fortepian[Convert.ToInt32((sender as PictureBox).Name.ToString())]);
            (sender as PictureBox).BackColor = bg_picturebox_played;
            if (prev_open != sender&&prev_open!=null)
            {
                (prev_open as PictureBox).BackColor = bg_picturebox;

            } 
            prev_open = sender;
            
        }
        #endregion
        //------------------------------------------------------------------------------------------------------------------------------------------------
        #region event orkiestra
        void p_podklad_O_Paint(object sender, PaintEventArgs e)
        {
            int i = Convert.ToInt32((sender as PictureBox).Name.Remove(0,1));
            //string name = pliki_orkiestra[i].Substring(sciezka_podklad_orkiestra.Count() + 1, pliki_orkiestra[i].LastIndexOf('.') - sciezka_podklad_orkiestra.Count() - 1);
            string name = pliki_orkiestra_Title[i];
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;

            int rozmiar = (sender as PictureBox).Height * 2 / 10;

            using (Font myFont = new Font("Arial", rozmiar))
            {
                e.Graphics.DrawString(name, myFont, Brushes.White, 0, (sender as PictureBox).Height / 2 - rozmiar / 2, stringFormat);
            }
        }

        void p_podklad_O_Click(object sender, EventArgs e)
        {

            if ((sender as PictureBox).BackColor != bg_picturebox_played)
            {
                if ((sender as PictureBox).BackColor == bg_picturebox)
                {
                    if (prev != prev_open)
                    {
                        try { (prev as PictureBox).BackColor = bg_picturebox; }
                        catch { }
                    }
                    (sender as PictureBox).BackColor = bg_picturebox_checked;
                }
                else (sender as PictureBox).BackColor = bg_picturebox;

                prev = sender;
            }

        }

        private void p_podklad_O_DoubleClick(object sender, EventArgs e)
        {

            bg_music_on = false;
            odtworz(pliki_orkiestra[ Convert.ToInt32((sender as PictureBox).Name.Remove(0,1).ToString())]);
            (sender as PictureBox).BackColor = bg_picturebox_played;
            if (prev_open != sender && prev_open != null)
            {
                (prev_open as PictureBox).BackColor = bg_picturebox;

            }
            prev_open = sender;

        }
        #endregion
        //------------------------------------------------------------------------------------------------------------------------------------------------
        #region event wokal
        void p_podklad_W_Paint(object sender, PaintEventArgs e)
        {
            int i = Convert.ToInt32((sender as PictureBox).Name.Remove(0,1));
            
            //string name = pliki_wokal[i].Substring(sciezka_wokal.Count() + 1, pliki_wokal[i].LastIndexOf('.')-sciezka_wokal.Count()-1);
            string name = pliki_wokal_Title[i];

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;

            int rozmiar = (sender as PictureBox).Height * 2 / 10;

            using (Font myFont = new Font("Arial", rozmiar))
            {
                e.Graphics.DrawString(name, myFont, Brushes.White, 0, (sender as PictureBox).Height / 2 - rozmiar / 2, stringFormat);
            }
        }

        void p_podklad_W_Click(object sender, EventArgs e)
        {

            if ((sender as PictureBox).BackColor != bg_picturebox_played)
            {
                if ((sender as PictureBox).BackColor == bg_picturebox)
                {
                    if (prev != prev_open)
                    {
                        try { (prev as PictureBox).BackColor = bg_picturebox; }
                        catch { }
                    }
                    (sender as PictureBox).BackColor = bg_picturebox_checked;
                }
                else (sender as PictureBox).BackColor = bg_picturebox;

                prev = sender;
            }

        }

        private void p_podklad_W_DoubleClick(object sender, EventArgs e)
        {

            bg_music_on = false;
            odtworz(pliki_wokal[ Convert.ToInt32((sender as PictureBox).Name.Remove(0,1).ToString())]);
            (sender as PictureBox).BackColor = bg_picturebox_played;
            if (prev_open != sender && prev_open != null)
            {
                (prev_open as PictureBox).BackColor = bg_picturebox;

            }
            prev_open = sender;

        }
#endregion
        //------------------------------------------------------------------------------------------------------------------------------------------------

    }
}
