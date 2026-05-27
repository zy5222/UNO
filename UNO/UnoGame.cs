using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO
{
    public enum GameState
    {
        Playing,
        PlayerWin,
        ComputerWin
    }

    public class UnoGame
    {
        public UnoDeck Deck { get; private set; }
        public UnoPlayer Player { get; private set; }
        public UnoPlayer Computer { get; private set; }
        public UnoCard TopCard { get; private set; }       // 牌堆最上面的牌
        public CardColor CurrentColor { get; private set; } // 當前有效顏色（Wild 牌用）
        public bool IsPlayerTurn { get; private set; }
        public bool PlayerShouldCallUno { get; private set; } = false;
        public GameState State { get; private set; }
        public string Message { get; private set; }        // 給 UI 顯示的訊息

        private Random _rng = new Random();

        public UnoGame()
        {
            Deck = new UnoDeck();
            Player = new UnoPlayer("玩家", true);
            Computer = new UnoPlayer("電腦", false);
        }
        public void CallUno()
        {
            PlayerShouldCallUno = false;
        }
        // 開始新遊戲
        public void Start()
        {
            Deck.Reset();
            Player.Hand.Clear();
            Computer.Hand.Clear();

            // 各發 7 張牌
            Player.DrawCard(Deck, 7);
            Computer.DrawCard(Deck, 7);

            // 翻開第一張（不能是 Wild）
            do { TopCard = Deck.Draw(); }
            while (TopCard.Type == CardType.Wild ||
                   TopCard.Type == CardType.WildDrawFour);

            CurrentColor = TopCard.Color;
            IsPlayerTurn = true;
            State = GameState.Playing;
            Message = "你的回合，請出牌！";
        }

        // 玩家出牌
        public bool PlayerPlay(UnoCard card, CardColor chosenColor = CardColor.Wild)
        {
            if (!IsPlayerTurn) return false;
            if (!card.CanPlayOn(TopCard, CurrentColor)) return false;

            Player.PlayCard(card);

            if (Player.CardCount == 0)
            {
                if (PlayerShouldCallUno)
                {
                    Player.DrawCard(Deck, 2);
                    PlayerShouldCallUno = false;
                    Message = "忘記喊 UNO！罰抽 2 張！";
                    ApplyCard(card, chosenColor);
                    IsPlayerTurn = false;
                    ComputerPlay();
                    return true;
                }
                TopCard = card;
                CurrentColor = card.Color == CardColor.Wild ? chosenColor : card.Color;
                State = GameState.PlayerWin;
                Message = "你贏了！ 真棒";
                return true;
            }

            if (Player.CardCount == 1)   // 只留這一個
                PlayerShouldCallUno = true;

            ApplyCard(card, chosenColor);

            if (!IsPlayerTurn)
            {
                Message = "電腦被跳過！換你了。";
                IsPlayerTurn = true;
            }
            else
            {
                ComputerPlay();
            }

            return true;
        }

        // 玩家抽牌
        public void PlayerDraw()
        {
            if (!IsPlayerTurn) return;
            Player.DrawCard(Deck);
            Message = "你抽了一張牌，換電腦了。";
            IsPlayerTurn = false;
            ComputerPlay();
        }

        // 電腦出牌 AI
        private void ComputerPlay()
        {
            if (State != GameState.Playing) return;

            // 找出可以出的牌
            UnoCard chosen = null;
            foreach (UnoCard card in Computer.Hand)
            {
                if (card.CanPlayOn(TopCard, CurrentColor))
                {
                    chosen = card;
                    break;
                }
            }

            if (chosen == null)
            {
                // 沒牌可出就抽牌
                Computer.DrawCard(Deck);
                Message = "電腦沒牌可出，抽了一張。換你了！";
                IsPlayerTurn = true;
                return;
            }

            Computer.PlayCard(chosen);

            // Wild 牌由電腦隨機選色
            CardColor aiColor = chosen.Color;
            if (chosen.Type == CardType.Wild || chosen.Type == CardType.WildDrawFour)
            {
                CardColor[] colors = { CardColor.Red, CardColor.Yellow,
                                       CardColor.Green, CardColor.Blue };
                aiColor = colors[_rng.Next(4)];
            }

            ApplyCard(chosen, aiColor);

            if (Computer.CardCount == 0)
            {
                State = GameState.ComputerWin;
                Message = "電腦贏了！加油好嗎?";
                return;
            }

            if (IsPlayerTurn == false)
            {
                // 電腦被自己的牌跳過玩家（Reverse/Skip），再出一次不處理
                Message = "你被跳過了！電腦再出一張。";
                IsPlayerTurn = true;
            }
        }

        // 套用牌的效果
        private void ApplyCard(UnoCard card, CardColor chosenColor)
        {
            TopCard = card;
            CurrentColor = card.Color == CardColor.Wild ? chosenColor : card.Color;

            switch (card.Type)
            {
                case CardType.Skip:
                    // 跳過對方
                    IsPlayerTurn = !IsPlayerTurn;
                    IsPlayerTurn = !IsPlayerTurn; // 兩次等於維持現狀，對方被跳
                    break;

                case CardType.Reverse:
                    // 兩人遊戲 Reverse = Skip
                    break;

                case CardType.DrawTwo:
                    if (IsPlayerTurn)      // 玩家剛出，所以電腦抽
                        Computer.DrawCard(Deck, 2);
                    else                   // 電腦剛出，所以玩家抽
                        Player.DrawCard(Deck, 2);
                    break;

                case CardType.WildDrawFour:
                    if (IsPlayerTurn)
                        Computer.DrawCard(Deck, 4);
                    else
                        Player.DrawCard(Deck, 4);
                    break;
            }

            // 換人
            IsPlayerTurn = !IsPlayerTurn;
        }
    }
}
