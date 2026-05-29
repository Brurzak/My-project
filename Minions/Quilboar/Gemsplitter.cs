// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.Gemsplitter
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class Gemsplitter(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionLostDiv,
  IEntity
{
  public const string CardId = "BG21_037";
  public const string Text = "<b>Divine Shield</b> After a friendly minion loses <b>Divine Shield</b>, get a <b>Blood Gem</b>.";
  public const string GoldenText = "<b>Divine Shield</b> After a friendly minion loses <b>Divine Shield</b>, get 2 <b>Blood Gems</b>.";

  public Action? OnFriendlyMinionLostDiv(Minion lost)
  {
    return (Action) (() =>
    {
      this.AddBloodGemToFriendlyHand();
      if (!this.golden)
        return;
      this.AddBloodGemToFriendlyHand();
    });
  }
}
