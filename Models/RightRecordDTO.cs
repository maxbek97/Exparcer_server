namespace server.Models
{
    public class RecordDTO
    {
        public string name_guid { get; set; }
        public string struct_name { get; set; }
        public string id_guid { get; set; }
        public DateOnly acceptance_day { get; set; }
        public DateOnly check_day { get; set; }
    }
}
