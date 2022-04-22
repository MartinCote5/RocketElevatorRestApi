namespace RocketElevatorREST.Models
{
    public class Building
    {
        // TODO
        public long Id { get; set; }
        public long customer_id { get; set; }
         public string? full_name_of_the_building_administrator {get; set;}
        public string? email_of_the_administrator_of_the_building {get; set;}
        public string? phone_number_of_the_building_administrator {get; set;}

        

    }
}