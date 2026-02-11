using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO;

public class PdfService
{
    private readonly IConverter _converter;

    public PdfService(IConverter converter)
    {
        _converter = converter;
    }

    public string GenerateAndSaveBill(string name, string date, string crates, string weight, string amount)
    {
        string html = $@"
            <h2>श्री {name}</h2>
            <p>📅 लिलाव तारीख: {date}</p>
            <p>🧺 जाळी संख्या: {crates}</p>
            <p>📏 एकूण वजन: {weight}</p>
            <p>💵 एकूण रक्कम: {amount}</p>";

        var doc = new HtmlToPdfDocument()
        {
            GlobalSettings = { PaperSize = PaperKind.A4 },
            Objects = { new ObjectSettings { HtmlContent = html } }
        };

        byte[] pdf = _converter.Convert(doc);

        // Save locally
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdfs");
        if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

        string fileName = $"bill_{Guid.NewGuid()}.pdf";
        string filePath = Path.Combine(folderPath, fileName);
        File.WriteAllBytes(filePath, pdf);

        // Public link
        return $"https://yourdomain.com/pdfs/{fileName}";
    }
}
