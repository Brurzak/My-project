// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.LesserBloodboundEarrings
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class LesserBloodboundEarrings(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnAfterAnySpellCast,
  IEntity
{
  public const string CardId = "BG32_MagicItem_808";
  private bool _initialized;
  private int _counter;

  public Action? OnAfterAnySpellCast(Entity? source, Minion? target)
  {
    return (Action) (() =>
    {
      if (source != null && source is Trinket)
        return;
      if (!this._initialized)
      {
        this._counter = this.ScriptDataNum1;
        this._initialized = true;
      }
      ++this._counter;
      if (this._counter < 5)
        return;
      foreach (Minion target1 in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>())
        this.Simulator.CastBloodGem(target1, (Entity) this);
      this._counter = 0;
    });
  }
}
