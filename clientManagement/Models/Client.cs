namespace clientManagement.Models
{
    public class Client
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string CPF { get; set; }
        public int ClientTypeId { get; set; }
        public int ClientStatusId { get; set; }
        public char? Gender { get; set; }
    }
}
