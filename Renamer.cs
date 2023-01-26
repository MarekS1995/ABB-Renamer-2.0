using System.Collections.Generic;
using System.Linq;

namespace ABB_Renamer_2._0
{
    public class Renamer
    {

        static List<string> preparedCode = new List<string>();
        // Lists with robot points
        static List<string> listOfMovementPoints = new List<string>();
        static List<string> listOfProcessPoints = new List<string>();
        // Lists with robot targets
        static List<string> listOfRobotTargets = new List<string>();
        static List<string> listOfMovementRobotTargets = new List<string>();
        static List<string> listOfProcessRobotTargets = new List<string>();
        static List<string> listOfNotUsingRobtargets = new List<string>();



        public static void MainFunction(string inputStream)
        {
            preparedCode = CodePreparator(inputStream);
            listOfMovementPoints = MovementsPointFinder(preparedCode);
            listOfProcessPoints = ProcesPointsFinder(preparedCode);
            listOfRobotTargets = RobotTargetsFinder(preparedCode);
            listOfMovementRobotTargets = MovementTargetsFinder(listOfRobotTargets, listOfMovementPoints);
            listOfProcessRobotTargets = ProcessTargetsFinder(listOfRobotTargets, listOfProcessPoints);
            listOfNotUsingRobtargets = NotUsingTargetsFinder(listOfRobotTargets, listOfProcessRobotTargets, listOfMovementRobotTargets);

        }



        static List<string> CodePreparator(string input)
        {
            List<string> workingList = new List<string>();
            string[] workingArray = input.Split('\n');

            foreach (string line in workingArray)
            {
                string preparedLine = line.TrimStart(' ');
                preparedLine = preparedLine.Replace(", ", ",");
                if (preparedLine.Contains("KL_LIN\\auf")) preparedLine.Replace("KL_LIN\\auf", "KL_LIN\\Auf");
                if (preparedLine.Contains("KL_LIN\\zu")) preparedLine.Replace("KL_LIN\\zu", "KL_LIN\\Zu");
                if (preparedLine.Contains("KL_LIN\\stop")) preparedLine.Replace("KL_LIN\\stop", "KL_LIN\\Stop");
                if (preparedLine.Contains("KL_LIN\\start")) preparedLine.Replace("KL_LIN\\start", "KL_LIN\\Start");
                workingList.Add(preparedLine);
            }
            return workingList;
        }
        static List<string> MovementsPointFinder(List<string> codeToProcess) // Wyrzucić wyjątek z dużą literą P jeśli tablica pusta !!
        {
            List<string> workingList = codeToProcess.Where(y => ContainerOfConstans.MovementsPoints.Any(x => y.Contains(x))).ToList();
            workingList = workingList.Where(y => ContainerOfConstans.TypeOfMovementPoints.Any(x => y.Contains(x))).ToList();
            workingList.RemoveAll(x => x.Contains("!"));
            return workingList;
        }
        static List<string> ProcesPointsFinder(List<string> codeToProcess)
        {
            List<string> workingList = codeToProcess.Where(y => ContainerOfConstans.TypeOfProcessPoints.Any(x => y.Contains(x))).ToList();
            workingList.RemoveAll(y => ContainerOfConstans.MovementsPoints.Any(x => y.Contains(x) && (y.Contains("MovePTP") || y.Contains("MoveLIN"))));
            workingList.RemoveAll(x => x.Contains("!"));
            return workingList;
        }
        static List<string> RobotTargetsFinder(List<string> codeToProcess)
        {
            return codeToProcess.FindAll(x => x.Contains("robtarget") && !x.Contains("!"));
        }
        static List<string> MovementTargetsFinder(List<string> robTargets, List<string> movementPoints)
        {
            List<string> pointsList = new List<string>();
            List<string> toReturn = new List<string>();

            foreach (string item in movementPoints)
            {
                string[] arrayToMovementPoints = item.Split(' ');
                arrayToMovementPoints = arrayToMovementPoints[1].Split(',');
                pointsList.Add(arrayToMovementPoints[0] + ":");
            }

            foreach (string item in pointsList)
            {
                foreach (string item2 in robTargets)
                {
                    if (item2.Contains(item)) toReturn.Add(item2);
                }
            }

            return toReturn;
        }
        static List<string> ProcessTargetsFinder(List<string> robTargets, List<string> processPoints)
        {
            List<string> pointsList = new List<string>();
            List<string> toReturn = new List<string>();

            foreach (string item in processPoints)
            {
                string[] arrayToProccesPoints = item.Split(',');
                if (arrayToProccesPoints[0].Contains("\\")) pointsList.Add(arrayToProccesPoints[1] + ":");
                else
                {
                    string[] arrayToProcessPoints2 = arrayToProccesPoints[0].Split(' ');
                    pointsList.Add(arrayToProcessPoints2[1] + ":");
                }
            }

            foreach (string item in pointsList)
            {
                foreach (string item2 in robTargets)
                {
                    if (item2.ToLower().Contains(item.ToLower())) toReturn.Add(item2);
                }
            }
            return toReturn;
        }
        static List<string> NotUsingTargetsFinder(List<string> robTargets, List<string> processTargets, List<string> movementTargets)
        {
            List<string> workingList = new List<string>();
            processTargets.ForEach(x => workingList.Add(x));
            movementTargets.ForEach(x => workingList.Add(x));

            return robTargets.Except(workingList).Select(z => z = "!" + z).ToList();
        }
    }
}
