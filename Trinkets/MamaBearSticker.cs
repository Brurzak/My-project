// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.MamaBearSticker
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Trinkets;

public class MamaBearSticker(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnFriendlyMinionSummoned,
  IEntity
{
  public const string CardId = "BG35_MagicItem_871";

  public Action? OnFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      if (!summoned.IsBeast())
        return;
      summoned.IncreaseStats(4, 4, (Entity) this);
    });
  }
}
