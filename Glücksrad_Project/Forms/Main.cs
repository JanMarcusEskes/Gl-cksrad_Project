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
        //position grafik
        int index = 0;
        //zu ratendes wort
        string Wort = String.Empty;
        //wort als char[]
        char[] WortKette;
        char[] Sternchen;
        //geratener Buchstabe
        char Guess;
        //aktueller Spieler
        int Player = -1;
        bool letterChanged = false;
        bool youLoose = false;
        bool yesItChanged = false;
        bool extraTwist = false;
        //Spielerkonten
        int[] Gesamtkonten = new int[3];
        int[] Rundenkonto = new int[3];
        //Schon gecklickte Buttons
        List<Button> consonantsActivated = new List<Button>();
        //erdrehter betrag
        int Zugkonto;
        string ergebnis;
        bool Turn = false;
        int gameno = 1;
        public Main()
        {
            InitializeComponent();
            //Form zur auswahl der Kategorie
            Category category = new Category();
            category.ShowDialog();
            //Eigenschaft aus "category" wird in "Wort" geschrieben
            Wort = category.Wort.ToLower();
            //TextBox leeren
            txtGuessTerm.Text = "";
            //"WortKette" erstellen
            WortKette = Wort.ToLower().ToCharArray();
            Sternchen = new char[Wort.Length];
            //Schildbeschriftung
            lblRound.Text = "Spiel Nummer: " + gameno.ToString();
            TurnNeeded();
            Verschluesseln();
            output();
            startMoney();
            RoundRating();
            nextPlayer();
        }
        /// <summary>
        /// Trägt die Gesamtkonten ein und "Verschlüsselt" den Begriff
        /// </summary>
        private void Verschluesseln()
        {
            //Konten eintragen
            for (int x = 0; x < Gesamtkonten.Length; x++)
            {
                txtRating.Text += "Spieler " + (x + 1) + " " + Environment.NewLine + Gesamtkonten[x].ToString() + Environment.NewLine + Environment.NewLine;
            }
            //Wort "verschlüsseln"
            for (int i = 0; i < Wort.Length; i++)
            {
                Sternchen[i] = '_';
            }
        }
        /// <summary>
        /// Deaktiviert alle Buttons in der gpbConsonants GroupBox
        /// </summary>
        private void TurnNeeded()
        {
            foreach (Control button in gpbConsonants.Controls)
                button.Enabled = false;
        }
        /// <summary>
        /// Löst aus, wenn ein Konsonant oder Sonderzeichen gedrückt wird
        /// </summary>
        private void Letter_Click(object sender, EventArgs e)
        {
            //"sender" wird in auslöser geschrieben
            Button auslöser = (Button)sender;
            //geratener Buchstabe wir gespeichert
            Guess = Convert.ToChar(auslöser.Text.ToLower());
            //auslöser wird deaktiviert
            auslöser.Enabled = false;
            //auslöser wird in Liste geadded, damit er nichtmehr aktiviert wird
            consonantsActivated.Add(auslöser);
            checkLetter();
        }
        /// <summary>
        /// Löst aus wenn der "Drehen" Button gedrückt wird
        /// </summary>
        private void btnTwist_Click(object sender, EventArgs e)
        {
            //Button deaktivieren
            btnTwist.Enabled = false;
            //Rad wird gedreht und das Ergebnis wird in ergebnis geschrieben
            ergebnis = twist();
            Turn = true;
            checkTurn();
            RoundRating();

        }
        /// <summary>
        /// Löst aus, wenn ein Vokal gedrückt wird
        /// </summary>
        private void LetterVocal_Click(object sender, EventArgs e)
        {
            //Prüfen, ob genug Geld vorha´nden ist
            if (Rundenkonto[Player] >= 200)
            {
                //Geldbetrag abziehen
                Rundenkonto[Player] -= 200;
                //sender wird in auslöser geschrieben
                Button auslöser = (Button)sender;
                //Erratener Buchstabe wird gespeichert
                Guess = Convert.ToChar(auslöser.Text.ToLower());
                //auslöser wird deaktiviert
                auslöser.Enabled = false;
                checkLetter();
            }
            else
                //Zu wenig Geld
                MessageBox.Show("Sie haben zu wenig Geld", "Vokal kaufen", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Löst aus wenn "Begriff raten" gedrückt wurde
        /// </summary>
        private void btnGuessTerm_Click(object sender, EventArgs e)
        {
            //Prüfen, ob der richtige Begriff eingegeben wurde
            if (txtGuessTerm.Text.ToLower() == Wort)
            {
                //Begriff wird angezeigt
                lblTerm.Text = Wort;
                //Gewinnmeldung
                MessageBox.Show("Sie haben das Spiel gewonnen!", "Glücksrad", MessageBoxButtons.OK, MessageBoxIcon.Information);
                guthaben();
                nextGame();
            }
            else
            {
                nextPlayer();
            }
        }
        /// <summary>
        /// "Dreht" das Glücksrad und gibt das Ergebnis als string zurück
        /// </summary>
        private string twist()
        {
            //ResourceArray wird erstellt
            Bitmap[] glücksrad = { Resources.Glücksrad_0, Resources.Glücksrad_1, Resources.Glücksrad_2, Resources.Glücksrad_3, Resources.Glücksrad_4, Resources.Glücksrad_5, Resources.Glücksrad_6, Resources.Glücksrad_7, Resources.Glücksrad_8, Resources.Glücksrad_9, Resources.Glücksrad_10, Resources.Glücksrad_11, Resources.Glücksrad_12, Resources.Glücksrad_13, Resources.Glücksrad_14, Resources.Glücksrad_15, Resources.Glücksrad_16, Resources.Glücksrad_17, Resources.Glücksrad_18, Resources.Glücksrad_19, Resources.Glücksrad_20, Resources.Glücksrad_21, Resources.Glücksrad_22, Resources.Glücksrad_23 };
            //Zugehörige Werte werden angelegt
            string[] values = { "200", "400", "Aussetzen", "200", "100", "200", "50", "250", "Extradreh", "250", "50", "100", "50", "300", "Aussetzen", "150", "50", "100", "150", "300", "Bankrott", "1000", "100", "50" };
            //Zufallsgenerator wird initialisiert
            Random rnd = new Random(DateTime.Now.Ticks.GetHashCode());
            //Anzahl der Umdrehungen wird mit der TrackBar multipliziert
            int rounds = rnd.Next(2, 10) * trackBar1.Value;
            //Zählvariabele wird angelegt
            int i = rounds + 1;
            for (i = rounds; 0 < i; i--)
            {
                //Position des Glücksrades speichern
                index++;
                //Bei zu großem Index "index" auf 0 setzen
                if (index >= glücksrad.Length)
                {
                    index = 0;
                }
                //PictureBox wird das nächste Bild zugewiesen
                picbWheelOfFortune.Image = glücksrad[index];
                //Sorgt dafür, dass das Rad langsamer wird
                Thread.Sleep(500 / i);
                //Initialisiert das "Klappern"
                SoundPlayer player = new SoundPlayer(Resources.turn);
                //Spielt es ab
                player.Play();
                //Aktualisiert Die Form
                Refresh();
                Invalidate();
            }
            //Gibt den erdrehten Wert zurück
            return values[index];

        }
        /// <summary>
        /// Gibt "Sternchen" in "lblTerm" aus
        /// </summary>
        private void output()
        {
            //Reset von lblTerm
            lblTerm.Text = "";
            for (int i = 0; i < Sternchen.Length; i++)
            {
                lblTerm.Text += Sternchen[i] + " ";
            }
        }
        /// <summary>
        /// Prüft, ob der eingegebene Buchstabe im Begriff vorkommt
        /// </summary>
        private void checkLetter()
        {
            //Geht den ganzen Begriff durch
            for (int i = 0; i < WortKette.Length; i++)
            {
                //Wenn ein Buchstabe gefunden wurde .. 
                if (WortKette[i] == Guess)
                {
                    //wird lblTerm zurückgesetzt
                    lblTerm.Text = "";
                    //Der Unterstrich ausgetauscht, durch den echten Buchstaben
                    Sternchen[i] = WortKette[i];
                    //und lblTerm neu geschrieben
                    for (int z = 0; z < Sternchen.Length; z++)
                    {
                        lblTerm.Text += Sternchen[z] + " ";
                    }
                    letterChanged = true;
                    LetterRight();
                }
            }
            //Prüft ob die Prüfung erfolgreich war
            if (letterChanged == false)
            {
                nextPlayer();
            }
            else
            {
                letterChanged = false;
            }
            //Prüft, ob der Begriff komplett angezeigt wird
            if (lblTerm.Text.ToLower() == Wort)
            {
                //wort wird in lblTerm geschrieben
                lblTerm.Text = Wort;
                //Gewinnmitteilung
                MessageBox.Show("Sie haben das Spiel gewonnen!", "Glücksrad", MessageBoxButtons.OK, MessageBoxIcon.Information);
                guthaben();
                nextGame();
            }
        }
        /// <summary>
        /// Setzt das Startguthaben des Rundenkontos fest
        /// </summary>
        private void startMoney()
        {
            for (int i = 0; i < Rundenkonto.Length; i++)
            {
                Rundenkonto[i] = 100;
            }
        }
        /// <summary>
        /// Schreibt die Rundenkonten in txtRoundRating
        /// </summary>
        private void RoundRating()
        {
            txtRoundRating.Text = "";

            for (int x = 0; x < Rundenkonto.Length; x++)
            {
                txtRoundRating.Text += "Spieler " + (x + 1) + " " + Environment.NewLine + Rundenkonto[x].ToString() + Environment.NewLine + Environment.NewLine;
            }
        }
        /// <summary>
        /// Überweist das Rundenkonto des Gewinners auf sein Gesamtkonto und gibt diese dann neu aus
        /// </summary>
        private void guthaben()
        {

            Gesamtkonten[Player] = Rundenkonto[Player];
            {
                txtRating.Text = "";
                //Schreibt Gesamtkonten neu
                for (int x = 0; x < Gesamtkonten.Length; x++)
                {
                    txtRating.Text += "Spieler " + (x + 1) + " " + Environment.NewLine + Gesamtkonten[x].ToString() + Environment.NewLine + Environment.NewLine;
                }
            }



        }
        /// <summary>
        /// Fragt bei einem Extradreh nach selbigem, fragt dann den Spieler ab und setzt das lblPlayerTurn
        /// </summary>
        private void nextPlayer()
        {
            // Bei extraTwist nach einlösung fragen
            if (extraTwist)
            {
                //Abfrage durch MessageBox
                DialogResult auswahl = MessageBox.Show("Wollen Sie noch einmal derhen?", "Extra-Dreh", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (auswahl == DialogResult.Yes)
                {
                    Player--;
                }
            }
            extraTwist = false;
            //Setzt lblExtraTwist zurück
            lblExtraTwist.Text = "";
            TurnNeeded();
            btnTwist.Enabled = true;
            //Setzen der lblTurn Pfeile
            Player++;
            if (Player >= 3)
            {
                Player = 0;
            }

            if (Player == 0)
            {
                lblPlayer1Turn.Show();
                lblPlayer2Turn.Hide();
                lblPlayer3Turn.Hide();
            }
            else if (Player == 1)
            {
                lblPlayer2Turn.Show();
                lblPlayer1Turn.Hide();
                lblPlayer3Turn.Hide();
            }
            else if (Player == 2)
            {
                lblPlayer3Turn.Show();
                lblPlayer1Turn.Hide();
                lblPlayer2Turn.Hide();
            }

        }
        /// <summary>
        /// Prüft ob beim drehen des Glücksrades ein "Spezialfeld" ausgewählt wurde und wenn ja welches
        /// </summary>
        private void turn()
        {
            btnTwist.Enabled = false;
            if (ergebnis == "Aussetzen")
            {
                Zugkonto = 0;
                nextPlayer();

                youLoose = true;
                btnTwist.Enabled = true;
            }
            else if (ergebnis == "Extradreh")
            {
                btnTwist.Enabled = true;
                lblExtraTwist.Text = "Extra-Dreh";
                extraTwist = true;
            }
            else if (ergebnis == "Bankrott")
            {
                Rundenkonto[Player] = 0;
                Zugkonto = 0;
                nextPlayer();
                youLoose = true;
                btnTwist.Enabled = true;
            }
            else
            {

            }
        }
        /// <summary>
        /// Dient zum "Reset" der Anwendung, wenn eine Runde abgeschlossen ist
        /// </summary>
        private void nextGame()
        {
            //Aktiviert alle Buttons
            foreach (Control button in gpbConsonants.Controls)
                button.Enabled = Enabled;
            foreach (Control button in gpbVocal.Controls)
                button.Enabled = Enabled;
            //Resettet alle eingegebenen Konsoonanten
            consonantsActivated = new List<Button>();
            //Fragt nach einer neuen Kategorie
            Category category = new Category();
            category.ShowDialog();
            //Holt den gewählten Begriff aus der Form 
            Wort = category.Wort.ToLower();
            //Setzt den Begriff in "Main" zurück
            txtGuessTerm.Text = "";
            WortKette = Wort.ToLower().ToCharArray();
            Sternchen = new char[Wort.Length];
            TurnNeeded();
            Verschluesseln();
            output();
            startMoney();
            RoundRating();
            nextPlayer();
            gameno++;
            checkGameNo();
            lblRound.Text = "Spiel Nummer: " + gameno.ToString();
        }
        /// <summary>
        /// Reaktiviert alle Buttons außer denen in consonantsActivaded
        /// </summary>
        private void checkTurn()
        {
            //Kann theoretisch nicht auftreten (Relikt früherer Zeit)
            if (Turn == false)
            {
                MessageBox.Show("Du musst zuerst drehen.", "Glücksrad", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                turn();
                if (youLoose == false)
                {
                    foreach (Control button in gpbConsonants.Controls)
                        if (!consonantsActivated.Contains(button))
                            button.Enabled = true;
                }
                else
                {
                    youLoose = false;
                }
            }
        }
        /// <summary>
        /// Prüft ob über 6 Runden gespielt wurden und gibt, wenn ja den Gewinner aus
        /// </summary>
        private void checkGameNo()
        {
            if (gameno > 6)
            {
                //ermitteln des besten Spielers
                int bestPlayer = -1, max = int.MinValue;
                for (int i = 0; i < Gesamtkonten.Length; i++)
                    if (Gesamtkonten[i] > max)
                    {
                        max = Gesamtkonten[i];
                        bestPlayer = i+1;
                    }
                //Gewinnmeldung
                MessageBox.Show("Das Spiel ist zuende! Gewonnen hat: Spieler " + bestPlayer.ToString(), "Spiel vorbei", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Löst aus wenn ein richtiger Buchstabe erkannt wurde und addiert die erdrehten Punkte
        /// </summary>
        private void LetterRight()
        {
            Zugkonto = Convert.ToInt32(ergebnis);
            Rundenkonto[Player] += Zugkonto;
            Zugkonto = 0;
            RoundRating();
        }
    }
}