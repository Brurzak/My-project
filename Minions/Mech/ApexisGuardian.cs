// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.ApexisGuardian
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class ApexisGuardian(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity,
  IOnRally
{
  public const string CardId = "BG34_173";
  public const string Text = "<b>Deathrattle and Rally:</b> <b>Magnetize</b> a random <b>Magnetic</b> Volumizer to another friendly Mech.";
  public const string GoldenText = "<b>Deathrattle and Rally:</b> <b>Magnetize</b> a random <b>Magnetic</b> Volumizer to 2 other friendly Mechs.";
  private static readonly List<string> MagneticCardIds = new List<string>()
  {
    "BG34_170t3",
    "BG34_170t",
    "BG34_170t2"
  };
  private static readonly Queue<string> _forcedMagneticCardIds = new Queue<string>();

  internal static void ForceNextMagneticCardId(string cardId)
  {
    ApexisGuardian._forcedMagneticCardIds.Enqueue(cardId);
  }

  private static bool TryGetNextMagneticCardId(out string cardId)
  {
    if (ApexisGuardian._forcedMagneticCardIds.Count <= 0)
      return ApexisGuardian.MagneticCardIds.TryGetRandom<string>(out cardId);
    cardId = ApexisGuardian._forcedMagneticCardIds.Dequeue();
    return true;
  }

  public Action<Minion> GetDeathrattle() => this.Deathrattle(this.golden);

  public Action<Minion> Deathrattle(bool isGolden)
  {
    return (Action<Minion>) (minion =>
    {
      foreach (Minion randomElement in minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (m => m != minion && m.IsMech() && m.IsAlive())).ToList<Minion>().GetRandomElements<Minion>(isGolden ? 2 : 1))
      {
        string cardId;
        if (ApexisGuardian.TryGetNextMagneticCardId(out cardId))
          minion.Simulator.MagnetizeMech(randomElement, cardId, (Entity) this, this.ScriptDataNum1, this.ScriptDataNum2);
      }
    });
  }

  public Action<Minion>? OnRally(bool isGolden, Minion target) => this.Deathrattle(isGolden);
}
