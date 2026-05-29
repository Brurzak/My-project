// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.TimewarpedThorncaller
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class TimewarpedThorncaller(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity,
  IBattlecry
{
  public const string CardId = "BG34_Giant_078";
  public const string Text = "<b>Battlecry and Deathrattle:</b> Get a Blood Gem Barrage.";
  public const string GoldenText = "<b>Battlecry and Deathrattle:</b> Get 2 Blood Gem Barrages.";

  public Action OnBattlecry() => (Action) (() => this.GetDeathrattle()((Minion) this));

  public Action<Minion> GetDeathrattle() => TimewarpedThorncaller.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      minion.AddBloodGemToFriendlyHand();
      if (!golden)
        return;
      minion.AddBloodGemToFriendlyHand();
    });
  }
}
