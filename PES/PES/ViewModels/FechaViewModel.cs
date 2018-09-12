using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

namespace PES.ViewModels
{
    public class FechaViewModel
    {
        public String CorregirMes(string month)
        {
            string MesCorrectoInicio ="";
            switch (month)
            {
                case "01":
                    {
                        MesCorrectoInicio = "JAN";
                        break;
                    }
                case "02":
                    {
                        MesCorrectoInicio = "FEB";
                        break;
                    }
                case "03":
                    {
                        MesCorrectoInicio = "MAR";
                        break;
                    }
                case "04":
                    {
                        MesCorrectoInicio = "APR";
                        break;
                    }
                case "05":
                    {
                        MesCorrectoInicio = "MAY";
                        break;
                    }
                case "06":
                    {
                        MesCorrectoInicio = "JUN";
                        break;
                    }
                case "07":
                    {
                        MesCorrectoInicio = "JUL";
                        break;
                    }
                case "08":
                    {
                        MesCorrectoInicio = "AUG";
                        break;
                    }
                case "09":
                    {
                        MesCorrectoInicio = "SEP";
                        break;
                    }
                case "10":
                    {
                        MesCorrectoInicio = "OCT";
                        break;
                    }
                case "11":
                    {
                        MesCorrectoInicio = "NOV";
                        break;
                    }
                case "12":
                    {
                        MesCorrectoInicio = "DEC";
                        break;
                    }
               
            }
            return MesCorrectoInicio;
        }
    }
}