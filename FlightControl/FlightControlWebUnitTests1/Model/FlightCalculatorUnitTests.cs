using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightControlWeb.Model;
using System;
using System.Collections.Generic;
using System.Text;
//using Xunit.Sdk;
using FlightControlWeb.Flight;

namespace FlightControlWeb.Model.UnitTests
{
    [TestClass()]
    public class FlightCalculatorUnitTests
    {
        [TestMethod()]
        public void AskFlightBeforeLaunch()
        {
            FlightCalculator calculator = new FlightCalculator();

            DateTime launchTime = new DateTime(2020, 5, 24, 20, 4, 30);
            InitialLocation launch = new InitialLocation(100, 100, launchTime);

            FlightStatus middle = new FlightStatus(200, 200, 300);
            FlightStatus end = new FlightStatus(250, 250, 200);

            FlightPlan flightPlan = new FlightPlan(256, "TestCompany", launch, new FlightStatus[2] { middle, end });


            DateTime beforeLaunch = new DateTime(2019, 6, 27, 22, 41, 35);


            Assert.IsNull(calculator.CreateFlightFromPlan(flightPlan, beforeLaunch, false));
        }


        [TestMethod()]
        public void AskFlightAfterLand()
        {
            FlightCalculator calculator = new FlightCalculator();

            DateTime launchTime = new DateTime(2020, 5, 24, 20, 4, 30);
            InitialLocation launch = new InitialLocation(100, 100, launchTime);

            FlightStatus middle = new FlightStatus(200, 200, 300);
            FlightStatus end = new FlightStatus(250, 250, 200);

            FlightPlan flightPlan = new FlightPlan(256, "TestCompany", launch, new FlightStatus[2] { middle, end });


            DateTime afterLand = new DateTime(2021, 6, 27, 22, 41, 35);


            Assert.IsNull(calculator.CreateFlightFromPlan(flightPlan, afterLand, false));
        }


        [TestMethod()]
        public void AskFlightLaunchTime()
        {
            FlightCalculator calculator = new FlightCalculator();

            DateTime launchTime = new DateTime(2020, 5, 24, 20, 4, 30);
            InitialLocation launch = new InitialLocation(100, 100, launchTime);

            FlightStatus middle = new FlightStatus(200, 200, 300);
            FlightStatus end = new FlightStatus(250, 250, 200);

            FlightPlan flightPlan = new FlightPlan(256, "TestCompany", launch, new FlightStatus[2] { middle, end });


            var res = calculator.CreateFlightFromPlan(flightPlan, launchTime, false);

            Assert.AreEqual(launch.Longitude, res.Longitude);
            Assert.AreEqual(launch.Latitude, res.Latitude);
        }


        [TestMethod()]
        public void AskFlightMiddleTime()
        {
            FlightCalculator calculator = new FlightCalculator();

            DateTime launchTime = new DateTime(2020, 5, 24, 20, 4, 30);
            InitialLocation launch = new InitialLocation(100, 100, launchTime);

            FlightStatus middle = new FlightStatus(200, 200, 300);
            FlightStatus end = new FlightStatus(250, 250, 200);

            FlightPlan flightPlan = new FlightPlan(256, "TestCompany", launch, new FlightStatus[2] { middle, end });

            DateTime middleTime = launchTime.AddSeconds(300);

            var res = calculator.CreateFlightFromPlan(flightPlan, middleTime, false);

            Assert.AreEqual(middle.Longitude, res.Longitude);
            Assert.AreEqual(middle.Latitude, res.Latitude);
        }


        [TestMethod()]
        public void AskFlightEndTime()
        {
            FlightCalculator calculator = new FlightCalculator();

            DateTime launchTime = new DateTime(2020, 5, 24, 20, 4, 30);
            InitialLocation launch = new InitialLocation(100, 100, launchTime);

            FlightStatus middle = new FlightStatus(200, 200, 300);
            FlightStatus end = new FlightStatus(250, 250, 200);

            FlightPlan flightPlan = new FlightPlan( 256, "TestCompany", launch, new FlightStatus[2] { middle, end });


            DateTime endTime = launchTime.AddSeconds(300).AddSeconds(200);
            var res = calculator.CreateFlightFromPlan(flightPlan, endTime, false);

            Assert.AreEqual(end.Longitude, res.Longitude);
            Assert.AreEqual(end.Latitude, res.Latitude);
        }


        [TestMethod()]
        public void AskFlightMereTime()
        {
            FlightCalculator calculator = new FlightCalculator();

            DateTime launchTime = new DateTime(2020, 5, 24, 20, 4, 30);
            InitialLocation launch = new InitialLocation(100, 100, launchTime);

            FlightStatus middle = new FlightStatus(200, 200, 300);
            FlightStatus end = new FlightStatus(250, 250, 200);

            FlightPlan flightPlan = new FlightPlan( 256, "TestCompany", launch, new FlightStatus[2] { middle, end });


            DateTime mereTime = launchTime.AddSeconds(150);


            var res = calculator.CreateFlightFromPlan(flightPlan, mereTime, false);

            Assert.AreEqual(150, res.Longitude);
            Assert.AreEqual(150, res.Latitude);
        }
    }
}