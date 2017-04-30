using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace Glücksrad_Project
{
    public partial class Main : Form
    {
        
        int index = 0;
        public Main()
        {
            InitializeComponent();
        }

        private void Letter_Click(object sender, EventArgs e)
        {

        }

        private void btnTwist_Click(object sender, EventArgs e)
        {
            btnTwist.Enabled = false;
            string ergebnis = twist();
            btnTwist.Enabled = true;
        }

        private void LetterVocal_Click(object sender, EventArgs e)
        {

        }

        private void btnExtraTwist_Click(object sender, EventArgs e)
        {

        }

        private void btnGuessTerm_Click(object sender, EventArgs e)
        {

        }

        private string twist()
        {
            Bitmap[] glücksrad = { Resources.Glücksrad_0, Resources.Glücksrad_1, Resources.Glücksrad_2, Resources.Glücksrad_3, Resources.Glücksrad_4, Resources.Glücksrad_5, Resources.Glücksrad_6, Resources.Glücksrad_7, Resources.Glücksrad_8, Resources.Glücksrad_9, Resources.Glücksrad_10, Resources.Glücksrad_11, Resources.Glücksrad_12, Resources.Glücksrad_13, Resources.Glücksrad_14, Resources.Glücksrad_15, Resources.Glücksrad_16, Resources.Glücksrad_17, Resources.Glücksrad_18, Resources.Glücksrad_19, Resources.Glücksrad_20, Resources.Glücksrad_21, Resources.Glücksrad_22, Resources.Glücksrad_23 };
            string[] values = { "200", "400", "Aussetzen", "200", "100", "200", "50", "250", "Extradreh", "250", "50", "100", "50", "300", "Aussetzen", "150", "50", "100", "150", "300", "Bankrott", "1000", "100", "50" };
            Random rnd = new Random(DateTime.Now.Ticks.GetHashCode());
            int rounds = rnd.Next(2, 10) * trackBar1.Value;
            int i = rounds + 1;
            for (i = rounds; 0 < i; i--)
            {
                index++;
                if (index >= glücksrad.Length)
                {
                    index = 0;
                }
                picbWheelOfFortune.Image = glücksrad[index];
                Thread.Sleep(500 / i);
                SoundPlayer player = new SoundPlayer(Resources.turn);
                player.Play();
                Refresh();
                Invalidate();
            }
            return values[index];
            
        }
    }
}
