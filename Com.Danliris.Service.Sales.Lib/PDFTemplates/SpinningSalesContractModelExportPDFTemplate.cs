using System;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Nut;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.ViewModels.Spinning;

namespace Com.Danliris.Service.Sales.Lib.PDFTemplates
{
    public class SpinningSalesContractModelExportPDFTemplate
    {
        public MemoryStream GeneratePdfTemplate(SpinningSalesContractViewModel viewModel, int timeoffset)
        {
            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 18);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);

            Document document = new Document(PageSize.A4, 40, 40, 40, 40);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();

            #region customViewModel

            var uom = "";
            double convertion = 0;
            if (viewModel.UomUnit == "BALL")
            {
                uom = "BALES";
                convertion = (viewModel.OrderQuantity) * (181.44);
            }

            var ppn = viewModel.IncomeTax;
            if (ppn == "Include PPn")
            {
                ppn = "Include PPn 10%";
            }

            string QuantityToText = NumberToTextEN.toWords(viewModel.OrderQuantity);
            double amount = Convert.ToDouble((viewModel.Price * convertion).ToString("N2"));
            string AmountToText = NumberToTextEN.toWords(amount);

            var tax = viewModel.IncomeTax == "Include PPn" ? "Include PPn 10%" : viewModel.IncomeTax;

            var detailprice = viewModel.AccountBank.Currency.Symbol + " " + viewModel.Price + " / KG";

            var appx = "";
            var date = viewModel.DeliverySchedule.Value.Day;
            if (date >= 1 && date <= 10)
            {
                appx = "EARLY";
            }
            else if (date >= 11 && date <= 20)
            {
                appx = "MIDDLE";
            }
            else if (date >= 21 && date <= 31)
            {
                appx = "END";
            }

            #endregion

            #region Header

            string blankString = " ";
            Paragraph bankSpace = new Paragraph(blankString, normal_font);
            bankSpace.SpacingAfter = 100f;
            document.Add(bankSpace);


            string codeNoString = "FM-PJ-00-03-003";
            Paragraph codeNo = new Paragraph(codeNoString, bold_font) { Alignment = Element.ALIGN_RIGHT };
            document.Add(codeNo);

            string titleString = "SALES CONTRACT";
            Paragraph title = new Paragraph(titleString, bold_font) { Alignment = Element.ALIGN_CENTER };
            title.SpacingAfter = 10f;
            document.Add(title);
            bold_font.SetStyle(Font.NORMAL);

            #endregion

            #region Identity

            PdfPTable tableIdentity = new PdfPTable(3);
            tableIdentity.SetWidths(new float[] { 0.5f, 4.5f, 2.5f });
            PdfPCell cellIdentityContentLeft = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellIdentityContentRight = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            tableIdentity.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            tableIdentity.AddCell(cellIdentityContentLeft);
            cellIdentityContentRight.Phrase = new Phrase($"Sukoharjo, {viewModel.CreatedUtc.AddHours(timeoffset).ToString("dd MMMM yyyy", new CultureInfo("en-US"))}", normal_font);
            tableIdentity.AddCell(cellIdentityContentRight);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            tableIdentity.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            tableIdentity.AddCell(cellIdentityContentLeft);
            cellIdentityContentRight.Phrase = new Phrase("");
            tableIdentity.AddCell(cellIdentityContentRight);
            PdfPCell cellIdentity = new PdfPCell(tableIdentity); // dont remove
            tableIdentity.ExtendLastRow = false;
            tableIdentity.SpacingAfter = 10f;
            document.Add(tableIdentity);

            PdfPTable tableIdentityOpeningLetter = new PdfPTable(3);
            cellIdentityContentLeft.Phrase = new Phrase("MESSRS,", normal_font);
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentRight.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentRight);
            cellIdentityContentRight.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentRight);
            cellIdentityContentLeft.Phrase = new Phrase(viewModel.Buyer.Name, normal_font);
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentRight.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentRight);
            cellIdentityContentRight.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentRight);
            cellIdentityContentLeft.Phrase = new Phrase(viewModel.Buyer.Address, normal_font);
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentRight.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentRight);
            cellIdentityContentRight.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentRight);
            cellIdentityContentLeft.Phrase = new Phrase(viewModel.Buyer.City, normal_font);
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentRight.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentRight);
            cellIdentityContentRight.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentRight);
            PdfPCell cellIdentityOpeningLetter = new PdfPCell(tableIdentityOpeningLetter); // dont remove
            tableIdentityOpeningLetter.ExtendLastRow = false;
            tableIdentityOpeningLetter.SpacingAfter = 10f;
            document.Add(tableIdentityOpeningLetter);

            #endregion

            string HeaderParagraphString = "On behalf of :";
            Paragraph HeaderParagraph = new Paragraph(HeaderParagraphString, normal_font) { Alignment = Element.ALIGN_LEFT };
            document.Add(HeaderParagraph);

            string firstParagraphString = "P.T. DAN LIRIS KELURAHAN BANARAN, KECAMATAN GROGOL SUKOHARJO - INDONESIA, we confrm the order under the following terms and conditions as mentioned below: ";
            Paragraph firstParagraph = new Paragraph(firstParagraphString, normal_font) { Alignment = Element.ALIGN_JUSTIFIED };
            firstParagraph.SpacingAfter = 10f;
            document.Add(firstParagraph);

            #region body
            PdfPTable tableBody = new PdfPTable(2);
            tableBody.SetWidths(new float[] { 0.75f, 2f });
            PdfPCell bodyContentCenter = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell bodyContentLeft = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell bodyContentRight = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            bodyContentLeft.Phrase = new Phrase("Contract Number", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.SalesContractNo, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Comodity", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.Comodity.Name + " " + viewModel.ComodityDescription, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Quality", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.Quality.Name, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Quantity", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": ABOUT " + viewModel.OrderQuantity + " " + uom + " ( ABOUT : " + QuantityToText + " KG) ", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Price & Payment", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + detailprice, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(" ", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("  " + viewModel.TermOfShipment, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(" ", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("  " + viewModel.TermOfPayment.Name, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Amount", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.AccountBank.AccountCurrencyCode + " " + amount + " ( " + AmountToText + " " + viewModel.AccountBank.Currency.Description.ToUpper() + " ) (APPROXIMATELLY)", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Shipment", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + appx + " " + (viewModel.DeliverySchedule.Value.AddHours(timeoffset).ToString("MMMM yyyy", new CultureInfo("en-US"))).ToUpper() + " " + viewModel.ShipmentDescription, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Destination", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.DeliveredTo, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Packing", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.Packing, normal_font);
            tableBody.AddCell(bodyContentLeft);
            PdfPCell cellBody = new PdfPCell(tableBody); // dont remove
            tableBody.ExtendLastRow = false;
            document.Add(tableBody);

            PdfPTable conditionListBody = new PdfPTable(3);
            conditionListBody.SetWidths(new float[] { 0.4f, 0.025f, 1f });

            bodyContentLeft.Phrase = new Phrase("Condition", normal_font);
            conditionListBody.AddCell(bodyContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("-", normal_font);
            conditionListBody.AddCell(cellIdentityContentLeft);
            bodyContentLeft.Phrase = new Phrase("THIS CONTRACT IS IRREVOCABLE UNLESS AGREED UPON BY THE TWO PARTIES, THE BUYER AND SELLER.", normal_font);
            conditionListBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            conditionListBody.AddCell(bodyContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("-", normal_font);
            conditionListBody.AddCell(cellIdentityContentLeft);
            bodyContentLeft.Phrase = new Phrase("+/- " + viewModel.ShippingQuantityTolerance + " % FROM QUANTITY ORDER SHOULD BE ACCEPTABLE.", normal_font);
            conditionListBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionListBody.AddCell(bodyContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("-", normal_font);
            conditionListBody.AddCell(cellIdentityContentLeft);
            bodyContentLeft.Phrase = new Phrase("LOCAL CONTAINER DELIVERY CHARGES AT DESTINATION FOR BUYER'S ACCOUNT.", normal_font);
            conditionListBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionListBody.AddCell(bodyContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("- ", normal_font);
            conditionListBody.AddCell(cellIdentityContentLeft);
            bodyContentLeft.Phrase = new Phrase(viewModel.Condition, normal_font);
            conditionListBody.AddCell(bodyContentLeft);
            bodyContentRight.Phrase = new Phrase("");
            conditionListBody.AddCell(bodyContentRight);
            PdfPCell cellConditionList = new PdfPCell(conditionListBody); // dont remove
            conditionListBody.ExtendLastRow = false;
            conditionListBody.SpacingAfter = 10f;
            document.Add(conditionListBody);

            #endregion

            #region signature
            PdfPTable signature = new PdfPTable(2);
            signature.SetWidths(new float[] { 1f, 1f });
            PdfPCell cell_signature = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 2 };
            signature.SetWidths(new float[] { 1f, 1f });
            cell_signature.Phrase = new Phrase("Accepted and confrmed :", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("PT DANLIRIS", normal_font);
            signature.AddCell(cell_signature);

            cell_signature.Phrase = new Phrase("", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("", normal_font);
            signature.AddCell(cell_signature);

            string signatureArea = string.Empty;
            for (int i = 0; i < 5; i++)
            {
                signatureArea += Environment.NewLine;
            }

            cell_signature.Phrase = new Phrase(signatureArea, normal_font);
            signature.AddCell(cell_signature);
            signature.AddCell(cell_signature);

            cell_signature.Phrase = new Phrase("(...........................)", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("( SRI HENDRATNO )", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("Authorized signature", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("Marketing Textile", normal_font);
            signature.AddCell(cell_signature);
            cellIdentityContentRight.Phrase = new Phrase("");
            signature.AddCell(cellIdentityContentRight);

            PdfPCell signatureCell = new PdfPCell(signature); // dont remove
            signature.ExtendLastRow = false;
            signature.SpacingAfter = 10f;
            document.Add(signature);
            #endregion


            #region ConditionPage
            document.NewPage();

            string blankSpaceCondition = " ";
            Paragraph blankConditionSpace = new Paragraph(blankSpaceCondition, normal_font);
            blankConditionSpace.SpacingAfter = 100f;
            document.Add(blankConditionSpace);

            string ConditionString = "Remark";
            Paragraph ConditionName = new Paragraph(ConditionString, header_font) { Alignment = Element.ALIGN_LEFT };
            document.Add(ConditionName);

            string bulletListSymbol = "\u2022";
            PdfPCell bodyContentJustify = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_JUSTIFIED };

            PdfPTable conditionList = new PdfPTable(2);
            conditionList.SetWidths(new float[] { 0.01f, 1f });

            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase("All instructions regarding sticker, shipping marks etc. to be received 1 (one) month prior to shipment.", normal_font);
            conditionList.AddCell(bodyContentJustify);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase("Benefciary :  P.T. DAN LIRIS KELURAHAN BANARAN, KECAMATAN GROGOL SUKOHARJO - INDONESIA  (Phone No. 0271 - 740888 / 714400). ", normal_font);
            conditionList.AddCell(bodyContentJustify);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("Payment Transferred to: ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(viewModel.AccountBank.BankName, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(viewModel.AccountBank.BankAddress, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("ACCOUNT NAME : " + viewModel.AccountBank.AccountName, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("ACCOUNT NO : " + viewModel.AccountBank.AccountNumber, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("SWIFT CODE : " + viewModel.AccountBank.SwiftCode, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase("Payment 30% TT in advance + 70% TT after receipt copies of shipping document to be negotiable with BANK PT Bank Unit Test.", normal_font);
            conditionList.AddCell(bodyContentJustify);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase("Please find enclosed some Indonesia Banking Regulations.", normal_font);
            conditionList.AddCell(bodyContentJustify);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase("If you find anything not order, please let us know immediately.", normal_font);
            conditionList.AddCell(bodyContentJustify);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            PdfPCell conditionListData = new PdfPCell(conditionList); // dont remove
            conditionList.ExtendLastRow = false;
            document.Add(conditionList);
            #endregion

            #region agentTemplate
            if (viewModel.Agent.Id != 0)
            {
                document.NewPage();

                string blankSpaceConditionAgent = " ";
                Paragraph blankConditionSpaceAgent = new Paragraph(blankSpaceConditionAgent, normal_font);
                blankConditionSpaceAgent.SpacingAfter = 100f;
                document.Add(blankConditionSpaceAgent);


                #region Identity
                PdfPTable agentIdentity = new PdfPTable(3);
                agentIdentity.SetWidths(new float[] { 0.5f, 4.5f, 2.5f });
                cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
                agentIdentity.AddCell(cellIdentityContentLeft);
                cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
                agentIdentity.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase($"Sukoharjo, {viewModel.CreatedUtc.AddHours(timeoffset).ToString("dd MMMM yyyy", new CultureInfo("en-US"))}", normal_font);
                agentIdentity.AddCell(cellIdentityContentRight);
                cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
                agentIdentity.AddCell(cellIdentityContentLeft);
                cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
                agentIdentity.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentity.AddCell(cellIdentityContentRight);
                PdfPCell agentCellIdentity = new PdfPCell(agentIdentity); // dont remove
                agentIdentity.ExtendLastRow = false;
                agentIdentity.SpacingAfter = 10f;
                document.Add(agentIdentity);

                PdfPTable agentIdentityOpeningLetter = new PdfPTable(3);
                agentIdentityOpeningLetter.SetWidths(new float[] { 2f, 4.5f, 2.5f });
                cellIdentityContentLeft.Phrase = new Phrase("MESSRS,", normal_font);
                agentIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentLeft.Phrase = new Phrase(viewModel.Buyer.Name, normal_font);
                agentIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentLeft.Phrase = new Phrase(viewModel.Buyer.Address, normal_font);
                agentIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentLeft.Phrase = new Phrase(viewModel.Buyer.Country, normal_font);
                agentIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentLeft.Phrase = new Phrase(viewModel.Buyer.Contact, normal_font);
                agentIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                PdfPCell agentCellIdentityOpeningLetter = new PdfPCell(agentIdentityOpeningLetter); // dont remove
                agentIdentityOpeningLetter.ExtendLastRow = false;
                agentIdentityOpeningLetter.SpacingAfter = 10f;
                document.Add(agentIdentityOpeningLetter);


                PdfPTable agentIdentityOpeningLetterHeader = new PdfPTable(1);
                bodyContentCenter.Phrase = new Phrase("COMMISSION AGREEMENT NO: " + viewModel.DispositionNumber, bold_font);
                agentIdentityOpeningLetterHeader.AddCell(bodyContentCenter);
                bodyContentCenter.Phrase = new Phrase("FOR SALES CONTRACT NO: " + viewModel.SalesContractNo, bold_font);
                agentIdentityOpeningLetterHeader.AddCell(bodyContentCenter);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetterHeader.AddCell(cellIdentityContentRight);
                PdfPCell agentIdentityOpeningLetterHeaderCell = new PdfPCell(agentIdentityOpeningLetterHeader); // dont remove
                agentIdentityOpeningLetterHeader.ExtendLastRow = false;
                agentIdentityOpeningLetterHeader.SpacingAfter = 10f;
                document.Add(agentIdentityOpeningLetterHeader);

                #endregion

                #region agentBody
                string agentFirstParagraphString = "This is to confirm that your order for " + viewModel.Buyer.Name + " concerning " + viewModel.OrderQuantity + " ( " + QuantityToText + ") " + uom + " ( ABOUT: " + convertion.ToString("N2") + " KG ) of "+ viewModel.Comodity.Name +" "+ viewModel.ComodityDescription;
                Paragraph agentFirstParagraph = new Paragraph(agentFirstParagraphString, normal_font) { Alignment = Element.ALIGN_JUSTIFIED };
                agentFirstParagraph.SpacingAfter = 10f;
                document.Add(agentFirstParagraph);
                string agentSecondParagraphString = "Placed with us, P.T. DAN LIRIS - SOLO INDONESIA, is inclusive of " + viewModel.Comission + " sales commission each KG on " + viewModel.TermOfShipment + " value, payable to you upon final negotiation and clearance of " + viewModel.TermOfPayment.Name + '.';
                Paragraph agentSecondParagraph = new Paragraph(agentSecondParagraphString, normal_font) { Alignment = Element.ALIGN_JUSTIFIED };
                agentSecondParagraph.SpacingAfter = 10f;
                document.Add(agentSecondParagraph);
                string agentThirdParagraphString = "Kindly acknowledge receipt by undersigning this Commission Agreement letter and returned one copy to us after having been confirmed and signed by you.";
                Paragraph agentThirdParagraph = new Paragraph(agentThirdParagraphString, normal_font) { Alignment = Element.ALIGN_JUSTIFIED };
                agentThirdParagraph.SpacingAfter = 30f;
                document.Add(agentThirdParagraph);
                #endregion

                #region signature
                PdfPTable signatureAgent = new PdfPTable(2);
                signatureAgent.SetWidths(new float[] { 1f, 1f });
                signatureAgent.SetWidths(new float[] { 1f, 1f });
                cell_signature.Phrase = new Phrase("Accepted and confrmed :", normal_font);
                signatureAgent.AddCell(cell_signature);
                cell_signature.Phrase = new Phrase("PT DANLIRIS", normal_font);
                signatureAgent.AddCell(cell_signature);

                cell_signature.Phrase = new Phrase("", normal_font);
                signatureAgent.AddCell(cell_signature);
                cell_signature.Phrase = new Phrase("", normal_font);
                signatureAgent.AddCell(cell_signature);

                string signatureAreaAgent = string.Empty;
                for (int i = 0; i < 5; i++)
                {
                    signatureAreaAgent += Environment.NewLine;
                }

                cell_signature.Phrase = new Phrase(signatureArea, normal_font);
                signatureAgent.AddCell(cell_signature);
                signatureAgent.AddCell(cell_signature);

                cell_signature.Phrase = new Phrase("(...........................)", normal_font);
                signatureAgent.AddCell(cell_signature);
                cell_signature.Phrase = new Phrase("( SRI HENDRATNO )", normal_font);
                signatureAgent.AddCell(cell_signature);
                cell_signature.Phrase = new Phrase("Authorized signature", normal_font);
                signatureAgent.AddCell(cell_signature);
                cell_signature.Phrase = new Phrase("Marketing Textile", normal_font);
                signatureAgent.AddCell(cell_signature);
                cellIdentityContentRight.Phrase = new Phrase("");
                signatureAgent.AddCell(cellIdentityContentRight);

                PdfPCell signatureCellAgent = new PdfPCell(signatureAgent); // dont remove
                signatureAgent.ExtendLastRow = false;
                signatureAgent.SpacingAfter = 10f;
                document.Add(signatureAgent);
            }
            #endregion

            #endregion

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }
}

