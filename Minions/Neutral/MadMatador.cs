// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.MadMatador
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class MadMatador(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  ITryPreventDamage,
  IEntity
{
  public const string CardId = "BG28_404";
  public const string Text = "<b><b>Taunt</b>.</b> When this would take damage, deal it to a random enemy minion instead. <i>(Once per combat.)</i>";
  public const string GoldenText = "<b><b>Taunt</b>.</b> When this would take damage, deal it to a random enemy minion instead. <i>(Twice per combat.)</i>";
  private int _triggers;

  public bool TryPreventDamage(int amount, out Action? action)
  {
    action = (Action) null;
    Minion target;
    if (this._triggers >= this.DoubleIfGolden(1) || !this.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>().TryGetRandom<Minion>(out target))
      return false;
    ++this._triggers;
    action = (Action) (() => this.Simulator.ProcessDamage(amount, target, (Entity) this));
    return true;
  }
}
