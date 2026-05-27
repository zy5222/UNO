# UNO_s1131547

## 核心功能介紹

### 1. 遊戲核心邏輯
- **功能說明：** 實作標準 UNO 規則，支援玩家對電腦 1 對 1 對戰。開局各發 7 張牌，率先出完手牌者獲勝。
- **資料設計：** 以 `UnoCard`、`UnoDeck`、`UnoPlayer`、`UnoGame` 四個類別分離資料與邏輯，`UnoGame` 統一管理回合流程、特殊牌效果與勝負判定。

### 2. 手牌繪製（GDI+）
- **功能說明：** 使用 `Panel` 的 `Paint` 事件搭配 GDI+ 自行繪製每一張牌，依牌色填入對應底色，牌面顯示數字或符號（⊘ ⇄ +2 W +4）。
- **動態間距：** 手牌數量多時自動縮小牌與牌的間距，確保所有牌張不超出 Panel 寬度。

### 3. 特殊牌效果
- **Skip（⊘）：** 對方跳過本回合。
- **Reverse（⇄）：** 兩人模式視同 Skip。
- **Draw Two（+2）：** 對方強制抽 2 張並跳過。
- **Wild（W）：** 出牌者自選下一有效顏色。
- **Wild Draw Four（+4）：** 對方抽 4 張並跳過，出牌者自選顏色。

### 4. 功能按鈕
- **抽牌：** 無牌可出時點擊抽一張，自動換電腦回合。
- **UNO：** 手牌剩 1 張時啟動 5 秒計時，時限內須點擊喊 UNO，否則罰抽 2 張。
- **新遊戲：** 重新洗牌發牌。
- **結束：** 關閉程式。

---

## 防呆機制與設計

### 1. UNO 喊聲計時機制
- **問題解決：** 防止玩家忘記喊 UNO 直接獲勝。
- **判斷邏輯：** 手牌降至 1 張時設定 `PlayerShouldCallUno = true` 並啟動 5 秒 Timer；玩家點擊 UNO 按鈕後將 `_unoCalled = true` 並停止計時器。Timer 到期若旗標未設則自動罰抽 2 張。

### 2. 出牌合法性驗證
- **問題解決：** 防止玩家出不符合規則的牌。
- **判斷邏輯：** 每張牌透過 `CanPlayOn(TopCard, CurrentColor)` 驗證，顏色或數字任一符合才允許出牌，否則顯示「這張牌不能出！」並維持玩家回合。

### 3. 遊戲結束鎖定
- **問題解決：** 防止遊戲結束後仍可操作抽牌或出牌。
- **判斷邏輯：** `RefreshUI` 偵測到 `State != Playing` 時立即將 `btnDraw.Enabled = false`、`btnUno.Enabled = false` 並 `return`，阻止後續邏輯再次啟用按鈕。

### 4. 顏色選擇視窗
- **問題解決：** 出 Wild 牌時需強制選色，避免顏色停留在 Wild（無色）狀態。
- **判斷邏輯：** 偵測到 `CardType.Wild` 或 `CardType.WildDrawFour` 時自動彈出 `frmColorPicker`，取得玩家選擇後才執行出牌。

---

## 執行畫面

> 開局各發 7 張牌，玩家手牌顯示於下方，電腦手牌以牌背呈現於上方。

- 遊戲初始畫面
  
  <img width="600" height="400" alt="image" src="https://github.com/user-attachments/assets/ef4c4577-d9b2-48a4-aff0-0fc4a5740e75" />
 
- 玩家出牌（點擊手牌）

 

- 手牌剩 1 張，出現 UNO 計時提示
  
  <img width="600" height="400" alt="image" src="https://github.com/user-attachments/assets/4962e168-23fb-475e-8b6c-9201f0e8f27e" />

  - 喊成功:
  <img width="600" height="400" alt="image" src="https://github.com/user-attachments/assets/799cd6d7-8d64-45e9-97fa-d4e15ea4ef77" />


  - 忘記喊:
  <img width="600" height="400" alt="image" src="https://github.com/user-attachments/assets/0414b1ba-55dc-4d03-9dc1-824884cd16ba" />

  
- 出 Wild 牌，彈出顏色選擇視窗
  
  <img width="600" height="400" alt="image" src="https://github.com/user-attachments/assets/5bb4e2ca-589a-4cce-89a0-29e5ebcfa711" />


- 遊戲結束，彈出勝負訊息

  <img width="600" height="400" alt="image" src="https://github.com/user-attachments/assets/3cc6e2cc-120a-4125-b6c3-699fee4accce" />
