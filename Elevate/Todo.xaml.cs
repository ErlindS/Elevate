using System;
using Microsoft.Maui.Controls;
namespace Elevate;

public partial class Todo : ContentPage
{
    private Random _random = new Random();
    public Todo()
	{
		InitializeComponent();
	}

    private void OnAppearButtonClicked(object sender, EventArgs e)
    {
        // 1. Bestimme die Größe des Zeichenbereichs (AbsoluteLayout)
        double canvasWidth = DrawingCanvas.Width;
        double canvasHeight = DrawingCanvas.Height;

        // Prüfen, ob die Größe schon verfügbar ist (Layout muss berechnet sein)
        if (canvasWidth <= 0 || canvasHeight <= 0)
        {
            // Noch keine Größe, vielleicht kurz nach dem Start. Man könnte warten oder nichts tun.
            System.Diagnostics.Debug.WriteLine("Canvas size not available yet.");
            return;
        }

        // 2. Definiere die Eigenschaften des Punktes
        double dotSize = 10; // Größe des Punktes (Durchmesser)
        Color dotColor = Colors.Red; // Farbe des Punktes

        // 3. Erzeuge zufällige X/Y Koordinaten *innerhalb* des Canvas
        //    Beachte: Die Koordinate ist der obere linke Punkt des Elements.
        //    Daher ziehen wir die dotSize ab, damit der Punkt nicht über den Rand hinausragt.
        double randomX = _random.NextDouble() * (canvasWidth - dotSize);
        double randomY = _random.NextDouble() * (canvasHeight - dotSize);

        // 4. Erzeuge den Punkt (wir verwenden eine BoxView mit abgerundeten Ecken)
        var dot = new BoxView
        {
            Color = dotColor,
            WidthRequest = dotSize,
            HeightRequest = dotSize,
            CornerRadius = dotSize / 2 // Macht die BoxView rund (Kreis)
        };

        // 5. Setze die Position und Größe des Punktes im AbsoluteLayout
        //    Die Bounds sind (X, Y, Breite, Höhe)
        AbsoluteLayout.SetLayoutBounds(dot, new Rect(randomX, randomY, dotSize, dotSize));

        // 6. Füge den erstellten Punkt zum AbsoluteLayout hinzu
        DrawingCanvas.Children.Add(dot);

        System.Diagnostics.Debug.WriteLine($"Dot added at ({randomX:F0}, {randomY:F0})");
    }
}