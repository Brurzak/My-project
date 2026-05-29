// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.GlowscalePortrait
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class GlowscalePortrait(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnAfterSpellcraftSpellCast,
  IEntity
{
  public const string CardId = "BG30_MagicItem_548";

  public Action? OnAfterSpellcraftSpellCast(Minion? target)
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.hasDiv)).ToList<Minion>())
        minion.IncreaseStats(3, 3, (Entity) this);
    });
  }
}
