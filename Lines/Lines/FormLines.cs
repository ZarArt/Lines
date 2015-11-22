using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lines
{
    public delegate void ShowItem(Ball ball, Item item);
    public delegate void ShowStat(int balls);
    public delegate void SendInfo(bool info);
    public partial class FormLines : Form
    {
        PictureBox[,] boxes;      // Массив картинок для отображения на форме.
        int numberOfCells = 9;    // Размер поля. В данном случае 9 * 9.
        int cellSize = 64;        // Размер клетки. В данном случае 40 * 40.
        Game game;
        Statistics statistics;
        public FormLines()
        {
            InitializeComponent();
            game = new Game(numberOfCells, ShowItem, ShowStat, HandlingFinishInfo);
            CreateBoxes();
            this.Size = new Size(numberOfCells * cellSize + 10, numberOfCells * cellSize + 55);
            timer.Enabled = true;
            statistics = new Statistics("ToR.dat");
        }

        private void HandlingFinishInfo(bool full)
        {
            timer.Enabled = false;
            if (full)
            {
                DialogResult resultQuestionToSave = MessageBox.Show("Игра окончена.\nХотите сохранить результат?",
                    "Game over", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultQuestionToSave == System.Windows.Forms.DialogResult.Yes)
                {
                    statistics.WriteTable(toolStripTextBoxRealName.Text, toolStripTextBoxRealScore.Text);
                    if (QuestionToPlayAgain() == System.Windows.Forms.DialogResult.Yes)
                    {
                        timer.Enabled = true;
                        return;
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    if (QuestionToPlayAgain() == System.Windows.Forms.DialogResult.Yes)
                    {
                        timer.Enabled = true;
                        return;
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
        }

        private DialogResult QuestionToPlayAgain()
        {
            DialogResult result = MessageBox.Show("Хотите сыграть заново?",
                "Game over", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return result;
        }

        public void CreateBoxes()
        {
            boxes = new PictureBox[numberOfCells, numberOfCells];
            for (int x = 0; x < numberOfCells; x++)
            {
                for (int y = 0; y < numberOfCells; y++)
                {
                    boxes[x, y] = new PictureBox();
                    boxes[x, y].BorderStyle = BorderStyle.FixedSingle;
                    boxes[x, y].Location = new Point(x * (cellSize - 1), y * (cellSize - 1));
                    boxes[x, y].Size = new Size(cellSize, cellSize);
                    boxes[x, y].Image = Properties.Resources.empty;
                    boxes[x, y].SizeMode = PictureBoxSizeMode.StretchImage;
                    boxes[x, y].Click += new System.EventHandler(pictureBox1_Click);
                    boxes[x, y].Tag = new Point(x, y);
                    panel.Controls.Add(boxes[x, y]);
                }
            }
            //panel.Size = new Size(numberOfCells * (cellSize - 1) + 2, numberOfCells * (cellSize - 1) + 2);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Point xy = (Point)((PictureBox)sender).Tag;
            game.ClickBox(xy.X, xy.Y);
        }

        private void ShowStat(int balls) // Метод для анимации счета игры
        {
            try
            {
                int score = Convert.ToInt32(toolStripTextBoxRealScore.Text);
                string str = Convert.ToString(score + balls);
                toolStripTextBoxRealScore.Text = str;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка считывания данных", "Ошибка!");
            }
        }

        private void ShowItem(Ball ball, Item item)
        {
            Image image;
            switch (item)
            {
                case Item.none: image = Properties.Resources.empty; break;
                case Item.aver: image = ChooseAver(ball.color); break;
                case Item.small: image = ChooseSmall(ball.color); break;
                case Item.big: image = ChooseBig(ball.color); break;
                case Item.path: image = Properties.Resources.path; break;
                default: image = Properties.Resources.empty; break;
            }
            boxes[ball.x, ball.y].Image = image;
        }

        private Bitmap ChooseAver(int item)
        {
            switch (item)
            {
                case 1: return Properties.Resources.Aver_B_3d; break;
                case 2: return Properties.Resources.Aver_C_3d; break;
                case 3: return Properties.Resources.Aver_G_3d; break;
                case 4: return Properties.Resources.Aver_M_3d; break;
                case 5: return Properties.Resources.Aver_R_3d; break;
                case 6: return Properties.Resources.Aver_Y_3d; break;
                default: return null; break;
            }
        }

        private Bitmap ChooseSmall(int item)
        {
            switch (item)
            {
                case 1: return Properties.Resources.Small_B_3d; break;
                case 2: return Properties.Resources.Small_C_3d; break;
                case 3: return Properties.Resources.Small_G_3d; break;
                case 4: return Properties.Resources.Small_M_3d; break;
                case 5: return Properties.Resources.Small_R_3d; break;
                case 6: return Properties.Resources.Small_Y_3d; break;
                default: return null; break;
            }
        }

        private Bitmap ChooseBig(int item)
        {
            switch (item)
            {
                case 1: return Properties.Resources.Big_B_3d; break;
                case 2: return Properties.Resources.Big_C_3d; break;
                case 3: return Properties.Resources.Big_G_3d; break;
                case 4: return Properties.Resources.Big_M_3d; break;
                case 5: return Properties.Resources.Big_R_3d; break;
                case 6: return Properties.Resources.Big_Y_3d; break;
                default: return null; break;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            game.Step();
        }

        private void toolStripComboBox10_Click(object sender, EventArgs e)
        {
            toolStripComboBox10.Items.Clear();
            for (int i = 0; i < 10; i++)
            {
                if (i < statistics.table.Count)
                {
                    toolStripComboBox10.Items.Add((i + 1).ToString() + ". " + statistics.ViewItemTable(i));
                }
            }
        }

        private void toolStripMenuItem1Help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Классическая игра Lines.\nЦель игры - достигнуть как можно большего счета," +
                "\nс помощью выстраивания и сокращения линий \nиз шариков одинакового цвета.", "Правила игры",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripMenuItemRestart_Click(object sender, EventArgs e)
        {
            HandlingRestartGame();
        }

        private void HandlingRestartGame() //Метод обрабатывает рестарт игры
        {
            timer.Enabled = false;
            DialogResult resultQuestionToSave = MessageBox.Show("Хотите сохранить результат?",
                "Сохранение игры", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultQuestionToSave == System.Windows.Forms.DialogResult.Yes)
            {
                statistics.WriteTable(toolStripTextBoxRealName.Text, toolStripTextBoxRealScore.Text);
                game.ClickRefresh();
                toolStripTextBoxRealScore.Text = "0";
                timer.Enabled = true;
                return;
            }
            else
            {
                game.ClickRefresh();
                toolStripTextBoxRealScore.Text = "0";
                timer.Enabled = true;
                return;
            }
        }
    }
}
