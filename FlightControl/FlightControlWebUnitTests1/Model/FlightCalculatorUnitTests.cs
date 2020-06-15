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
        /// <summary>
        /// Test calculate flight for a time before launch.
        /// </summary>
        [TestMethod()]
        public void AskFlightBeforeLaunch()
        {
            FlightCalculator calculator = new FlightCalculator();


            // Positions at the flight plan.
            DateTime launchTime = new DateTime(2020, 5, 24, 20, 4, 30);
            InitialLocation launch = new InitialLocation(100, 100, launchTime);
            FlightStatus middle = new FlightStatus(100, 200, 300);
            FlightStatus end = new FlightStatus(100, 250, 200);

            // Generate the flight plan based on those positions.
            FlightPlan flightPlan = new FlightPlan(256, "TestCompany", launch, new FlightStatus[2] { middle, end });

            // The test asks for a point before the launch time.
            DateTime beforeLaunch = new DateTime(2019, 6, 27, 22, 41, 35);

            // The expected value is null.
            Assert.IsNull(calculator.CreateFlightFromPlan(flightPlan, beforeLaunch, false));
        }


        /// <summary>
        /// Ask for a flight after its land.
        /// </summary>
        [TestMethod()]
        public void AskFlightAfterLand()
        {
            FlightCalculator calculator = new FlightCalculator();

            // Positions at the flight plan.
            DateTime launchTime = new DateTime(2020, 5, 24, 20, 4, 30);
            InitialLocation launch = new InitialLocation(100, 100, launchTime);
            FlightStatus middle = new FlightStatus(100, 200, 300);
            FlightStatus end = new FlightStatus(100, 250, 200);

            // Generate the flight plan based on those positions.
            FlightPlan flightPlan = new FlightPlan(256, "TestCompany", launch, new FlightStatus[2] { middle, end });

            // The test asks for a point after land time.
            DateTime afterLand = new DateTime(2021, 6, 27, 22, 41, 35);

            // The expected value is null.
            Assert.IsNull(calculator.CreateFlightFromPlan(flightPlan, afterLand, false));
        }


        /// <summary>
        /// Ask for a flight at its launch time.
        /// </summary>
        [TestMethod()]
        public void AskFlightLaunchTime()
        {
            FlightCalculator calculator = new FlightCalculator();

            // Positions at the flight plan.
            DateTime launchTime = new DateTime(2020, 5, 24, 20, 4, 30);
            InitialLocation launch = new InitialLocation(100, 100, launchTime);
            FlightStatus middle = new FlightStatus(120, 200, 300);
            FlightStatus end = new FlightStatus(140, 250, 200);

            // Generate the flight plan based on those positions.
            FlightPlan flightPlan = new FlightPlan(256, "TestCompany", launch, new FlightStatus[2] { middle, end });

            // The test asks for a point at launch time.
            var res = calculator.CreateFlightFromPlan(flightPlan, launchTime, false);

            // Check that expected values are retrieved.
            Assert.AreEqual(launch.Longitude, res.Longitude);
            Assert.AreEqual(launch.Latitude, res.Latitude);
        }


        [TestMethod()]
        public void AskFlightMiddleTime()
        {
            FlightCalculator calculator = new FlightCalculator();

            // Positions at the flight plan.
            DateTime launchTime = new DateTime(2020, 5, 24, 20, 4, 30);
            InitialLocation launch = new InitialLocation(100, 100, launchTime);
            FlightStatus middle = new FlightStatus(120, 200, 300);
            FlightStatus end = new FlightStatus(140, 250, 200);

            // Generate the flight plan based on those positions.
            FlightPlan flightPlan = new FlightPlan(256, "TestCompany", launch, new FlightStatus[2] { middle, end });

            // The test asks for a point at the middle of the flight.
            DateTime middleTime = launchTime.AddSeconds(300);
            var res = calculator.CreateFlightFromPlan(flightPlan, middleTime, false);

            // Check that expected values are retrieved.
            Assert.AreEqual(middle.Longitude, res.Longitude);
            Assert.AreEqual(middle.Latitude, res.Latitude);
        }


        [TestMethod()]
        public void AskFlightEndTime()
        {
            FlightCalculator calculator = new FlightCalculator();

            // Positions at the flight plan.
            DateTime launchTime = new DateTime(2020, 5, 24, 20, 4, 30);
            InitialLocation launch = new InitialLocation(100, 100, launchTime);
            FlightStatus middle = new FlightStatus(120, 200, 300);
            FlightStatus end = new FlightStatus(140, 250, 200);

            // Generate the flight plan based on those positions.
            FlightPlan flightPlan = new FlightPlan( 256, "TestCompany", launch, new FlightStatus[2] { middle, end });

            // The test asks for a point at the land time.
            DateTime endTime = launchTime.AddSeconds(300).AddSeconds(200);
            var res = calculator.CreateFlightFromPlan(flightPlan, endTime, false);

            // Check that expected values are retrieved.
            Assert.AreEqual(end.Longitude, res.Longitude);
            Assert.AreEqual(end.Latitude, res.Latitude);
        }


        [TestMethod()]
        public void AskFlightMereTime()
        {
            FlightCalculator calculator = new FlightCalculator();

            // Positions at the flight plan.
            DateTime launchTime = new DateTime(2020, 5, 24, 20, 4, 30);
            InitialLocation launch = new InitialLocation(100, 100, launchTime);
            FlightStatus middle = new FlightStatus(120, 200, 300);
            FlightStatus end = new FlightStatus(140, 250, 200);

            // Generate the flight plan based on those positions.
            FlightPlan flightPlan = new FlightPlan( 256, "TestCompany", launch, new FlightStatus[2] { middle, end });


            DateTime mereTime = launchTime.AddSeconds(150);

            // The test asks for a point at mere time.
            var res = calculator.CreateFlightFromPlan(flightPlan, mereTime, false);

            // Check that expected values are retrieved.
            Assert.AreEqual(110, res.Longitude);
            Assert.AreEqual(150, res.Latitude);
        }
    }
}