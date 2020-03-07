//Правила
//Место действия этой игры — «вселенная» — это размеченная на клетки поверхность или плоскость — безграничная, ограниченная, или замкнутая(в пределе — бесконечная плоскость).
//Каждая клетка на этой поверхности может находиться в двух состояниях: быть «живой» (заполненной) или быть «мёртвой» (пустой). Клетка имеет восемь соседей, окружающих её.
//Распределение живых клеток в начале игры называется первым поколением.Каждое следующее поколение рассчитывается на основе предыдущего по таким правилам: 
//в пустой(мёртвой) клетке, рядом с которой ровно три живые клетки, зарождается жизнь;
//если у живой клетки есть две или три живые соседки, то эта клетка продолжает жить; в противном случае, если соседей меньше двух или больше трёх, клетка умирает(«от одиночества» 
//или «от перенаселённости»)
//также в игру введены дополнительные условия выживания клетки: максимальное время жизни и риск внезпаной смерти
//Игра прекращается, если:
//на поле не останется ни одной «живой» клетки - РЕАЛИЗОВАНО
//конфигурация на очередном шаге в точности(без сдвигов и поворотов) повторит себя же на одном из более ранних шагов(складывается периодическая конфигурация) - НЕ РЕАЛИЗОВАНО
//при очередном шаге ни одна из клеток не меняет своего состояния(складывается стабильная конфигурация; предыдущее правило, вырожденное до одного шага назад) - РЕАЛИЗОВАНО
//Эти простые правила приводят к огромному разнообразию форм, которые могут возникнуть в игре.
//Игрок не принимает прямого участия в игре, а лишь расставляет или генерирует начальную конфигурацию «живых» клеток, которые затем взаимодействуют согласно правилам уже без его 
//участия (он является наблюдателем).

using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Родилось клеток
        /// </summary>
        int born = 0;
        /// <summary>
        /// Умерло клеток
        /// </summary>
        int dead = 0;
        /// <summary>
        /// Живых клеток
        /// </summary>
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
        private MainForm Instance;
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

        private static CancellationTokenSource CTS;

        /// <summary>
        /// Запуск приложения
        /// </summary>
        public MainForm()
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

            //запускаем асинхронную задачу пошаговой трансформации поля в фоновом потоке
            Task.Run(() => {
                while (true)
                {
                    //если флаг паузы выставлен в true, ждем и проверяем через каждые 100мс
                    while (suspended)
                    {
                        Task.Delay(100);
                    }
                    try
                    {
                        Transform();
                        DrawBitmap();
                    }
                    catch (Exception ex)
                    {
                        Instance.Invoke((MethodInvoker)delegate//делегируем отрисовку GUI основному потоку, в котором обрабатывается весь интерфейс
                        {
                            MessageBox.Show(Instance, string.Format("Ошибка на шаге {0}: {1}{2}{3}", movenumber, ex.Message, Environment.NewLine, ex.InnerException?.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        });
                    }
                    movenumber++;
                    //проверяем признак пошагового выполнения
                    if (OneStep)
                    {
                        //снимаем флаг пошагового выполнения
                        OneStep = false;
                        //Устанавливаем флаг паузы
                        suspended = true;
                        //делаем доступными кнопки "пуск" и "пошаговое выполнения" и ползунок, блокируем кнопку "пауза"
                        PauseButton.Enabled = false;
                        RunButton.Enabled = true;
                        RunOneStepButton.Enabled = true;
                        FillingPercentileTracker.Enabled = true;
                    }
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
                    if (zoomFactor < 5) zoomFactor++;
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
        private void Transform()
        {
            born = 0;
            dead = 0;
            alive = 0;
            Changed = false;
            NextState = new bool[FieldHeight, FieldWidth];
            int divider = (int)(FieldHeight / 2);
            CTS = new CancellationTokenSource();
            //обсчитывать трансформацию будем в два параллельных потока
            //создаем массив асинхронных задач, делим поле пополам
            var task1 = Task.Run(() => ProcessTransform(0, divider), CTS.Token);
            var task2 = Task.Run(() => ProcessTransform(divider, FieldHeight), CTS.Token);
            Task.WhenAll(task1, task2).Wait();//дожидаемся конца выполнения всех потоков
            Instance.Invoke((MethodInvoker)delegate//делегируем отрисовку GUI основному потоку, в котором обрабатывается весь интерфейс
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
            }
            else
            {
                //Игра прекращается, если при очередном шаге ни одна из клеток не меняет своего состояния, ставим на паузу и показываем сообщение
                Instance.Invoke((MethodInvoker)delegate//делегируем отрисовку GUI основному потоку, в котором обрабатывается весь интерфейс
                {
                    CTS.Cancel();
                    button2_Click(null, null);//нажимаем кнопку "Пауза"
                    MessageBox.Show(Instance, string.Format("Cложилась стабильная конфигурация на шаге# {0}", movenumber), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });
            }
        }

        private async Task<bool> ProcessTransform(int from, int to)
        {
            await Task.Run(() =>
            {
                Random sdrnd = new Random(Guid.NewGuid().GetHashCode());
                for (int y = from; y < to; y++)
                {
                    for (int x = 0; x < FieldWidth; x++)
                    {
                        int ncount = GetNeighborsMask(y, x);//получаем количество соседей из байта состояния
                        bool state = CurrentState[y, x];//текущее состояние клетки: true = жива
                        switch (state)
                        {
                            case true:// клетка жива на текущем шаге
                                LifeTime[y, x]++;
                                alive++;
                                var CurrentSuddenDeathRisk = SuddenDeathPercent * (LifeTime[y, x] / MaxTTL) * 1.00;// риск внезапной смерти растет с возрастом клетки

                                // проверяем смертельные факторы, если хоть один сработал - клетке пиздец
                                // TODO: назначить старым клеткам пенсию и поиграть с ее выдачей и пенсионным возрастом
                                if (ncount > MaxNeighbors// перенаселённость
                                || ncount < MinNeighbors// одиночество
                                || (LifeTime[y, x] >= MaxTTL)// старость
                                || (sdrnd.NextDouble() * 100 <= SuddenDeathPercent))// Внезапная (случайная) смерть
                                {
                                    // клетка умирает
                                    dead++;
                                    alive--;
                                    NextState[y, x] = false;
                                    LifeTime[y, x] = 0;
                                }
                                else
                                {
                                    // клетка продолжает жить
                                    NextState[y, x] = true;
                                }
                                break;
                            case false:// клетка мертва на текущем шаге
                                if (!state && ncount == NeighborsBornNew)
                                {
                                    // в пустой (мёртвой) клетке, рядом с которой определенное количество живых клеток, зарождается жизнь
                                    // TODO: назначить окружающим клеткам выплату материнского капитала, ускорить старение "многодетных" клеток и т. д...
                                    NextState[y, x] = true;
                                    born++;
                                }
                                else
                                {
                                    // клетка остается мертвой
                                    NextState[y, x] = false;
                                }
                                break;
                        }
                        // Вычисляем есть ли разница между текущим и следующим шагом с помощью логического ИЛИ
                        // (достаточно одного изменения в массиве, чтобы итоговое значение было Changed == true)
                        Changed |= CurrentState[y, x] != NextState[y, x];
                    }
                }
            });
            return true;
        }

        /// <summary>
        /// Находит соседей указанной клетки и возвращает их количество
        /// </summary>
        /// <param name="y">координата y указанной клетки</param>
        /// <param name="x">координата x указанной клетки</param>
        /// <returns>1 байт с битовой маской соседей указанной клетки, начинай с левой верхней</returns>
        private int GetNeighborsMask(int y, int x)
        {
            int res = 0;
            //Противоположные края поля замкнуты друг на друга, т.е.при переходе через границы точка уходит на противоположную сторону картинки
            var Yinc = (y + 1) % FieldHeight;//инкрементированное значение координаты Y
            var Ydec = (FieldHeight + y - 1) % FieldHeight;//декрементированное значение координаты Y
            var Xinc = (x + 1) % FieldWidth;//инкрементированное значение координаты X
            var Xdec = (FieldWidth + x - 1) % FieldWidth;//декрементированное значение координаты X
            if (CurrentState[Yinc, Xdec]) res++;
            if (CurrentState[Yinc, (x)]) res++;
            if (CurrentState[Yinc, Xinc]) res++;
            if (CurrentState[(y), Xdec]) res++;
            if (CurrentState[(y), Xinc]) res++;
            if (CurrentState[Ydec, Xdec]) res++;
            if (CurrentState[Ydec, (x)]) res++;
            if (CurrentState[Ydec, Xinc]) res++;
            return res;
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
                        SetBit(linePtr, x, CurrentState[y, x]);
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
            PauseButton.Enabled = true;
            RunButton.Enabled = false;
            RunOneStepButton.Enabled = false;
            FillingPercentileTracker.Enabled = false;
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
            PauseButton.Enabled = false;
            RunButton.Enabled = true;
            RunOneStepButton.Enabled = true;
            FillingPercentileTracker.Enabled = true;
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
            InitialDensityPercent = FillingPercentileTracker.Value;
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
                RunButton.Enabled = true;
                RunOneStepButton.Enabled = true;
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
            PauseButton.Enabled = true;
            RunButton.Enabled = false;
            RunOneStepButton.Enabled = false;
            FillingPercentileTracker.Enabled = false;
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
