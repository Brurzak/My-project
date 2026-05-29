// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Factory.SpellcraftFactory
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Spells.SpellcraftSpells;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable enable
namespace BobsBuddy.Factory;

public class SpellcraftFactory
{
  private static readonly Dictionary<string, Type> SpellcraftByCardId;
  private static readonly Queue<ISpellcraftSpell> _forcedSpells = new Queue<ISpellcraftSpell>();

  internal static void ForceNextSpell(ISpellcraftSpell spell)
  {
    SpellcraftFactory._forcedSpells.Enqueue(spell);
  }

  static SpellcraftFactory()
  {
    IEnumerable<Type> types = ((IEnumerable<Type>) Assembly.GetExecutingAssembly().GetTypes()).Where<Type>((Func<Type, bool>) (t => t.IsClass && !t.IsAbstract && typeof (ISpellcraftSpell).IsAssignableFrom(t)));
    SpellcraftFactory.SpellcraftByCardId = new Dictionary<string, Type>();
    foreach (Type type in types)
    {
      FieldInfo field = type.GetField("CardId", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
      if (field != (FieldInfo) null)
      {
        string key = field.GetValue((object) null) as string;
        if (!string.IsNullOrEmpty(key))
          SpellcraftFactory.SpellcraftByCardId[key] = type;
      }
    }
  }

  public static ISpellcraftSpell? CreateByCardId(string cardId)
  {
    if (string.IsNullOrEmpty(cardId))
      return (ISpellcraftSpell) null;
    Type type;
    return SpellcraftFactory.SpellcraftByCardId.TryGetValue(cardId, out type) ? Activator.CreateInstance(type) as ISpellcraftSpell : (ISpellcraftSpell) null;
  }

  public static ISpellcraftSpell? GetRandomSpellcraftSpell()
  {
    if (SpellcraftFactory._forcedSpells.Count > 0)
      return SpellcraftFactory._forcedSpells.Dequeue();
    List<Type> list = SpellcraftFactory.SpellcraftByCardId.Values.ToList<Type>();
    if (list.Count == 0)
      return (ISpellcraftSpell) null;
    int index = SafeRandom.Next(0, list.Count);
    return Activator.CreateInstance(list[index]) as ISpellcraftSpell;
  }

  public static IEnumerable<string> GetImplementedCardIds()
  {
    return (IEnumerable<string>) SpellcraftFactory.SpellcraftByCardId.Keys;
  }
}
