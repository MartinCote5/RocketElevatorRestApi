namespace RocketElevatorREST.Models
{
    public class User
    {
        // TODO
        public long Id { get; set; }
        public long? admin { get; set; }
        public string? email { get; set; }  
        public long? role {get; set;}
    }
}