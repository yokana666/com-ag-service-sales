using Com.Danliris.Service.Sales.Lib.ViewModels.SalesInvoice;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Com.Danliris.Service.Sales.Lib.PDFTemplates
{
    public class SalesInvoicePdfTemplate
    {
        public MemoryStream GeneratePdfTemplate(SalesInvoiceViewModel viewModel, int clientTimeZoneOffset)
        {
            const int MARGIN = 15;

            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 18);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);

            Document document = new Document(PageSize.A4, MARGIN, MARGIN, MARGIN, MARGIN);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();

            #region Header

            PdfPTable headerTable = new PdfPTable(2);
            headerTable.SetWidths(new float[] { 10f, 10f });
            headerTable.WidthPercentage = 100;
            PdfPTable headerTable1 = new PdfPTable(1);
            PdfPTable headerTable2 = new PdfPTable(2);
            headerTable2.SetWidths(new float[] { 15f, 40f });
            headerTable2.WidthPercentage = 100;

            PdfPCell cellHeader1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeader2 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeaderBody = new PdfPCell() { Border = Rectangle.NO_BORDER };

            PdfPCell cellHeaderCS2 = new PdfPCell() { Border = Rectangle.NO_BORDER, Colspan = 2 };


            cellHeaderCS2.Phrase = new Phrase("SURAT JALAN", bold_font);
            cellHeaderCS2.HorizontalAlignment = Element.ALIGN_CENTER;
            headerTable.AddCell(cellHeaderCS2);

            cellHeaderCS2.Phrase = new Phrase("", bold_font);
            cellHeaderCS2.HorizontalAlignment = Element.ALIGN_CENTER;
            headerTable.AddCell(cellHeaderCS2);

            cellHeaderBody.Phrase = new Phrase("PT. DANLIRIS", normal_font);
            headerTable1.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("Kel. Banaran, (St.Lawehan), Telp.(0271)-714400", normal_font);
            headerTable1.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("Sukoharjo - Indonesia", normal_font);
            headerTable1.AddCell(cellHeaderBody);

            cellHeader1.AddElement(headerTable1);
            headerTable.AddCell(cellHeader1);

            cellHeaderCS2.Phrase = new Phrase("", bold_font);
            headerTable2.AddCell(cellHeaderCS2);

            cellHeaderBody.Phrase = new Phrase("No.", normal_font);
            headerTable2.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase(": " + viewModel.DeliveryOrderNo, normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase("No. Fakt./Inv.", normal_font);
            headerTable2.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase(": " + viewModel.SalesInvoiceNo, normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase("Tgl. Fakt./Inv.", normal_font);
            headerTable2.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase(": " + viewModel.SalesInvoiceDate.AddHours(clientTimeZoneOffset).ToString("dd MMMM yyyy", new CultureInfo("id-ID")), normal_font);
            headerTable2.AddCell(cellHeaderBody);

            //List<string> supplier = model.Details.Select(m => m.SupplierName).Distinct().ToList();
            cellHeaderBody.Phrase = new Phrase("Kepada Yth.", normal_font);
            headerTable2.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase(": " + viewModel.BuyerName, normal_font);
            headerTable2.AddCell(cellHeaderBody);


            cellHeaderBody.Phrase = new Phrase("NPWP", normal_font);
            headerTable2.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase(": " + viewModel.BuyerNPWP, normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeader2.AddElement(headerTable2);
            headerTable.AddCell(cellHeader2);

            cellHeaderCS2.Phrase = new Phrase("", normal_font);
            headerTable.AddCell(cellHeaderCS2);

            document.Add(headerTable);

            #endregion Header

            Dictionary<string, double> units = new Dictionary<string, double>();
            Dictionary<string, double> percentageUnits = new Dictionary<string, double>();

            int index = 1;
            double result = 0;
            double totalItem = 0;
            if (viewModel.CurrencyCode == "IDR")
            {
                #region Body

                PdfPTable bodyTable = new PdfPTable(4);
                PdfPCell bodyCell = new PdfPCell();

                float[] widthsBody = new float[] { 5f, 10f, 7f, 7f };
                bodyTable.SetWidths(widthsBody);
                bodyTable.WidthPercentage = 100;

                bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyCell.Phrase = new Phrase("No.", bold_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Phrase = new Phrase("Nama Barang", bold_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Phrase = new Phrase("Kurs", bold_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Phrase = new Phrase("Jumlah", bold_font);
                bodyTable.AddCell(bodyCell);

                foreach (SalesInvoiceDetailViewModel item in viewModel.SalesInvoiceDetails)
                {
                    bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    bodyCell.VerticalAlignment = Element.ALIGN_TOP;
                    bodyCell.Phrase = new Phrase((index++).ToString(), normal_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    bodyCell.Phrase = new Phrase(item.UnitName, normal_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    bodyCell.Phrase = new Phrase(viewModel.CurrencyCode, normal_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    bodyCell.Phrase = new Phrase(string.Format("{0:n2}", item.Amount), normal_font);
                    bodyTable.AddCell(bodyCell);

                    foreach (var amount in viewModel.SalesInvoiceDetails)
                    {
                        result += item.Amount;
                    }
                    
                    totalItem = result * 0.1 + result;
                }

                bodyCell.Colspan = 1;
                bodyCell.Border = Rectangle.NO_BORDER;
                bodyCell.Phrase = new Phrase("", normal_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Colspan = 1;
                bodyCell.Border = Rectangle.BOX;
                bodyCell.HorizontalAlignment = Element.ALIGN_LEFT;
                bodyCell.Phrase = new Phrase("Total", bold_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Colspan = 1;
                bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyCell.Phrase = new Phrase(viewModel.CurrencyCode, bold_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Colspan = 1;
                bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyCell.Phrase = new Phrase(string.Format("{0:n2}", totalItem), bold_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Colspan = 1;
                bodyCell.Border = Rectangle.NO_BORDER;
                bodyCell.Phrase = new Phrase("", normal_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                bodyCell.Phrase = new Phrase(string.Format("{0:n4}", result), bold_font);
                bodyTable.AddCell(bodyCell);

                document.Add(bodyTable);

                #endregion Body
            }
            //else
            //{
            //    #region BodyNonIDR

            //    PdfPTable bodyNonIDRTable = new PdfPTable(6);
            //    PdfPCell bodyCell = new PdfPCell();

            //    float[] widthsBodyNonIDR = new float[] { 5f, 10f, 10f, 10f, 15f, 7f };
            //    bodyNonIDRTable.SetWidths(widthsBodyNonIDR);
            //    bodyNonIDRTable.WidthPercentage = 100;

            //    bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    bodyCell.Phrase = new Phrase("No.", bold_font);
            //    bodyNonIDRTable.AddCell(bodyCell);

            //    bodyCell.Phrase = new Phrase("Nama Barang", bold_font);
            //    bodyNonIDRTable.AddCell(bodyCell);

            //    bodyCell.Phrase = new Phrase("Kurs", bold_font);
            //    bodyNonIDRTable.AddCell(bodyCell);

            //    bodyCell.Phrase = new Phrase("Jumlah", bold_font);
            //    bodyNonIDRTable.AddCell(bodyCell);

            //    bodyCell.Phrase = new Phrase("Jumlah (IDR)", bold_font);
            //    bodyNonIDRTable.AddCell(bodyCell);

            //    bodyCell.Phrase = new Phrase("Keterangan", bold_font);
            //    bodyNonIDRTable.AddCell(bodyCell);

            //    foreach (SalesInvoiceDetailViewModel item in viewModel.SalesInvoiceDetails)
            //    {
            //        bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //        bodyCell.VerticalAlignment = Element.ALIGN_TOP;
            //        bodyCell.Phrase = new Phrase((index++).ToString(), normal_font);
            //        bodyNonIDRTable.AddCell(bodyCell);

            //        bodyCell.HorizontalAlignment = Element.ALIGN_LEFT;
            //        bodyCell.Phrase = new Phrase(item.UnitName, normal_font);
            //        bodyNonIDRTable.AddCell(bodyCell);

            //        bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //        bodyCell.Phrase = new Phrase(viewModel.CurrencyCode, normal_font);
            //        bodyNonIDRTable.AddCell(bodyCell);

            //        bodyCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //        bodyCell.Phrase = new Phrase(string.Format("{0:n2}", item.Amount), normal_font);
            //        bodyNonIDRTable.AddCell(bodyCell);

            //        bodyCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //        bodyCell.Phrase = new Phrase(string.Format("{0:n4}", (item.Amount * viewModel.CurrencyRate)), normal_font);
            //        bodyNonIDRTable.AddCell(bodyCell);

            //        bodyCell.HorizontalAlignment = Element.ALIGN_LEFT;
            //        bodyCell.Phrase = new Phrase(viewModel.Notes, normal_font);
            //        bodyNonIDRTable.AddCell(bodyCell);

            //        foreach (var amount in viewModel.SalesInvoiceDetails)
            //        {
            //            result += item.Amount;
            //        }

            //        totalItem = result * 0.1 + result;
            //    }

            //    bodyCell.Colspan = 3;
            //    bodyCell.Border = Rectangle.NO_BORDER;
            //    bodyCell.Phrase = new Phrase("", normal_font);
            //    bodyNonIDRTable.AddCell(bodyCell);

            //    bodyCell.Colspan = 1;
            //    bodyCell.Border = Rectangle.BOX;
            //    bodyCell.HorizontalAlignment = Element.ALIGN_LEFT;
            //    bodyCell.Phrase = new Phrase("Total", bold_font);
            //    bodyNonIDRTable.AddCell(bodyCell);

            //    bodyCell.Colspan = 1;
            //    bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    bodyCell.Phrase = new Phrase(viewModel.CurrencyCode, bold_font);
            //    bodyNonIDRTable.AddCell(bodyCell);

            //    bodyCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //    bodyCell.Phrase = new Phrase(string.Format("{0:n4}", result), bold_font);
            //    bodyNonIDRTable.AddCell(bodyCell);

            //    document.Add(bodyNonIDRTable);

            //    #endregion BodyNonIDR
            //}

            #region Footer

            PdfPTable footerTable = new PdfPTable(2);
            PdfPCell cellFooterLeft = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellFooterRight = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };

            float[] widthsFooter = new float[] { 10f, 5f };
            footerTable.SetWidths(widthsFooter);
            footerTable.WidthPercentage = 100;

            cellFooterLeft.Phrase = new Phrase("\nCatatan : " + viewModel.Notes, bold_font);
            footerTable.AddCell(cellFooterLeft);

            cellFooterLeft.Phrase = new Phrase("\n\n\nKami harap Surat Jalan ini dikirim kembali kepada kami. Terima kasih.\n\n\n", normal_font);
            footerTable.AddCell(cellFooterLeft);

            cellFooterLeft.Phrase = new Phrase("", normal_font);
            footerTable.AddCell(cellFooterLeft);

            cellFooterLeft.Phrase = new Phrase("Sukoharjo,                     " , normal_font);
            footerTable.AddCell(cellFooterLeft);

            cellFooterLeft.Phrase = new Phrase("", normal_font);
            footerTable.AddCell(cellFooterLeft);

            PdfPTable signatureTable = new PdfPTable(3);
            PdfPCell signatureCell = new PdfPCell() { HorizontalAlignment = Element.ALIGN_CENTER };
            
            signatureCell.Phrase = new Phrase("Penerima", normal_font);
            signatureTable.AddCell(signatureCell);
            signatureCell.Phrase = new Phrase("Angkutan", normal_font);
            signatureTable.AddCell(signatureCell);
            signatureCell.Phrase = new Phrase("Div. Pemasaran Textile", normal_font);
            signatureTable.AddCell(signatureCell);

            signatureTable.AddCell(new PdfPCell()
            {
                Phrase = new Phrase("---------------------------", normal_font),
                FixedHeight = 40,
                VerticalAlignment = Element.ALIGN_BOTTOM,
                HorizontalAlignment = Element.ALIGN_CENTER
            }); signatureTable.AddCell(new PdfPCell()
            {
                Phrase = new Phrase("---------------------------", normal_font),
                FixedHeight = 40,
                VerticalAlignment = Element.ALIGN_BOTTOM,
                HorizontalAlignment = Element.ALIGN_CENTER
            }); signatureTable.AddCell(new PdfPCell()
            {
                Phrase = new Phrase("---------------------------", normal_font),
                FixedHeight = 40,
                VerticalAlignment = Element.ALIGN_BOTTOM,
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            footerTable.AddCell(new PdfPCell(signatureTable));

            cellFooterLeft.Phrase = new Phrase("", normal_font);
            footerTable.AddCell(cellFooterLeft);
            document.Add(footerTable);

            #endregion Footer

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }
}
