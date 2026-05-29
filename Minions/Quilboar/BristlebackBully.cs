// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.BristlebackBully
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class BristlebackBully(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG35_432";
  public const string Text = "<b>Taunt</b> <b>Deathrattle:</b> Get a <b>Blood Gem</b> that also gives a Quilboar <b>Taunt</b>.";
  public const string GoldenText = "<b>Taunt</b> <b>Deathrattle:</b> Get 2 <b>Blood Gems</b> that also give a Quilboar <b>Taunt</b>.";

  public Action<Minion> GetDeathrattle()
  {
    return (Action<Minion>) (minion =>
    {
      minion.AddBloodGemToFriendlyHand();
      if (!minion.golden)
        return;
      minion.AddBloodGemToFriendlyHand();
    });
  }
}
