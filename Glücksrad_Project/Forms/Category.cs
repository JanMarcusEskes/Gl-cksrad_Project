using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glücksrad_Project
{

  public partial class Category : Form
  {
    public string Wort;
    Random random = new Random(DateTime.Now.Ticks.GetHashCode());
    String[] categoryGaming = { "Rockstar Games", "Fifa", "League of Legends", "Call of Duty", "Minecraft", "Diablo", "Overwatch", "Counterstrike", "Battlefield", "Zocker", "Sims", "Solitair", "Gamer", "Energydrink", "World of Warcraft", "Blizzard", "Tetris", "Pokemon", "Yu-Gi-Oh", "Fallout", "Super Mario" };
    String[] categoryMovies = { "Horrorfilm", "Jumpscare", "Adventure", "Thriller", "Action", "Comedy", "Fantasy", "Sci-Fi", "Drama", "Herr der Ringe", "Harry Potter", "Simpsons", "Spongebob Schwammkopf", "Tom und Jerry", "Caillou", "Star Wars", "The Transporter", "Transformers", "Iron Man", "Marvel", "Spiderman", "Batman", "Superman", "The Joker", "The Hobbit", "Der Hobbit", "The Lord of the Rings", "Scary Movie", "The Ring", "Adam Sandler", "Jason Statham", "George Clooney", "Mr Bean", "Eiskönigin", "Hangover", "Disney", "Die Schöne und das Biest", "Barbie", "Die sieben Zwerge" };
    String[] categoryFood = { "Tomate", "Gurke", "Erbsen", "Apfel", "Obst", "Gemüse", "Fruchteis", "Splasheis", "Schokoeis", "Vanilleeis", "Spaghettieis", "Banane", "McDonalds", "BurgerKing", "Burger", "FastFood", "Salat", "Döner", "Pommes", "Schnitzel", "Steak", "Schnitzel mit Jägersauce und Käsespätzle", "Nudelsalat", "Kartoffelsalat", "Gummibärchen", "Chips", "Snack", "Schokoriegel", "Mars", "Snickers", "Twix", "MilkyWay", "Amerragout", "Kotelett", "Pizza", "Nudeln", "Nutella", "Lutscher", "Brot", "Toast", "Brötchen", "Käse", "Salami", "Schinken" };
    String[] categorySport = { "Fußball", "Basketball", "Football", "Eishockey", "Bayern München", "Borussia Mönchengladbach", "Dortmund", "Schalke", "RB Leipzig", "RealMadrid", "FC Barcelona", "FC Köln", "Superbowl", "Wrestling", "Boxen", "Tischtennis", "Philipp Lahm", "Bastian Schweinsteiger", "Thomas Müller", "Cristiano Ronaldo", "Manuel Neuer", "Rene Adler", "Oliver Kahn", "Toni Kroos", "Jogi Löw", "Lionel Messi", "Marco Reus", "Mats Hummels", "Mario Götze", "Andre Schürrle", "Robert Lewandowski" };
    String[] categoryMusic = { "Adele", "Adel Tawil", "Die Toten Hosen", "Die Ärzte", "Böhse Onkelz", "Pink Fluffy Unicorn", "30 Seconds to Mars", "ACDC", "Alligatoah", "Avicii", "Billy Talent", "Cro", "Dame", "Disturbed", "Green Day", "Linkin Park", "Metallica", "Papa Roach", "Rammstein", "Rihanna", "Rise Against", "Sarah Connor", "Silbermond", "Sido", "Slipknot", "Sunrise Avenue", "Xavier Naidoo", "Elektro", "Techno", "Dubstep", "House", "Nightcore", "Metal", "Pop", "Rock", "Metalcore", "Schlager", "Rap", "HipHop" };
    public Category()
    {
      InitializeComponent();
      //ArrayList categories = ;
    }

    private void btnCat1_Click(object sender, EventArgs e)
    {
      int zufall;
      do
      {
        zufall = random.Next(0, 100);
      } while (zufall > categoryGaming.Length);


      Wort = categoryGaming[zufall];
      Close();

    }

    private void btnCat6_Click(object sender, EventArgs e)
    {
      ArrayList categories = new ArrayList { categoryGaming, categoryFood, categoryMovies, categoryMusic, categorySport };
      object Temp = categories[random.Next(0, 4)];
      string[] categoryLocal = null;
      if (Temp is string[])
         categoryLocal = (string[])Temp;
      int zufall;
      do
      {
        zufall = random.Next(0, 100);
      } while (zufall > categoryLocal.Length);


      Wort = categoryLocal[zufall];
      Close();
    }

    private void btnCat2_Click(object sender, EventArgs e)
    {
      int zufall;
      do
      {
        zufall = random.Next(0, 100);
      } while (zufall > categoryMovies.Length);


      Wort = categoryMovies[zufall];
      Close();
    }

    private void btnCat3_Click(object sender, EventArgs e)
    {
      int zufall;
      do
      {
        zufall = random.Next(0, 100);
      } while (zufall > categoryFood.Length);


      Wort = categoryFood[zufall];
      Close();
    }

    private void btnCat4_Click(object sender, EventArgs e)
    {
      int zufall;
      do
      {
        zufall = random.Next(0, 100);
      } while (zufall > categorySport.Length);


      Wort = categorySport[zufall];
      Close();
    }

    private void btnCat5_Click(object sender, EventArgs e)
    {
      int zufall;
      do
      {
        zufall = random.Next(0, 100);
      } while (zufall > categoryMusic.Length);


      Wort = categoryMusic[zufall];
      Close();
    }
  }
}
