using Com.Danliris.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.FinishingPrintingCostCalculation
{
    public class FinishingPrintingCostCalculationViewModel : BaseViewModel, IValidatableObject
    {

        public double CurrencyRate { get; set; }
        public double ProductionUnitValue { get; set; }
        public int TKLQuantity { get; set; }
        public double PreparationFabricWeight { get; set; }
        public double RFDFabricWeight { get; set; }
        public double ActualPrice { get; set; }
        public double CargoCost { get; set; }
        public double InsuranceCost { get; set; }
        public string Remark { get; set; }
        public List<FinishingPrintingCostCalculationMachineViewModel> Machines { get; set; }
        public DateTimeOffset Date { get; set; }
        public string ProductionOrderNo { get; set; }
        public InstructionViewModel Instruction { get; set; }
        public FinishingPrintingPreSalesContractViewModel PreSalesContract { get; set; }
        public UomViewModel UOM { get; set; }
        public ProductViewModel Greige { get; set; }
        public double OrderQuantity { get; set; }
        public MaterialViewModel Material { get; set; }
        public string Color { get; set; }
        public AccountViewModel Sales { get; set; }
        public decimal ConfirmPrice { get; set; }
        public double Comission { get; set; }
        public decimal FreightCost { get; set; }
        public decimal OTL1 { get; set; }
        public decimal OTL2 { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PreSalesContract == null)
                yield return new ValidationResult("Sales Contract harus diisi!", new List<string> { "PreSalesContract" });

            if (Instruction == null)
                yield return new ValidationResult("Instruksi harus diisi!", new List<string> { "Instruction" });

            if (UOM == null)
            {
                yield return new ValidationResult("Satuan harus diisi!", new List<string> { "UOM" });
            }

            if (Sales == null)
                yield return new ValidationResult("Sales harus diisi!", new List<string> { "Sales" });

            if (string.IsNullOrEmpty(Color))
                yield return new ValidationResult("Warna harus diisi!", new List<string> { "Color" });

            if (Greige == null)
                yield return new ValidationResult("Greige harus diisi!", new List<string> { "Greige" });

            if(Date == null)
                yield return new ValidationResult("Tanggal harus diisi!", new List<string> { "Date" });

            if (Material == null)
                yield return new ValidationResult("Material harus diisi!", new List<string> { "Material" });

            if (CurrencyRate <= 0)
                yield return new ValidationResult("Kurs harus lebih dari 0!", new List<string> { "CurrencyRate" });

            if (ProductionUnitValue <= 0)
                yield return new ValidationResult("Produksi Unit harus lebih dari 0!", new List<string> { "ProductionUnitValue" });

            if (TKLQuantity <= 0)
                yield return new ValidationResult("Jumlah TKL harus lebih dari 0!", new List<string> { "TKLQuantity" });


            if (PreparationFabricWeight <= 0)
                yield return new ValidationResult("Berat Kain Prep harus lebih dari 0!", new List<string> { "PreparationFabricWeight" });

            if (RFDFabricWeight <= 0)
                yield return new ValidationResult("Berat Kain RFD harus lebih dari 0!", new List<string> { "RFDFabricWeight" });

            if (ActualPrice <= 0)
                yield return new ValidationResult("Harga Real harus lebih dari 0!", new List<string> { "ActualPrice" });

            if(ConfirmPrice <=0)
                yield return new ValidationResult("Confirm Price harus diisi!", new List<string> { "ConfirmPrice" });


            if (CargoCost <= 0)
                yield return new ValidationResult("Biaya Kargo harus lebih dari 0!", new List<string> { "CargoCost" });

            if (InsuranceCost <= 0)
                yield return new ValidationResult("Asuransi harus lebih dari 0!", new List<string> { "InsuranceCost" });

            if (Machines == null || Machines.Count == 0)
                yield return new ValidationResult("Asuransi harus lebih dari 0!", new List<string> { "Machine" });

            else
            {
                var anyError = false;
                var machinesErrors = "[ ";
                foreach (var machine in Machines)
                {
                    machinesErrors += "{";

                    if (machine.Machine == null)
                    {
                        anyError = true;
                        machinesErrors += "Machine: 'Mesin harus diisi!', ";
                    }

                    if (machine.Step == null)
                    {
                        anyError = true;
                        machinesErrors += "StepProcess: 'Proses harus diisi!', ";
                    }

                    if (machine.Chemicals == null || machine.Chemicals.Count == 0)
                    {
                        anyError = true;
                        machinesErrors += "Chemical: 'Biaya Chemical harus diisi!', ";
                    }
                    else
                    {
                        machinesErrors += "Chemicals: [ ";

                        foreach (var chemical in machine.Chemicals)
                        {
                            machinesErrors += "{";

                            if (chemical.Chemical == null)
                            {
                                anyError = true;
                                machinesErrors += "Chemical: 'Chemical harus diisi!', ";
                            }

                            if (chemical.ChemicalQuantity <= 0)
                            {
                                anyError = true;
                                machinesErrors += "ChemicalQuantity: 'Chemical Quantity harus lebih dari 0!', ";
                            }

                            machinesErrors += "}, ";
                        }

                        machinesErrors += "], ";
                    }

                    machinesErrors += "}, ";
                }
                machinesErrors += " ]";

                if (anyError)
                {
                    yield return new ValidationResult(machinesErrors, new List<string> { "Machines" });
                }
            }
        }
    }
}
