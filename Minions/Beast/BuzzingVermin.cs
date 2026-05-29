// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.BuzzingVermin
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class BuzzingVermin(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG31_803";
  public const string Text = "<b>Taunt</b> <b>Deathrattle:</b> Summon a {0}/{1} Beetle.";
  public const string GoldenText = "<b>Taunt</b> <b>Deathrattle:</b> Summon two {0}/{1} Beetles.";

  public Action<Minion> GetDeathrattle() => BuzzingVermin.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      minion.TrySummonMinion((Summon) "BG28_603t");
      if (!golden)
        return;
      minion.TrySummonMinion((Summon) "BG28_603t");
    });
  }
}
