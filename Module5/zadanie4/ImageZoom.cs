using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadanie4
{
    internal class ImageZoom
    {
        public double Zoom = 1.0;   // Масштаб: 1 = 100%

        // Установка масштаба (от 10% до 1000%)
        public void Set(double value)
        {
            Zoom = Math.Max(0.1, Math.Min(10, value));
        }

        public void ZoomIn()
        {
            Set(Zoom * 1.25);
        }

        public void ZoomOut()
        {
            Set(Zoom / 1.25);
        }

        public void Reset()
        {
            Set(1.0);
        }

        // Масштаб, при котором изображение целиком помещается в область
        public void Fit(Size image, Size area)
        {
            double byWidth = (double)area.Width / image.Width;
            double byHeight = (double)area.Height / image.Height;
            Set(Math.Min(byWidth, byHeight));
        }

        // Размер изображения после масштабирования
        public Size GetSize(Size image)
        {
            return new Size((int)(image.Width * Zoom), (int)(image.Height * Zoom));
        }

        // Текст масштаба для подписи ("100%")
        public string GetText()
        {
            return (int)(Zoom * 100) + "%";
        }
    }
}