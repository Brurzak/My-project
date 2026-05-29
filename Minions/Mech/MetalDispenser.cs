// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.MetalDispenser
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class MetalDispenser(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG34_176";
  public const string Text = "<b>Divine Shield</b> <b>Avenge ({0}):</b> <b>Magnetize</b> a random Volumizer to this. Get a copy of it.";
  public const string GoldenText = "<b>Divine Shield</b> <b>Avenge ({0}):</b> <b>Magnetize</b> a random Volumizer to this. Get 2 copies of it.";
  private static readonly List<string> MagneticCardIds = new List<string>()
  {
    "BG34_170t3",
    "BG34_170t",
    "BG34_170t2"
  };
  private static readonly Queue<string> _forcedMagneticCardIds = new Queue<string>();

  internal static void ForceNextMagneticCardId(string cardId)
  {
    MetalDispenser._forcedMagneticCardIds.Enqueue(cardId);
  }

  private static bool TryGetNextMagneticCardId(out string cardId)
  {
    if (MetalDispenser._forcedMagneticCardIds.Count <= 0)
      return MetalDispenser.MagneticCardIds.TryGetRandom<string>(out cardId);
    cardId = MetalDispenser._forcedMagneticCardIds.Dequeue();
    return true;
  }

  public int AvengeRequirement => 4;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      string cardId;
      if (!MetalDispenser.TryGetNextMagneticCardId(out cardId))
        return;
      this.Simulator.MagnetizeMech((Minion) this, cardId, (Entity) this, this.ScriptDataNum1, this.ScriptDataNum2);
      this.AddMinionToFriendlyHand(cardId);
      if (!this.golden)
        return;
      this.AddMinionToFriendlyHand(cardId);
    });
  }
}
