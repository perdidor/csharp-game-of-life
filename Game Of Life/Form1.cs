//Правила
//Место действия этой игры — «вселенная» — это размеченная на клетки поверхность или плоскость — безграничная, ограниченная, или замкнутая(в пределе — бесконечная плоскость).
//Каждая клетка на этой поверхности может находиться в двух состояниях: быть «живой» (заполненной) или быть «мёртвой» (пустой). Клетка имеет восемь соседей, окружающих её.
//Распределение живых клеток в начале игры называется первым поколением.Каждое следующее поколение рассчитывается на основе предыдущего по таким правилам: 
//в пустой(мёртвой) клетке, рядом с которой ровно три живые клетки, зарождается жизнь;
//если у живой клетки есть две или три живые соседки, то эта клетка продолжает жить; в противном случае, если соседей меньше двух или больше трёх, клетка умирает(«от одиночества» 
//или «от перенаселённости»)
//Игра прекращается, если
//на поле не останется ни одной «живой» клетки - РЕАЛИЗОВАНО
//НЕ РЕАЛИЗОВАНО - конфигурация на очередном шаге в точности(без сдвигов и поворотов) повторит себя же на одном из более ранних шагов(складывается периодическая конфигурация) - НЕ РЕАЛИЗОВАНО
//НЕ РЕАЛИЗОВАНО - при очередном шаге ни одна из клеток не меняет своего состояния(складывается стабильная конфигурация; предыдущее правило, вырожденное до одного шага назад) - НЕ РЕАЛИЗОВАНО
//Эти простые правила приводят к огромному разнообразию форм, которые могут возникнуть в игре.
//Игрок не принимает прямого участия в игре, а лишь расставляет или генерирует начальную конфигурацию «живых» клеток, которые затем взаимодействуют согласно правилам уже без его 
//участия (он является наблюдателем).

using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public partial class Form1 : Form
    {
        ///родится на следующем шаге, умрет на следующем шаге и текущее число живых
        int born = 0;
        int dead = 0;
        int alive = 0;
        /// <summary>
        /// минимальное количество соседей для выживания
        /// </summary>
        int MinNeighbors = 2;
        /// <summary>
        /// максимальное количество соседей для выживания
        /// </summary>
        int MaxNeighbors = 4;
        /// <summary>
        /// количество соседей, при котором в мертвой клетке зарождается жизнь
        /// </summary>
        int NeighborsBornNew = 3;
        /// <summary>
        /// Риск внезапной смерти %
        /// </summary>
        int SuddenDeathPercent = 7;
        /// <summary>
        /// максимальное время жизни клетки
        /// </summary>
        int MaxTTL = 6;
        /// <summary>
        /// массив значений текущей продолжительности жизни
        /// </summary>
        int[,] LifeTime;
        ///Признак того, что текущее состояние будет изменено на следующем шаге
        bool Changed = false;
        /// <summary>
        /// Флаг паузы задачи пошаговой трансформации
        /// </summary>
        private static bool suspended = true;
        /// <summary>
        /// Флаг признака одного шага трансформации
        /// </summary>
        private static bool OneStep = false;
        /// <summary>
        /// Количество отображаемых точек графика, может регулироваться во время работы
        /// </summary>
        private static int ChartPoints = 0;
        /// <summary>
        /// Максимальное количество отображаемых точек графика
        /// </summary>
        private static int MaxChartPoints = 0;
        /// <summary>
        /// Максимальное значение живых клеток
        /// </summary>
        private static int MaxAlive = 0;
        /// <summary>
        /// Минимальное значение живых клеток
        /// </summary>
        private static int MinAlive = 0;
        /// <summary>
        /// Максимальное значение родившихся клеток
        /// </summary>
        private static int MaxBorn = 0;
        /// <summary>
        /// Минимальное значение родившихся клеток
        /// </summary>
        private static int MinBorn = 0;
        /// <summary>
        /// Максимальное значение умерших клеток
        /// </summary>
        private static int MaxDead = 0;
        /// <summary>
        /// Минимальное значение умерших клеток
        /// </summary>
        private static int MinDead = 0;
        /// <summary>
        /// Флаг признака начального заполнения при старте программы
        /// </summary>
        private static bool seedcomplete = false;
        /// <summary>
        /// Аксессор для экземпляра формы
        /// </summary>
        private Form1 Instance;
        /// <summary>
        /// Черная кисть для мертвых клеток
        /// </summary>
        private Brush BlackBrush = new SolidBrush(Color.Black);
        /// <summary>
        /// Белая кисть для живых клеток
        /// </summary>
        private Brush WhiteBrush = new SolidBrush(Color.White);
        /// <summary>
        /// Высота поля
        /// </summary>
        private static int FieldHeight = 0;
        /// <summary>
        /// Ширина поля
        /// </summary>
        private static int FieldWidth = 0;
        /// <summary>
        /// Номер хода
        /// </summary>
        private static int movenumber = 0;
        /// <summary>
        /// Плотность живых клеток при начальном заполнении
        /// </summary>
        private int InitialDensityPercent = 2;
        /// <summary>
        /// Двумерный массив текущего состояния
        /// </summary>
        private bool[,] CurrentState = new bool[FieldHeight, FieldWidth];
        /// <summary>
        /// Двумерный массив состояния на следующем ходу
        /// </summary>
        private bool[,] NextState = new bool[FieldHeight, FieldWidth];
        /// <summary>
        /// ОБъект картинки, изображающей поле
        /// </summary>
        Bitmap CurrentBitMap;
        /// <summary>
        /// кратность увеличения
        /// </summary>
        int zoomFactor = 1;


        /// <summary>
        /// Запуск приложения
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            //инициализируем переменные
            Instance = this;
            MaxTTL = (int)ttl.Value;
            SuddenDeathPercent = (int)sdprob.Value;
            FieldHeight = pic.Height;
            FieldWidth = pic.Width;
            LifeTime = new int[FieldHeight, FieldWidth];
            CurrentBitMap = new Bitmap(FieldWidth, FieldHeight, PixelFormat.Format1bppIndexed);
            //zoomcb.SelectedIndex = 0;
            cpcb.SelectedIndex = 0;
            pic.MouseWheel += Pic_MouseWheel;
            ShowBitMap();
            Refresh();
            ChartPoints = Convert.ToInt16(cpcb.Items[0].ToString());
            //подключаем обработчик события изменения положения ползунка (если подключить раньше или в свойствах контрола, будет ошибка на две строки ранее при изменении индекса)
            cpcb.SelectedIndexChanged += cpcb_SelectedIndexChanged;
            //Выбираем максимальное значение, доступное в выпаджающем списке (должен быть отсортирован по возрастанию)
            MaxChartPoints = Convert.ToInt32(cpcb.Items[cpcb.Items.Count - 1].ToString());
            //запускаем асинхронную задачу пошаговой трансформации поля в фоне
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task.Run(() => {
                try
                {
                    while (true)
                    {
                        //если флаг паузы выставлен в true, ждем и проверяем через каждые 100мс
                        while (suspended)
                        {
                            Task.Delay(100);
                        }
                        Transform();
                        DrawBitmap();
                        movenumber++;
                        //проверяем признак одного шага
                        if (OneStep)
                        {
                            //снимаем флаг одного шага
                            OneStep = false;
                            //Устанавливаем флаг паузы
                            suspended = true;
                            //делаем доступными кнопки "пуск" и "пошаговое выполнения" и ползунок, блокируем кнопку "пауза"
                            button2.Enabled = false;
                            button1.Enabled = true;
                            button3.Enabled = true;
                            trackBar1.Enabled = true;
                        }
                    }
                }
                catch
                {

                }
            });
        }
        /// <summary>
        /// зум картинки меняется в пределах х1..х4 прокручиванием колесика мыши, на 4 Гб ОЗУ больше 4 ставить не надо памяти не хватит.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pic_MouseWheel(object sender, MouseEventArgs e)
        {
            Instance.Invoke((MethodInvoker)delegate
            {
                if (e.Delta > 3)
                {
                    if (zoomFactor < 4) zoomFactor++;
                    if (seedcomplete && suspended)
                    {
                        ShowBitMap();
                        Refresh();
                    }
                }
                if (e.Delta < -3)
                {
                    if (zoomFactor > 1) zoomFactor--;
                    if (seedcomplete && suspended)
                    {
                        ShowBitMap();
                        Refresh();
                    }
                }
            });
        }
        /// <summary>
        /// Выводим картинку с текущей степенью увеличения
        /// </summary>
        private void ShowBitMap()
        {
            Size newSize = new Size((int)(CurrentBitMap.Width * zoomFactor), (int)(CurrentBitMap.Height * zoomFactor));
            Bitmap tmpbitmap = new Bitmap(CurrentBitMap, newSize);
            pic.Width = tmpbitmap.Width;
            pic.Height = tmpbitmap.Height;
            pic.Image = tmpbitmap;
            panel1.VerticalScroll.Visible = (pic.Height > panel1.Height);
            panel1.HorizontalScroll.Visible = (pic.Width > panel1.Width);
            zoomlabel.Text = string.Format("x{0}", zoomFactor);
        }

        /// <summary>
        /// Заполнение поля случайным способом
        /// </summary>
        private void InitialSeed()
        {
            int alive = 0;
            MaxAlive = 0;
            MinAlive = 0;
            for (int y = 0; y < FieldHeight; y++)
            {
                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                for (int x = 0; x < FieldWidth; x++)
                {
                    CurrentState[y, x] = (rnd.Next(100) <= InitialDensityPercent);
                    if (CurrentState[y, x])
                    {
                        alive++;
                        MaxAlive++;
                        MinAlive++;
                    }
                }
            }
            MaxBorn = 0;
            MinBorn = 999999999;
            MaxDead = 0;
            MinDead = 999999999;
        }
        /// <summary>
        /// Трансформация текущего состояния по правилам
        /// </summary>
        private async void Transform()
        {
            born = 0;
            dead = 0;
            alive = 0;
            Changed = false;
            NextState = new bool[FieldHeight, FieldWidth];
            int divider = (int)(FieldHeight / 2);
            //обсчитывать трансформацию будем в два параллельных потока
            //создаем массив асинхронных задач, делим поле пополам
            var task1 = ProcessTransform(0, divider);
            var task2 = ProcessTransform(divider, FieldHeight);
            await Task.WhenAll(task1, task2);
            Instance.Invoke((MethodInvoker)delegate
            {
                if (alive > MaxAlive) MaxAlive = alive;
                if (alive < MinAlive) MinAlive = alive;
                if (born > MaxBorn) MaxBorn = born;
                if (born < MinBorn) MinBorn = born;
                if (dead > MaxDead) MaxDead = dead;
                if (dead < MinDead) MinDead = dead;
                movelabel.Text = string.Format("Move# {0}", movenumber);
                bornlabel.Text = string.Format("Born: {0} (Max: {1}/Min: {2})", born, MaxBorn, MinBorn);
                deadlabel.Text = string.Format("Dead: {0} (Max: {1}/Min: {2})", dead, MaxDead, MinDead);
                alivelabel.Text = string.Format("Alive: {0} (Max: {1}/Min: {2})", alive, MaxAlive, MinAlive);
                //если превышено максимальное количество отображаемых точек графика, удаляем "лишние" точки из начала коллекции, оставляем последние в количестве = MaxChartPoints
                if (chart.Series[0].Points.Count > MaxChartPoints)
                {
                    for (int i = 0; i < chart.Series[0].Points.Count - MaxChartPoints; i++)
                    {
                        chart.Series[0].Points.RemoveAt(0);
                        chart.Series[1].Points.RemoveAt(0);
                        chart.Series[2].Points.RemoveAt(0);
                    }
                }
                //Устанавливаем максимальное и минимальное значение по оси X, в зависимости от того, сколько точек выбрано для отображения и номера текущего шага
                chart.ChartAreas[0].AxisX.Minimum = (movenumber <= ChartPoints) ? 0 : (movenumber - ChartPoints);
                chart.ChartAreas[0].AxisX.Maximum = (movenumber <= ChartPoints) ? ChartPoints : (chart.ChartAreas[0].AxisX.Minimum + ChartPoints);
                //Меняем масштаб графика по оси Y, в зависимости от максимального отображаемого значения
                chart.ChartAreas[0].AxisY.Minimum = (movenumber <= ChartPoints) ? Math.Floor(Math.Min(MinAlive, Math.Min(MinDead, MinBorn)) * 0.5) : Math.Floor(Math.Min(chart.Series[1].Points.Reverse().Take(ChartPoints).Reverse().Min(a => a.YValues[0]), chart.Series[2].Points.Reverse().Take(ChartPoints).Reverse().Min(b => b.YValues[0])) * 0.5);
                chart.ChartAreas[0].AxisY.Maximum = (movenumber <= ChartPoints) ? Math.Floor(Math.Max(MaxAlive, Math.Max(MaxDead, MaxBorn)) * 1.1) : Math.Floor(chart.Series[0].Points.Reverse().Take(ChartPoints).Reverse().Max(a => a.YValues[0]) * 1.1);
                //Добавляем точки на графики количества живых, умерших и родившихся клеток на текущем шаге
                chart.Series[0].Points.AddXY(movenumber, alive);
                chart.Series[1].Points.AddXY(movenumber, born);
                chart.Series[2].Points.AddXY(movenumber, dead);
            });
            if (Changed)
            {
                //Есть изменения, копируем состояние следующего шага в текущий массив
                Array.Copy(NextState, CurrentState, NextState.Length);
            } else
            {
                //Игра прекращается, если при очередном шаге ни одна из клеток не меняет своего состояния, ставим на паузу и показываем сообщение
                suspended = true;
                MessageBox.Show(string.Format("Life freezed with no change at move# {0}", movenumber),"Information",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async Task<bool> ProcessTransform(int from, int to)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            Random sdrnd = new Random(Guid.NewGuid().GetHashCode());
            for (int y = from; y < to; y++)
            {
                for (int x = 0; x < FieldWidth; x++)
                {
                    byte nmask = GetNeighborsMask(y, x);
                    int ncount = countSetBits((int)nmask);
                    // 1. в пустой (мёртвой) клетке, рядом с которой ровно три живые клетки, зарождается жизнь;
                    // 2. если у живой клетки есть две или три живые соседки, то эта клетка продолжает жить; в противном случае, 
                    // если соседей меньше двух или больше трёх, клетка умирает («от одиночества» или «от перенаселённости»)
                    bool state = CurrentState[y, x];
                    if (state)
                    {
                        LifeTime[y, x]++;
                        alive++;
                    }
                    if (state && (ncount <= MaxNeighbors && ncount >= MinNeighbors))
                    {
                        NextState[y, x] = true;
                    }
                    if (state && (ncount > MaxNeighbors || ncount < MinNeighbors || (LifeTime[y, x] == MaxTTL) || (sdrnd.Next(100) <= SuddenDeathPercent)))
                    {
                        dead++;
                        alive--;
                        NextState[y, x] = false;
                        CurrentState[y, x] = false;
                        LifeTime[y, x] = 0;
                        Changed = true;
                    }
                    if (!state && ncount == NeighborsBornNew)
                    {
                        NextState[y, x] = true;
                        born++;
                        Changed = true;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Находит соседей указанной клетки
        /// Противоположные края поля замкнуты друг на друга, т.е. при переходе через границы точка уходит на противоположную сторону картинки
        /// </summary>
        /// <param name="y">координата y указанной клетки</param>
        /// <param name="x">координата x указанной клетки</param>
        /// <returns>1 байт с битовой маской соседей указанной клетки, начинай с левой верхней</returns>
        private byte GetNeighborsMask(int y, int x)
        {
            byte res = 0x00;
            //клетка НЕ находится на краю поля
            if (y > 0 && x > 0)
            {
                if (CurrentState[(y - 1) % FieldHeight, (x - 1) % FieldWidth]) res |= (1 << 0);
                if (CurrentState[(y - 1) % FieldHeight, (x) % FieldWidth]) res |= (1 << 1);
                if (CurrentState[(y - 1) % FieldHeight, (x + 1) % FieldWidth]) res |= (1 << 2);
                if (CurrentState[(y) % FieldHeight, (x - 1) % FieldWidth]) res |= (1 << 3);
                if (CurrentState[(y) % FieldHeight, (x + 1) % FieldWidth]) res |= (1 << 4);
                if (CurrentState[(y + 1) % FieldHeight, (x - 1) % FieldWidth]) res |= (1 << 5);
                if (CurrentState[(y + 1) % FieldHeight, (x) % FieldWidth]) res |= (1 << 6);
                if (CurrentState[(y + 1) % FieldHeight, (x + 1) % FieldWidth]) res |= (1 << 7);
            }
            //клетка находится на левом краю поля
            if (x == 0 && y > 0)
            {
                if (CurrentState[(y - 1) % FieldHeight, FieldWidth - 1]) res |= (1 << 0);
                if (CurrentState[(y - 1) % FieldHeight, (x) % FieldWidth]) res |= (1 << 1);
                if (CurrentState[(y - 1) % FieldHeight, (x + 1) % FieldWidth]) res |= (1 << 2);
                if (CurrentState[(y) % FieldHeight, FieldWidth - 1]) res |= (1 << 3);
                if (CurrentState[(y) % FieldHeight, (x + 1) % FieldWidth]) res |= (1 << 4);
                if (CurrentState[(y + 1) % FieldHeight, FieldWidth - 1]) res |= (1 << 5);
                if (CurrentState[(y + 1) % FieldHeight, (x) % FieldWidth]) res |= (1 << 6);
                if (CurrentState[(y + 1) % FieldHeight, (x + 1) % FieldWidth]) res |= (1 << 7);
            }
            //клетка находится на верхнем краю поля
            if (x > 0 && y == 0)
            {
                if (CurrentState[FieldHeight - 1 - y, (x - 1) % FieldWidth]) res |= (1 << 0);
                if (CurrentState[FieldHeight - 1 - y, (x) % FieldWidth]) res |= (1 << 1);
                if (CurrentState[FieldHeight - 1 - y, (x + 1) % FieldWidth]) res |= (1 << 2);
                if (CurrentState[(y) % FieldHeight, (x - 1) % FieldWidth]) res |= (1 << 3);
                if (CurrentState[(y) % FieldHeight, (x + 1) % FieldWidth]) res |= (1 << 4);
                if (CurrentState[(y + 1) % FieldHeight, (x - 1) % FieldWidth]) res |= (1 << 5);
                if (CurrentState[(y + 1) % FieldHeight, (x) % FieldWidth]) res |= (1 << 6);
                if (CurrentState[(y + 1) % FieldHeight, (x + 1) % FieldWidth]) res |= (1 << 7);
            }
            //клетка находится в левом верхнем углу
            if (x == 0 && y == 0)
            {
                if (CurrentState[FieldHeight - 1 - y, FieldWidth - 1]) res |= (1 << 0);
                if (CurrentState[FieldHeight - 1 - y, (x) % FieldWidth]) res |= (1 << 1);
                if (CurrentState[FieldHeight - 1 - y, (x + 1) % FieldWidth]) res |= (1 << 2);
                if (CurrentState[(y) % FieldHeight, FieldWidth - 1]) res |= (1 << 3);
                if (CurrentState[(y) % FieldHeight, (x + 1) % FieldWidth]) res |= (1 << 4);
                if (CurrentState[(y + 1) % FieldHeight, FieldWidth - 1]) res |= (1 << 5);
                if (CurrentState[(y + 1) % FieldHeight, (x) % FieldWidth]) res |= (1 << 6);
                if (CurrentState[(y + 1) % FieldHeight, (x + 1) % FieldWidth]) res |= (1 << 7);
            }
            return res;
        }
        /// <summary>
        /// Считает число соседей
        /// </summary>
        /// <param name="n">1 байт с битовой маской соседей указанной клетки, начинай с левой верхней</param>
        /// <returns></returns>
        static int countSetBits(int n)
        {
            int count = 0;
            while (n > 0)
            {
                n &= (n - 1);
                count++;
            }
            return count;
        }
        /// <summary>
        /// Рисуем картинку из объекта текущего состояния
        /// </summary>
        private void DrawBitmap()
        {
            //делаем инвок для синхронизации доступа в поток GUI
            Instance.Invoke((MethodInvoker)delegate
            {
                BitmapData data = CurrentBitMap.LockBits(new Rectangle(0, 0, CurrentBitMap.Width, CurrentBitMap.Height),
                ImageLockMode.WriteOnly, CurrentBitMap.PixelFormat);
                for (int y = 0; y < CurrentBitMap.Height; y++)
                {
                    //извлекаем указатель на ряд пикселей
                    IntPtr linePtr = new IntPtr(data.Scan0.ToInt32() + data.Stride * y);
                    for (int x = 0; x < CurrentBitMap.Width; x++)
                    {
                        //Вызываем метод для отображения текущего состояния клетки на картинке
                        SetBit(linePtr, x, CurrentState[y,x]);
                    }
                }
                CurrentBitMap.UnlockBits(data);
                ShowBitMap();
                Refresh();
            });
        }
        /// <summary>
        /// Рисует точку на поле
        /// </summary>
        /// <param name="ptr">Указатель на ряд пикселей картинки</param>
        /// <param name="pixelNumber">Номер пикселя в ряду</param>
        /// <param name="pixelValue">Жива ли клетка</param>
        void SetBit(IntPtr ptr, int pixelNumber, bool pixelValue)
        {
            //вычисляем номер байта
            int byteNumber = pixelNumber / 8;
            //вычисляем номер бита в байте
            int bitNumber = pixelNumber % 8;
            byte b = Marshal.ReadByte(ptr, byteNumber);
            //Модифицируем бит в зависимости от состояния клетки
            if (pixelValue)
            {
                b |= (byte)(1 << bitNumber);
            }
            else
            {
                b &= (byte)(~(1 << bitNumber));
            }
            Marshal.WriteByte(ptr, byteNumber, b);
        }
        /// <summary>
        /// Нажатие на кнопку Пуск
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //запускаем симуляцию, делаем недоступными кнопки "пуск" и "пошаговое выполнения" и ползунок, разблокируем кнопку "пауза"
            button2.Enabled = true;
            button1.Enabled = false;
            button3.Enabled = false;
            trackBar1.Enabled = false;
            //снимаем флаг паузы
            suspended = false;
        }
        /// <summary>
        /// нажатие на кнопку пауза
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //останавливаем симуляцию, делаем доступными кнопки "пуск" и "пошаговое выполнения" и ползунок, блокируем кнопку "пауза"
            button2.Enabled = false;
            button1.Enabled = true;
            button3.Enabled = true;
            trackBar1.Enabled = true;
            //устанавливаем флаг паузы
            suspended = true;
        }
        /// <summary>
        /// Движение ползунка приводит к заполнению поля с указанным процентом живых клеток
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            chart.Series[0].Points.Clear();
            chart.Series[1].Points.Clear();
            chart.Series[2].Points.Clear();
            InitialDensityPercent = trackBar1.Value;
            seedpercent.Text = InitialDensityPercent.ToString();
            CurrentState = new bool[FieldHeight, FieldWidth];
            NextState = new bool[FieldHeight, FieldWidth];
            InitialSeed();
            movenumber = 0;
            movelabel.Text = string.Format("Move# {0}", movenumber);
            bornlabel.Text = string.Format("Born: {0} (Max: {1}/Min: {2})", 0, MaxBorn, MinBorn);
            deadlabel.Text = string.Format("Dead: {0} (Max: {1}/Min: {2})", 0, MaxDead, MinDead);
            alivelabel.Text = string.Format("Alive: {0} (Max: {1}/Min: {2})", 0, MaxAlive, MinAlive);
            DrawBitmap();
            if (!seedcomplete)
            {
                button1.Enabled = true;
                button3.Enabled = true;
                seedcomplete = true;
            }
        }
        /// <summary>
        /// Нажатие кнопки пошагового выполнения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click_1(object sender, EventArgs e)
        {
            //запускаем симуляцию, делаем недоступными кнопки "пуск" и "пошаговое выполнения" и ползунок, разблокируем кнопку "пауза"
            button2.Enabled = true;
            button1.Enabled = false;
            button3.Enabled = false;
            trackBar1.Enabled = false;
            //устанавливаем признак одного шага
            OneStep = true;
            //снимаем флаг паузы
            suspended = false;
        }
        /// <summary>
        /// Изменение числа отображаемых точек приводит к изменению масштаба графика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cpcb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChartPoints = Convert.ToInt16(cpcb.Text);
            //Делаем инвок для синхронизации доступа к GUI
            Instance.Invoke((MethodInvoker)delegate
            {
                chart.ChartAreas[0].AxisX.Minimum = (movenumber <= ChartPoints) ? 0 : (movenumber - chart.Series[0].Points.Count);
                chart.ChartAreas[0].AxisX.Maximum = (movenumber <= ChartPoints) ? ChartPoints : (chart.ChartAreas[0].AxisX.Minimum + ChartPoints);
            });
        }
        /// <summary>
        /// меняем значение риска внезапной смерти
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sdprob_ValueChanged(object sender, EventArgs e)
        {
            SuddenDeathPercent = (int)sdprob.Value;
        }
        /// <summary>
        /// меняем значение максимальной продолжительности жизни
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ttl_ValueChanged(object sender, EventArgs e)
        {
            MaxTTL = (int)ttl.Value;
        }

        private void nmin_ValueChanged(object sender, EventArgs e)
        {
            MinNeighbors = (int)nmin.Value;
        }

        private void nmax_ValueChanged(object sender, EventArgs e)
        {
            MaxNeighbors = (int)nmax.Value;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            NeighborsBornNew = (int)newborn.Value;
        }
    }
}
