using Project.Model.Domain;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Project.PdfGenerator
{
    public class EmployeePdfDocument : IDocument
    {
        private readonly EmployeeData _data;

        public EmployeePdfDocument(EmployeeData data)
        {
            _data = data;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(50);
                page.Content()
                    .Column(col =>
                    {
                        col.Item().Text("Employee Info").FontSize(20).Bold().Underline();
                        col.Item().Text($"Name     : {_data.Name}").FontSize(14);
                        col.Item().Text($"ID       : {_data.Id}").FontSize(14);
                        col.Item().Text($"Salary   : {_data.Salary}").FontSize(14);
                        col.Item().Text($"Position : {_data.Position}").FontSize(14);
                    });
            });
        }
    }
}
