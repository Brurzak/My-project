// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Naga.ProfoundThinker
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Factory;
using BobsBuddy.Simulation;
using BobsBuddy.Spells.SpellcraftSpells;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Naga;

public class ProfoundThinker(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG34_929";
  public const string Text = "<b>Rally:</b> Get a random <b>Spellcraft</b> spell. This casts a copy of it <i>(targets this if possible)</i>.";
  public const string GoldenText = "<b>Rally:</b> Get 2 random <b>Spellcraft</b> spells. This casts copies of them <i>(targets this if possible)</i>.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      int num = isGolden ? 2 : 1;
      for (int index = 0; index < num; ++index)
      {
        ISpellcraftSpell randomSpellcraftSpell = SpellcraftFactory.GetRandomSpellcraftSpell();
        if (randomSpellcraftSpell?.GetType().Name == "GlowscaleSpell" && minion.hasDiv)
        {
          Minion target1;
          if (minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && !x.hasDiv)).ToList<Minion>().TryGetRandom<Minion>(out target1))
            minion.Simulator.CastSpellcraftSpell(randomSpellcraftSpell, (Entity) minion, minion.golden, target1);
        }
        else
          minion.Simulator.CastSpellcraftSpell(randomSpellcraftSpell, (Entity) minion, minion.golden, minion);
      }
    });
  }
}
