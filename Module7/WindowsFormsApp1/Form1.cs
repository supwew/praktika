using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectDraw;
using Newtonsoft.Json;

namespace PingPong
{
    // Перечисление состояний игры
    public enum GameState { Menu, Playing, Pause, GameOver, Rules }
    // Перечисление уровней сложности
    public enum Difficulty { Легко, Средне, Сложно }
    // Точка шлейфа мяча
    public struct TrailPoint
    {
        public PointF Position;
        public float Size;
        public int Alpha;
    }
    public enum DecorationType { Paddle, Ball }
    // Декорация фона меню
    public struct MenuDecoration
    {
        public PointF Position;
        public float SpeedX;
        public float SpeedY;
        public int Size;
        public int BaseAlpha;
        public float AlphaPhase;
        public float Rotation;
        public float RotationSpeed;
        public DecorationType Type;
        public Color ThemeColor;
    }
    // Звезда фона меню
    public struct MenuStar
    {
        public PointF Position;
        public float SpeedY;
        public float AlphaPhase;
        public float AlphaSpeed;
    }
    public partial class Form1 : Form
    {
        // Константы игрового поля
        private const int FieldWidth = 800;
        private const int FieldHeight = 600;
        private const int UIHeaderHeight = 70;
        private const int TotalHeight = FieldHeight + UIHeaderHeight;
        // Размеры объектов
        private const int PaddleWidth = 20;
        private const int PaddleHeight = 100;
        private const int BallSize = 20;
        // DirectDraw
        private Microsoft.DirectX.DirectDraw.Device ddrawDevice;
        private Surface primarySurface;   // первичная поверхность
        private Surface backSurface;      // задний буфер
        private Clipper windowClipper;    // обрезка
        // Позиции объектов
        private float leftPaddleX, leftPaddleY;
        private float rightPaddleX, rightPaddleY;
        private float ballX, ballY;
        private float ballVx, ballVy;
        // Физические параметры
        private const float InitialSpeed = 0.42f;
        private const float SpeedIncrement = 1.05f;
        private const float MaxBallSpeed = 1.4f;
        private const float PaddleSpeedBase = 0.5f;
        // Фиксированный шаг физики
        private DateTime lastFrameTime;
        private float accumulator = 0f;
        private const float FixedDeltaTime = 1000f / 120f;
        // Анимация гола
        private bool isGoalAnimating = false;
        private float goalAnimationTimer = 0f;
        private const float GoalAnimationDuration = 600f;
        private PointF goalEpicenter;
        private Color goalParticleColor;
        // Шлейф мяча
        private List<TrailPoint> ballTrail = new List<TrailPoint>();
        private const int MaxTrailPoints = 12;
        // Фоновые элементы меню
        private List<MenuDecoration> menuDecorations = new List<MenuDecoration>();
        private List<MenuStar> menuStars = new List<MenuStar>();
        private float gridOffsetY = 0f;
        // Состояние игры
        private GameState currentState = GameState.Menu;
        private Difficulty currentDifficulty = Difficulty.Средне;
        private bool isVsComputer = true;
        private int leftScore = 0, rightScore = 0, highScore = 0;
        private int menuIndex = 0;
        // Таймер и звук
        private Timer gameTimer;
        private System.Media.SoundPlayer hitSound;
        private System.Media.SoundPlayer goalSound;
        // Флаги клавиш
        private bool isWDown, isSDown, isUpDown, isDownDown;

        public Form1()
        {
            InitializeComponent();
            // Настройка окна
            this.Text = "PING PONG";
            this.ClientSize = new Size(FieldWidth, TotalHeight);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;
            // Загрузка данных и инициализация
            LoadHighScore();           // загрузка рекорда из файла
            LoadSettings();            // загрузка настроек из файла
            InitAudio();               // инициализация звука
            InitDirectDraw();          // инициализация DirectDraw
            InitMenuDecorations();     // инициализация декораций меню
            ResetMatch();              // сброс матча
            lastFrameTime = DateTime.Now;
            // Запуск игрового таймера
            gameTimer = new Timer { Interval = 10 };
            gameTimer.Tick += GameLoop;
            gameTimer.Start();
        }
        // Инициализация звука
        private void InitAudio()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hit.wav");
                if (File.Exists(path)) { hitSound = new System.Media.SoundPlayer(path); hitSound.Load(); }
                string goalPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "goal.wav");
                if (File.Exists(goalPath)) { goalSound = new System.Media.SoundPlayer(goalPath); goalSound.Load(); }
            }
            catch { }
        }
        // Инициализация фоновых декораций меню
        private void InitMenuDecorations()
        {
            Random rnd = new Random();
            Color[] colors = { Color.Cyan, Color.OrangeRed, Color.SpringGreen };
            for (int i = 0; i < 16; i++)
            {
                menuDecorations.Add(new MenuDecoration
                {
                    Position = new PointF(rnd.Next(50, FieldWidth - 50), rnd.Next(UIHeaderHeight + 50, TotalHeight - 50)),
                    SpeedX = (float)(rnd.NextDouble() * 0.05 - 0.025),
                    SpeedY = (float)(rnd.NextDouble() * 0.05 - 0.025),
                    Size = rnd.Next(25, 45),
                    BaseAlpha = rnd.Next(35, 65),
                    AlphaPhase = (float)(rnd.NextDouble() * Math.PI * 2),
                    Rotation = rnd.Next(0, 360),
                    RotationSpeed = (float)(rnd.NextDouble() * 0.05 - 0.025),
                    Type = (rnd.Next(0, 2) == 0) ? DecorationType.Paddle : DecorationType.Ball,
                    ThemeColor = colors[rnd.Next(0, colors.Length)]
                });
            }
            for (int i = 0; i < 30; i++)
            {
                menuStars.Add(new MenuStar
                {
                    Position = new PointF(rnd.Next(0, FieldWidth), rnd.Next(UIHeaderHeight, TotalHeight)),
                    SpeedY = (float)(rnd.NextDouble() * 0.015 + 0.005),
                    AlphaPhase = (float)(rnd.NextDouble() * Math.PI * 2),
                    AlphaSpeed = (float)(rnd.NextDouble() * 0.002 + 0.001)
                });
            }
        }
        // Инициализация DirectDraw
        private void InitDirectDraw()
        {
            try
            {
                ddrawDevice = new Microsoft.DirectX.DirectDraw.Device();
                ddrawDevice.SetCooperativeLevel(this, CooperativeLevelFlags.Normal);
                // Создание первичной поверхности
                SurfaceDescription desc = new SurfaceDescription();
                desc.SurfaceCaps.PrimarySurface = true;
                primarySurface = new Surface(desc, ddrawDevice);
                // Установка обрезки
                windowClipper = new Clipper(ddrawDevice) { Window = this };
                primarySurface.Clipper = windowClipper;
                // Создание заднего буфера
                desc.Clear();
                desc.SurfaceCaps.OffScreenPlain = true;
                desc.Width = FieldWidth;
                desc.Height = TotalHeight;
                backSurface = new Surface(desc, ddrawDevice);
            }
            catch (Exception ex)
            {
                MessageBox.Show("DirectDraw Error: " + ex.Message);
                Application.Exit();
            }
        }
        // Главный игровой цикл
        private void GameLoop(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            float elapsedMs = (float)(now - lastFrameTime).TotalMilliseconds;
            lastFrameTime = now;
            if (elapsedMs > 100f) elapsedMs = 100f;
            // Обновление декораций фона
            UpdateMenuDecorations(elapsedMs);
            if (currentState == GameState.Playing)
            {
                if (isGoalAnimating)
                {
                    goalAnimationTimer += elapsedMs;
                    if (goalAnimationTimer >= GoalAnimationDuration)
                    {
                        isGoalAnimating = false;
                        ResetBall(ballX < 0 ? 1 : -1);
                    }
                }
                else
                {
                    // Накопление времени для фиксированного шага
                    accumulator += elapsedMs;
                    while (accumulator >= FixedDeltaTime)
                    {
                        UpdatePhysics(FixedDeltaTime);
                        accumulator -= FixedDeltaTime;
                    }
                }
            }
            // Отрисовка кадра
            Render();
        }
        // Обновление фоновых декораций
        private void UpdateMenuDecorations(float dt)
        {
            gridOffsetY += 0.025f * dt;
            if (gridOffsetY >= 40f) gridOffsetY = 0f;
            // Обновление декораций
            for (int i = 0; i < menuDecorations.Count; i++)
            {
                var dec = menuDecorations[i];
                dec.Position.X += dec.SpeedX * dt;
                dec.Position.Y += dec.SpeedY * dt;
                dec.AlphaPhase += dt * 0.002f;
                dec.Rotation += dec.RotationSpeed * dt;
                // Отражение от границ
                if (dec.Position.X <= 0 || dec.Position.X + dec.Size >= FieldWidth) dec.SpeedX = -dec.SpeedX;
                if (dec.Position.Y <= UIHeaderHeight || dec.Position.Y + dec.Size >= TotalHeight) dec.SpeedY = -dec.SpeedY;
                menuDecorations[i] = dec;
            }
            // Обновление звёзд
            for (int i = 0; i < menuStars.Count; i++)
            {
                var star = menuStars[i];
                star.Position.Y += star.SpeedY * dt;
                star.AlphaPhase += star.AlphaSpeed * dt;
                if (star.Position.Y > TotalHeight)
                {
                    star.Position.Y = UIHeaderHeight;
                    Random rnd = new Random();
                    star.Position.X = rnd.Next(0, FieldWidth);
                }
                menuStars[i] = star;
            }
        }
        // Обновление физики
        private void UpdatePhysics(float dt)
        {
            if (isGoalAnimating) return;
            // Движение левой ракетки
            if (isWDown && leftPaddleY > UIHeaderHeight)
            {
                leftPaddleY -= PaddleSpeedBase * dt;
                if (leftPaddleY < UIHeaderHeight) leftPaddleY = UIHeaderHeight;
            }
            if (isSDown && leftPaddleY + PaddleHeight < TotalHeight)
            {
                leftPaddleY += PaddleSpeedBase * dt;
                if (leftPaddleY + PaddleHeight > TotalHeight) leftPaddleY = TotalHeight - PaddleHeight;
            }
            // Движение правой ракетки (режим 2 игроков)
            if (!isVsComputer)
            {
                if (isUpDown && rightPaddleY > UIHeaderHeight)
                {
                    rightPaddleY -= PaddleSpeedBase * dt;
                    if (rightPaddleY < UIHeaderHeight) rightPaddleY = UIHeaderHeight;
                }
                if (isDownDown && rightPaddleY + PaddleHeight < TotalHeight)
                {
                    rightPaddleY += PaddleSpeedBase * dt;
                    if (rightPaddleY + PaddleHeight > TotalHeight) rightPaddleY = TotalHeight - PaddleHeight;
                }
            }
            // ИИ
            if (isVsComputer) UpdateAI(dt);
            // Прогноз движения мяча
            float nextBallX = ballX + ballVx * dt;
            float nextBallY = ballY + ballVy * dt;
            // Отскок от верхней и нижней границ
            if (nextBallY <= UIHeaderHeight) { nextBallY = UIHeaderHeight; ballVy = -ballVy; PlaySnd(); }
            if (nextBallY + BallSize >= TotalHeight) { nextBallY = TotalHeight - BallSize; ballVy = -ballVy; PlaySnd(); }
            // Проверка столкновения с левой ракеткой
            if (ballVx < 0)
            {
                float paddleRightEdge = leftPaddleX + PaddleWidth;
                if (ballX >= paddleRightEdge && nextBallX <= paddleRightEdge)
                {
                    float t = (paddleRightEdge - ballX) / ballVx;
                    float collisionY = ballY + ballVy * t;
                    if (collisionY + BallSize >= leftPaddleY && collisionY <= leftPaddleY + PaddleHeight)
                    {
                        ballX = paddleRightEdge;
                        ballY = collisionY;
                        CalculateBounce(leftPaddleY);
                        PlaySnd();
                        goto MovementApplied;
                    }
                }
            }
            // Проверка столкновения с правой ракеткой
            if (ballVx > 0)
            {
                float paddleLeftEdge = rightPaddleX;
                if (ballX + BallSize <= paddleLeftEdge && nextBallX + BallSize >= paddleLeftEdge)
                {
                    float t = (paddleLeftEdge - (ballX + BallSize)) / ballVx;
                    float collisionY = ballY + ballVy * t;
                    if (collisionY + BallSize >= rightPaddleY && collisionY <= rightPaddleY + PaddleHeight)
                    {
                        ballX = paddleLeftEdge - BallSize;
                        ballY = collisionY;
                        CalculateBounce(rightPaddleY);
                        PlaySnd();
                        goto MovementApplied;
                    }
                }
            }
            ballX = nextBallX;
            ballY = nextBallY;
        MovementApplied:
            // Проверка гола (выход за левую границу)
            if (ballX < -BallSize)
            {
                rightScore++;
                PlayGoalSnd();
                TriggerGoalAnimation(new PointF(0, ballY + BallSize / 2f), Color.OrangeRed);
                ballVx = 0; ballVy = 0; ballX = -500;
                CheckWin();
            }
            // Проверка гола (выход за правую границу)
            else if (ballX > FieldWidth)
            {
                leftScore++;
                PlayGoalSnd();
                TriggerGoalAnimation(new PointF(FieldWidth, ballY + BallSize / 2f), Color.Cyan);
                ballVx = 0; ballVy = 0; ballX = FieldWidth + 500;
                UpdateRecordIfNeeded();
                CheckWin();
            }
            // Обновление шлейфа
            UpdateTrail();
        }
        // Обновление рекорда
        private void UpdateRecordIfNeeded()
        {
            if (leftScore > highScore)
            {
                highScore = leftScore;
                SaveHighScore();
            }
        }
        // Запуск анимации гола
        private void TriggerGoalAnimation(PointF epicenter, Color themeColor)
        {
            isGoalAnimating = true;
            goalAnimationTimer = 0f;
            goalEpicenter = epicenter;
            goalParticleColor = themeColor;
        }
        // Расчёт отскока от ракетки
        private void CalculateBounce(float paddleY)
        {
            float ballCenterY = ballY + BallSize / 2f;
            float paddleCenterY = paddleY + PaddleHeight / 2f;
            float offset = (ballCenterY - paddleCenterY) / (PaddleHeight / 2f);
            ballVx = -ballVx * SpeedIncrement;
            ballVy = offset * 0.4f;
            // Ограничение скорости
            if (Math.Abs(ballVx) > MaxBallSpeed) ballVx = Math.Sign(ballVx) * MaxBallSpeed;
        }
        // Обновление ИИ
        private void UpdateAI(float dt)
        {
            if (ballVx > 0)
            {
                float paddleCenterY = rightPaddleY + PaddleHeight / 2f;
                float ballCenterY = ballY + BallSize / 2f;
                float diffY = ballCenterY - paddleCenterY;
                float botSpeedFactor = 1.0f;
                float errorMargin = 0f;
                // Выбор параметров в зависимости от сложности
                if (currentDifficulty == Difficulty.Легко)
                {
                    botSpeedFactor = 0.45f;
                    errorMargin = 22f;
                }
                else if (currentDifficulty == Difficulty.Средне)
                {
                    botSpeedFactor = 0.65f;
                    errorMargin = 10f;
                }
                float currentSpeed = PaddleSpeedBase * botSpeedFactor * dt;
                // Плавное замедление при приближении к цели
                if (Math.Abs(diffY) < 40f)
                {
                    currentSpeed *= (Math.Abs(diffY) / 40f);
                }
                if (currentSpeed > 3.5f) currentSpeed = 3.5f;
                // Движение к цели с учётом погрешности
                if (Math.Abs(diffY) > (currentSpeed / 2f) + errorMargin)
                {
                    if (diffY > 0) rightPaddleY += currentSpeed;
                    else rightPaddleY -= currentSpeed;
                }
            }
            // Ограничение движения ИИ
            if (rightPaddleY < UIHeaderHeight) rightPaddleY = UIHeaderHeight;
            if (rightPaddleY + PaddleHeight > TotalHeight) rightPaddleY = TotalHeight - PaddleHeight;
        }
        // Обновление шлейфа мяча
        private void UpdateTrail()
        {
            ballTrail.Insert(0, new TrailPoint { Position = new PointF(ballX, ballY), Size = BallSize, Alpha = 180 });
            if (ballTrail.Count > MaxTrailPoints) ballTrail.RemoveAt(ballTrail.Count - 1);
            for (int i = 0; i < ballTrail.Count; i++)
            {
                var tp = ballTrail[i];
                float f = (float)(ballTrail.Count - i) / ballTrail.Count;
                tp.Size = BallSize * f;
                tp.Alpha = (int)(180 * f);
                ballTrail[i] = tp;
            }
        }
        // Отрисовка кадра
        private void Render()
        {
            if (backSurface == null) return;
            IntPtr hdc = backSurface.GetDc();
            using (Graphics g = Graphics.FromHdc(hdc))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.FromArgb(10, 10, 22));
                // Фоновые элементы
                DrawBackgroundElements(g);
                // Верхняя панель
                using (LinearGradientBrush headerBg = new LinearGradientBrush(new Rectangle(0, 0, FieldWidth, UIHeaderHeight),
                    Color.FromArgb(20, 20, 35), Color.FromArgb(10, 10, 22), 90f))
                {
                    g.FillRectangle(headerBg, 0, 0, FieldWidth, UIHeaderHeight);
                }
                g.DrawLine(new Pen(Color.Cyan, 2), 0, UIHeaderHeight, FieldWidth, UIHeaderHeight);
                // Информационная панель (не в меню и не в правилах)
                if (currentState != GameState.Menu && currentState != GameState.Rules)
                {
                    Font uiFont = new Font("Segoe UI", 10, FontStyle.Bold);
                    g.DrawString($"РЕКОРД: {highScore}", uiFont, Brushes.Gold, 20, 15);
                    string modeStr = isVsComputer ? $"ПРОТИВ БОТА [Сложность: {currentDifficulty}]" : "РЕЖИМ: 2 ИГРОКА";
                    g.DrawString(modeStr, uiFont, Brushes.Cyan, 20, 38);
                    g.DrawString($"{leftScore} : {rightScore}", new Font("Impact", 32), Brushes.White, 350, 5);
                }
                // Отрисовка состояния
                if (currentState == GameState.Menu)
                {
                    RenderMenu(g);
                }
                else if (currentState == GameState.Rules)
                {
                    RenderRules(g);
                }
                else
                {
                    // Пунктирная линия
                    using (Pen p = new Pen(Color.FromArgb(40, Color.Cyan), 2))
                    {
                        p.DashPattern = new float[] { 10, 10 };
                        g.DrawLine(p, FieldWidth / 2, UIHeaderHeight, FieldWidth / 2, TotalHeight);
                    }
                    // Ракетки
                    g.FillRectangle(Brushes.Cyan, leftPaddleX, leftPaddleY, PaddleWidth, PaddleHeight);
                    g.DrawRectangle(Pens.White, leftPaddleX, leftPaddleY, PaddleWidth, PaddleHeight);
                    g.FillRectangle(Brushes.OrangeRed, rightPaddleX, rightPaddleY, PaddleWidth, PaddleHeight);
                    g.DrawRectangle(Pens.White, rightPaddleX, rightPaddleY, PaddleWidth, PaddleHeight);
                    // Мяч и шлейф
                    if (currentState != GameState.GameOver && !isGoalAnimating)
                    {
                        foreach (var tp in ballTrail)
                        {
                            using (SolidBrush b = new SolidBrush(Color.FromArgb(tp.Alpha, Color.SpringGreen)))
                            {
                                float off = (BallSize - tp.Size) / 2f;
                                g.FillEllipse(b, tp.Position.X + off, tp.Position.Y + off, tp.Size, tp.Size);
                            }
                        }
                        g.FillEllipse(Brushes.White, ballX, ballY, BallSize, BallSize);
                        g.DrawEllipse(new Pen(Color.SpringGreen, 2), ballX, ballY, BallSize, BallSize);
                    }
                    // Анимация гола
                    if (isGoalAnimating && currentState != GameState.GameOver)
                    {
                        float progress = goalAnimationTimer / GoalAnimationDuration;
                        float maxRadius = 160f;
                        float currentRadius = maxRadius * progress;
                        int alpha = (int)(255 * (1f - progress));
                        if (alpha > 0)
                        {
                            using (Pen pulsePen = new Pen(Color.FromArgb(alpha, goalParticleColor), 4f))
                            {
                                g.DrawEllipse(pulsePen, goalEpicenter.X - currentRadius, goalEpicenter.Y - currentRadius, currentRadius * 2, currentRadius * 2);
                            }
                            using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(alpha, Color.White)))
                            {
                                g.DrawString("ГОЛ!", new Font("Impact", 30, FontStyle.Italic), textBrush,
                                    goalEpicenter.X == 0 ? 30 : FieldWidth - 120, goalEpicenter.Y - 25);
                            }
                        }
                    }
                    // Пауза
                    if (currentState == GameState.Pause)
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(150, 0, 0, 0)), 0, UIHeaderHeight, FieldWidth, FieldHeight);
                        g.DrawString("ПАУЗА", new Font("Impact", 40), Brushes.Yellow, 330, 260);
                        Font hintFont = new Font("Arial", 12, FontStyle.Regular);
                        g.DrawString("Нажмите [ P ] — Продолжить игру", hintFont, Brushes.White, 275, 350);
                        g.DrawString("Нажмите [ Esc ] — Выйти в меню", hintFont, Brushes.Tomato, 282, 385);
                    }
                    // Конец игры
                    if (currentState == GameState.GameOver)
                    {
                        RenderGameOverScreen(g);
                    }
                }
            }
            backSurface.ReleaseDc(hdc);
            // Вывод на экран
            try
            {
                Point p = this.PointToScreen(Point.Empty);
                primarySurface.Draw(new Rectangle(p.X, p.Y, FieldWidth, TotalHeight), backSurface, DrawFlags.Wait);
            }
            catch { }
        }
        // Отрисовка фоновых элементов
        private void DrawBackgroundElements(Graphics g)
        {
            // Звёзды
            foreach (var star in menuStars)
            {
                int alpha = 35 + (int)(25 * Math.Sin(star.AlphaPhase));
                using (SolidBrush starBrush = new SolidBrush(Color.FromArgb(alpha, Color.White)))
                {
                    g.FillRectangle(starBrush, star.Position.X, star.Position.Y, 2, 2);
                }
            }
            // Декорации
            foreach (var dec in menuDecorations)
            {
                int alpha = dec.BaseAlpha + (int)(15 * Math.Sin(dec.AlphaPhase));
                if (alpha < 0) alpha = 0; if (alpha > 255) alpha = 255;
                var oldMatrix = g.Transform;
                g.TranslateTransform(dec.Position.X + dec.Size / 2f, dec.Position.Y + dec.Size / 2f);
                g.RotateTransform(dec.Rotation);
                using (Pen p = new Pen(Color.FromArgb(alpha, dec.ThemeColor), 1.8f))
                {
                    if (dec.Type == DecorationType.Paddle)
                    {
                        float w = dec.Size * 0.25f;
                        float h = dec.Size * 0.8f;
                        g.DrawRectangle(p, -w / 2f, -h / 2f, w, h);
                    }
                    else if (dec.Type == DecorationType.Ball)
                    {
                        float d = dec.Size * 0.5f;
                        g.DrawEllipse(p, -d / 2f, -d / 2f, d, d);
                        g.DrawArc(p, -d / 2f, -d / 2f, d, d, 45, 90);
                    }
                }
                g.Transform = oldMatrix;
            }
        }
        // Отрисовка меню
        private void RenderMenu(Graphics g)
        {
            // Сетка
            using (Pen gridPen = new Pen(Color.FromArgb(12, Color.Cyan), 1f))
            {
                for (int x = 0; x <= FieldWidth; x += 40) g.DrawLine(gridPen, x, UIHeaderHeight, x, TotalHeight);
                for (float y = UIHeaderHeight + gridOffsetY; y <= TotalHeight; y += 40)
                {
                    if (y >= UIHeaderHeight) g.DrawLine(gridPen, 0, y, FieldWidth, y);
                }
            }
            // Заголовок
            g.DrawString("PING PONG", new Font("Impact", 65, FontStyle.Bold), Brushes.Black, 223, 113);
            g.DrawString("PING PONG", new Font("Impact", 65, FontStyle.Bold), Brushes.Cyan, 220, 110);
            // Блок рекорда
            using (LinearGradientBrush recordBox = new LinearGradientBrush(new Rectangle(250, 220, 300, 45),
                Color.FromArgb(40, 255, 215, 0), Color.FromArgb(5, 255, 215, 0), 0f))
            {
                g.FillRectangle(recordBox, 250, 220, 300, 45);
                g.DrawRectangle(new Pen(Color.Gold, 1.5f), 250, 220, 300, 45);
            }
            string recText = $"РЕКОРД: {highScore}";
            Font recFont = new Font("Segoe UI", 12, FontStyle.Bold);
            SizeF recSize = g.MeasureString(recText, recFont);
            g.DrawString(recText, recFont, Brushes.Gold, 250 + (300 - recSize.Width) / 2, 232);
            // Пункты меню
            string[] items = { "Правила и Управление", "Два Игрока", "Против БОТА", $"Сложность: {currentDifficulty}", "Сброс рекорда", "Выход" };
            for (int i = 0; i < items.Length; i++)
            {
                bool sel = (i == menuIndex);
                bool isDifficulty = (i == 3);
                bool isTwoPlayersSelected = (menuIndex == 1); // выбран пункт "Два Игрока"
                Color textColor;
                FontStyle fontStyle = sel ? FontStyle.Bold : FontStyle.Regular;
                // Если выбран "Два Игрока", пункт "Сложность" становится серым и недоступным
                if (isDifficulty && isTwoPlayersSelected)
                {
                    textColor = Color.Gray;
                    fontStyle = FontStyle.Regular;
                }
                else if (sel)
                {
                    textColor = Color.Yellow;
                }
                else
                {
                    textColor = Color.White;
                }
                g.DrawString(sel ? "> " + items[i] : items[i], new Font("Arial", 18, fontStyle),
                    new SolidBrush(textColor), 250, 300 + i * 42);
            }
        }
        // Отрисовка экрана правил
        private void RenderRules(Graphics g)
        {
            g.DrawString("ПРАВИЛА И УПРАВЛЕНИЕ", new Font("Impact", 36), Brushes.Cyan, 170, 110);
            g.DrawRectangle(new Pen(Color.FromArgb(100, Color.Cyan), 2), 80, 190, 640, 380);
            g.FillRectangle(new SolidBrush(Color.FromArgb(45, 10, 10, 30)), 80, 190, 640, 380);
            Font titleF = new Font("Arial", 14, FontStyle.Bold);
            Font textF = new Font("Arial", 12, FontStyle.Regular);
            g.DrawString("УПРАВЛЕНИЕ:", titleF, Brushes.Yellow, 110, 220);
            g.DrawString("• Левый игрок: Клавиши [ W ] — Вверх, [ S ] — Вниз", textF, Brushes.White, 130, 255);
            g.DrawString("• Правый игрок: Клавиши [ Стрелка Вверх ], [ Стрелка Вниз ]", textF, Brushes.White, 130, 285);
            g.DrawString("• Пауза в игре: Клавиша [ P ]", textF, Brushes.White, 130, 315);
            g.DrawString("• Вернуться в меню во время матча: Клавиша [ Esc ]", textF, Brushes.White, 130, 345);
            g.DrawString("ПРАВИЛА ИГРЫ:", titleF, Brushes.Yellow, 110, 395);
            g.DrawString("• Матч ведется до достижения одним из игроков 11 очков.", textF, Brushes.White, 130, 430);
            g.DrawString("• Мяч ускоряется на 5% после каждого успешного удара о ракетку!", textF, Brushes.SpringGreen, 130, 460);
            g.DrawString("• Бот имеет честную скорость, равную уровню сложности.", textF, Brushes.White, 130, 490);
            g.DrawString("Нажмите [ Esc ] или [ Enter ] для возврата в меню", new Font("Arial", 12, FontStyle.Italic), Brushes.Tomato, 240, 540);
        }
        // Отрисовка экрана окончания игры
        private void RenderGameOverScreen(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(160, 0, 0, 15)), 0, UIHeaderHeight, FieldWidth, FieldHeight);
            int boxW = 500, boxH = 260;
            int boxX = (FieldWidth - boxW) / 2;
            int boxY = UIHeaderHeight + (FieldHeight - boxH) / 2;
            using (LinearGradientBrush boxBg = new LinearGradientBrush(new Rectangle(boxX, boxY, boxW, boxH),
                Color.FromArgb(235, 15, 15, 35), Color.FromArgb(235, 25, 25, 55), 45f))
            {
                g.FillRectangle(boxBg, boxX, boxY, boxW, boxH);
            }
            Color winnerColor = (leftScore >= 11) ? Color.Cyan : Color.OrangeRed;
            using (Pen borderPen = new Pen(winnerColor, 2.5f))
            {
                g.DrawRectangle(borderPen, boxX, boxY, boxW, boxH);
            }
            g.DrawString("МАТЧ ЗАВЕРШЕН", new Font("Impact", 36, FontStyle.Bold), Brushes.White, boxX + 80, boxY + 25);
            string winnerText = "";
            Brush winnerBrush = Brushes.White;
            if (leftScore >= 11)
            {
                winnerText = "ПОБЕДИЛ ИГРОК 1 (СЛЕВА)";
                winnerBrush = Brushes.Cyan;
            }
            else
            {
                winnerText = isVsComputer ? "ПОБЕДИЛ РОБОТ (БОТ)" : "ПОБЕДИЛ ИГРОК 2 (СПРАВА)";
                winnerBrush = Brushes.OrangeRed;
            }
            Font winFont = new Font("Segoe UI", 16, FontStyle.Bold);
            SizeF winTextSize = g.MeasureString(winnerText, winFont);
            g.DrawString(winnerText, winFont, winnerBrush, boxX + (boxW - winTextSize.Width) / 2, boxY + 105);
            string finalScoreText = $"Финальный счет: {leftScore} - {rightScore}";
            Font scoreFont = new Font("Arial", 13, FontStyle.Regular);
            SizeF scoreSize = g.MeasureString(finalScoreText, scoreFont);
            g.DrawString(finalScoreText, scoreFont, Brushes.LightGray, boxX + (boxW - scoreSize.Width) / 2, boxY + 155);
            g.DrawString("Нажмите [ Enter ] для возврата в меню", new Font("Arial", 11, FontStyle.Italic), Brushes.Yellow, boxX + 120, boxY + 210);
        }
        // Воспроизведение звуков
        private void PlaySnd() { try { hitSound?.Play(); } catch { } }
        private void PlayGoalSnd() { try { goalSound?.Play(); } catch { } }
        // Выполнение действия в меню
        private void ExecuteMenuAction()
        {
            if (menuIndex == 0) { currentState = GameState.Rules; } // Правила и Управление
            else if (menuIndex == 1) { isVsComputer = false; ResetMatch(); currentState = GameState.Playing; } // Два Игрока
            else if (menuIndex == 2) { isVsComputer = true; ResetMatch(); currentState = GameState.Playing; } // Против БОТА
            else if (menuIndex == 3) // Сложность
            {
                if (isVsComputer) // Меняем сложность только если активен режим "Против БОТА"
                {
                    currentDifficulty = (Difficulty)(((int)currentDifficulty + 1) % 3);
                    SaveSettings();
                }
            }
            else if (menuIndex == 4) { highScore = 0; SaveHighScore(); } // Сброс рекорда
            else if (menuIndex == 5) Application.Exit(); // Выход
        }
        // Сброс матча
        private void ResetMatch()
        {
            leftScore = rightScore = 0;
            leftPaddleX = 20;
            leftPaddleY = UIHeaderHeight + FieldHeight / 2 - PaddleHeight / 2;
            rightPaddleX = FieldWidth - 40;
            rightPaddleY = UIHeaderHeight + FieldHeight / 2 - PaddleHeight / 2;
            isGoalAnimating = false;
            ResetBall(1);
        }
        // Сброс мяча в центр
        private void ResetBall(int d)
        {
            ballX = FieldWidth / 2 - BallSize / 2;
            ballY = UIHeaderHeight + FieldHeight / 2 - BallSize / 2;
            ballVx = d * InitialSpeed;
            ballVy = 0.18f;
            ballTrail.Clear();
        }
        // Проверка победы
        private bool CheckWin()
        {
            if (leftScore >= 11 || rightScore >= 11)
            {
                currentState = GameState.GameOver;
                return true;
            }
            return false;
        }
        // Обработка нажатия клавиш
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) isWDown = true; if (e.KeyCode == Keys.S) isSDown = true;
            if (e.KeyCode == Keys.Up) isUpDown = true; if (e.KeyCode == Keys.Down) isDownDown = true;
            if (currentState == GameState.Menu)
            {
                if (e.KeyCode == Keys.Up) menuIndex = (menuIndex - 1 + 6) % 6;
                if (e.KeyCode == Keys.Down) menuIndex = (menuIndex + 1) % 6;
                if (e.KeyCode == Keys.Enter) ExecuteMenuAction();
            }
            else if (currentState == GameState.Rules)
            {
                if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter) currentState = GameState.Menu;
            }
            else if (currentState == GameState.Playing)
            {
                if (e.KeyCode == Keys.P) currentState = GameState.Pause;
                else if (e.KeyCode == Keys.Escape) currentState = GameState.Menu;
            }
            else if (currentState == GameState.Pause)
            {
                if (e.KeyCode == Keys.P) currentState = GameState.Playing;
                else if (e.KeyCode == Keys.Escape) currentState = GameState.Menu;
            }
            else if (currentState == GameState.GameOver)
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape) currentState = GameState.Menu;
            }
            base.OnKeyDown(e);
        }
        // Обработка отпускания клавиш
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) isWDown = false; if (e.KeyCode == Keys.S) isSDown = false;
            if (e.KeyCode == Keys.Up) isUpDown = false; if (e.KeyCode == Keys.Down) isDownDown = false;
            base.OnKeyUp(e);
        }
        // Загрузка рекорда из файла
        private void LoadHighScore() { try { string p = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "highscore.txt"); if (File.Exists(p)) highScore = int.Parse(File.ReadAllText(p)); } catch { } }
        // Сохранение рекорда в файл
        private void SaveHighScore() { try { File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "highscore.txt"), highScore.ToString()); } catch { } }
        // Загрузка настроек из файла
        private void LoadSettings()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.json");
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                    if (dict != null && dict.ContainsKey("Difficulty"))
                    {
                        currentDifficulty = (Difficulty)Enum.Parse(typeof(Difficulty), dict["Difficulty"]);
                    }
                }
            }
            catch { }
        }
        // Сохранение настроек в файл
        private void SaveSettings()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.json");
                var dict = new Dictionary<string, string> { { "Difficulty", currentDifficulty.ToString() } };
                string json = JsonConvert.SerializeObject(dict, Formatting.Indented);
                File.WriteAllText(path, json);
            }
            catch { }
        }
        // Закрытие формы
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            gameTimer?.Stop(); ddrawDevice?.Dispose(); base.OnFormClosing(e);
        }
    }
}