using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movemate_api.Models
{
    public class PathFacade
    {
        public static PathView ViewFromPath(Path p)
        {
            var path = new PathView();
            var poi = new PointOfInterest();
            poi = p.Start;
            path.StartAddress = poi.Address;
            poi = p.Destination;
            path.DestinationAddress = poi.Address;
            path.PathId = p.PathId;
            path.PathName = p.PathName;
            path.Vehicle = p.Vehicle;
            return path;
        }

        public static PathSpecifiedView ViewFromPathSpecified(Path app)
        {
            var path = new PathSpecifiedView();
            var poi = new PointOfInterest();
            path.PathName = app.PathName;
            path.Vehicle = app.Vehicle;
            poi = app.Start;
            path.StartAddress = poi.Address;
            poi = app.Destination;
            path.DestinationAddress = poi.Address;
            path.Description = app.Description;
            path.Seats = app.AvailableSeats;
            path.Price = app.Price;
            path.Train = app.Train;
            path.Bus = app.Bus;
            path.Metro = app.Metro;
            path.Tram = app.Tram;
            path.Head = app.AvailableHeadgear;
            return path;
        }
    }
}