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

            var movies = Desene.DAL.GetMoviesForPDF(pdfGenParams);

            //var style = document.Styles["Normal"];
            //style.Font.Name = "Arial Narrow";
            //style.Font.Size = 10;

            var section = document.AddSection();
            section.PageSetup.PageFormat = PageFormat.A4;
            section.PageSetup.MirrorMargins = true;
            section.PageSetup.TopMargin = Unit.FromCentimeter(0.5);
            section.PageSetup.BottomMargin = Unit.FromCentimeter(0.5);
            section.PageSetup.LeftMargin = Unit.FromCentimeter(2);
            section.PageSetup.RightMargin = Unit.FromCentimeter(1);

            var table = section.AddTable();

            var sectionWidth =
                (int)document.DefaultPageSetup.PageWidth -
                (int)section.PageSetup.LeftMargin -
                (int)section.PageSetup.RightMargin;

            var columnWidth = sectionWidth / 4;

            var column = table.AddColumn();
            column.Width = columnWidth;
            var column2 = table.AddColumn();
            column2.Width = columnWidth;
            var column3 = table.AddColumn();
            column3.Width = columnWidth;
            var column4 = table.AddColumn();
            column4.Width = columnWidth;

            Row row = null;

            var indexPos = 0;
            for (var i = 0; i < movies.Count; i += 4)
            {
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                row = table.AddRow();

                for (var j = 0; j < 4; j++)
                {
                    var k = i + j;
                    if (k >= movies.Count) break;

                    var movieObj = movies[k];

                    indexPos++;
                    sender.SetProgress(indexPos, movieObj.FN);

                    var imgOgj = GraphicsHelpers.CreatePosterThumbnailForPDF(150, 232, movieObj.Cover, movieObj.R, movieObj.A,
                        pdfGenParams.PDFGenType == PDFGenType.All
                            ? movieObj.T == "Craciun"
                                ? Properties.Resources.Christmas_Tree_icon
                                : movieObj.T == "Helloween"
                                    ? Properties.Resources.Pumpkin_icon
                                    : null
                            : null);

                    row.Cells[j].Format.Alignment = ParagraphAlignment.Center;
                    row.Cells[j].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[j].AddParagraph().AddImage(MigraDocFilenameFromByteArray(imgOgj));
                }

                row = table.AddRow();

                for (var j = 0; j < 4; j++)
                {
                    var k = i + j;
                    if (k >= movies.Count) break;

                    var movieObj = movies[k];

                    var dataTable = new Table();
                    var columnD1 = dataTable.AddColumn();
                    columnD1.Width = columnWidth;

                    var rowD1 = dataTable.AddRow();
                    rowD1.Cells[0].Format.Alignment = ParagraphAlignment.Center;
                    rowD1.Cells[0].Format.Font.Name = "Arial Narrow";
                    rowD1.Cells[0].Format.Font.Size = 10;

                    var tm = new TextMeasurement(rowD1.Cells[0].Format.Font);
                    //var strWidth = tm.MeasureString(movieObj.FN).Width;

                    var movieTitle = movieObj.FN;
                    var lineCount = tm.GetSplittedLineCount(movieTitle, columnWidth, null);

                    if (lineCount > 2)
                        movieTitle = tm.GetStringWithEllipsis(movieObj.FN, columnWidth, 2);

                    rowD1.Cells[0].AddParagraph(movieTitle);

                    row.Cells[j].Elements.Add(dataTable);
                }
            }

            //e.Result = document;
        }
    }
}
