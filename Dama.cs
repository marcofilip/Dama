using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace dama
{
    public partial class Dama : Form
    {
        public Label pezziMangiatiBIANCO = new Label();
        public Label pezziMangiatiNERO = new Label();
        public Label lbl_VinciteBIANCO = new Label();
        public Label lbl_VinciteNERO = new Label();
        public Label divisore = new Label();
        static Casella CasellaPrecedente;
        static Casella CasellaPremuta;
        static string CartellaCorrente = Environment.CurrentDirectory;

        public Dama()
        {
            InitializeComponent();
            Scacchiera.PezzoBianco = new Bitmap(new Bitmap(CartellaCorrente + @"\img\pezzobianco.png"), new Size(Casella.GrandezzaCasella - 10, Casella.GrandezzaCasella - 10));
            Scacchiera.PezzoNero = new Bitmap(new Bitmap(CartellaCorrente + @"\img\pezzonero.png"), new Size(Casella.GrandezzaCasella - 10, Casella.GrandezzaCasella - 10));
            Icon = new Icon(CartellaCorrente + @"\img\icon.ico");
            Text = "Dama";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = Color.FromArgb(240, 217, 181);
            ForeColor = Color.FromArgb(181, 136, 99);
            Font = new Font("Comic Sans MS", 12);
            Closed += new EventHandler(ChiudiTutto);
            Inizializza();
        }

        public void ChiudiTutto(object sender, EventArgs e) => Application.Exit();

        public void Inizializza()
        {
            Scacchiera.GiocatoreAttuale = 2;
            Scacchiera.InAzione = false;
            CasellaPrecedente = null;
            Scacchiera.PezziMangiati.Add(0);
            Scacchiera.PezziMangiati.Add(0);
            Scacchiera.InizializzaScacchiera();
            CreaScacchiera();
            CreaControlli();
        }

        public void CreaScacchiera()
        {
            var SpazioPerConteggio = 150;
            Width = (Scacchiera.RigheColonne) * Casella.GrandezzaCasella + 15;
            Height = (Scacchiera.RigheColonne) * Casella.GrandezzaCasella + 40 + SpazioPerConteggio;


            for (int i = 0; i < Scacchiera.RigheColonne; i++)
            {
                for (int j = 0; j < Scacchiera.RigheColonne; j++)
                {
                    Casella casella = new Casella();
                    casella.Location = new Point(j * Casella.GrandezzaCasella, i * Casella.GrandezzaCasella);
                    casella.Size = new Size(Casella.GrandezzaCasella, Casella.GrandezzaCasella);
                    casella.Click += new EventHandler(ClickCasella);
                    if (Scacchiera.scacchiera[i, j] == 1)
                        casella.Image = Scacchiera.PezzoBianco;
                    else if 
                        (Scacchiera.scacchiera[i, j] == 2) casella.Image = Scacchiera.PezzoNero;
                    casella.BackColor = Scacchiera.ColoreCasellaDefault(casella);
                    casella.ForeColor = Color.Black;
                    Scacchiera.Caselle[i, j] = casella;
                    Controls.Add(casella);
                }
            }
        }

        public void CreaControlli()
        {
            pezziMangiatiBIANCO.Text = "Pezzi mangiati giocatore BIANCO: 0";
            pezziMangiatiBIANCO.Location = new Point(10, Height - 2 * Casella.GrandezzaCasella - 15);
            pezziMangiatiBIANCO.AutoSize = true;
            pezziMangiatiBIANCO.BackColor = Color.FromArgb(220, 197, 161);
            pezziMangiatiBIANCO.ForeColor = Color.FromArgb(100, 80, 70);

            pezziMangiatiNERO.Text = "Pezzi mangiati giocatore NERO: 0";
            pezziMangiatiNERO.Location = new Point(10, Height - Casella.GrandezzaCasella - 15);
            pezziMangiatiNERO.AutoSize = true;
            pezziMangiatiNERO.BackColor = Color.FromArgb(220, 197, 161);
            pezziMangiatiNERO.ForeColor = Color.FromArgb(100, 80, 70);

            lbl_VinciteBIANCO.Text = $"Vincite BIANCO: {Scacchiera.VinciteBIANCO}";
            lbl_VinciteBIANCO.Location = new Point(Width/2 + 50, pezziMangiatiBIANCO.Location.Y);
            lbl_VinciteBIANCO.AutoSize = true;
            lbl_VinciteBIANCO.BackColor = Color.FromArgb(220, 197, 161);
            lbl_VinciteBIANCO.ForeColor = Color.FromArgb(100, 80, 70);

            lbl_VinciteNERO.Text = $"Vincite NERO: {Scacchiera.VinciteNERO}";
            lbl_VinciteNERO.Location = new Point(Width/2 + 50, pezziMangiatiNERO.Location.Y);
            lbl_VinciteNERO.AutoSize = true;
            lbl_VinciteNERO.BackColor = Color.FromArgb(220, 197, 161);
            lbl_VinciteNERO.ForeColor = Color.FromArgb(100, 80, 70);

            divisore.Text = "|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|";
            divisore.Font = new Font(divisore.Font.FontFamily, 3);
            divisore.AutoSize = true;
            divisore.Location = new Point(Width/2, pezziMangiatiBIANCO.Location.Y - 10);
            divisore.BackColor = Color.FromArgb(100, 80, 70);
            divisore.ForeColor = Color.FromArgb(100, 80, 70);

            Controls.Add(pezziMangiatiBIANCO);
            Controls.Add(pezziMangiatiNERO);
            Controls.Add(lbl_VinciteBIANCO);
            Controls.Add(lbl_VinciteNERO);
            Controls.Add(divisore);
        }

        public void VerificaVincita()
        {
            bool Giocatore1 = false;
            bool Giocatore2 = false;

            foreach(int casella in Scacchiera.scacchiera)
            {
                if (casella == 1)
                    Giocatore1 = true;
                if (casella == 2)
                    Giocatore2 = true;
            }   

            if (!Giocatore1 || !Giocatore2)
            {
                string vincitore;
                if (Giocatore1)
                {
                    Scacchiera.VinciteBIANCO++;
                    vincitore = "BIANCO";
                }
                else
                {
                    Scacchiera.VinciteNERO++;
                    vincitore = "NERO";
                }
                Vincita(vincitore);
            }
        }

        public void Vincita(string vincitore)
        {
            FormVincitore vincita = new FormVincitore(vincitore);
            vincita.Show();
            vincita.FormClosed += new FormClosedEventHandler(Restarta);
        }

        public void Restarta(object sender, EventArgs e)
        {
            Controls.Clear();
            Scacchiera.PezziMangiati.Clear();
            Inizializza();
        }

        public void AggiornaCounter(int giocatore)
        {
            Scacchiera.PezziMangiati[giocatore]++;
            if (giocatore == 0)
                pezziMangiatiBIANCO.Text = pezziMangiatiBIANCO.Text.Substring(0, 33) + Scacchiera.PezziMangiati[giocatore];
            else
                pezziMangiatiNERO.Text = pezziMangiatiNERO.Text.Substring(0, 31) + Scacchiera.PezziMangiati[giocatore];
        }

        public void MostraMosseMangia(int riga, int colonna, bool èMossaSemplice = true)
        {
            int direzioneX = riga - CasellaPremuta.Location.Y / Casella.GrandezzaCasella;
            int direzioneY = colonna - CasellaPremuta.Location.X / Casella.GrandezzaCasella;
            direzioneX = direzioneX < 0 ? -1 : 1;
            direzioneY = direzioneY < 0 ? -1 : 1;
            int nuovaRiga = riga;
            int nuovaColonna = colonna;
            bool èVuoto = true;
            while (Scacchiera.ControllaDentroBordi(nuovaRiga, nuovaColonna))
            {
                if (Scacchiera.scacchiera[nuovaRiga, nuovaColonna] != 0 && Scacchiera.scacchiera[nuovaRiga, nuovaColonna] != Scacchiera.GiocatoreAttuale)
                {
                    èVuoto = false;
                    break;
                }
                nuovaRiga += direzioneX;
                nuovaColonna += direzioneY;

                if (èMossaSemplice)
                    break;
            }

            if (èVuoto)
                return;

            List<Casella> daChiudere = new List<Casella>();
            bool chiudiSemplice = false;
            int rigaSuccessiva = nuovaRiga + direzioneX;
            int colonnaSuccessiva = nuovaColonna + direzioneY;
            while (Scacchiera.ControllaDentroBordi(rigaSuccessiva, colonnaSuccessiva))
            {
                if (Scacchiera.scacchiera[rigaSuccessiva, colonnaSuccessiva] == 0)
                {
                    if (Scacchiera.ControllaMangiaDisponibile(rigaSuccessiva, colonnaSuccessiva, èMossaSemplice, new int[2] { direzioneX, direzioneY }))
                        chiudiSemplice = true;
                    else
                        daChiudere.Add(Scacchiera.Caselle[rigaSuccessiva, colonnaSuccessiva]);

                    Scacchiera.Caselle[rigaSuccessiva, colonnaSuccessiva].BackColor = Color.FromArgb(180, 50, 30);
                    Scacchiera.Caselle[rigaSuccessiva, colonnaSuccessiva].Text = "XX";
                    Scacchiera.Caselle[rigaSuccessiva, colonnaSuccessiva].Enabled = true;
                    Scacchiera.ContoMangiate++;
                }
                else break;

                if (èMossaSemplice)
                    break;

                colonnaSuccessiva += direzioneY;
                rigaSuccessiva += direzioneX;
            }
            if (chiudiSemplice && daChiudere.Count > 0)
                Scacchiera.PulisciScacchieraMosseSemplici(daChiudere);
        }

        public void ClickCasella(object sender, EventArgs e)
        {
            if (CasellaPrecedente != null)
                CasellaPrecedente.BackColor = Scacchiera.ColoreCasellaDefault(CasellaPrecedente);

            CasellaPremuta = sender as Casella;

            if (Scacchiera.scacchiera[CasellaPremuta.Location.Y / Casella.GrandezzaCasella, CasellaPremuta.Location.X / Casella.GrandezzaCasella] != 0 && Scacchiera.scacchiera[CasellaPremuta.Location.Y / Casella.GrandezzaCasella, CasellaPremuta.Location.X / Casella.GrandezzaCasella] == Scacchiera.GiocatoreAttuale)
            {
                Scacchiera.PulisciScacchieraMosse();
                CasellaPremuta.BackColor = Color.FromArgb(255, 255, 0);
                Scacchiera.AttivaDisattivaCaselle(false);
                CasellaPremuta.Enabled = true;
                Scacchiera.ContoMangiate = 0;
                if (CasellaPremuta.Text == "👑")
                    MostraMosse(CasellaPremuta.Location.Y / Casella.GrandezzaCasella, CasellaPremuta.Location.X / Casella.GrandezzaCasella, false);
                else MostraMosse(CasellaPremuta.Location.Y / Casella.GrandezzaCasella, CasellaPremuta.Location.X / Casella.GrandezzaCasella);

                if (Scacchiera.InAzione)
                {
                    Scacchiera.PulisciScacchieraMosse();
                    CasellaPremuta.BackColor = Scacchiera.ColoreCasellaDefault(CasellaPremuta);
                    Scacchiera.MostraMossePossibili();
                    Scacchiera.InAzione = false;
                }
                else
                    Scacchiera.InAzione = true;
            }
            else
            {
                if (Scacchiera.InAzione)
                {
                    Scacchiera.ContinuaMossaOppureNo = false;
                    if (Math.Abs(CasellaPremuta.Location.X / Casella.GrandezzaCasella - CasellaPrecedente.Location.X / Casella.GrandezzaCasella) > 1)
                    {
                        Scacchiera.ContinuaMossaOppureNo = true;
                        EliminaMangiato(CasellaPremuta, CasellaPrecedente);
                    }
                    int temp = Scacchiera.scacchiera[CasellaPremuta.Location.Y / Casella.GrandezzaCasella, CasellaPremuta.Location.X / Casella.GrandezzaCasella];

                    Scacchiera.scacchiera[CasellaPremuta.Location.Y / Casella.GrandezzaCasella, CasellaPremuta.Location.X / Casella.GrandezzaCasella] =
                        Scacchiera.scacchiera[CasellaPrecedente.Location.Y / Casella.GrandezzaCasella, CasellaPrecedente.Location.X / Casella.GrandezzaCasella];

                    Scacchiera.scacchiera[CasellaPrecedente.Location.Y / Casella.GrandezzaCasella, CasellaPrecedente.Location.X / Casella.GrandezzaCasella] = temp;
                    CasellaPremuta.Image = CasellaPrecedente.Image;
                    CasellaPrecedente.Image = null;
                    CasellaPremuta.Text = CasellaPrecedente.Text;
                    CasellaPrecedente.Text = "";
                    Scacchiera.ControllaPromozione(CasellaPremuta);
                    Scacchiera.ContoMangiate = 0;
                    Scacchiera.InAzione = false;

                    Scacchiera.PulisciScacchieraMosse();
                    Scacchiera.AttivaDisattivaCaselle(false);

                    if (CasellaPremuta.Text == "👑")
                        MostraMosse(CasellaPremuta.Location.Y / Casella.GrandezzaCasella, CasellaPremuta.Location.X / Casella.GrandezzaCasella, false);

                    else
                        MostraMosse(CasellaPremuta.Location.Y / Casella.GrandezzaCasella, CasellaPremuta.Location.X / Casella.GrandezzaCasella);

                    if (Scacchiera.ContoMangiate == 0 || !Scacchiera.ContinuaMossaOppureNo)
                    {
                        Scacchiera.PulisciScacchieraMosse();
                        Scacchiera.CambiaGiocatore();
                        VerificaVincita();
                        Scacchiera.MostraMossePossibili();
                        Scacchiera.ContinuaMossaOppureNo = false;
                    }

                    else if (Scacchiera.ContinuaMossaOppureNo)
                    {
                        CasellaPremuta.BackColor = Color.FromArgb(100, 100, 20);
                        CasellaPremuta.Enabled = true;
                        Scacchiera.InAzione = true;
                    }
                }
            }

            CasellaPrecedente = CasellaPremuta;
        }

        public void EliminaMangiato(Casella fineCasella, Casella inizioCasella)
        {
            int conteggio = Math.Abs(fineCasella.Location.Y / Casella.GrandezzaCasella - inizioCasella.Location.Y / Casella.GrandezzaCasella);
            int indiceInizioX = fineCasella.Location.Y / Casella.GrandezzaCasella - inizioCasella.Location.Y / Casella.GrandezzaCasella;
            int indiceInizioY = fineCasella.Location.X / Casella.GrandezzaCasella - inizioCasella.Location.X / Casella.GrandezzaCasella;

            if (indiceInizioX < 0)
                indiceInizioX = -1;
            else
                indiceInizioX = 1;

            if (indiceInizioY < 0)
                indiceInizioY = -1;
            else
                indiceInizioY = 1;

            int conteggioCorrente = 0;
            int i = inizioCasella.Location.Y / Casella.GrandezzaCasella + indiceInizioX;
            int j = inizioCasella.Location.X / Casella.GrandezzaCasella + indiceInizioY;
            while (conteggioCorrente < conteggio - 1)
            {
                Scacchiera.scacchiera[i, j] = 0;
                Scacchiera.Caselle[i, j].Image = null;
                Scacchiera.Caselle[i, j].Text = "";
                i += indiceInizioX;
                j += indiceInizioY;
                conteggioCorrente++;

                AggiornaCounter(Scacchiera.GiocatoreAttuale - 1);
            }
        }

        public void MostraMosse(int rigaPedina, int colonnaPedina, bool èMossaSemplice = true)
        {
            Scacchiera.MosseSemplici.Clear();
            MostraMosseDiagonali(rigaPedina, colonnaPedina, èMossaSemplice);
            if (Scacchiera.ContoMangiate > 0)
                Scacchiera.PulisciScacchieraMosseSemplici(Scacchiera.MosseSemplici);
        }

        public bool DeterminaPercorso(int riga, int colonna)
        {
            if (Scacchiera.scacchiera[riga, colonna] == 0 && !Scacchiera.ContinuaMossaOppureNo)
            {
                Scacchiera.Caselle[riga, colonna].BackColor = Color.FromArgb(100, 150, 20);
                Scacchiera.Caselle[riga, colonna].Text = "X";
                Scacchiera.Caselle[riga, colonna].Enabled = true;
                Scacchiera.MosseSemplici.Add(Scacchiera.Caselle[riga, colonna]);
            }
            else
            {

                if (Scacchiera.scacchiera[riga, colonna] != Scacchiera.GiocatoreAttuale)
                {
                    if (CasellaPremuta.Text == "👑")
                        MostraMosseMangia(riga, colonna, false);
                    else MostraMosseMangia(riga, colonna);
                }

                return false;
            }
            return true;
        }

        public void MostraMosseDiagonali(int rigaCorrente, int colonnaCorrente, bool èMossaSemplice = false)
        {
            int colonna = colonnaCorrente + 1;
            for (int riga = rigaCorrente - 1; riga >= 0; riga--)
            {
                if (Scacchiera.GiocatoreAttuale == 1 && èMossaSemplice && !Scacchiera.ContinuaMossaOppureNo) break;
                if (Scacchiera.ControllaDentroBordi(riga, colonna))
                {
                    if (!DeterminaPercorso(riga, colonna))
                        break;
                }
                if (colonna < 7)
                    colonna++;
                else break;

                if (èMossaSemplice)
                    break;
            }

            colonna = colonnaCorrente - 1;
            for (int riga = rigaCorrente - 1; riga >= 0; riga--)
            {
                if (Scacchiera.GiocatoreAttuale == 1 && èMossaSemplice && !Scacchiera.ContinuaMossaOppureNo) break;
                if (Scacchiera.ControllaDentroBordi(riga, colonna))
                {
                    if (!DeterminaPercorso(riga, colonna))
                        break;
                }
                if (colonna > 0)
                    colonna--;
                else break;

                if (èMossaSemplice)
                    break;
            }

            colonna = colonnaCorrente - 1;
            for (int riga = rigaCorrente + 1; riga < 8; riga++)
            {
                if (Scacchiera.GiocatoreAttuale == 2 && èMossaSemplice && !Scacchiera.ContinuaMossaOppureNo) break;
                if (Scacchiera.ControllaDentroBordi(riga, colonna))
                {
                    if (!DeterminaPercorso(riga, colonna))
                        break;
                }
                if (colonna > 0)
                    colonna--;
                else break;

                if (èMossaSemplice)
                    break;
            }

            colonna = colonnaCorrente + 1;
            for (int riga = rigaCorrente + 1; riga < 8; riga++)
            {
                if (Scacchiera.GiocatoreAttuale == 2 && èMossaSemplice && !Scacchiera.ContinuaMossaOppureNo) break;
                if (Scacchiera.ControllaDentroBordi(riga, colonna))
                {
                    if (!DeterminaPercorso(riga, colonna))
                        break;
                }
                if (colonna < 7)
                    colonna++;
                else break;

                if (èMossaSemplice)
                    break;
            }
        }
    }
}