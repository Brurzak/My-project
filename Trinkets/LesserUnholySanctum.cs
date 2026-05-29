// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.LesserUnholySanctum
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class LesserUnholySanctum(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnFriendlyDeathrattle,
  IEntity
{
  public const string CardId = "BG32_MagicItem_862";

  public Action OnFriendlyDeathrattle()
  {
    return (Action) (() => this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).LastOrDefault<Minion>()?.IncreaseStats(3, 2));
  }
}
