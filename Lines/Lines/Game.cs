using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lines
{
    class Game
    {
        enum Status 
        {
            init,       //начало
            wait,       //ожидание выбора шарика для перемещения
            ball_selected,  //шарик выбран/отмечен (специальное отображение выбранного шарика), ожидание выбора точки назначения для выбранного шарика
            path_show,  //отображение кратчайшего пути следования шарика к точке назначения
            ball_move,  //процесс перемещения шарика
            next_balls,  //отображение мест для следующих шариков
            line_strip, //удаление составленных линий
            stop,       //игровое поле полностью заполнено
            refresh     //пользователь нажал кнопку "Начать заново"
        }

        int[,] map;                // Карта значений от 1 до 6, определяющая шарик какого цвета стоит в этой клетке.
        int[,] fmap;               // Матрица нахождения пути перемещения шарика
        Ball[] path;               // Массив шариков(вообще клеток поля) пути перемещения выбранного шарика
        Ball[] line;               // Массив шариков для удаления при составлении линии.
        private int numberOfCells; // Размер поля.
        ShowItem show;
        ShowStat stat;
        SendInfo send;
        Status status;             // Текущий статус игры.
        Ball[] balls = new Ball[3]; //Массив шариков, новых шариков, которые появляются каждый ход (если не составлена линия).
        Random rand = new Random();
        Ball selectedBall; //Выбранный для перемещения шарик
        Ball destinationPlaceBall; //Место назначения, куда именно игрок хочет выбранный шарик переместить
        int changedBall; //Служебная переменная для определения анимации выбранного шарика(0 - большой, 1 - обычный средний)

        public Game(int numberOfCells, ShowItem show, ShowStat stat, SendInfo send)
        {
            this.numberOfCells = numberOfCells;
            this.show = show;
            this.stat = stat;
            this.send = send;
            map = new int[numberOfCells, numberOfCells];
            fmap = new int[numberOfCells, numberOfCells];
            path = new Ball[numberOfCells * numberOfCells];
            line = new Ball[99];
            status = Status.init;
        }

        private void InitMap() //Метод очищает поле игры.
        {
            Ball none;
            none.color = 0;
            for (int x = 0; x < numberOfCells; x++)
            {
                for (int y = 0; y < numberOfCells; y++)
                {
                    map[x, y] = 0;
                    none.x = x;
                    none.y = y;
                    show(none, Item.none);
                }
            }
        }

        public void Step() // Основная функция программы. Следит за этапами работы программы.
        {
            switch (status)
            {
                case Status.init:
                                    InitMap();
                                    SelectNextBalls();
                                    ShowNextBalls();
                                    SelectNextBalls();
                                    status = Status.wait;
                                    break;
                case Status.wait:
                                    break;
                case Status.ball_selected:
                                    BallSelected();
                                    break;
                case Status.path_show:
                                    ShowPath();
                                    break;
                case Status.ball_move:
                                    MoveBall();
                                    break;
                case Status.next_balls:
                                    ShowNextBalls();
                                    SelectNextBalls();
                                    break;
                case Status.line_strip:
                                    DeleteReadyLines();
                                    break;
                case Status.stop:
                                    send(true);
                                    status = Status.init;
                                    break;
                case Status.refresh:
                                    status = Status.init;
                                    break;

                default:
                                    break;
            }
        }

        public void ClickBox(int x, int y) //Метод срабатывает при клики на какой-либо ячейке игрового поля
        {
            if (status == Status.wait || status == Status.ball_selected)
            {
                if (map[x, y] > 0)
                {
                    if (status == Status.ball_selected)
                    {
                        show(selectedBall, Item.aver);
                    }
                    selectedBall.x = x;
                    selectedBall.y = y;
                    selectedBall.color = map[x, y];
                    status = Status.ball_selected;
                }
            }
            if (status == Status.ball_selected)
            {
                if (map[x, y] <= 0)
                {
                    destinationPlaceBall.x = x;
                    destinationPlaceBall.y = y;
                    destinationPlaceBall.color = selectedBall.color;
                    if (FindPath())
                    {
                        status = Status.path_show;
                    }
                    return;
                }
            }
            /*if (status == Status.stop)
            {
                status = Status.init;
            }*/
        }

        private void ShowHiddenBalls()
        {
            for (int i = 0; i < 3; i++)
            {
                ShowHiddenBall(balls[i]); 
            }
        }

        private void ShowHiddenBall(Ball next)
        {
            if (next.x < 0)
            {
                return;
            }
            show(next, Item.small);
        }

        private void SelectNextBalls() //Выбирает места появления будущих шариков. Отображает маленькие шарики.
        {
            for (int i = 0; i < balls.Length; i++)
            {
                balls[i] = SelectOneBall();
            }
        }

        private Ball SelectOneBall()
        {
            return SelectOneBall(rand.Next(1, 7));
        }

        private Ball SelectOneBall(int color) //Выбирает место появления будущего шарика
        {
            Ball nextBall;
            nextBall.color = color;
            int loop = 100;
            do
            {
                nextBall.x = rand.Next(0, numberOfCells);
                nextBall.y = rand.Next(0, numberOfCells);
                if (--loop < 0)
                {
                    nextBall.x = -1;
                    return nextBall;
                }
            } while (map[nextBall.x, nextBall.y] != 0);
            map[nextBall.x, nextBall.y] = -1;
            show(nextBall, Item.small);
            return nextBall;
        }

        private void ShowNextBalls() //Отображает обычные, средние шарики.
        {
            for (int i = 0; i < balls.Length; i++)
            {
                ShowOneBall(balls[i]);
            }
            if (FindReadyLines())
            {
                status = Status.line_strip;
            }
            else
            {
                if (IsMapFull())
                {
                    status = Status.stop;
                }
                else
                {
                    status = Status.wait;
                }
            }
        }

        private void ShowOneBall(Ball ball) //Отображает 1 средний шарик.
        {
            if (ball.x < 0)
            {
                return;
            }
            if (map[ball.x, ball.y] > 0)
            {
                ball = SelectOneBall(ball.color);
                if (ball.x < 0)
                {
                    return;
                }
            }
            map[ball.x, ball.y] = ball.color;
            show(ball, Item.aver);
        }

        private void BallSelected() //Отображает анимацию выбранного шарика (изменение размера шарика со среднего на большого)
        {
            if (changedBall == 0)
            {
                show(selectedBall, Item.big);
            }
            else
            {
                show(selectedBall, Item.aver);
            }
            changedBall = 1 - changedBall;
        }

        int pathLenght; //Длина найденного пути перемещения шарика
        private bool FindPath() //Ищет путь для перемещение шарика
        {
            if (!(map[selectedBall.x, selectedBall.y] > 0 &&
                map[destinationPlaceBall.x, destinationPlaceBall.y] <= 0))
            {
                return false;
            }
            for (int x = 0; x < numberOfCells; x++)
            {
                for (int y = 0; y < numberOfCells; y++)
                {
                    fmap[x, y] = 0;
                }
            }
            bool added;
            bool found = false;
            fmap[selectedBall.x, selectedBall.y] = 1;
            int numberOfMove = 1;
            do
            {
                added = false;
                for (int x = 0; x < numberOfCells; x++)
                {
                    for (int y = 0; y < numberOfCells; y++)
                    {
                        if (fmap[x, y] == numberOfMove)
                        {
                            MarkPath(x + 1, y, numberOfMove + 1);
                            MarkPath(x - 1, y, numberOfMove + 1);
                            MarkPath(x, y + 1, numberOfMove + 1);
                            MarkPath(x, y - 1, numberOfMove + 1);
                            added = true;
                        }
                    }
                }
                if (fmap[destinationPlaceBall.x, destinationPlaceBall.y] > 0)
                {
                    found = true;
                    break;
                }
                numberOfMove++;
            } while (added);
            if (!found)
            {
                return false;
            }
            int pathX = destinationPlaceBall.x;
            int pathY = destinationPlaceBall.y;
            pathLenght = numberOfMove;
            while (numberOfMove >= 0)
            {
                path[numberOfMove].x = pathX;
                path[numberOfMove].y = pathY;
                if (IsPathDesired(pathX + 1, pathY, numberOfMove)) //Здесь может быть ошибка - может (numberOfMove - 1)
                {
                    pathX++;
                }
                if (IsPathDesired(pathX - 1, pathY, numberOfMove)) //Здесь может быть ошибка - может (numberOfMove - 1)
                {
                    pathX--;
                }
                if (IsPathDesired(pathX, pathY + 1, numberOfMove)) //Здесь может быть ошибка - может (numberOfMove - 1)
                {
                    pathY++;
                }
                if (IsPathDesired(pathX, pathY - 1, numberOfMove)) //Здесь может быть ошибка - может (numberOfMove - 1)
                {
                    pathY--;
                }
                numberOfMove--;
            }
            stepShowPath = 0;
            return true;
        }

        private void MarkPath(int x, int y, int nOM) //Служебный метод для метода FinfPath()
        {
            if (x < 0 || x >= numberOfCells || y < 0 || y >= numberOfCells)
            {
                return;
            }
            if (map[x, y] > 0)
            {
                return;
            }
            if (fmap[x, y] > 0)
            {
                return;
            }
            fmap[x, y] = nOM;
        }

        private bool IsPathDesired(int x, int y, int nOM) //Служебный метод для метода FinfPath()
        {
            if (x < 0 || x >= numberOfCells || y < 0 || y >= numberOfCells)
            {
                return false;
            }
            return fmap[x, y] == nOM;
        }

        int stepShowPath; //Этап отображение найденного пути для перемещения шарика
        private void ShowPath()//Отображает найденный путь для перемещения шарика.
        {
            if (stepShowPath == 0)
            {
                for (int i = 1; i <= pathLenght; i++)
                {
                    show(path[i], Item.path);
                }
                stepShowPath++;
                return;
            }
            Ball movingBall;

            movingBall = path[stepShowPath - 1];
            show(movingBall, Item.none);

            movingBall = path[stepShowPath];
            movingBall.color = selectedBall.color;
            show(movingBall, Item.aver);

            stepShowPath++;

            if (stepShowPath > pathLenght)
            {
                ShowHiddenBalls();
                status = Status.ball_move;
            }
        }

        private void MoveBall() //Перемещает выбранный шарик к выбранному пустому месту
        {
            if (status != Status.ball_move)
            {
                return;
            }
            if (map[selectedBall.x, selectedBall.y] > 0 &&
                map[destinationPlaceBall.x, destinationPlaceBall.y] <= 0)
            {
                map[selectedBall.x, selectedBall.y] = 0;
                map[destinationPlaceBall.x, destinationPlaceBall.y] = selectedBall.color;
                show(selectedBall, Item.none);
                show(destinationPlaceBall, Item.aver);
                if (FindReadyLines())
                {
                    status = Status.line_strip;
                }
                else
                {
                    status = Status.next_balls;
                }
            }
        }

        int lines; //Длина линии из шариков - сколько шариков составляют линию
        int lineStep; //Определяет этап анимации процесса удаления шарика
        private bool FindReadyLines() //Проверяет составление линий из 5-ти больше шариков одного цвета
        {
            lines = 0;
            for (int x = 0; x < numberOfCells; x++)
            {
                for (int y = 0; y < numberOfCells; y++)
                {
                    CheckLine(x, y, 1, 0);
                    CheckLine(x, y, 1, 1);
                    CheckLine(x, y, 0, 1);
                    CheckLine(x, y, -1, 1);
                }
            }
            if (lines == 0)
            {
                return false;
            }
            lineStep = 4;
            return true;
        }

        private void CheckLine(int x, int y, int sx, int sy) //Служебный метод для метода FindReadyLines()
        {
            int lenght = 4;
            if (x < 0 || x >= numberOfCells || y < 0 || y >= numberOfCells)
            {
                return;
            }
            if (x + lenght * sx < 0 || x + lenght * sx >= numberOfCells ||
                y + lenght * sy < 0 || y + lenght * sy >= numberOfCells)
            {
                return;
            }
            int color = map[x, y];
            if (color <= 0)
            {
                return;
            }
            for (int i = 1; i <= lenght; i++)
            {
                if (map[x + i * sx, y + i * sy] != color)
                {
                    return;
                }
            }
            Ball checkingBall;
            for (int i = 0; i <= lenght; i++)
            {
                checkingBall.x = x + i * sx;
                checkingBall.y = y + i * sy;
                checkingBall.color = color;
                if (!(line.Contains(checkingBall)))
                {
                    line[lines] = checkingBall;
                    lines++;
                }
                /*line[lines].x = x + i * sx;
                line[lines].y = y + i * sy;
                line[lines].color = color;
                lines++;*/
            }
        }

        private void DeleteReadyLines() //Производит анимацию удаления линии шариков
        {
            if (lineStep <= 0)
            {
                for (int i = 0; i < lines; i++)
                {
                    map[line[i].x, line[i].y] = 0;
                    ShowHiddenBalls();
                }
                stat(lines);
                status = Status.wait;
                return;
            }
            lineStep--;
            for (int i = 0; i < lines; i++)
            {
                switch (lineStep)
                {
                    case 3: show(line[i], Item.big); break;
                    case 2: show(line[i], Item.aver); break;
                    case 1: show(line[i], Item.small); break;
                    case 0: show(line[i], Item.none); break;
                    default: break;
                }
            }
        }

        private bool IsMapFull() //Метод проверяет заполненность поля.
        {
            for (int x = 0; x < numberOfCells; x++)
            {
                for (int y = 0; y < numberOfCells; y++)
                {
                    if (map[x, y] <= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void ClickRefresh()
        {
            status = Status.refresh;
        }
    }
}
