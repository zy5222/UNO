using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO
{
    public class UnoDeck
    {
        private List<UnoCard> _cards = new List<UnoCard>();
        private Random _rng = new Random();

        // 初始化一副完整的 UNO 牌
        public UnoDeck()
        {
            Reset();
        }

        public void Reset()
        {
            _cards.Clear();

            CardColor[] colors = { CardColor.Red, CardColor.Yellow,
                                   CardColor.Green, CardColor.Blue };

            foreach (CardColor color in colors)
            {
                // 數字牌：0 只有一張，1-9 各兩張
                _cards.Add(new UnoCard(color, CardType.Number, 0));
                for (int n = 1; n <= 9; n++)
                {
                    _cards.Add(new UnoCard(color, CardType.Number, n));
                    _cards.Add(new UnoCard(color, CardType.Number, n));
                }

                // 功能牌各兩張
                for (int i = 0; i < 2; i++)
                {
                    _cards.Add(new UnoCard(color, CardType.Skip));
                    _cards.Add(new UnoCard(color, CardType.Reverse));
                    _cards.Add(new UnoCard(color, CardType.DrawTwo));
                }
            }

            // Wild 牌各四張
            for (int i = 0; i < 4; i++)
            {
                _cards.Add(new UnoCard(CardColor.Wild, CardType.Wild));
                _cards.Add(new UnoCard(CardColor.Wild, CardType.WildDrawFour));
            }

            Shuffle();
        }

        // 洗牌
        public void Shuffle()
        {
            for (int i = _cards.Count - 1; i > 0; i--)
            {
                int j = _rng.Next(i + 1);
                UnoCard tmp = _cards[i];
                _cards[i] = _cards[j];
                _cards[j] = tmp;
            }
        }

        // 抽一張牌
        public UnoCard Draw()
        {
            if (_cards.Count == 0)
                Reset(); // 牌抽完就重新洗牌

            UnoCard card = _cards[0];
            _cards.RemoveAt(0);
            return card;
        }

        // 剩餘張數
        public int Count => _cards.Count;
    }
}
