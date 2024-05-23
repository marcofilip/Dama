using System;
using System.Drawing;
using System.Windows.Forms;

namespace dama
{
    public partial class FormVincitore : Form
    {
        public FormVincitore(string vincitore)
        {
            InitializeComponent();

            Width = 500;
            Height = 120;

            Label lbl = new Label();
            lbl.AutoSize = true;
            lbl.ForeColor = Color.Black;
            lbl.BackColor = Color.Gold;
            lbl.Text = $"Giocatore {vincitore} ha vinto!";
            lbl.Location = new Point(20, 20);
            lbl.Font = new Font("Comic Sans MS", 24);

            BackColor = Color.Goldenrod;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;

            Controls.Add(lbl);

            timer.Interval = 2000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        static Timer timer = new Timer();

        private void Timer_Tick(object sender, EventArgs e) => Close();
    }
}
