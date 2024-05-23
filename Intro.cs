using System;
using System.Drawing;
using System.Windows.Forms;

namespace dama
{
    public partial class Intro : Form
    {
        public Intro()
        {
            InitializeComponent();

            //grandezza form
            Width = 500;
            Height = 300;

            //abbellisco un po' la form
            #region
            BackColor = Color.FromArgb(181, 136, 99);
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            #endregion

            //titolo
            #region
            Label lbl = new Label();
            lbl.Text = "Dama";
            lbl.Font = new Font("Comic Sans MS", 56);
            lbl.AutoSize = true;
            lbl.Location = new Point(Width / 8, Height / 4);
            lbl.BackColor = Color.Transparent;
            Controls.Add(lbl);
            #endregion

            //immagine pezzo
            #region
            PictureBox damaimage = new PictureBox();
            damaimage.ImageLocation = Environment.CurrentDirectory + @"\img\pezzobianco.png";
            damaimage.SizeMode = PictureBoxSizeMode.StretchImage;
            damaimage.Location = new Point(lbl.Location.X + lbl.Size.Width + 20, lbl.Location.Y);
            damaimage.Size = new Size(100, 100);
            Controls.Add(damaimage);
            #endregion

            //timer intro
            #region
            introtimer.Interval = 2000;
            introtimer.Tick += EndIntro;
            introtimer.Start();
            #endregion
        }

        private void EndIntro(object sender, EventArgs e)
        {
            introtimer.Stop();
            //apriamo la form con dama
            Dama frm = new Dama();
            frm.Show();
            Hide();
        }

        static Timer introtimer = new Timer();
    }
}
