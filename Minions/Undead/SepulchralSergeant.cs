// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.SepulchralSergeant
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class SepulchralSergeant(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionBuffed,
  IEntity,
  IDeathrattle
{
  public const string CardId = "BG34_111";
  public const string Text = "<b>Deathrattle:</b> Give your other minions +{0} Health. <i>(Improves permanently after this gains Attack!)</i>";
  public const string GoldenText = "<b>Deathrattle:</b> Give your other minions +{0} Health. <i>(Improves permanently after this gains Attack!)</i>";
  private int _combatCounter;

  private int GlobalCounter => this.ScriptDataNum1;

  public override int SelfAttackPassive() => this.DoubleIfGolden(this.Counter * 2);

  public override int SelfHealthPassive() => this.DoubleIfGolden(this.Counter);

  private int Counter => this.GlobalCounter + this._combatCounter;

  public Action? OnFriendlyMinionBuffed(
    Minion minion,
    int attackChange,
    int healthChange,
    Entity? source)
  {
    SepulchralSergeant ss = minion as SepulchralSergeant;
    return ss == null || ss != this || attackChange <= 0 ? (Action) null : (Action) (() => ss._combatCounter += this.DoubleIfGolden(2));
  }

  public Action<Minion> GetDeathrattle() => SepulchralSergeant.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      if (!(minion is SepulchralSergeant sepulchralSergeant2))
        return;
      foreach (Minion minion1 in minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (m => m.IsAlive() && m != minion)))
        minion1.IncreaseStats(0, sepulchralSergeant2.Counter);
    });
  }
}
