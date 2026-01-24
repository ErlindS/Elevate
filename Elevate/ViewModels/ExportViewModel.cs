using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using Elevate.Services;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Diagnostics;

namespace Elevate.ViewModels
{
    public partial class ExportViewModel : ObservableObject, IDocument
    {
        private readonly ElevateTaskService _taskService;
        public LiteDbService db;

        [ObservableProperty]
        private string statusMessage = string.Empty;

        [ObservableProperty]
        private bool isSuccess = true;

        public ExportViewModel(ElevateTaskService taskService, LiteDbService dbService)
        {
            _taskService = taskService;
            db = dbService;
        }

        [RelayCommand]
        public void GeneratePdf()
        {
            try
            {
                StatusMessage = "Generating PDF...";
                IsSuccess = true;

                QuestPDF.Settings.License = LicenseType.Community;

                var filePath = Path.Combine(FileSystem.AppDataDirectory, "TaskReport.pdf");

                // Create a simple PDF document directly
                QuestPDF.Fluent.Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Margin(50);
                        page.Header().Text("Task Report").FontSize(24).Bold();
                        page.Content().Column(col =>
                        {
                            col.Item().Text($"Generated: {DateTime.Now}");
                            col.Item().Text("");
                            
                            if (_taskService.sortedTasks?.SubTasks != null)
                            {
                                foreach (var task in _taskService.sortedTasks.SubTasks)
                                {
                                    col.Item().Text($"• {task.Name ?? "Unnamed"} (ID: {task.Id})");
                                }
                            }
                            else
                            {
                                col.Item().Text("No tasks to display.");
                            }
                        });
                    });
                }).GeneratePdf(filePath);

                StatusMessage = $"PDF saved: {filePath}";
                IsSuccess = true;

                try { Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true }); } catch { }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed: {ex.Message}";
                IsSuccess = false;
            }
        }

        [RelayCommand]
        public async Task Load()
        {
            try
            {
                var success = await ElevateTaskStorage.LoadAsync(_taskService);
                if (success)
                {
                    StatusMessage = "Tasks loaded successfully!";
                    IsSuccess = true;
                }
                else
                {
                    StatusMessage = "No saved data found.";
                    IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Load failed: {ex.Message}";
                IsSuccess = false;
            }
        }

        [RelayCommand]
        public async Task Save()
        {
            try
            {
                await ElevateTaskStorage.SaveAsync(_taskService);
                StatusMessage = $"Saved to: {ElevateTaskStorage.GetDefaultFilePath()}";
                IsSuccess = true;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Save failed: {ex.Message}";
                IsSuccess = false;
            }
        }

        [RelayCommand]
        public async Task LoadFromFile()
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Select a JSON file to load",
                    FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        { DevicePlatform.WinUI, new[] { ".json" } },
                        { DevicePlatform.Android, new[] { "application/json" } },
                        { DevicePlatform.iOS, new[] { "public.json" } },
                        { DevicePlatform.MacCatalyst, new[] { "public.json" } }
                    })
                });

                if (result == null)
                {
                    StatusMessage = "No file selected.";
                    IsSuccess = false;
                    return;
                }

                var success = await ElevateTaskStorage.LoadFromFileAsync(result.FullPath, _taskService);
                if (success)
                {
                    StatusMessage = $"Loaded from: {result.FileName}";
                    IsSuccess = true;
                }
                else
                {
                    StatusMessage = "Failed to load file. Invalid format?";
                    IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Load failed: {ex.Message}";
                IsSuccess = false;
            }
        }

        [RelayCommand]
        public async Task SaveToFile()
        {
            try
            {
                // Generate default filename with timestamp
                var fileName = $"elevate_tasks_{DateTime.Now:yyyyMMdd_HHmmss}.json";
                var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                await ElevateTaskStorage.SaveToFileAsync(filePath, _taskService);
                
                StatusMessage = $"Saved to: {filePath}";
                IsSuccess = true;

                // Try to open the folder
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = FileSystem.AppDataDirectory,
                        UseShellExecute = true
                    });
                }
                catch { }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Save failed: {ex.Message}";
                IsSuccess = false;
            }
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
                        if (_taskService.sortedTasks?.SubTasks != null)
                        {
                            foreach (var task in _taskService.sortedTasks.SubTasks)
                            {
                                RenderTaskNode(col.Item(), task);
                            }
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
            if (task == null) return;

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
                       row.RelativeItem().PaddingLeft(10).Text(task.Name ?? "Unnamed");
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
