// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.DisguisedGraverobber
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class DisguisedGraverobber(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG28_303";
  public const string Text = "<b>Battlecry:</b> Destroy a friendly Undead to get a plain copy of it.";
  public const string GoldenText = "<b>Battlecry:</b> Destroy a friendly Undead to get 2 plain copies of it.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      Minion source;
      if (!this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsUndead() && x.IsAlive())).ToList<Minion>().TryGetRandom<Minion>(out source))
        return;
      this.Simulator.Destroy((Minion) this, (Entity) source);
      this.AddMinionToFriendlyHand(source.CardID);
      if (!this.golden)
        return;
      this.AddMinionToFriendlyHand(source.CardID);
    });
  }
}
