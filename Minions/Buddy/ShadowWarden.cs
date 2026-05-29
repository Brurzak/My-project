// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.ShadowWarden
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class ShadowWarden(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "TB_BaconShop_HERO_62_Buddy";
  public const string Text = "<b>Battlecry:</b> Your next Hero Power makes the target Golden.";
  public const string GoldenText = "<b>Battlecry:</b> Your next 2 Hero Powers make the target Golden.";

  public Action? OnBattlecry() => (Action) null;
}
