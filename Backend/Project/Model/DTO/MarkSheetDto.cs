namespace Project.Model.DTO
{
    public class MarkSheetDto
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string Standard { get; set; }
        public string ClassRoom { get; set; }
        public string Subject { get; set; }
        public decimal Marks { get; set; }
        public DateTime ResultDate { get; set; }
    }

}
