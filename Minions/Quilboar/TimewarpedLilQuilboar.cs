// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.TimewarpedLilQuilboar
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class TimewarpedLilQuilboar(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_Giant_608";
  public const string Text = "<b>Reborn</b> <b>Deathrattle:</b> This plays {0} <b>Blood Gems</b> on all your Quilboar.";
  public const string GoldenText = "<b>Reborn</b> <b>Deathrattle:</b> This plays {0} <b>Blood Gems</b> on all your Quilboar.";

  public Action<Minion> GetDeathrattle() => TimewarpedLilQuilboar.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int num = golden ? 6 : 3;
      foreach (Minion target in minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsQuilboar() && x.IsAlive())))
      {
        for (int index = 0; index < num; ++index)
          minion.Simulator.CastBloodGem(target, (Entity) minion);
      }
    });
  }
}
