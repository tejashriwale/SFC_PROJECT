using Project.Model.Domain;
using Project.Repository;

public class MarkSheetRepository : IMarkSheetRepository
{
    public async Task<List<MarkSheet>> GetAllAsyn()
    {
        return await Task.FromResult(new List<MarkSheet>
        {
            new MarkSheet { StudentID = 1, StudentName = "Sanket", Standard = "10th", ClassRoom = "A", Subject = "Math", Marks = 90, ResultDate = DateTime.Now },
            new MarkSheet { StudentID = 1, StudentName = "Sanket", Standard = "10th", ClassRoom = "A", Subject = "Science", Marks = 85, ResultDate = DateTime.Now },
            new MarkSheet { StudentID = 2, StudentName = "Ravi", Standard = "10th", ClassRoom = "B", Subject = "Math", Marks = 78, ResultDate = DateTime.Now }
        });
    }
}
