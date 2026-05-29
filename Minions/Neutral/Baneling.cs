// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.Baneling
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class Baneling(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG31_HERO_811t5";
  public const string Text = "<b>Deathrattle:</b> Deal damage equal to this minion's Attack to a random enemy minion. <i>(Morphs each turn!)</i>[x]<b>Deathrattle:</b> Deal damage equal to this minion's Attack to a random enemy minion.";
  public const string GoldenText = "<b>Deathrattle:</b> Deal damage equal to this minion's Attack to a random enemy minion, twice. <i>(Morphs each turn!)</i> [x]<b>Deathrattle:</b> Deal damage equal to this minion's Attack to a random enemy minion, twice.";

  public Action<Minion> GetDeathrattle() => Baneling.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int num = golden ? 2 : 1;
      int amount = minion.attack();
      for (int index = 0; index < num; ++index)
      {
        Minion target;
        if (minion.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>().TryGetRandom<Minion>(out target))
          minion.Simulator.ProcessDamage(amount, target, (Entity) minion);
      }
    });
  }
}
