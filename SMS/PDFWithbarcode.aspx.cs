using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Barcode;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;

namespace SMS
{
    public partial class PDFWithbarcode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Creating a new PDF Document.
            PdfDocument document = new PdfDocument();
            //Add the margins.
            document.PageSettings.Margins.All = 0;
            //Adding a new page to a PDF document.
            PdfPage page = document.Pages.Add();
            //Initialize a new PdfCode39Barcode instance.
            PdfCode39Barcode barcode = new PdfCode39Barcode();
            //Set the height and text for barcode.
            barcode.BarHeight = 45;
            barcode.Text = "CODE39$";
            //Convert the barcode to image.
            System.Drawing.Image barcodeImage = barcode.ToImage(new SizeF(180, 100));
            //Creates a new pdf rubber stamp annotation.
            RectangleF rectangle = new RectangleF(40, 60, 180, 100);
            PdfRubberStampAnnotation rubberStampAnnotation = new PdfRubberStampAnnotation(rectangle, " Barcode");
            rubberStampAnnotation.Appearance.Normal.Graphics.DrawImage(new PdfBitmap(barcodeImage), 0, 0, rectangle.Width, rectangle.Height);
            //rubberStampAnnotation.Text = "Barcode Properties Rubber Stamp Annotation";
            //Adds annotation to the page.
            page.Annotations.Add(rubberStampAnnotation);
            //Save and closes the document.
            document.Save("../../Barcode.pdf");
            document.Close(true);
        }
    }
}