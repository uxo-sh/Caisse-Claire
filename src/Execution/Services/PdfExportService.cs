using System.Collections.Generic;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using CaisseClaire.Execution.Entities;

namespace CaisseClaire.Execution.Services;

public class PdfExportService
{
    public PdfExportService()
    {
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public void ExportCatalogToPdf(IEnumerable<ProductEntity> products, string outputPath)
    {
        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header()
                    .Text("Caisse Claire - Product Catalog")
                    .SemiBold().FontSize(24).FontColor(Colors.Blue.Darken2);

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(80);
                            columns.RelativeColumn();
                            columns.ConstantColumn(100);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Code").Bold();
                            header.Cell().Text("Name").Bold();
                            header.Cell().AlignRight().Text("Price (MGA)").Bold();

                            header.Cell().ColumnSpan(3)
                                  .PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
                        });

                        foreach (var product in products)
                        {
                            table.Cell().Text(product.Code);
                            table.Cell().Text(product.Name);
                            table.Cell().AlignRight().Text($"{product.Price:N0}");
                        }
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                        x.Span(" of ");
                        x.TotalPages();
                    });
            });
        })
        .GeneratePdf(outputPath);
    }
}
