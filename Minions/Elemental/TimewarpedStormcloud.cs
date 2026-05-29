// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Elemental.TimewarpedStormcloud
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Elemental;

public class TimewarpedStormcloud(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity,
  IAvenge
{
  public const string CardId = "BG34_PreMadeChamp_031";
  public const string Text = "<b>Deathrattle and Avenge ({0}):</b> Get a Tavern Tempest.";
  public const string GoldenText = "<b>Deathrattle and Avenge ({0}):</b> Get 2 Tavern Tempests.";

  public int AvengeRequirement => 3;

  public Action? OnAvenge() => (Action) (() => this.GetDeathrattle()((Minion) this));

  public Action<Minion> GetDeathrattle() => TimewarpedStormcloud.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      minion.AddMinionToFriendlyHand("BGS_123");
      if (!minion.golden)
        return;
      minion.AddMinionToFriendlyHand("BGS_123");
    });
  }
}
