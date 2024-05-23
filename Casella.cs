using System.Drawing;
using System.Windows.Forms;

namespace dama
{
    public class Casella : Button
    {
        public static int GrandezzaCasella = 75;

        public Casella() : base()
        {
            Font = new Font("Comic Sans MS", 16);
            FlatStyle = FlatStyle.Flat;
        }
    }
}
