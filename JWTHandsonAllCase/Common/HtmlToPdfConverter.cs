



using iText.Html2pdf;
using iText.Kernel.Pdf;
using iTextSharp.text;

namespace JWTHandsonAllCase.Common
{
    public static class HtmlToPdfConverter
    {

       
        public static byte[] ConvertHtmlToPdf(string htmlContent)
        {


            byte[] bytes;
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            HtmlConverter.ConvertToPdf(htmlContent, writer);
            bytes = stream.ToArray();
            return bytes;
            //byte[] pdfBytes;

            //using (var outputStream = new MemoryStream())
            //{
            //    using (var pdfWriter = new PdfWriter(outputStream))
            //    {
                   
            //           HtmlConverter.ConvertToPdf(htmlContent, pdfWriter);
                  
            //    }
            //    pdfBytes = outputStream.ToArray();
            //}

            //return pdfBytes;
        }
    }
}
