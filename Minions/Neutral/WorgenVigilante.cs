// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.WorgenVigilante
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class WorgenVigilante(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "BG26_921";
  public const string Text = "<b>Windfury</b> This minion always attacks an enemy minion that it can kill <i>(if possible).</i>";
  public const string GoldenText = "<b>Windfury</b> This minion always attacks an enemy minion that it can kill <i>(if possible).</i>";

  public override Minion? ChooseAttackTarget()
  {
    List<Minion> list = this.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>();
    int atk = this.attack();
    Minion minion;
    return list.Any<Minion>() && list.Where<Minion>((Func<Minion, bool>) (x => !x.hasDiv && x.health() <= atk)).ToList<Minion>().TryGetRandom<Minion>(out minion) ? minion : base.ChooseAttackTarget();
  }
}
