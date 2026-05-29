// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.OldSoul
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class OldSoul(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IHandOnAfterFriendlyMinionDied,
  IEntity
{
  public const string CardId = "BG34_231";
  public const string Text = "After {1} friendly minions die while this is in your hand, make this Golden. <i>({0} left!)</i>";
  public const string GoldenText = "After {1} friendly minions die while this is in your hand, make this Golden. <i>(Done!)</i>";

  public Action? HandOnAfterFriendlyMinionDied(Minion died)
  {
    return (Action) (() =>
    {
      if (this.golden)
        return;
      ++this.ScriptDataNum1;
      if (this.ScriptDataNum1 < 15)
        return;
      this.TryMakeGolden(true, (Entity) this);
    });
  }
}
