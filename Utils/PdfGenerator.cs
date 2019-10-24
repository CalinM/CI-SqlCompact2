using Common;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using System;
using MigraDoc.DocumentObjectModel.Tables;
using System.Collections.Generic;
using System.Windows.Forms;
using DAL;
using System.ComponentModel;

namespace Utils
{
    public class PdfGenerator
    {
        internal static string MigraDocFilenameFromByteArray(byte[] image)
        {
            return "base64:" + Convert.ToBase64String(image);
        }

        public static OperationResult CreateCatalog(PdfGenParams pdfGenParams)
        {
            var result = new OperationResult();

            try
            {
                var toBeProcessedCount = Desene.DAL.GetCount(pdfGenParams.ForMovies, pdfGenParams.PDFGenType);

                var document = new Document();
                document.Info.Title = "Movies Catalog";
                document.Info.Author = "Calin Marinescu";

                var fromProgressIndicator = new FrmProgressIndicator("Movies Catalog generator", "Loading, please wait ...", toBeProcessedCount);
                fromProgressIndicator.Argument = new KeyValuePair<Document, PdfGenParams>(document, pdfGenParams);
                fromProgressIndicator.DoWork += formPI_DoWork_GenerateMoviesCatalog;

                switch (fromProgressIndicator.ShowDialog())
                {
                    case DialogResult.Cancel:
                        result.Success = false;
                        result.CustomErrorMessage = "Operation has been canceled";

                        return result;

                    case DialogResult.Abort:
                        result.Success = false;
                        result.CustomErrorMessage = fromProgressIndicator.Result.Error.Message;

                        return result;

                    case DialogResult.OK:
                        //var fillDoc = (Document)fromProgressIndicator.Result.Result;
                        //MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToFile(document, "d:\\MigraDoc.pdf");

                        var pdfRenderer = new PdfDocumentRenderer(false, PdfFontEmbedding.Always);
                        pdfRenderer.Document = document;
                        pdfRenderer.RenderDocument();

                        pdfRenderer.PdfDocument.Save(pdfGenParams.FileName);

                        break;
                }
            }
            catch (Exception ex)
            {
                return result.FailWithMessage(ex);
            }

            return result;
        }

        private static void formPI_DoWork_GenerateMoviesCatalog(FrmProgressIndicator sender, DoWorkEventArgs e)
        {
            var catalogGenDet = (KeyValuePair<Document, PdfGenParams>)e.Argument;
            var document = catalogGenDet.Key;
            var pdfGenParams = catalogGenDet.Value;

            var movies = Desene.DAL.GetMoviesForWeb(true, pdfGenParams.PDFGenType);

            //var style = document.Styles["Normal"];

            var section = document.AddSection();
            section.PageSetup.PageFormat = PageFormat.A4;

            var table = section.AddTable();

            var sectionWidth =
                (int)document.DefaultPageSetup.PageWidth -
                (int)document.DefaultPageSetup.LeftMargin -
                (int)document.DefaultPageSetup.RightMargin;

            var columnWidth = sectionWidth / 3;

            var column = table.AddColumn();
            column.Width = columnWidth;
            var column2 = table.AddColumn();
            column2.Width = columnWidth;
            var column3 = table.AddColumn();
            column3.Width = columnWidth;


            Row row = null;
            //var christmasTree = MigraDocFilenameFromByteArray(Properties.Resources.Christmas_Tree_icon_png);
            //var helloweenPunmkin = MigraDocFilenameFromByteArray(Properties.Resources.Pumpkin_icon_png);

            var indexPos = 0;
            for (var i = 0; i < movies.Count; i += 3)
            {
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                row = table.AddRow();

                for (var j = 0; j < 3; j++)
                {
                    var k = i + j;
                    if (k >= movies.Count) break;

                    var movieObj = movies[k];

                    indexPos++;
                    sender.SetProgress(indexPos, movieObj.FN);

                    if (movieObj.Cover != null)
                    {
                        var imgOgj = GraphicsHelpers.CreatePosterThumbnail(150, 232, movieObj.Cover);

                        row.Cells[j].Format.Alignment = ParagraphAlignment.Center;
                        row.Cells[j].VerticalAlignment = VerticalAlignment.Center;
                        row.Cells[j].AddParagraph().AddImage(MigraDocFilenameFromByteArray(imgOgj));

                        //if (movieObj.T == "Craciun")
                        //    row.Cells[j].AddParagraph().AddImage(christmasTree);

                        //if (movieObj.T == "Helloween")
                        //    row.Cells[j].AddParagraph().AddImage(helloweenPunmkin);
                    }
                }

                row = table.AddRow();

                for (var j = 0; j < 3; j++)
                {
                    var k = i + j;
                    if (k >= movies.Count) break;

                    var movieObj = movies[k];

                    //var tm = new TextMeasurement(style.Font);
                    var str = movieObj.A.Replace(" ", "").Replace(",", "/");
                    //var strWidth = (float)XGraphics.MeasureString(str, rowD.Cells[1].Format.Font).Width;
                    //var strWidth = tm.MeasureString(str).Width;

                    var dataTable = new Table();
                    var columnD1 = dataTable.AddColumn();
                    columnD1.Width = columnWidth;

                    var rowD1 = dataTable.AddRow();
                    rowD1.Cells[0].Format.Alignment = ParagraphAlignment.Center;
                    rowD1.Cells[0].AddParagraph(movieObj.FN);

                    var rowD2 = dataTable.AddRow();
                    rowD2.Cells[0].Format.Alignment = ParagraphAlignment.Center;
                    rowD2.Cells[0].AddParagraph(string.Format("{0}, {1}", movieObj.R, str));
                    rowD2.Cells[0].AddParagraph("");


                    row.Cells[j].Elements.Add(dataTable);
                }
            }

            //e.Result = document;
        }
    }
}
