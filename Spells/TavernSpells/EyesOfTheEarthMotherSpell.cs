// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.TavernSpells.EyesOfTheEarthMotherSpell
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Spells.TavernSpells;

public class EyesOfTheEarthMotherSpell : ITavernSpell
{
  public const string CardId = "EBG_Spell_017";
  public const string Text = "Choose a friendly minion from Tier 4 or below. Make it Golden.";

  public void Cast(Entity source, Simulator simulator, Minion? _)
  {
    Minion minion;
    if (!source.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.CanBeMadeGolden() && x.tier <= 4 && x.IsAlive())).ToList<Minion>().TryGetRandom<Minion>(out minion))
      return;
    minion.TryMakeGolden(true, (Entity) null);
  }
}
