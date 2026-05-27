using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO
{
    public enum CardColor
    {
        Red, Yellow, Green, Blue, Wild
    }

    public enum CardType
    {
        Number,
        Skip,
        Reverse,
        DrawTwo,
        Wild,
        WildDrawFour
    }

    public class UnoCard
    {
        public CardColor Color { get; set; }
        public CardType Type { get; set; }
        public int Number { get; set; }

        public UnoCard(CardColor color, CardType type, int number = -1)
        {
            Color = color;
            Type = type;
            Number = number;
        }

        public bool CanPlayOn(UnoCard target, CardColor currentWildColor)
        {
            if (this.Type == CardType.Wild || this.Type == CardType.WildDrawFour)
                return true;

            if (this.Color == target.Color || this.Color == currentWildColor)
                return true;

            if (this.Type == target.Type && this.Type != CardType.Number)
                return true;

            if (this.Type == CardType.Number && target.Type == CardType.Number
                && this.Number == target.Number)
                return true;

            return false;
        }

        public override string ToString()
        {
            switch (Type)
            {
                case CardType.Number: return $"{Color} {Number}";
                case CardType.Skip: return $"{Color} Skip";
                case CardType.Reverse: return $"{Color} Reverse";
                case CardType.DrawTwo: return $"{Color} +2";
                case CardType.Wild: return "Wild";
                case CardType.WildDrawFour: return "Wild +4";
                default: return "Unknown";
            }
        }
    }
}
