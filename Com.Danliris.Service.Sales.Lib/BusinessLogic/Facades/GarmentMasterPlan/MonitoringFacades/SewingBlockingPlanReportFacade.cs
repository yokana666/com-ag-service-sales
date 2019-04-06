using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentMasterPlan.MonitoringInterfaces;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.MonitoringLogics;
using Com.Danliris.Service.Sales.Lib.Models.GarmentSewingBlockingPlanModel;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MonitoringViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.MonitoringFacades
{
    public class SewingBlockingPlanReportFacade : ISewingBlockingPlanReportFacade
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<GarmentSewingBlockingPlan> DbSet;
        private IdentityService IdentityService;
        private SewingBlockingPlanReportLogic SewingBlockingPlanReportLogic;

        public SewingBlockingPlanReportFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<GarmentSewingBlockingPlan>();
            IdentityService = serviceProvider.GetService<IdentityService>();
            SewingBlockingPlanReportLogic = serviceProvider.GetService<SewingBlockingPlanReportLogic>();
        }

        public Tuple<MemoryStream, string> GenerateExcel(string filter = "{}")
        {
            var Query = SewingBlockingPlanReportLogic.GetQuery(filter);
            var data = Query.ToList();

            DataTable result = new DataTable();

            List<object> rowValuesForEmptyData = new List<object>();
            Dictionary<string, List<UnitDataTable>> rowData = new Dictionary<string, List<UnitDataTable>>();
            Dictionary<string, int> column = new Dictionary<string, int>();

            result.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "BUYER-KOMODITI", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "SMV Sewing", DataType = typeof(string) });
            rowValuesForEmptyData.Add("");
            
            foreach (var weeks in data.First().items)
            {
                string week = string.Concat("W", weeks.weekNumber, "  ", weeks.weekEndDate.ToString("dd MMM"));
                result.Columns.Add(new DataColumn() { ColumnName = week, DataType = typeof(string) });
                if (!column.ContainsKey(week))
                {
                    column.Add(week, weeks.weekNumber);
                }
            }
            

            Dictionary<string, int> count = new Dictionary<string, int>();
            Dictionary<string, double> smv = new Dictionary<string, double>();
            Dictionary<string, double> total = new Dictionary<string, double>();
            Dictionary<string, string> totalConfirmed = new Dictionary<string, string>();
            Dictionary<string, string> units = new Dictionary<string, string>();
            Dictionary<string, double> totalPerUnit = new Dictionary<string, double>();
            Dictionary<string, unitTable> unitBuyers = new Dictionary<string, unitTable>();
            Dictionary<string, double> totalWHConfirm = new Dictionary<string, double>();
            var BGC = "";
            foreach (var dt in data)
            {
                string uw = string.Concat(dt.unit, "-", dt.weekSewingBlocking);
                string uwb = string.Concat(dt.unit, "-", dt.weekSewingBlocking, "-", dt.buyer);
                string ub = string.Concat(dt.unit, "-", dt.buyer);

                if (!totalPerUnit.ContainsKey(uw))
                {
                    totalPerUnit.Add(uw, dt.bookingQty);
                }
                else
                {
                    totalPerUnit[uw] += dt.bookingQty;
                }

                if (!totalWHConfirm.ContainsKey(uw))
                {
                    if(dt.isConfirmed)
                        totalWHConfirm.Add(uw, dt.UsedEH);
                }
                else
                {
                    if (dt.isConfirmed)
                        totalWHConfirm[uw] += dt.UsedEH;
                }

                if (!count.ContainsKey(ub))
                {
                    count.Add(ub, 1);
                }
                else
                {
                    count[ub] += 1;
                }

                if (!smv.ContainsKey(ub))
                {
                    smv.Add(ub, dt.SMVSewing);
                }
                else
                {
                    smv[ub] += dt.SMVSewing;
                }

                if (!total.ContainsKey(uwb))
                {
                    total.Add(uwb, dt.bookingQty);
                }
                else
                {
                    total[uwb] += dt.bookingQty;
                }

                if (!totalConfirmed.ContainsKey(uwb))
                {
                    BGC = dt.bookingOrderItems.Count == 0 ? "yellow" :
                        dt.bookingOrderItems.Sum(a => a.ConfirmQuantity) < dt.bookingOrderQty ? "orange" :
                        "transparent";
                    totalConfirmed.Add(uwb, BGC);
                }
                else
                {
                    BGC = dt.bookingOrderItems.Count == 0 ? "yellow" :
                        dt.bookingOrderItems.Sum(a => a.ConfirmQuantity) < dt.bookingOrderQty ? "orange" :
                        "transparent";
                    totalConfirmed[uwb] = BGC;
                }

                if (!units.ContainsKey(dt.unit))
                {
                    units.Add(dt.unit, dt.unit);
                }
                unitTable ut = new unitTable
                {
                    unit=dt.unit,
                    buyer=dt.buyer,
                    week=dt.weekSewingBlocking
                };

                if (!unitBuyers.ContainsKey(ub))
                {
                    unitBuyers.Add(ub, ut);
                }
            }

            Dictionary<string, string> color = new Dictionary<string, string>();

            foreach (var un in units)
            {
                foreach (var ub in unitBuyers)
                {
                    if (un.Value == ub.Value.unit)
                    {
                        foreach (var w in column)
                        {
                            string unitBuyerWeek = string.Concat(ub.Value.unit, "-", w.Value, "-", ub.Value.buyer);
                            string unitBuyer = string.Concat(ub.Value.unit, "-", ub.Value.buyer);
                            string unitWeek = string.Concat(ub.Value.unit, "-", w.Value);
                            UnitDataTable unitDataTable = new UnitDataTable
                            {
                                Unit = ub.Value.unit,
                                buyer = ub.Value.buyer,
                                qty = total.ContainsKey(unitBuyerWeek) ? total[unitBuyerWeek].ToString() : "-",
                                week = w.Value
                            };

                            if (rowData.ContainsKey(unitBuyer))
                            {
                                var rowValue = rowData[unitBuyer];
                                var unit = rowValue.FirstOrDefault(a => a.week == w.Value && a.buyer == ub.Value.buyer && a.Unit == ub.Value.unit);
                                if (unit == null)
                                {
                                    rowValue.Add(unitDataTable);
                                }
                            }
                            else
                            {
                                rowData.Add(unitBuyer, new List<UnitDataTable> { unitDataTable });
                            }

                        }
                    }
                }
                #region Footer per Unit
                foreach (var weekNum in column)
                {
                    string totalKey = un.Value + "-" + "TOTAL";
                    string effKey = un.Value + "-" + "Efisiensi";
                    string opKey = un.Value + "-" + "Total Operator Sewing";
                    string whKey = un.Value + "-" + "Working Hours";
                    string AHKey = un.Value + "-" + "Total AH";
                    string EHKey = un.Value + "-" + "Total EH";
                    string useEHKey = un.Value + "-" + "Used EH";
                    string remEHKey = un.Value + "-" + "Remaining EH";
                    string whBookKey = un.Value + "-" + "WH Booking";
                    string whConfirmKey = un.Value + "-" + "WH Confirm";

                    string unWeek = string.Concat(un.Value, "-", weekNum.Value);
                    UnitDataTable unitDataTable1 = new UnitDataTable
                    {
                        Unit = un.Value,
                        buyer = "TOTAL",
                        qty = totalPerUnit.ContainsKey(unWeek) ? totalPerUnit[unWeek].ToString() : "-",
                        week = weekNum.Value
                    };
                    if (!rowData.ContainsKey(totalKey))
                    {
                        
                        rowData.Add(totalKey, new List<UnitDataTable> { unitDataTable1 });
                    }
                    else
                    {
                        rowData[totalKey].Add(unitDataTable1);
                    }

                    var dataStatic = data.FirstOrDefault(a => a.unit == un.Value).items;
                    var dtItem = dataStatic.FirstOrDefault(a => a.weekNumber == weekNum.Value);
                    double eff = dtItem == null ? 0 : dtItem.efficiency;
                    double op = dtItem == null ? 0 : dtItem.head;
                    double EHTot = dtItem == null ? 0 : dtItem.EHTotal;
                    double AHTot = dtItem == null ? 0 : dtItem.AHTotal;
                    double usedEH = dtItem == null ? 0 : dtItem.usedTotal;
                    double remainingEH = dtItem == null ? 0 : dtItem.remainingEH;
                    double workingHours = dtItem == null ? 0 : dtItem.workingHours;
                    double whBooking = dtItem == null ? 0 : dtItem.WHBooking;

                    UnitDataTable unitDataTableEfficiency = new UnitDataTable
                    {
                        Unit = un.Value,
                        buyer = "Efisiensi",
                        qty = eff.ToString() + "%",
                        week = weekNum.Value
                    };

                    if (!rowData.ContainsKey(effKey))
                    {
                        rowData.Add(effKey, new List<UnitDataTable> { unitDataTableEfficiency });
                    }
                    else
                    {
                        rowData[effKey].Add(unitDataTableEfficiency);
                    }

                    UnitDataTable unitDataTableOP = new UnitDataTable
                    {
                        Unit = un.Value,
                        buyer = "Operator",
                        qty = op.ToString(),
                        week = weekNum.Value
                    };

                    if (!rowData.ContainsKey(opKey))
                    {
                        rowData.Add(opKey, new List<UnitDataTable> { unitDataTableOP });
                    }
                    else
                    {
                        rowData[opKey].Add(unitDataTableOP);
                    }

                    UnitDataTable unitDataTableWH = new UnitDataTable
                    {
                        Unit = un.Value,
                        buyer = "WorkingHours",
                        qty = workingHours.ToString(),
                        week = weekNum.Value
                    };

                    if (!rowData.ContainsKey(whKey))
                    {
                        rowData.Add(whKey, new List<UnitDataTable> { unitDataTableWH });
                    }
                    else
                    {
                        rowData[whKey].Add(unitDataTableWH);
                    }

                    UnitDataTable unitDataTableAH = new UnitDataTable
                    {
                        Unit = un.Value,
                        buyer = "Total AH",
                        qty = AHTot.ToString(),
                        week = weekNum.Value
                    };

                    if (!rowData.ContainsKey(AHKey))
                    {
                        rowData.Add(AHKey, new List<UnitDataTable> { unitDataTableAH });
                    }
                    else
                    {
                        rowData[AHKey].Add(unitDataTableAH);
                    }

                    UnitDataTable unitDataTableEH = new UnitDataTable
                    {
                        Unit = un.Value,
                        buyer = "Total EH",
                        qty = EHTot.ToString(),
                        week = weekNum.Value
                    };

                    if (!rowData.ContainsKey(EHKey))
                    {
                        rowData.Add(EHKey, new List<UnitDataTable> { unitDataTableEH });
                    }
                    else
                    {
                        rowData[EHKey].Add(unitDataTableEH);
                    }

                    UnitDataTable unitDataTableUseEH = new UnitDataTable
                    {
                        Unit = un.Value,
                        buyer = "Used EH",
                        qty = usedEH.ToString(),
                        week = weekNum.Value
                    };

                    if (!rowData.ContainsKey(useEHKey))
                    {
                        rowData.Add(useEHKey, new List<UnitDataTable> { unitDataTableUseEH });
                    }
                    else
                    {
                        rowData[useEHKey].Add(unitDataTableUseEH);
                    }

                    UnitDataTable unitDataTableRemEH = new UnitDataTable
                    {
                        Unit = un.Value,
                        buyer = "Used EH",
                        qty = remainingEH.ToString(),
                        week = weekNum.Value
                    };

                    if (!rowData.ContainsKey(remEHKey))
                    {
                        rowData.Add(remEHKey, new List<UnitDataTable> { unitDataTableRemEH });
                    }
                    else
                    {
                        rowData[remEHKey].Add(unitDataTableRemEH);
                    }

                    UnitDataTable unitDataTableWHBook = new UnitDataTable
                    {
                        Unit = un.Value,
                        buyer = "WH Booking",
                        qty = Math.Round(whBooking,2).ToString(),
                        week = weekNum.Value
                    };

                    if (!rowData.ContainsKey(whBookKey))
                    {
                        rowData.Add(whBookKey, new List<UnitDataTable> { unitDataTableWHBook });
                    }
                    else
                    {
                        rowData[whBookKey].Add(unitDataTableWHBook);
                    }

                    double ehConf = totalWHConfirm.ContainsKey(unWeek) ? totalWHConfirm[unWeek] : 0;
                    double whConf = ehConf != 0 ? (ehConf / op) * eff / 100 : 0;

                    UnitDataTable unitDataTableWHConfirm = new UnitDataTable
                    {
                        Unit = un.Value,
                        buyer = "WH Confirm",
                        qty = Math.Round(whConf, 2).ToString(),
                        week = weekNum.Value
                    };

                    if (!rowData.ContainsKey(whConfirmKey))
                    {
                        rowData.Add(whConfirmKey, new List<UnitDataTable> { unitDataTableWHConfirm });
                    }
                    else
                    {
                        rowData[whConfirmKey].Add(unitDataTableWHConfirm);
                    }
                }
                #endregion
            }

            Dictionary<string, string> Rowcount = new Dictionary<string, string>();
            int idx = 1;
            var rCount = 0;
            if (data.Count == 0)
            {
                result.Rows.Add(rowValuesForEmptyData.ToArray());
            }
            else
            {
                foreach (var rowValue in rowData)
                {
                    idx++;
                    List<object> rowValues = new List<object>();

                    var keys = rowValue.Key.Split("-");

                    string unBuy = "";


                    var unitCode= keys[0];

                    if (!Rowcount.ContainsKey(unitCode))
                    {
                        rCount = 0;
                        var index = idx;
                        Rowcount.Add(unitCode, index.ToString() );
                    }
                    else
                    {
                        rCount += 1;
                        Rowcount[unitCode] = Rowcount[unitCode] + "-" + rCount.ToString();
                        var val = Rowcount[unitCode].Split("-");
                        if ((val).Length > 0)
                        {
                            Rowcount[unitCode] = val[0] + "-" + rCount.ToString();
                        }
                    }

                    if (keys.Length > 2)
                    {
                        rowValues.Add(keys[0]);//unit
                        rowValues.Add(keys[1].Trim() + " - " + keys[2].Trim());//buyer-como
                        unBuy = string.Concat(keys[0], "-", keys[1], "-", keys[2]);
                    }
                    else
                    {
                        rowValues.Add(keys[0]);//unit
                        rowValues.Add(keys[1].Trim());//TOTAL
                        unBuy = string.Concat(keys[0], "-", keys[1]);
                    }

                    var smvAvg = smv.ContainsKey(unBuy) && count.ContainsKey(unBuy) ? (smv[unBuy] / count[unBuy]).ToString() : "-";
                    rowValues.Add(smvAvg);

                    foreach (var a in rowValue.Value)
                    {
                        foreach (var w in column)
                        {
                            if(w.Value==a.week)
                                rowValues.Add(a.qty);
                            
                        }
                    }
                    result.Rows.Add(rowValues.ToArray());
                }
            }

            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Blocking Plan Sewing");
            sheet.Cells["A1"].LoadFromDataTable(result, true, OfficeOpenXml.Table.TableStyles.Light16);

            for (int y = 1; y <= sheet.Dimension.Rows; y++)
            {
                for (int x = 1; x <= sheet.Dimension.Columns; x++)
                {
                    var cell = sheet.Cells[y, x];

                    cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    cell.Style.Font.Size = 12;
                    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cell.Style.Fill.BackgroundColor.SetColor(Color.WhiteSmoke);

                    if (y > 1)
                    {
                        if (x == 2)
                        {
                            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        }
                        if (sheet.Cells[y, 2].Value.ToString() == "Remaining EH")
                        {
                            if (x > 3 && x <= sheet.Dimension.Columns)
                            {
                                cell.Style.Fill.BackgroundColor.SetColor(
                                    Convert.ToInt32(cell.Value) > 0 ? Color.Yellow :
                                    Convert.ToInt32(cell.Value) < 0 ? Color.Red :
                                    Color.Green);
                            }
                        }
                        if (sheet.Cells[y, 2].Value.ToString() == "WH Booking" || sheet.Cells[y, 2].Value.ToString() == "WH Confirm")
                        {
                            if (x > 3 && x <= sheet.Dimension.Columns)
                            {
                                cell.Style.Fill.BackgroundColor.SetColor(
                                    Convert.ToDouble(cell.Value) <= 45.5 ? Color.Yellow :
                                    Convert.ToDouble(cell.Value) > 45.5 && Convert.ToDouble(cell.Value) <=50.5 ? Color.Red :
                                    Convert.ToDouble(cell.Value) > 50.5 && Convert.ToDouble(cell.Value) <= 56.5 ? Color.Green :
                                    Color.LightSlateGray);
                            }
                        }

                        if (sheet.Cells[y, 2].Value.ToString().Contains("-"))
                        {
                            if (x > 3 && x <= sheet.Dimension.Columns)
                            {
                                var categoryUWB = string.Concat(sheet.Cells[y, 1].Value.ToString(), "-", (x - 3).ToString(), "-", sheet.Cells[y, 2].Value.ToString());
                                if (cell.Value.ToString() != "-")
                                {
                                    cell.Style.Fill.BackgroundColor.SetColor(
                                        totalConfirmed.ContainsKey(categoryUWB) ? totalConfirmed[categoryUWB] == "yellow" ?
                                        Color.Yellow : totalConfirmed[categoryUWB] == "orange" ? Color.Orange : Color.WhiteSmoke : Color.WhiteSmoke);
                                }
                            }
                        }
                        else
                        {
                            if (x > 1 && x <= sheet.Dimension.Columns)
                            {
                                cell.Style.Font.Bold = true;
                            }
                            if (x == 3 && cell.Value.ToString() == "-")
                            {
                                cell.Value = "";
                            }
                        }
                    }
                    else
                    {
                        if (x > 0 && x <= sheet.Dimension.Columns)
                        {
                            cell.Style.Fill.BackgroundColor.SetColor(Color.SteelBlue);
                        }
                    }

                }
            }

            foreach (var rowMerge in Rowcount)
            {
                var UnitrowNum= rowMerge.Value.Split("-");
                int rowNum2 = 1;
                int rowNum1 = Convert.ToInt32(UnitrowNum[0]);
                if (UnitrowNum.Length > 0)
                {
                    rowNum2 = Convert.ToInt32(rowNum1) + Convert.ToInt32(UnitrowNum[1]);
                }
                else
                {
                    rowNum2 = Convert.ToInt32(rowNum1);
                }
                
                sheet.Cells[$"A{rowNum1}:A{rowNum2}"].Merge = true;
                sheet.Cells[$"A{rowNum1}:A{rowNum2}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                sheet.Cells[$"A{rowNum1}:A{rowNum2}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }

            
            sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            MemoryStream streamExcel = new MemoryStream();
            package.SaveAs(streamExcel);

            Dictionary<string, string> FilterDictionary = new Dictionary<string, string>(JsonConvert.DeserializeObject<Dictionary<string, string>>(filter), StringComparer.OrdinalIgnoreCase);
            string fileName = string.Concat("Sewing Blocking Plan Report ", FilterDictionary["year"], ".xlsx");

            return Tuple.Create(streamExcel, fileName);
        }

        public Tuple<List<SewingBlockingPlanReportViewModel>, int> Read(int page = 1, int size = 25, string filter = "{}")
        {
            var Query = SewingBlockingPlanReportLogic.GetQuery(filter);
            var data = Query.ToList();
            return Tuple.Create(data, data.Count);
        }

        class UnitDataTable
        {
            public string Unit { get; set; }
            public string buyer { get; set; }
            public string qty { get; set; }
            public int week { get; set; }
        }


        class rowCountTable
        {
            public int index { get; set; }
            public int count { get; set; }
        }

        class unitTable
        {
            public string unit { get; set; }
            public string buyer { get; set; }
            public byte week { get; set; }
        }
    }
}
