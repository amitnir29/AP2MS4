using FlightControlWeb.Flight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Model
{
    public class FlightCalculator : IFlightCalculator
    {
        /// <summary>
        /// Generate a flight from a given flight plan and a time.
        /// </summary>
        /// <param name="plan"> The flight plan to generate from. </param>
        /// <param name="relativeTo"> The time (to calculate the plane's position. </param>
        /// <param name="isExternal"> Indicate if the flight is given from an
        /// external server. </param>
        /// <returns> A corresponding flight or null if there is no flight at the
        /// given time. </returns>
        public Flight.Flight CreateFlightFromPlan(FlightPlan plan, DateTime relativeTo,
            bool isExternal)
        {
            // Get launch time.
            DateTime launch = DateTime.ParseExact(plan.InitLocation.Time,
                "yyyy-MM-ddTHH:mm:ssZ", null).ToUniversalTime();

            // If time is before launch return null - no flight at this time.
            if (relativeTo < launch)
                return null;

            // If time is launch time return a flight corresponds to the launch time.
            if (relativeTo.Equals(launch))
                return new Flight.Flight(plan.GetID(), plan.InitLocation.Longitude,
                    plan.InitLocation.Latitude, plan.Passengers,
                    plan.Company, launch, isExternal);

            

            DateTime temp = launch;
            DateTime last = launch;

            int i;

            IList<FlightStatus> segments = new List<FlightStatus>(plan.Segments);
            segments.Insert(0, new FlightStatus(plan.InitLocation.Longitude,
                plan.InitLocation.Latitude, 0));

            /* Iterate over al segments of the flight plan. Find the first segment which
               its time is greater then the time in the requested flight. */
            for (i = 0; temp < relativeTo && i < plan.Segments.Count; last = temp,
                temp = temp.AddSeconds(plan.Segments[i++].DeltaTime));

            // The given time might be greater than the landing time or equal to it.
            if (i == plan.Segments.Count)
            {
                // If equal, return a flight corresponds to the landing.
                if (temp.Equals(relativeTo))
                    return new Flight.Flight(plan.GetID(),
                        plan.Segments[plan.Segments.Count - 1].Longitude,
                    plan.Segments[plan.Segments.Count - 1].Latitude, plan.Passengers,
                    plan.Company, launch, isExternal);

                // If greater, return null - no flight at this time.
                if (temp < relativeTo)
                    return null;
            }   


            // Otherwise, calculate the corresponding longitude and latitude.
            double newLongitude = (segments[i - 1].Longitude * (temp - relativeTo).TotalSeconds +
                segments[i].Longitude * (relativeTo - last).TotalSeconds) /
                segments[i].DeltaTime;

            double newLatitude = (segments[i - 1].Latitude * (temp - relativeTo).TotalSeconds +
                segments[i].Latitude * (relativeTo - last).TotalSeconds) /
                segments[i].DeltaTime;


            // Return flight based on the above calculations.
            return new Flight.Flight(plan.GetID(), newLongitude, newLatitude, plan.Passengers,
                    plan.Company, launch, isExternal);
        }
    }
}
