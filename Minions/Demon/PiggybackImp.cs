// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.PiggybackImp
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class PiggybackImp(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG_AV_309";
  public const string Text = "<b>Deathrattle:</b> Summon a 4/1 Imp.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon two 4/1 Imps.";

  public Action<Minion> GetDeathrattle() => PiggybackImp.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      minion.TrySummonMinion(new Summon("BG_AV_309t"));
      if (!golden)
        return;
      minion.TrySummonMinion(new Summon("BG_AV_309t"));
    });
  }
}
