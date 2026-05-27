using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UNO
{
    public partial class frmMain : Form
    {
        private UnoGame _game = new UnoGame();
        private System.Windows.Forms.Timer _unoTimer = new System.Windows.Forms.Timer();
        private bool _unoCalled = false; // 玩家有沒有喊到

        // 每張牌的顯示大小
        private const int CARD_W = 55;
        private const int CARD_H = 70;
        private const int CARD_GAP = 5;
        public frmMain()
        {
            InitializeComponent();
            _unoTimer.Interval = 5000; // 3 秒
            _unoTimer.Tick += UnoTimer_Tick;
            this.Shown += (s, e) => StartNewGame();
        }
        private void StartNewGame()
        {
            _game.Start();
            RefreshUI();
            this.Refresh();
        }
        private void RefreshUI()
        {
            lblMessage.Text = _game.Message;
            lblComputerCount.Text = $"電腦\n{_game.Computer.CardCount} 張";

            pnlComputer.Invalidate();
            pnlCenter.Invalidate();
            pnlPlayer.Invalidate();

            if (!_game.IsPlayerTurn)
            {
                if (_game.TopCard?.Type == CardType.DrawTwo)
                    PlaySound("draw2.wav");
                else if (_game.TopCard?.Type == CardType.WildDrawFour)
                    PlaySound("draw4.wav");
            }

            // 遊戲結束處理
            if (_game.State != GameState.Playing)
            {
                _unoTimer.Stop();
                btnDraw.Enabled = false;
                btnUno.Enabled = false;
                if (_game.State == GameState.PlayerWin)
                    PlaySound("win.wav");
                else
                    PlaySound("lose.wav");
                MessageBox.Show(_game.Message, "遊戲結束",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            btnDraw.Enabled = _game.IsPlayerTurn;
            // 手牌剩 1 張 → 啟動 UNO 計時器
            if (_game.Player.CardCount == 1 && _game.IsPlayerTurn
                && !_unoTimer.Enabled)
            {
                _unoCalled = false;
                _unoTimer.Start();
                btnUno.Enabled = true;
                lblMessage.Text = "⚡ 快喊 UNO！你有 5 秒！";
            }
            else if (_game.Player.CardCount != 1)
            {
                _unoTimer.Stop();
                btnUno.Enabled = false;
            }
        }
        private void UnoTimer_Tick(object sender, EventArgs e)
        {
            _unoTimer.Stop();

            // 時間到，玩家沒有喊 UNO
            if (!_unoCalled && _game.Player.CardCount == 1)
            {
                _game.Player.DrawCard(_game.Deck, 2);
                _game.CallUno();
                PlaySound("draw.wav");
                lblMessage.Text = "忘記喊 UNO！罰抽 2 張！";
                pnlPlayer.Invalidate();
                lblComputerCount.Text = $"電腦\n{_game.Computer.CardCount} 張";
            }
        }
        private void DrawCard(Graphics g, UnoCard card, int x, int y,
                              bool faceDown = false, bool highlight = false)
        {
            // 牌的外框（圓角矩形效果用普通矩形代替）
            Rectangle rect = new Rectangle(x, y, CARD_W, CARD_H);

            if (faceDown)
            {
                // 牌背面
                g.FillRectangle(Brushes.DarkBlue, rect);
                g.DrawRectangle(Pens.White, rect);
                g.DrawString("UNO", new Font("Arial", 10, FontStyle.Bold),
                    Brushes.White, x + 8, y + 38);
                return;
            }

            // 選取哪個顏色
            CardColor displayColor = card.Color == CardColor.Wild ?
                _game.CurrentColor : card.Color;

            Color bgColor;
            switch (displayColor)
            {
                case CardColor.Red: bgColor = Color.Crimson; break;
                case CardColor.Yellow: bgColor = Color.Gold; break;
                case CardColor.Green: bgColor = Color.ForestGreen; break;
                case CardColor.Blue: bgColor = Color.RoyalBlue; break;
                default: bgColor = Color.DimGray; break;
            }

            // 畫牌底色
            g.FillRectangle(new SolidBrush(bgColor), rect);

            // 高亮選取效果
            if (highlight)
                g.DrawRectangle(new Pen(Color.Yellow, 4), rect);
            else
                g.DrawRectangle(Pens.White, rect);

            // 白色橢圓
            g.FillEllipse(Brushes.White,
                x + 8, y + 20, CARD_W - 16, CARD_H - 40);

            // 牌面文字
            string text;
            Font font;
            switch (card.Type)
            {
                case CardType.Number:
                    text = card.Number.ToString();
                    font = new Font("Arial", 22, FontStyle.Bold);
                    break;
                case CardType.Skip: text = "⊘"; font = new Font("Arial", 18, FontStyle.Bold); break;
                case CardType.Reverse: text = "⇄"; font = new Font("Arial", 16, FontStyle.Bold); break;
                case CardType.DrawTwo: text = "+2"; font = new Font("Arial", 16, FontStyle.Bold); break;
                case CardType.Wild: text = "W"; font = new Font("Arial", 18, FontStyle.Bold); break;
                case CardType.WildDrawFour: text = "+4"; font = new Font("Arial", 16, FontStyle.Bold); break;
                default: text = "?"; font = new Font("Arial", 18, FontStyle.Bold); break;
            }

            SizeF sz = g.MeasureString(text, font);
            g.DrawString(text, font,
                new SolidBrush(bgColor),
                x + (CARD_W - sz.Width) / 2,
                y + (CARD_H - sz.Height) / 2);
        }
        private void pnlComputer_Paint(object sender, PaintEventArgs e)
        {
            int count = _game.Computer.CardCount;
            if (count == 0) return;

            // 動態計算間距，確保不超出 Panel 寬度
            int totalWidth = pnlComputer.Width - 20;
            int gap = Math.Min(CARD_W + CARD_GAP, (totalWidth - CARD_W) / Math.Max(count - 1, 1));

            for (int i = 0; i < count; i++)
                DrawCard(e.Graphics, null, 10 + i * gap, 10, faceDown: true);
        }
        private void pnlCenter_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // 牌堆（背面）
            DrawCard(g, null, 50, 30, faceDown: true);
            g.DrawString("牌堆", new Font("微軟正黑體", 10),
                Brushes.White, 58, 115);

            // 目前頂牌
            if (_game.TopCard != null)
            {
                DrawCard(g, _game.TopCard, 200, 30);
                g.DrawString("棄牌堆", new Font("微軟正黑體", 10),
                    Brushes.White, 205, 115);
            }

            // 目前顏色提示
            Color c;
            switch (_game.CurrentColor)
            {
                case CardColor.Red: c = Color.Crimson; break;
                case CardColor.Yellow: c = Color.Gold; break;
                case CardColor.Green: c = Color.ForestGreen; break;
                case CardColor.Blue: c = Color.RoyalBlue; break;
                default: c = Color.Gray; break;
            }
            g.FillEllipse(new SolidBrush(c), 345, 50, 50, 50);
            g.DrawString("當前顏色", new Font("微軟正黑體", 9),
                Brushes.White, 345, 115);
        }
        private void pnlPlayer_Paint(object sender, PaintEventArgs e)
        {
            List<UnoCard> hand = _game.Player.Hand;
            int count = hand.Count;
            if (count == 0) return;

            // 動態計算間距，確保不超出 Panel 寬度
            int totalWidth = pnlPlayer.Width - 20;
            int gap = Math.Min(CARD_W + CARD_GAP, (totalWidth - CARD_W) / Math.Max(count - 1, 1));

            for (int i = 0; i < count; i++)
                DrawCard(e.Graphics, hand[i], 10 + i * gap, 10);
        }
        private void pnlPlayer_MouseClick(object sender, MouseEventArgs e)
        {
            if (!_game.IsPlayerTurn) return;

            List<UnoCard> hand = _game.Player.Hand;
            int count = hand.Count;
            if (count == 0) return;

            // 跟 Paint 一樣的動態間距計算
            int totalWidth = pnlPlayer.Width - 20;
            int gap = Math.Min(CARD_W + CARD_GAP, (totalWidth - CARD_W) / Math.Max(count - 1, 1));

            for (int i = 0; i < count; i++)
            {
                int x = 10 + i * gap;
                Rectangle rect = new Rectangle(x, 10, CARD_W, CARD_H);
                if (rect.Contains(e.Location))
                {
                    UnoCard card = hand[i];

                    if (card.Type == CardType.Wild ||
                        card.Type == CardType.WildDrawFour)
                    {
                        CardColor chosen = AskColor();
                        if (card.Type == CardType.WildDrawFour)
                            PlaySound("draw4.wav");
                        else
                            PlaySound("play.wav");
                        _game.PlayerPlay(card, chosen);
                    }
                    else
                    {
                        bool ok = _game.PlayerPlay(card);
                        if (!ok)
                        {
                            PlaySound("invalid.wav");
                            lblMessage.Text = "這張牌不能出！請選其他牌或抽牌。";
                            return;
                        }
                        // 出牌成功後依牌型播音效
                        if (card.Type == CardType.DrawTwo)
                            PlaySound("draw2.wav");
                        else
                            PlaySound("play.wav");
                    }

                    RefreshUI();
                    return;
                }
            }
        }
        private CardColor AskColor()
        {
            frmColorPicker picker = new frmColorPicker();
            picker.ShowDialog(this);
            return picker.ChosenColor;
        }

        private void btnDraw_Click_1(object sender, EventArgs e)
        {
            PlaySound("draw.wav");
            _game.PlayerDraw();
            RefreshUI();
        }

        private void btnUno_Click_1(object sender, EventArgs e)
        {
            if (_game.Player.CardCount == 1)
            {
                _unoCalled = true;
                _unoTimer.Stop();
                _game.CallUno(); // 成功喊到
                PlaySound("uno.wav");
                lblMessage.Text = "UNO！喊成功！";
            }
            else
            {
                lblMessage.Text = "現在不需要喊 UNO！";
            }
        }

        private void btnNewGame_Click_1(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void PlaySound(string fileName)
        {
            try
            {
                string path = System.IO.Path.Combine(
                    Application.StartupPath, "Sound", fileName);
                if (System.IO.File.Exists(path))
                {
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);
                    player.Play();
                }
            }
            catch { }
        }
    }
}
