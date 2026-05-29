// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.NestSwarmer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class NestSwarmer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG31_807";
  public const string Text = "<b>Deathrattle:</b> Summon three {0}/{1} Beetles.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon six {0}/{1} Beetles.";

  public Action<Minion> GetDeathrattle() => NestSwarmer.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int num = golden ? 6 : 3;
      for (int index = 0; index < num; ++index)
        minion.TrySummonMinion((Summon) "BG28_603t");
    });
  }
}
