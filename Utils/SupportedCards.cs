// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Utils.SupportedCards
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Common;
using HearthDb;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

#nullable enable
namespace BobsBuddy.Utils;

public static class SupportedCards
{
  private static Dictionary<string, CardData>? _knownCards;

  public static SupportedCards.Result VerifyCardIsSupported(Card card)
  {
    if (SupportedCards._knownCards == null)
    {
      using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("BobsBuddy.Embedded.KnownBaconCards.xml"))
      {
        using (StreamReader streamReader = new StreamReader(manifestResourceStream))
        {
          KnownBaconCards knownBaconCards = (KnownBaconCards) new XmlSerializer(typeof (KnownBaconCards)).Deserialize((TextReader) streamReader);
          SupportedCards._knownCards = new Dictionary<string, CardData>();
          foreach (CardData card1 in knownBaconCards.Cards)
            SupportedCards._knownCards[card1.Id] = card1;
        }
      }
    }
    CardData cardData;
    if (!SupportedCards._knownCards.TryGetValue(card.Id, out cardData))
      return SupportedCards.Result.UnknownCard;
    return (cardData.Text ?? "") != (card.Text ?? "") ? SupportedCards.Result.TextChanged : SupportedCards.Result.Supported;
  }

  public enum Result
  {
    Supported = 1,
    UnknownCard = 2,
    TextChanged = 3,
  }
}
