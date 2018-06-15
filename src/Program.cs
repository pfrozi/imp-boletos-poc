using System;
using DinkToPdf;

namespace PocBoletos
{
    class Program
    {
        private const string SAMPLE_CONTENT = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. In consectetur mauris eget ultrices  iaculis. Ut                               odio viverra, molestie lectus nec, venenatis turpis.";

        static void Main(string[] args)
        {
            Console.WriteLine("~~~ Projeto de Exemplo para geração de PDF a partir de uma Página HTML ~~~");

            SingleThreadExample(SAMPLE_CONTENT);

        }

        static void SingleThreadExample(string htmlContent){
            
            var converter = new BasicConverter(new PdfTools());

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4Plus,
                },
                Objects = {
                    new ObjectSettings() {
                        // PagesCount = true,
                        // HtmlContent = htmlContent,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        // HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 },
                        Page = "boleto/modbol.html"
                    }
                }
            };
            var pdf = converter.Convert(doc);
            var base64 = Convert.ToBase64String(pdf);
            
            Console.WriteLine(base64);

        }
        static void MultiThreadExample(string htmlContent){

            var converter = new SynchronizedConverter(new PdfTools());

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4Plus,
                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = htmlContent,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                    }
                }
            };
            var pdf = converter.Convert(doc);
            var base64 = Convert.ToBase64String(pdf);
            
            Console.WriteLine(base64);
            
        }
    }
}
