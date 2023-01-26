using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB_Renamer_2._0
{
    class ContainerOfConstans
    {
        public static List<string> MovementsPoints = new List<string> { "p0", "p1", "p2", "p3", "p4", "p5", "p6", "p7", "p8", "p9" };


        public static List<string> TypeOfMovementPoints = new List<string> { "MovePTP", "MoveLIN","MoveCIRC", "MovePTP_ROB", "MoveLIN_ROB", "MoveCIRC_ROB", "MovePTP_FGB", "MoveLIN_FRG", 
                                                                            "MoveCIRC_FRG", "MovePTP_FB", "MoveLIN_FB", "MoveCIRC_FB", "MoveLIN_DO", "MoveLIN_GO", "MoveLIN_AO", "MovePTP_DO", 
                                                                            "MovePTP_GO", "MovePTP_AO", "MoveCIRC_DO", "MoveCIRC_AO", "MoveCIRC_GO" };


        public static List<string> TypeOfProcessPoints = new List<string> { "RZ_PTP", "RZ_LIN", "RZ_PTP_DZ", "RZ_LIN_DZ", "MoveLIN", "BZ_PTP", "BZ_LIN", "BU_LIN", "BU_CIRC", "LZ_PTP", "LZ_LIN",
                                                                            "MS_LIN", "MS_CIRC", "NZ_LIN", "NZ_PTP", "NZ_LIN_V", "NZ_PTP_V", "PR_LIN", "PR_PTP", "KL_LIN", "KL_CIRC"};












    }
}
