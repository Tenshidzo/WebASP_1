using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebASP_1.Views
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ScoreLabel.Text = "Счёт: 0";
            }
            else
            {
                // Восстановление матрицы при постбеке
                RestoreMatrixFromHiddenField();
            }
        }

        protected void GenerateMatrix(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int matrixSize = int.Parse(clickedButton.Text[0].ToString());

            // Генерация матрицы с случайными числами
            int[,] matrix = GenerateMatrixWithRandomNumbers(matrixSize);

            // Сохранение матрицы в скрытое поле
            SaveMatrixToHiddenField(matrix);

            // Отображение матрицы
            DisplayMatrix(matrix);
        }

        private int[,] GenerateMatrixWithRandomNumbers(int size)
        {
            Random rand = new Random();
            int[,] matrix = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = rand.Next(1, 10); // Числа от 1 до 9
                }
            }
            return matrix;
        }

        private void DisplayMatrix(int[,] matrix)
        {
            MatrixPanel.Controls.Clear();
            int size = matrix.GetLength(0);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Button btn = new Button();
                    btn.Text = matrix[i, j].ToString();
                    btn.Click += new EventHandler(Button_Click);
                    btn.ID = "btn_" + i + "_" + j; // Уникальный ID для каждой кнопки
                    MatrixPanel.Controls.Add(btn);
                }
                MatrixPanel.Controls.Add(new LiteralControl("<br />"));
            }
        }

        private void SaveMatrixToHiddenField(int[,] matrix)
        {
            int size = matrix.GetLength(0);
            MatrixSizeHidden.Value = size.ToString();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    sb.Append(matrix[i, j]);
                    if (j < size - 1) sb.Append(",");
                }
                if (i < size - 1) sb.Append(";");
            }
            MatrixDataHidden.Value = sb.ToString();
        }

        private void RestoreMatrixFromHiddenField()
        {
            if (!string.IsNullOrEmpty(MatrixSizeHidden.Value) && !string.IsNullOrEmpty(MatrixDataHidden.Value))
            {
                int size = int.Parse(MatrixSizeHidden.Value);
                int[,] matrix = new int[size, size];
                string[] rows = MatrixDataHidden.Value.Split(';');

                for (int i = 0; i < size; i++)
                {
                    string[] cols = rows[i].Split(',');
                    for (int j = 0; j < size; j++)
                    {
                        matrix[i, j] = int.Parse(cols[j]);
                    }
                }

                // Отображение матрицы после восстановления
                DisplayMatrix(matrix);
            }
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            if (string.IsNullOrEmpty(FirstButtonValueHidden.Value))
            {
                // Если первая кнопка не была нажата
                FirstButtonValueHidden.Value = clickedButton.Text;
                FirstButtonIDHidden.Value = clickedButton.ID;
            }
            else
            {
                // Если уже нажата первая кнопка
                int firstValue = int.Parse(FirstButtonValueHidden.Value);
                int secondValue = int.Parse(clickedButton.Text);

                if (firstValue + secondValue == 10)
                {
                    // Если сумма равна 10, обе кнопки скрываются и счет увеличивается на 1
                    Button firstButton = (Button)FindControl(FirstButtonIDHidden.Value);
                    firstButton.Visible = false;
                    clickedButton.Visible = false;

                    int currentScore = int.Parse(ScoreLabel.Text.Split(' ')[1]);
                    ScoreLabel.Text = "Счёт: " + (currentScore + 1).ToString();
                }

                // Сброс значений скрытых полей
                FirstButtonValueHidden.Value = string.Empty;
                FirstButtonIDHidden.Value = string.Empty;
            }
        }

        protected void ResetGame(object sender, EventArgs e)
        {
            // Сброс игры
            MatrixPanel.Controls.Clear();
            ScoreLabel.Text = "Счёт: 0";
            MatrixSizeHidden.Value = string.Empty;
            MatrixDataHidden.Value = string.Empty;
            FirstButtonValueHidden.Value = string.Empty;
            FirstButtonIDHidden.Value = string.Empty;
        }
    }
}