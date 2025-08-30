namespace server.Models
{
    public class CreateRecordDTO
    {
        public string name_guid { get; set; }
        public string struct_name { get; set; }
        public string id_guid { get; set; }
        public DateOnly acceptance_day { get; set; }
        public DateOnly check_day { get; set; }
    }
    public class GetRecordDTO
    {
        public string name_guid { get; set; }
        public string struct_name { get; set; }
        public string id_guid { get; set; }
        public DateOnly acceptance_day { get; set; }
        public DateOnly check_day { get; set; }
        public int days_remaining { get; set; }
    }
}
