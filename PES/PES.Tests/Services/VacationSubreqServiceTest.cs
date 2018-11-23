using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using PES.ViewModels;
using PES.Services;

namespace PES.Tests.Services
{
    [TestClass]
    public class VacationSubreqServiceTest
    {
        [TestMethod]
        public void CalculateDaysByMonth_WithOneRequest_SameMonth()
        {
            //Arrange
            VacationSubreqService service = new VacationSubreqService();
            ChartRequestsViewModel vacationSubReq = new ChartRequestsViewModel();
            List<ChartRequestsViewModel> vacationSubReqs = new List<ChartRequestsViewModel>();
            vacationSubReq.StartDate = new DateTime(2018, 11, 15);
            vacationSubReq.EndDate = new DateTime(2018, 11, 23);
            vacationSubReq.VacationDays = 7;
            vacationSubReqs.Add(vacationSubReq);
            int[] actual = new int[12];
            int[] expected = new int[12];
            expected[10] = 7;

            //Act
            service.CalculateDaysByMonth(vacationSubReqs, 2018, actual);

            //Assert
            actual.SequenceEqual(expected);
        }

        [TestMethod]
        public void CalculateDaysByMonth_WithTwoRequest_DifferentMonth()
        {
            //Arrange
            VacationSubreqService service = new VacationSubreqService();
            ChartRequestsViewModel vacationSubReq = new ChartRequestsViewModel();
            List<ChartRequestsViewModel> vacationSubReqs = new List<ChartRequestsViewModel>();
            vacationSubReq.StartDate = new DateTime(2018, 11, 30);
            vacationSubReq.EndDate = new DateTime(2018, 12, 05);
            vacationSubReq.VacationDays = 4;
            vacationSubReqs.Add(vacationSubReq);
            vacationSubReq.StartDate = new DateTime(2018, 10, 30);
            vacationSubReq.EndDate = new DateTime(2018, 11, 06);
            vacationSubReq.VacationDays = 6;
            vacationSubReqs.Add(vacationSubReq);
            int[] actual = new int[12];
            int[] expected = new int[12];
            expected[9] = 2;
            expected[10] = 5;
            expected[11] = 3;

            //Act
            service.CalculateDaysByMonth(vacationSubReqs, 2018, actual);

            //Assert
            actual.SequenceEqual(expected);
        }
    }
}
