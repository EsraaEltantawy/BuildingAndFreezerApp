using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuildingAndFreezerApp.Models
{
    public class BuildingRepositry
    {
        public IEnumerable<Building> Buildings { get; set; }
        public IEnumerable<Employess> Employess { get; set; }
        public IEnumerable<Matriel> Matriels { get; set; }
        public IEnumerable<NonWorksTable> NonWorksTables { get; set; }
        public IEnumerable<NonWork> NonWorks { get; set; }
        public IEnumerable<Reserve> Reserves { get; set; }
        public Reserve oneClient { get; set; }
        public IEnumerable<BuildingMatrial> BuildingMatrial { get; set; }
        public IEnumerable<BankBuilding> BankBuilding { get; set; }
        public IEnumerable<ClientReserve> ClientReserve { get; set; }
        public IEnumerable<BuildingEmployee> BuildingEmployee { get; set; }
        public List<decimal?> sumEmployee { get; set; }
        public int buildId { get; set; }

        public BuildingRepositry()
        {
            BuildingandFreezerEntities db = new BuildingandFreezerEntities();
            Buildings = db.Buildings.ToList();
            Employess = db.Employesses.ToList();
            Matriels = db.Matriels.ToList();
            NonWorks = db.NonWorks.ToList();
            Reserves = db.Reserves.ToList();
            BuildingMatrial = db.BuildingMatrials.ToList();
            BankBuilding = db.BankBuildings.ToList();
            ClientReserve = db.ClientReserves.ToList();
            BuildingEmployee = db.BuildingEmployees.ToList();
            NonWorksTables = db.NonWorksTables.ToList();
        }
    }
}