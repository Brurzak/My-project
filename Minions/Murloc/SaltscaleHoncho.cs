// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.SaltscaleHoncho
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class SaltscaleHoncho(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAfterFriendlyMinionSummoned,
  IEntity
{
  public const string CardId = "BG21_008";
  public const string Text = "After you summon a Murloc, give a friendly Murloc other than it +{1} Health.";
  public const string GoldenText = "After you summon a Murloc, give a friendly Murloc other than it +{1} Health.";

  public Action OnAfterFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      Minion minion;
      if (!summoned.IsMurloc() || !this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsMurloc() && x != summoned)).ToList<Minion>().TryGetRandom<Minion>(out minion))
        return;
      minion.IncreaseStats(0, this.DoubleIfGolden(2));
    });
  }
}
