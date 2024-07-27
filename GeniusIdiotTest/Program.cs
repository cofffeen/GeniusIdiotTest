using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
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
            var userName = Console.ReadLine();

            while (true)
            {
                bool[] isQuestionAsked = isQuestionAskedStartValue(questions.Length);

                var countRightAnswers = 0;
                for (int i = 0; i < 5; i++)
                {
                    var randomQuestionIndex = random.Next(0, questions.Length);
                    while (isQuestionAsked[randomQuestionIndex])
                    {
                        randomQuestionIndex = random.Next(0, questions.Length);
                    }

                    Console.WriteLine(questions[randomQuestionIndex]);

                    int userAnswer = GetUserAnswer();
                    isQuestionAsked[randomQuestionIndex] = true;

                    if (userAnswer == answers[randomQuestionIndex])
                    {
                        countRightAnswers++;
                    }

                }

                Console.WriteLine(userName + ", Вы " + diagnoses[DiagnosesIndex(countRightAnswers, 5)]);
                SaveUserResults(userName, diagnoses[DiagnosesIndex(countRightAnswers, 5)]);

                Console.WriteLine("Хотите ли Вы посмотреть предыдущие результаты?");
                if (GetUserChoice())
                {
                    ShowUserResults();
                }

                Console.WriteLine("Хотите попробовать пройти тест снова?");
                if (!GetUserChoice())
                {
                    break;
                }
                
                

            }

            


        }

        private static void ShowUserResults()
        {
            var value = GetValue("userResults.txt").Split('!');
            Console.WriteLine();
            foreach(var item in value)
            {
                if(item.Length == 0)
                {
                    break;
                }
                var line = item.Split('#');
                var name = line[0];
                var diagnose = line[1];
                Console.WriteLine(name + " " + diagnose);
            }
        }

        private static string GetValue(string fileName)
        {
            var reader = new StreamReader(fileName, Encoding.UTF8);
            var value = reader.ReadLine();
            reader.Close();
            return value;
        }

        private static void SaveUserResults(string name, string diagnose)
        {
            var value = $"{name}#{diagnose}!";
            AppendToFile("userResults.txt", value);
        }

        private static void AppendToFile(string fileName, string value)
        {
            var writer = new StreamWriter(fileName, true, Encoding.UTF8);
            writer.Write(value);
            writer.Close();
        }

        private static int DiagnosesIndex(int countRightAnswers, int questions)
        {
            int percent = countRightAnswers * 100 / questions;

            if (percent < 20) { return 0; }
            else if (percent < 40) { return 1; }
            else if (percent < 60) { return 2; }
            else if (percent < 80) { return 3; }
            else if (percent < 90) { return 4; }
            else { return 5; }

        }

        private static bool GetUserChoice()
        {
            while (true)
            {
                string userAnswerToContinue = Console.ReadLine().ToLower();
                if (userAnswerToContinue == "нет")
                {
                    return false;
                }
                else if (userAnswerToContinue == "да")
                {
                    return true;
                }

                Console.WriteLine("Введите да или нет");
            }
        }
        private static int GetUserAnswer()
        {
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
            return userAnswer;
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
            questions[5] = "Сколько месяцев в году имеют 28 дней?";

            return questions;
        }
    }
}
