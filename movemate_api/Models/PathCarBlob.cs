using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movemate_api.Controllers
{
    public class PathCarBlob
    {
        public int StudentId { get; set; }
        public Boolean ToFrom { get; set; }
        public String PathName { get; set; }
        public String Date { get; set; }
        public int DepId { get; set; }
        public String Address { get; set; }
        public int Vehicle { get; set; }
        public Boolean Train { get; set; }
        public Boolean Bus { get; set; }
        public Boolean Metro { get; set; }
        public Boolean Tram { get; set; }
        public int Seats { get; set; }
        public String Price { get; set; }
        public Boolean Head { get; set; }
        public String Description { get; set; }
        public Pathblob(int studentId, Boolean toFrom, String pathname, String date, int depId, String address, int vehicle, Boolean train, Boolean bus, Boolean metro, Boolean tram, int seats, String price, Boolean head, String description)
        {
            this.StudentId = studentId;
            this.ToFrom = toFrom;
            this.PathName = pathname;
            this.Date = date;
            this.DepId = depId;
            this.Address = address;
            this.Vehicle = vehicle;
            this.Train = train;
            this.Bus = bus;
            this.Metro = metro;
            this.Tram = tram;
            this.Seats = seats;
            this.Price = price;
            this.Head = head;
            this.Description = description;
        }
        
    }
}