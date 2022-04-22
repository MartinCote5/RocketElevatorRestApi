namespace RocketElevatorREST.Models
{
    public class Product {
        public Customer? customer {get; set;}
        public List<Building>? buildings {get; set;}
        public List<Battery>? batteries {get; set;}
        public List<Column>? columns {get; set;}
         public List<Elevator>? elevators {get; set;}
    }
    
}


