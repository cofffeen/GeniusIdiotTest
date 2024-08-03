namespace GeniusIdiot
{
    public class DiagnoseStorage
    {
        public static int DiagnosesIndex(int countRightAnswers, int questions)
        {
            int percent = countRightAnswers * 100 / questions;

            return percent/20;
        }

        public static string GetDiagnoses(int countRightAnswers, int questions)
        {
            int index = DiagnosesIndex(countRightAnswers, questions);
            string[] diagnoses = new string[6];

            diagnoses[0] = "кретин";
            diagnoses[1] = "идиот";
            diagnoses[2] = "дурак";
            diagnoses[3] = "нормальный";
            diagnoses[4] = "талант";
            diagnoses[5] = "гений";

            return diagnoses[index];
        }
    }
}
