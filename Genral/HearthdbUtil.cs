// Decompiled with JetBrains decompiler
// Type: BobsBuddy.HearthdbUtil
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using HearthDb;

#nullable enable
namespace BobsBuddy;

public static class HearthdbUtil
{
  public static Card? CardFromID(string id)
  {
    Card card;
    return !Cards.All.TryGetValue(id, out card) ? (Card) null : card;
  }

  public static string CardNameFromID(string cardID)
  {
    Card card;
    return !Cards.All.TryGetValue(cardID, out card) ? "Could not find card with id " + card?.ToString() : card.Name;
  }

  public static string CardNameFromDBFID(int dbfid)
  {
    return Cards.GetFromDbfId(dbfid, false) != null ? Cards.GetFromDbfId(dbfid, false).Name : "Could not find card for dbifd " + dbfid.ToString();
  }

  public static int DbfidFromCardID(string cardID)
  {
    Card card;
    return Cards.All.TryGetValue(cardID, out card) ? card.DbfId : -1;
  }

  public static Card? CardFromCardID(string cardID)
  {
    Card card;
    return !Cards.All.TryGetValue(cardID, out card) ? (Card) null : card;
  }
}
