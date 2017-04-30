using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glücksrad_Project.Source
{
  public class Player
  {
    bool extraTwist = false;
    bool suspended = false;
    int roundCurrent = 100;
    int globalCurrent = 0;
    int twistCurrent = 0;
    string name = "Spieler";

    public bool ExtraTwist
    {
      get
      {
        return extraTwist;
      }
      set
      {
        extraTwist = value;
      }
    }
    public int RoundCurrent
    {
      get
      {
        return roundCurrent;
      }
      set
      {
        roundCurrent = value;
      }
    }
    public int GlobalCurrent
    {
      get
      {
        return globalCurrent;
      }
      set
      {
        globalCurrent = value;
      }
    }
    public int TwistCurrent
    {
      get
      {
        return twistCurrent;
      }
      set
      {
        twistCurrent = value;
      }
    }
    public string Name
    {
      get
      {
        return name;
      }
      set
      {
        name = value;
      }
    }


  }
}
