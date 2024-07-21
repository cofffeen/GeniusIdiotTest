using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeniusIdiotTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] questions = GetQuestion();
            int[] answers = GetAnswer();
            string[] diagnoses = GetDiagnoses();

            Random random = new Random();
            Console.WriteLine("Введите имя:");
            string userName = Console.ReadLine();

            bool wantToEnd = false;
            while (!wantToEnd)
            {
                bool[] isQuestionAsked = isQuestionAskedStartValue(questions.Length);

                int countRightAnswers = 0;
                for (int i = 0; i < diagnoses.Length; i++)
                {
                    int randomQuestionIndex = random.Next(0, questions.Length);
                    while (isQuestionAsked[randomQuestionIndex])
                    {
                        randomQuestionIndex = random.Next(0, questions.Length);
                    }

                    Console.WriteLine(questions[randomQuestionIndex]);

                    int userAnswer;
                    while (true)
                    {
                        try
                        {
                            userAnswer = Convert.ToInt32(Console.ReadLine());
                            break;
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("Введите число в диапозоне от -2*10**9 до 2*10**9");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Введите число");
                        }
                    }
                    isQuestionAsked[randomQuestionIndex] = true;

                    if (userAnswer == answers[randomQuestionIndex])
                    {
                        countRightAnswers++;
                    }

                }

                Console.WriteLine(userName + ", Вы " + diagnoses[countRightAnswers]);

                Console.WriteLine("Хотите попробовать пройти тест снова?");
                while (true)   
                {
                    string userAnswerToContinue = Console.ReadLine().ToLower();
                    if (userAnswerToContinue == "нет")
                    {
                        wantToEnd = true;
                        break;
                    }
                    else if (userAnswerToContinue == "")
                    {
                        break;
                    }

                    Console.WriteLine("Введите да или нет");
                }
                
                

            }

            


        }

        private static bool[] isQuestionAskedStartValue(int length)
        {
            bool[] isQuestionAsked = new bool[length];

            for (int i = 0; i < length; i++)
            {
                isQuestionAsked[i] = false;
            }

            return isQuestionAsked;
        }

        private static string[] GetDiagnoses()
        {
            string[] diagnoses = new string[6];

            diagnoses[0] = "кретин";
            diagnoses[1] = "идиот";
            diagnoses[2] = "дурак";
            diagnoses[3] = "нормальный";
            diagnoses[4] = "талант";
            diagnoses[5] = "гений";

            return diagnoses;
        }

        private static int[] GetAnswer()
        {
            int[] answers = new int[6];

            answers[0] = 6;
            answers[1] = 9;
            answers[2] = 25;
            answers[3] = 60;
            answers[4] = 2;
            answers[5] = 12;

            return answers;
        }

        private static string[] GetQuestion()
        {
            string[] questions = new string[6];

            questions[0] = "Сколько будет два плюс два умножить на два?";
            questions[1] = "Бревно нужно распилить на 10 частей. Сколько распилов нужно сделать?";
            questions[2] = "На двух руках десять пальцев. Сколько пальцев на пяти руках?";
            questions[3] = "Укол делают каждые полчаса. Сколько нужно минут, чтобы сделать три укола?";
            questions[4] = "Пять свечей горело, две потухли. Сколько свечей осталось?";
            questions[4] = "Сколько месяцев в году имеют 28 дней?";

            return questions;
        }
    }
}
