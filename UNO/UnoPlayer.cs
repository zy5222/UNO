using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO
{
    public class UnoPlayer
    {
        public string Name { get; set; }
        public List<UnoCard> Hand { get; set; } = new List<UnoCard>();
        public bool IsHuman { get; set; }

        public UnoPlayer(string name, bool isHuman)
        {
            Name = name;
            IsHuman = isHuman;
        }

        // 抽牌加入手牌
        public void DrawCard(UnoDeck deck, int count = 1)
        {
            for (int i = 0; i < count; i++)
                Hand.Add(deck.Draw());
        }

        // 出牌（從手牌移除）
        public void PlayCard(UnoCard card)
        {
            Hand.Remove(card);
        }

        // 手牌數量
        public int CardCount => Hand.Count;
    }
}
