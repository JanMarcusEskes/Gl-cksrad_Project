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
      Category category = new Category();
      category.ShowDialog();
      Wort = category.Wort.ToLower();
      txtGuessTerm.Text = "";
      WortKette = Wort.ToLower().ToCharArray();
      Sternchen = new char[Wort.Length];
      lblRound.Text = "Spiel Nummer: " + gameno.ToString();
      TurnNeeded();
      Verschluesseln();
      output();
      startMoney();
      RoundRating();
      nextPlayer();
    }
    //Erstellt aus Buchstaben "_"
    private void Verschluesseln()
    {
      for (int x = 0; x < Gesamtkonten.Length; x++)
      {
        txtRating.Text += "Spieler " + (x + 1) + " " + Environment.NewLine + Gesamtkonten[x].ToString() + Environment.NewLine + Environment.NewLine;
      }

      for (int i = 0; i < Wort.Length; i++)
      {
        Sternchen[i] = '_';
      }
    }
    //Deaktiviert Buttons
    private void TurnNeeded()
    {
      foreach (Control button in gpbConsonants.Controls)
        button.Enabled = false;

    }
    //Wenn ein Konsonant gedrückt wird
    private void Letter_Click(object sender, EventArgs e)
    {
      Button auslöser = (Button)sender;
      Guess = Convert.ToChar(auslöser.Text.ToLower());
      auslöser.Enabled = false;
      consonantsActivated.Add(auslöser);
      checkLetter();
    }
    //Wenn der "Drehen" Button gedrückt wird
    private void btnTwist_Click(object sender, EventArgs e)
    {

      btnTwist.Enabled = false;
      ergebnis = twist();
      Turn = true;
      checkTurn();
      RoundRating();
      //btnTwist.Enabled = true;
      
    }
    //Wenn ein Vokal gedrückt wird
    private void LetterVocal_Click(object sender, EventArgs e)
    {
      if (Rundenkonto[Player] >= 200)
      {
        Rundenkonto[Player] -= 200;
        Button auslöser = (Button)sender;
        Guess = Convert.ToChar(auslöser.Text.ToLower());
        auslöser.Enabled = false;
        checkLetter();
      }
      else
        MessageBox.Show("Sie haben zu wenig Geld", "Vokal kaufen", MessageBoxButtons.OK , MessageBoxIcon.Information);
    }
    //Wenn der Button "Raten" gedrückt wird
    private void btnGuessTerm_Click(object sender, EventArgs e)
    {
      if (txtGuessTerm.Text.ToLower() == Wort)
      {
        lblTerm.Text = Wort;
        MessageBox.Show("Sie haben das Spiel gewonnen!");
        guthaben();
        nextGame();
      }
      else
      {
        nextPlayer();
      }
    }
    //Dreht das Rad und gibt Wert zurück
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
    //Gibt Text in TextBox aus
    private void output()
    {
      lblTerm.Text = "";
      for (int i = 0; i < Sternchen.Length; i++)
      {
        lblTerm.Text += Sternchen[i] + " ";
      }
    }
    //Prüft ob der gedrückte Buchstabe im Begriff vorkommt
    private void checkLetter()
    {
      for (int i = 0; i < WortKette.Length; i++)
      {
        if (WortKette[i] == Guess)
        {
          lblTerm.Text = "";
          Sternchen[i] = WortKette[i];
          for (int z = 0; z < Sternchen.Length; z++)
          {
            lblTerm.Text += Sternchen[z]+ " ";
          }
          letterChanged = true;
          LetterRight();
        }
      }
      if (letterChanged == false)
      {
        nextPlayer();
      }
      else
      {

        letterChanged = false;
      }

      if (lblTerm.Text.ToLower() == Wort)
      {
        lblTerm.Text = Wort;
        MessageBox.Show("Sie haben das Spiel gewonnen!", "Glücksrad", MessageBoxButtons.OK, MessageBoxIcon.Information);
        guthaben();
        nextGame();
      }
    }
    //Gibt jede Runde das Startguthaben
    private void startMoney()
    {
      for (int i = 0; i < Rundenkonto.Length; i++)
      {
        Rundenkonto[i] = 100;
      }
    }
    //Füllt die "Rundenkonto" TextBox
    private void RoundRating()
    {
      txtRoundRating.Text = "";

      for (int x = 0; x < Rundenkonto.Length; x++)
      {
        txtRoundRating.Text += "Spieler " + (x + 1) + " " + Environment.NewLine + Rundenkonto[x].ToString() + Environment.NewLine + Environment.NewLine;
      }
    }
    //Füllt die "GesamtGuthaben" TextBox
    private void guthaben()
    {
      Gesamtkonten[Player] = Rundenkonto[Player];

      {
        txtRating.Text = "";
        for (int x = 0; x < Gesamtkonten.Length; x++)
        {
          txtRating.Text += "Spieler " + (x + 1) + " " + Environment.NewLine + Gesamtkonten[x].ToString() + Environment.NewLine + Environment.NewLine;
        }
      }



    }
    //Nach jedem Zug des Spielers + 
    private void nextPlayer()
    {
      if (extraTwist)
      {
        DialogResult auswahl = MessageBox.Show("Wollen Sie noch einmal derhen?", "Extra-Dreh", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (auswahl == DialogResult.Yes)
        {
          Player--;
        }
      }
      lblExtraTwist.Text = "";
      TurnNeeded();
      btnTwist.Enabled = true;
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
    private void nextGame()
    {
      foreach (Control button in gpbConsonants.Controls)
        button.Enabled = Enabled;
      foreach (Control button in gpbVocal.Controls)
        button.Enabled = Enabled;
      consonantsActivated = new List<Button>();
      Category category = new Category();
      category.ShowDialog();
      Wort = category.Wort.ToLower();
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
    private void checkTurn()
    {
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
    private void checkGameNo()
    {
      if (gameno > 6)
      {
        MessageBox.Show("Das Spiel ist zuende! Gewonnen hat: ", "Spiel vorbei", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }
    private void LetterRight()
    {

      Zugkonto = Convert.ToInt32(ergebnis);
      Rundenkonto[Player] += Zugkonto;
      Zugkonto = 0;
      RoundRating();
    }
  }
}