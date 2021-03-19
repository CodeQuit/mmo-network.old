//Copyright DarkTeam.zapto.org
using System.Windows.Forms;

namespace AutoUpdater
{
    public static class Tools
    {
        public static DialogResult InfoBowShow(string text)
        {
            return MessageBox.Show(text, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
