// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.TwilightHatchling
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class TwilightHatchling(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_630";
  public const string Text = "<b>Deathrattle:</b> Summon a {0}/{1} Whelp that attacks immediately.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon two {0}/{1} Whelps that attack immediately.";
  public const string SummonCardId = "BG34_630t";

  public Action<Minion> GetDeathrattle() => TwilightHatchling.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      using (minion.Simulator.state.TriggerScope.New(nameof (TwilightHatchling)))
      {
        minion.TrySummonMinion((Summon) "BG34_630t");
        if (!golden)
          return;
        minion.TrySummonMinion((Summon) "BG34_630t");
      }
    });
  }
}
