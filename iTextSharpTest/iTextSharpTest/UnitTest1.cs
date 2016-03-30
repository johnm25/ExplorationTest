namespace iTextSharpTest
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;
    using Helper;
    using iTextSharp;
    using iTextSharp.text;
    using iTextSharp.text.pdf;

    /// <summary>
    // http://www.mikesdotnetting.com/Category/20
    // https://mlichtenberg.wordpress.com/2011/04/13/using-c-and-itextsharp-to-create-a-pdf/
    //http://stackoverflow.com/questions/28368317/itext-or-itextsharp-move-text-in-an-existing-pdf
    /// </summary>

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var resourceName = "Brev_fra_Sor__Kommune.pdf";
            var gg = HelperResource.ReadResourceByte(resourceName);

            PdfReader reader = new PdfReader(gg);

            // step 1
            Rectangle pageSize = reader.GetPageSize(1);
            Rectangle toMove = new Rectangle(100, 500, 200, 600);
            Document document = new Document(pageSize);

            // step 2
            using (var ms = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, ms);

                // step 3
                document.Open();

                // step 4
                PdfImportedPage page = writer.GetImportedPage(reader, 1); 
                PdfContentByte cb = writer.DirectContent;

                PdfTemplate template1 = cb.CreateTemplate(pageSize.Width, pageSize.Height);
                template1.Rectangle(0, 0, pageSize.Width, pageSize.Height);
                template1.Rectangle(toMove.Left, toMove.Bottom, toMove.Width, toMove.Height);
                template1.EoClip();
                template1.NewPath();
                template1.AddTemplate(page, 0, 0);
                cb.AddTemplate(template1, 0, 0);

                //PdfTemplate template2 = cb.CreateTemplate(pageSize.Width, pageSize.Height);
                //template2.Rectangle(toMove.Left, toMove.Bottom, toMove.Width, toMove.Height);
                //template2.Clip();
                //template2.NewPath();
                //template2.AddTemplate(page, 0, 0);
                ////cb.AddTemplate(template2, -20, -2);
                //// step 4

                document.Close();
                reader.Close();

          
                HelperResource.WriteResource($@"{AppDomain.CurrentDomain.BaseDirectory}\{resourceName}", ms);
           
            }

        }
    }
}
