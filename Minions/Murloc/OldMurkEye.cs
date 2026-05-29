// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.OldMurkEye
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class OldMurkEye(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "EX1_062";
  public const string Text = "<b>Charge</b>. Has +1 Attack for each other Murloc on the battlefield.";

  public override int SelfAttackPassive()
  {
    int num = 0;
    foreach (Minion minion in this.FriendlySide)
    {
      if (minion != this && minion.IsMurloc())
        ++num;
    }
    foreach (Minion minion in this.OpposingSide)
    {
      if (minion.IsMurloc())
        ++num;
    }
    return num * this.DoubleIfGolden(1);
  }
}
