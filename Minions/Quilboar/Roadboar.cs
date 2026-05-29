// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.Roadboar
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class Roadboar(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG20_101";
  public const string Text = "<b>Rally:</b> Get 2 <b>Blood Gems</b>.";
  public const string GoldenText = "<b>Rally:</b> Get 4 <b>Blood Gems</b>.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      int num = isGolden ? 4 : 2;
      for (int index = 0; index < num; ++index)
        minion.AddBloodGemToFriendlyHand();
    });
  }
}
