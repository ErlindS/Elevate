using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using Elevate.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.IO;

namespace Elevate.ViewModels
{
    public partial class ExportViewModel : ObservableObject, IDocument
    {
        private readonly ElevateTask _rootTasks;

        public ExportViewModel(ElevateTaskService taskService)
        {
            _rootTasks = taskService.sortedTasks;
        }

        [RelayCommand]
        public void generate() {
            // REQUIRED: Configure QuestPDF License (Community is free for many use cases)
            QuestPDF.Settings.License = LicenseType.Community;

            // 2. Generate PDF
            var filePath = "TaskReport.pdf";
            // Fix: Use 'this' (the ExportViewModel, which implements IDocument) as the document
            this.GeneratePdf(filePath);

            //var filePath = "TaskReport.pdf";
            //var document = new TaskPdfDocument(myData);
            //document.GeneratePdf(filePath);

            Console.WriteLine($"PDF generated successfully at: {Path.GetFullPath(filePath)}");

            // Optional: Open the file automatically on Windows
            try { Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true }); } catch { }

        }


        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(50);
                page.DefaultTextStyle(x => x.FontSize(11).FontFamily("Arial"));

                // Header
                page.Header()
                    .Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Text("Task Hierarchy Report").FontSize(20).SemiBold().FontColor(QuestPDF.Helpers.Colors.Blue.Medium);
                            col.Item().Text($"Generated: {DateTime.Now:g}").FontSize(10).FontColor(QuestPDF.Helpers.Colors.Grey.Medium);
                        });
                    });

                // Content
                page.Content()
                    .PaddingVertical(20)
                    .Column(col =>
                    {
                        foreach (var task in _rootTasks.SubTasks)
                        {
                            RenderTaskNode(col.Item(), task);
                        }
                    });

                // Footer
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
        }

        // Recursive Helper Method
        private void RenderTaskNode(QuestPDF.Infrastructure.IContainer container, IElevateTaskComponent task)
        {
            container.Column(col =>
            {
                // Render the Task Row
                col.Item()
                   .BorderBottom(1).BorderColor(QuestPDF.Helpers.Colors.Grey.Lighten3)
                   .PaddingVertical(4)
                   .Row(row =>
                   {
                       // ID Box
                       row.ConstantItem(40)
                          .Background(QuestPDF.Helpers.Colors.Grey.Lighten4)
                          .AlignCenter()
                          .Text(task.Id.ToString()).FontSize(9).Bold();

                       // Task Name
                       row.RelativeItem().PaddingLeft(10).Text(task.Name);
                   });

                // Render SubTasks (Recursion)
                if (task.SubTasks != null && task.SubTasks.Count > 0)
                {
                    col.Item()
                       .PaddingLeft(25) // Indent children
                       .BorderLeft(1).BorderColor(QuestPDF.Helpers.Colors.Grey.Lighten2) // Tree line
                       .PaddingLeft(10) // Space after line
                       .Column(childCol =>
                       {
                           foreach (var subTask in task.SubTasks)
                           {
                               RenderTaskNode(childCol.Item(), subTask);
                           }
                       });
                }
            });
        }
    }
}
