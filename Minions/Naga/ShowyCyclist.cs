// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Naga.ShowyCyclist
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Naga;

public class ShowyCyclist(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnSetupPlayerStateCounters,
  IEntity,
  IDeathrattle
{
  public const string CardId = "BG31_925";
  public const string Text = "<b>Deathrattle:</b> Give your Naga +{1}/+{1}. <i>(Improved by every 4 spells you've cast this game!)</i>4[x]<b>Deathrattle:</b> Give your Naga +{1}/+{1}. <i>(Cast {2}/4 spells to improve!)</i>";
  public const string GoldenText = "<b>Deathrattle:</b> Give your Naga +{1}/+{1}. <i>(Improved by every 4 spells you've cast this game!)</i>4[x]<b>Deathrattle:</b> Give your Naga +{1}/+{1}. <i>(Cast {2}/4 spells to improve!)</i>";

  public void OnSetupPlayerStateCounters(GameState.PlayerState playerState)
  {
    if (this.ScriptDataNum2 <= 0 || playerState.AnySpellCounter != 0)
      return;
    playerState.AnySpellCounter = this.ScriptDataNum2 * (this.golden ? 2 : 4);
  }

  public Action<Minion> GetDeathrattle() => ShowyCyclist.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int by = (minion.ControlledByPlayer ? minion.Simulator.state.Player : minion.Simulator.state.Opponent).AnySpellCounter / (golden ? 2 : 4);
      foreach (Minion minion1 in minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsNaga())))
        minion1.IncreaseStats(by);
    });
  }
}
