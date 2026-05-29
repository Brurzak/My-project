// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Duos.Sandy
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Duos;

public class Sandy(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BGDUO_125";
  public const string Text = "<b>Start of Combat:</b> Transform into a copy of your teammate's highest- Health minion.";
  public const string GoldenText = "<b>Start of Combat:</b> Transform into a Golden copy of your teammate's highest-Health minion.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      if (this.TeammateSide == null)
        throw new UnsupportedInteractionException("Teammate's board is unknown", (Entity) this);
      if (this.TeammateSide.Count <= 0)
        return;
      IGrouping<int, \u003C\u003Ef__AnonymousType2<int, Minion>> source = this.TeammateSide.Select(x => new
      {
        Health = x.health(),
        Minion = x
      }).GroupBy(x => x.Health).OrderByDescending<IGrouping<int, \u003C\u003Ef__AnonymousType2<int, Minion>>, int>(x => x.Key).FirstOrDefault<IGrouping<int, \u003C\u003Ef__AnonymousType2<int, Minion>>>();
      var data;
      if (source == null || !source.ToList().TryGetRandom(out data))
        return;
      Minion minion = data.Minion.Clone(this.Simulator);
      int index = this.BoardPosition();
      if (minion.CanBeMadeGolden())
        minion.golden = this.golden;
      this.FriendlySide.Remove((Minion) this);
      this.FriendlySide.Insert(index, minion);
    });
  }
}
