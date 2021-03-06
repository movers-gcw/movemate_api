﻿using System;
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
            path.StartAddress = p.Start.Address;
            path.DestinationAddress = p.Destination.Address;
            path.ToFrom = p.ToFrom;
            path.DepartmentAddress = p.DepartmentAddress;
            path.PathId = p.PathId;
            path.PathName = p.PathName;
            path.Vehicle = p.Vehicle;
            path.Price = p.Price;
            path.Date = p.Start.DateTime;
            path.Open = p.Open;
            return path;
        }

        public static PathSpecifiedView ViewFromPathSpecified(Path app)
        {
            var path = new PathSpecifiedView();
            path.PathId = app.PathId;
            path.PathName = app.PathName;
            path.Vehicle = app.Vehicle;
            path.StartAddress = app.Start.Address;
            path.DestinationAddress = app.Destination.Address;
            path.ToFrom = app.ToFrom;
            path.DepartmentAddress = app.DepartmentAddress;
            path.Description = app.Description;
            path.Seats = app.AvailableSeats;
            path.Date = app.Start.DateTime;
            path.Price = app.Price;
            path.Train = app.Train;
            path.Bus = app.Bus;
            path.Metro = app.Metro;
            path.Tram = app.Tram;
            path.Head = app.AvailableHeadgear;
            path.Open = app.Open;
            return path;
        }
    }
}