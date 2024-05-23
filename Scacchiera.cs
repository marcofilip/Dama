using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Windows.Forms;

namespace dama
{
    internal class Scacchiera
    {
        public const int RigheColonne = 8;
        public static int VinciteBIANCO = 0;
        public static int VinciteNERO = 0;
        public static int GiocatoreAttuale = 0;
        public static int ContoMangiate = 0;

        public static bool ContinuaMossaOppureNo = false;
        public static bool InAzione = false;

        public static int[,] scacchiera = new int[RigheColonne, RigheColonne];

        public static Casella[,] Caselle = new Casella[RigheColonne, RigheColonne];
        public static List<Casella> MosseSemplici = new List<Casella>();
        public static List<int> PezziMangiati = new List<int>();

        public static Image PezzoBianco;
        public static Image PezzoNero;

        public static void InizializzaScacchiera()
        {
            scacchiera = new int[RigheColonne, RigheColonne] {
                { 0, 1, 0, 1, 0, 1, 0, 1 },
                { 1, 0, 1, 0, 1, 0, 1, 0 },
                { 0, 1, 0, 1, 0, 1, 0, 1 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 2, 0, 2, 0, 2, 0, 2, 0 },
                { 0, 2, 0, 2, 0, 2, 0, 2 },
                { 2, 0, 2, 0, 2, 0, 2, 0 }
            };
        }


        public static void CambiaGiocatore() => GiocatoreAttuale = (GiocatoreAttuale % 2) + 1;

        public static bool ControllaDentroBordi(int riga, int colonna) => riga >= 0 && riga < RigheColonne && colonna >= 0 && colonna < RigheColonne;

        static public void AttivaDisattivaCaselle(bool attiva)
        {
            foreach (Casella casella in Caselle)
                casella.Enabled = attiva;
        }

        public static void PulisciScacchieraMosse()
        {
            foreach (Casella casella in Caselle)
            {
                casella.BackColor = ColoreCasellaDefault(casella);
                if (casella.Text != "👑")
                    casella.Text = string.Empty;
            }
        }

        public static void ControllaPromozione(Casella casella)
        {
            int riga = casella.Location.Y;
            int colonna = casella.Location.X;
            int grandezzacasella = Casella.GrandezzaCasella;

            if (scacchiera[riga / grandezzacasella, colonna / grandezzacasella] == 1 && riga / grandezzacasella == RigheColonne - 1 ||
                    scacchiera[riga / grandezzacasella, colonna / grandezzacasella] == 2 && riga / grandezzacasella == 0)
                casella.Text = "👑";
        }

        public static Color ColoreCasellaDefault(Casella casella)
        {
            // per i neri
            if ((casella.Location.Y / Casella.GrandezzaCasella % 2) != 0)
            {
                if ((casella.Location.X / Casella.GrandezzaCasella % 2) == 0)
                    return Color.FromArgb(181, 136, 99);
            }
            if ((casella.Location.Y / Casella.GrandezzaCasella) % 2 == 0)
            {
                if ((casella.Location.X / Casella.GrandezzaCasella) % 2 != 0)
                    return Color.FromArgb(181, 136, 99);
            }

            // per i bianchi
            return Color.FromArgb(240, 217, 181);
        }

        public static bool ControllaMangiaDisponibile(int indiceRigaCorrente, int indiceColonnaCorrente, bool èMossaSemplice, int[] direzione)
        {
            bool mossaMangia = false;
            int colonna = indiceColonnaCorrente + 1;
            for (int riga = indiceRigaCorrente - 1; riga >= 0; riga--)
            {
                if (GiocatoreAttuale == 1 && èMossaSemplice && !ContinuaMossaOppureNo) break;
                if (direzione[0] == 1 && direzione[1] == -1 && !èMossaSemplice) break;
                if (ControllaDentroBordi(riga, colonna))
                {
                    if (scacchiera[riga, colonna] != 0 && scacchiera[riga, colonna] != GiocatoreAttuale)
                    {
                        mossaMangia = true;
                        if (!ControllaDentroBordi(riga - 1, colonna + 1))
                            mossaMangia = false;
                        else if (scacchiera[riga - 1, colonna + 1] != 0)
                            mossaMangia = false;
                        else return mossaMangia;
                    }
                }
                if (colonna < 7)
                    colonna++;
                else break;

                if (èMossaSemplice)
                    break;
            }

            colonna = indiceColonnaCorrente - 1;
            for (int riga = indiceRigaCorrente - 1; riga >= 0; riga--)
            {
                if (GiocatoreAttuale == 1 && èMossaSemplice && !ContinuaMossaOppureNo) break;
                if (direzione[0] == 1 && direzione[1] == 1 && !èMossaSemplice) break;
                if (ControllaDentroBordi(riga, colonna))
                {
                    if (scacchiera[riga, colonna] != 0 && scacchiera[riga, colonna] != GiocatoreAttuale)
                    {
                        mossaMangia = true;
                        if (!ControllaDentroBordi(riga - 1, colonna - 1))
                            mossaMangia = false;
                        else if (scacchiera[riga - 1, colonna - 1] != 0)
                            mossaMangia = false;
                        else return mossaMangia;
                    }
                }
                if (colonna > 0)
                    colonna--;
                else break;

                if (èMossaSemplice)
                    break;
            }

            colonna = indiceColonnaCorrente - 1;
            for (int riga = indiceRigaCorrente + 1; riga < 8; riga++)
            {
                if (GiocatoreAttuale == 2 && èMossaSemplice && !ContinuaMossaOppureNo) break;
                if (direzione[0] == -1 && direzione[1] == 1 && !èMossaSemplice) break;
                if (ControllaDentroBordi(riga, colonna))
                {
                    if (scacchiera[riga, colonna] != 0 && scacchiera[riga, colonna] != GiocatoreAttuale)
                    {
                        mossaMangia = true;
                        if (!ControllaDentroBordi(riga + 1, colonna - 1))
                            mossaMangia = false;
                        else if (scacchiera[riga + 1, colonna - 1] != 0)
                            mossaMangia = false;
                        else return mossaMangia;
                    }
                }
                if (colonna > 0)
                    colonna--;
                else break;

                if (èMossaSemplice)
                    break;
            }

            colonna = indiceColonnaCorrente + 1;
            for (int riga = indiceRigaCorrente + 1; riga < 8; riga++)
            {
                if (GiocatoreAttuale == 2 && èMossaSemplice && !ContinuaMossaOppureNo) break;
                if (direzione[0] == -1 && direzione[1] == -1 && !èMossaSemplice) break;
                if (ControllaDentroBordi(riga, colonna))
                {
                    if (scacchiera[riga, colonna] != 0 && scacchiera[riga, colonna] != GiocatoreAttuale)
                    {
                        mossaMangia = true;
                        if (!ControllaDentroBordi(riga + 1, colonna + 1))
                            mossaMangia = false;
                        else if (scacchiera[riga + 1, colonna + 1] != 0)
                            mossaMangia = false;
                        else return mossaMangia;
                    }
                }
                if (colonna < 7)
                    colonna++;
                else break;

                if (èMossaSemplice)
                    break;
            }
            return mossaMangia;
        }

        public static void MostraMossePossibili()
        {
            bool MangiaDisponibile = false;
            AttivaDisattivaCaselle(false);
            for (int riga = 0; riga < RigheColonne; riga++)
            {
                for (int colonna = 0; colonna < RigheColonne; colonna++)
                {
                    if (scacchiera[riga, colonna] == GiocatoreAttuale)
                    {
                        var èMossaSemplice = true;
                        if (Caselle[riga, colonna].Text == "👑")
                            èMossaSemplice = false;
                        else
                            èMossaSemplice = true;
                        if (ControllaMangiaDisponibile(riga, colonna, èMossaSemplice, new int[2] { 0, 0 }))
                        {
                            MangiaDisponibile = true;
                            Caselle[riga, colonna].Enabled = true;
                        }
                    }
                }
            }
            if (!MangiaDisponibile)
                AttivaDisattivaCaselle(true);
        }

        public static void PulisciScacchieraMosseSemplici(List<Casella> MosseSemplici)
        {
            if (MosseSemplici.Count > 0)
            {
                for (int i = 0; i < MosseSemplici.Count; i++)
                {
                    MosseSemplici[i].BackColor = ColoreCasellaDefault(MosseSemplici[i]);
                    MosseSemplici[i].Enabled = false;
                }
            }
        }
    }
}
