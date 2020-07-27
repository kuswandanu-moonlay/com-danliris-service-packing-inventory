﻿using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;
using System.IO;
using System.Data;
using System.Globalization;
using OfficeOpenXml;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval
{
    public class OutputAvalService : IOutputAvalService
    {
        private readonly IDyeingPrintingAreaOutputRepository _outputRepository;
        private readonly IDyeingPrintingAreaInputRepository _inputRepository;
        private readonly IDyeingPrintingAreaMovementRepository _movementRepository;
        private readonly IDyeingPrintingAreaSummaryRepository _summaryRepository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _inputProductionOrderRepository;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _outputProductionOrderRepository;

        private const string OUT = "OUT";
        private const string ADJ = "ADJ";

        private const string IM = "IM";
        private const string TR = "TR";
        private const string PC = "PC";
        private const string GJ = "GJ";
        private const string GA = "GA";
        private const string SP = "SP";
        private const string PJ = "PJ";
        private const string BY = "BY";
        private const string ADJ_IN = "ADJ IN";
        private const string ADJ_OUT = "ADJ OUT";


        private const string INSPECTIONMATERIAL = "INSPECTION MATERIAL";
        private const string TRANSIT = "TRANSIT";
        private const string PACKING = "PACKING";
        private const string GUDANGJADI = "GUDANG JADI";
        private const string GUDANGAVAL = "GUDANG AVAL";
        private const string SHIPPING = "SHIPPING";
        private const string PENJUALAN = "PENJUALAN";
        private const string BUYER = "BUYER";


        public OutputAvalService(IServiceProvider serviceProvider)
        {
            _outputRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputRepository>();
            _inputRepository = serviceProvider.GetService<IDyeingPrintingAreaInputRepository>();
            _movementRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _summaryRepository = serviceProvider.GetService<IDyeingPrintingAreaSummaryRepository>();
            _inputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _outputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
        }

        private OutputAvalViewModel MapToViewModel(DyeingPrintingAreaOutputModel model)
        {
            var vm = new OutputAvalViewModel();
            if (model.Type == null || model.Type == OUT)
            {
                vm = new OutputAvalViewModel()
                {
                    Active = model.Active,
                    Id = model.Id,
                    Type = OUT,
                    Area = model.Area,
                    BonNo = model.BonNo,
                    CreatedAgent = model.CreatedAgent,
                    CreatedBy = model.CreatedBy,
                    CreatedUtc = model.CreatedUtc,
                    Date = model.Date,
                    DeletedAgent = model.DeletedAgent,
                    DeletedBy = model.DeletedBy,
                    DeletedUtc = model.DeletedUtc,
                    IsDeleted = model.IsDeleted,
                    LastModifiedAgent = model.LastModifiedAgent,
                    LastModifiedBy = model.LastModifiedBy,
                    LastModifiedUtc = model.LastModifiedUtc,
                    Shift = model.Shift,
                    Group = model.Group,
                    DestinationArea = model.DestinationArea,
                    HasNextAreaDocument = model.HasNextAreaDocument,
                    DeliveryOrderSalesNo = model.DeliveryOrderSalesNo,
                    DeliveryOrdeSalesId = Convert.ToInt32(model.DeliveryOrderSalesId),
                    AvalItems = model.DyeingPrintingAreaOutputProductionOrders.Select(s => new OutputAvalItemViewModel()
                    {
                        Active = s.Active,
                        LastModifiedUtc = s.LastModifiedUtc,
                        CreatedAgent = s.CreatedAgent,
                        CreatedBy = s.CreatedBy,
                        CreatedUtc = s.CreatedUtc,
                        DeletedAgent = s.DeletedAgent,
                        DeletedBy = s.DeletedBy,
                        DeletedUtc = s.DeletedUtc,
                        IsDeleted = s.IsDeleted,
                        LastModifiedAgent = s.LastModifiedAgent,
                        LastModifiedBy = s.LastModifiedBy,

                        Id = s.Id,
                        AvalType = s.AvalType,
                        AvalCartNo = s.AvalCartNo,
                        AvalUomUnit = s.UomUnit,
                        AvalQuantity = s.AvalALength,
                        AvalQuantityKg = s.AvalBLength,
                        DeliveryNote = s.DeliveryNote,

                        AvalOutSatuan = s.Balance,
                        AvalOutQuantity = s.AvalQuantityKg,

                    }).ToList()
                };
            }
            else
            {
                vm = new OutputAvalViewModel()
                {
                    Active = model.Active,
                    Id = model.Id,
                    Type = ADJ,
                    Area = model.Area,
                    BonNo = model.BonNo,
                    CreatedAgent = model.CreatedAgent,
                    CreatedBy = model.CreatedBy,
                    CreatedUtc = model.CreatedUtc,
                    Date = model.Date,
                    DeletedAgent = model.DeletedAgent,
                    DeletedBy = model.DeletedBy,
                    DeletedUtc = model.DeletedUtc,
                    IsDeleted = model.IsDeleted,
                    LastModifiedAgent = model.LastModifiedAgent,
                    LastModifiedBy = model.LastModifiedBy,
                    LastModifiedUtc = model.LastModifiedUtc,
                    Shift = model.Shift,
                    Group = model.Group,
                    HasNextAreaDocument = model.HasNextAreaDocument,
                    AvalItems = model.DyeingPrintingAreaOutputProductionOrders.Select(s => new OutputAvalItemViewModel()
                    {
                        AdjDocumentNo = s.AdjDocumentNo,
                        Active = s.Active,
                        LastModifiedUtc = s.LastModifiedUtc,
                        CreatedAgent = s.CreatedAgent,
                        CreatedBy = s.CreatedBy,
                        CreatedUtc = s.CreatedUtc,
                        DeletedAgent = s.DeletedAgent,
                        DeletedBy = s.DeletedBy,
                        DeletedUtc = s.DeletedUtc,
                        IsDeleted = s.IsDeleted,
                        LastModifiedAgent = s.LastModifiedAgent,
                        LastModifiedBy = s.LastModifiedBy,

                        Id = s.Id,
                        AvalType = s.AvalType,

                        AvalQuantity = s.Balance,
                        AvalQuantityKg = s.AvalQuantityKg,

                    }).ToList()
                };
            }


            return vm;
        }

        public string GenerateBonNo(int totalPreviousData, DateTimeOffset date, string destinationArea)
        {
            switch (destinationArea)
            {
                //case SHIPPING:
                //    string.Format("{0}.{1}.{2}.{3}", GA, SP, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
                //    break;
                case PENJUALAN:
                    return string.Format("{0}.{1}.{2}.{3}", GA, PJ, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
                case BUYER:
                    return string.Format("{0}.{1}.{2}.{3}", GA, BY, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
                default:
                    return string.Format("{0}.{1}.{2}.{3}", GA, GA, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            //return string.Format("{0}.{1}.{2}.{3}", GA, SP, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));

        }

        private string GenerateBonNoAdj(int totalPreviousData, DateTimeOffset date, string area, IEnumerable<double> qtySatuan, IEnumerable<double> qtyWeight)
        {
            if (qtySatuan.All(s => s > 0) && qtyWeight.All(s => s > 0))
            {
                return string.Format("{0}.{1}.{2}.{3}", ADJ_IN, GA, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
            else
            {
                return string.Format("{0}.{1}.{2}.{3}", ADJ_OUT, GA, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }
        }

        private async Task<int> CreateOut(OutputAvalViewModel viewModel)
        {
            int result = 0;

            //Count Existing Document in Aval Output (and Destination Area) by Year
            int totalCurrentYearData = _outputRepository.ReadAllIgnoreQueryFilter().Count(s => s.Area == GUDANGAVAL &&
                                                                                               s.DestinationArea == viewModel.DestinationArea &&
                                                                                               s.CreatedUtc.Year == viewModel.Date.Year &&
                                                                                               s.Type == OUT);
            //Generate Bon Number
            string bonNo = string.Empty;
            var bonExist = _outputRepository.ReadAll().Where(s => s.Area == GUDANGAVAL &&
                                                                s.Date.Date == viewModel.Date.Date &&
                                                                s.Shift == viewModel.Shift &&
                                                                s.DestinationArea == viewModel.DestinationArea &&
                                                                s.DeliveryOrderSalesNo == viewModel.DeliveryOrderSalesNo &&
                                                                s.Type == OUT);
            int bonExistCount = bonExist.Count();
            if (bonExistCount == 0)
                bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date, viewModel.DestinationArea);
            else
                bonNo = bonExist.FirstOrDefault().BonNo;

            //Filter only Item Has Quantity and Quantity KG can be Inserted
            //viewModel.AvalItems = viewModel.AvalItems.Where(s => s.AvalQuantity > 0 && s.AvalQuantityKg > 0).ToList();
            DyeingPrintingAreaOutputModel model = null;
            if (bonExistCount == 0)
            {
                //Instantiate Output Model
                model = new DyeingPrintingAreaOutputModel(viewModel.Date,
                                                              viewModel.Area,
                                                              viewModel.Shift,
                                                              bonNo,
                                                              viewModel.DeliveryOrderSalesNo,
                                                              viewModel.DeliveryOrdeSalesId,
                                                              false,
                                                              viewModel.DestinationArea,
                                                              viewModel.Group,
                                                              viewModel.Type,
                                                              viewModel.AvalItems.Select(s => new DyeingPrintingAreaOutputProductionOrderModel(s.AvalType,
                                                                                                                                               s.AvalCartNo,
                                                                                                                                               s.AvalUomUnit,
                                                                                                                                               s.AvalOutSatuan,
                                                                                                                                               s.AvalOutQuantity,
                                                                                                                                               s.AvalQuantity,
                                                                                                                                               s.AvalQuantityKg,
                                                                                                                                               viewModel.Area,
                                                                                                                                               viewModel.DestinationArea,
                                                                                                                                               s.DeliveryNote))
                                                                                 .ToList());

                //Create New Row in Output and ProductionOrdersOutput in Each Repository 
                result = await _outputRepository.InsertAsync(model);
            }
            else
            {
                model = new DyeingPrintingAreaOutputModel(bonExist.FirstOrDefault().Date,
                                                              bonExist.FirstOrDefault().Area,
                                                              bonExist.FirstOrDefault().Shift,
                                                              bonNo,
                                                              viewModel.DeliveryOrderSalesNo,
                                                              viewModel.DeliveryOrdeSalesId,
                                                              false,
                                                              bonExist.FirstOrDefault().DestinationArea,
                                                              bonExist.FirstOrDefault().Group,
                                                              viewModel.Type,
                                                              viewModel.AvalItems.Select(s => new DyeingPrintingAreaOutputProductionOrderModel(s.AvalType,
                                                                                                                                               s.AvalCartNo,
                                                                                                                                               s.AvalUomUnit,
                                                                                                                                               s.AvalOutSatuan,
                                                                                                                                               s.AvalOutQuantity,
                                                                                                                                               s.AvalQuantity,
                                                                                                                                               s.AvalQuantityKg,
                                                                                                                                               bonExist.FirstOrDefault().Id,
                                                                                                                                               viewModel.Area,
                                                                                                                                               viewModel.DestinationArea,
                                                                                                                                               s.DeliveryNote))
                                                                                 .ToList());
                foreach (var avalitem in model.DyeingPrintingAreaOutputProductionOrders)
                {
                    result += await _outputProductionOrderRepository.InsertAsync(avalitem);
                }
            }

            //var productionOrderIds = _outputProductionOrderRepository.ReadAll().Where(o => o.DyeingPrintingAreaOutputId == model.Id).ToList();

            ////Movement from Aval Input Area to Aval Output Area
            //foreach (var DyeingPrintingMovement in viewModel.DyeingPrintingMovementIds)
            //{
            //    //Get Previous Summary
            //    var previousSummary = _summaryRepository.ReadAll()
            //                                            .FirstOrDefault(s => s.DyeingPrintingAreaDocumentId == DyeingPrintingMovement.DyeingPrintingAreaMovementId);
            //                                                                 //&& s.ProductionOrderId == DyeingPrintingMovement.AvalItemId);

            //    //Update Previous Summary
            //    result += await _summaryRepository.UpdateToAvalAsync(previousSummary, viewModel.Date, viewModel.Area, TYPE);
            //}

            //foreach (var item in viewModel.DyeingPrintingMovementIds)
            //{
            //    var vmItem = _inputProductionOrderRepository.GetInputProductionOrder(item.AvalItemId);

            //    result += await _inputProductionOrderRepository.UpdateFromOutputAsync(vmItem.Id, true);
            //}

            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                //var vmItem = viewModel.AvalItems.FirstOrDefault(s => s.AvalCartNo == item.AvalCartNo);

                //result += await _inputProductionOrderRepository.UpdateFromOutputAsync(vmItem.Id, true);

                var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, OUT, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                        item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id, item.ProductionOrderType, item.Balance, item.AvalQuantityKg, item.AvalType);

                result += await _movementRepository.InsertAsync(movementModel);
            }

            var groupedType = model.DyeingPrintingAreaOutputProductionOrders.GroupBy(s => s.AvalType,
                                                                                     s => s,
                                                                                     (key, item) => new { Key = key, Items = item });
            foreach (var type in groupedType)
            {
                //update bon Aval Sum
                var bonLastAval = _inputRepository.ReadAll().Where(s => s.Area == GUDANGAVAL &&
                                                                        s.IsTransformedAval &&
                                                                        s.AvalType == type.Key).OrderByDescending(s => s.Date).FirstOrDefault();
                if (bonLastAval != null)
                {
                    var sumType = type.Items.Sum(s => s.AvalQuantityKg);
                    var sumTypeQuantity = type.Items.Sum(s => s.Balance);
                    var substractSum = bonLastAval.TotalAvalWeight - sumType;
                    var substractQuantity = bonLastAval.TotalAvalQuantity - sumTypeQuantity;
                    bonLastAval.SetTotalAvalWeight(substractSum, "OUTPUTAVALSERVICE", "SERVICES");
                    bonLastAval.SetTotalAvalQuantity(substractQuantity, "OUTPUTAVALSERVICE", "SERVICES");
                    result += await _inputRepository.UpdateAsync(bonLastAval.Id, bonLastAval);
                }
            }


            return result;
        }

        private async Task<int> CreateAdj(OutputAvalViewModel viewModel)
        {
            int result = 0;
            string type = "";
            string bonNo = "";
            if (viewModel.AvalItems.All(d => d.AvalQuantity > 0 && d.AvalQuantityKg > 0))
            {
                int totalCurrentYearData = _outputRepository.ReadAllIgnoreQueryFilter().Count(s => s.Area == SHIPPING && s.Type == ADJ_IN && s.CreatedUtc.Year == viewModel.Date.Year);
                bonNo = GenerateBonNoAdj(totalCurrentYearData + 1, viewModel.Date, viewModel.Area, viewModel.AvalItems.Select(d => d.AvalQuantity), viewModel.AvalItems.Select(d => d.AvalQuantityKg));
                type = ADJ_IN;
            }
            else
            {
                int totalCurrentYearData = _outputRepository.ReadAllIgnoreQueryFilter().Count(s => s.Area == SHIPPING && s.Type == ADJ_OUT && s.CreatedUtc.Year == viewModel.Date.Year);
                bonNo = GenerateBonNoAdj(totalCurrentYearData + 1, viewModel.Date, viewModel.Area, viewModel.AvalItems.Select(d => d.AvalQuantity), viewModel.AvalItems.Select(d => d.AvalQuantityKg));
                type = ADJ_OUT;
            }
            var model = new DyeingPrintingAreaOutputModel(viewModel.Date, viewModel.Area, viewModel.Shift, bonNo, true, "", viewModel.Group,
                        viewModel.Type, viewModel.AvalItems.Select(s =>
                     new DyeingPrintingAreaOutputProductionOrderModel(viewModel.Area, true, s.AvalType, s.AvalQuantity, s.AvalQuantityKg, s.AdjDocumentNo)).ToList());

            result = await _outputRepository.InsertAsync(model);

            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                var movementModel = new DyeingPrintingAreaMovementModel(viewModel.Date, viewModel.Area, type, model.Id, model.BonNo, item.ProductionOrderId, item.ProductionOrderNo,
                       item.CartNo, item.Buyer, item.Construction, item.Unit, item.Color, item.Motif, item.UomUnit, item.Balance, item.Id, item.ProductionOrderType, item.Balance, item.AvalQuantityKg, item.AvalType);
                result += await _movementRepository.InsertAsync(movementModel);
            }

            return result;
        }

        public async Task<int> Create(OutputAvalViewModel viewModel)
        {
            int result = 0;

            if (viewModel.Type == OUT)
            {
                result = await CreateOut(viewModel);
            }
            else
            {
                result = await CreateAdj(viewModel);
            }

            return result;
        }

        public ListResult<IndexViewModel> Read(int page,
                                               int size,
                                               string filter,
                                               string order,
                                               string keyword)
        {
            var query = _outputRepository.ReadAll().Where(s => s.Area == GUDANGAVAL &&
            ((s.Type == OUT || s.Type == null) && !s.HasNextAreaDocument || (s.Type != OUT && s.Type != null)));
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaOutputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaOutputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaOutputModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new IndexViewModel()
            {
                BonNo = s.BonNo,
                Date = s.Date,
                Id = s.Id,
                Shift = s.Shift,
                Group = s.Group,
                Type = s.Type == null || s.Type == OUT ? OUT : ADJ,
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public ListResult<AvailableAvalIndexViewModel> ReadAvailableAval(DateTimeOffset searchDate,
                                                                         string searchShift,
                                                                         string searchGroup,
                                                                         int page,
                                                                         int size,
                                                                         string filter,
                                                                         string order,
                                                                         string keyword)
        {
            var query = _inputRepository.ReadAll().Where(s => s.Date <= searchDate &&
                                                         s.Shift == searchShift &&
                                                         s.Group == searchGroup &&
                                                         s.Area == GUDANGAVAL &&
                                                         s.DyeingPrintingAreaInputProductionOrders.Any(o => !o.HasOutputDocument));
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaInputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaInputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaInputModel>.Order(query, OrderDictionary);
            var data = new List<AvailableAvalIndexViewModel>();
            foreach (var avalInput in query.Skip((page - 1) * size).Take(size))
            {
                foreach (var avalInputItem in avalInput.DyeingPrintingAreaInputProductionOrders)
                {
                    var avalItems = new AvailableAvalIndexViewModel()
                    {
                        AvalInputId = avalInput.Id,
                        Date = avalInput.Date,
                        Area = avalInput.Area,
                        Shift = avalInput.Shift,
                        Group = avalInput.Group,
                        BonNo = avalInput.BonNo,
                        AvalItemId = avalInputItem.Id,
                        AvalType = avalInputItem.AvalType,
                        AvalCartNo = avalInputItem.AvalCartNo,
                        AvalUomUnit = avalInputItem.UomUnit,
                        AvalQuantity = avalInputItem.Balance,
                        AvalQuantityKg = avalInputItem.AvalQuantityKg
                    };

                    data.Add(avalItems);
                }
            }

            return new ListResult<AvailableAvalIndexViewModel>(data, page, size, query.Count());
        }

        public async Task<OutputAvalViewModel> ReadById(int id)
        {
            var model = await _outputRepository.ReadByIdAsync(id);
            if (model == null)
                return null;

            OutputAvalViewModel vm = MapToViewModel(model);

            return vm;
        }

        private MemoryStream GenerateExcelOut(DyeingPrintingAreaOutputModel model)
        {
            var query = model.DyeingPrintingAreaOutputProductionOrders;

            var indexNumber = 1;
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "NO", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "NAMA BARANG", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "KET", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "KG", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "");
            }
            else
            {
                foreach (var item in query)
                {
                    dt.Rows.Add(indexNumber,
                                item.AvalType,
                                item.Balance,
                                item.UomUnit,
                                item.AvalQuantityKg);
                    indexNumber++;
                }
            }

            ExcelPackage package = new ExcelPackage();
            #region Header
            var sheet = package.Workbook.Worksheets.Add("Bon Keluar Aval");
            sheet.Cells[1, 1].Value = "DIVISI";
            sheet.Cells[1, 2].Value = "DYEING PRINTING PT DANLIRIS";

            sheet.Cells[2, 1].Value = "TANGGAL";
            sheet.Cells[2, 2].Value = model.Date.ToString("dd MMMM yyyy", new CultureInfo("id-ID"));

            sheet.Cells[3, 1].Value = "GROUP";
            sheet.Cells[3, 2].Value = model.Shift;

            sheet.Cells[4, 1].Value = "MUTASI";
            sheet.Cells[4, 2].Value = "KELUAR";

            sheet.Cells[5, 1].Value = "ZONA";
            sheet.Cells[5, 2].Value = model.DestinationArea;
            sheet.Cells[5, 2, 5, 3].Merge = true;

            sheet.Cells[7, 1].Value = "BON PENYERAHAN BARANG";
            sheet.Cells[7, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[7, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[7, 1, 7, 5].Merge = true;

            sheet.Cells[8, 1].Value = "PT. DANLIRIS";
            sheet.Cells[8, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[8, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[8, 1, 8, 5].Merge = true;

            sheet.Cells[9, 1].Value = "SUKOHARJO";
            sheet.Cells[9, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[9, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[9, 1, 9, 3].Merge = true;

            sheet.Cells[10, 1].Value = "Dari Seksi/ Bagian :";
            sheet.Cells[10, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[10, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[10, 1, 10, 3].Merge = true;
            //sheet.Cells[10, 4].Value = model.OriginSection;

            sheet.Cells[11, 1].Value = "Untuk Seksi/ Bagian :";
            sheet.Cells[11, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            sheet.Cells[11, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[11, 1, 11, 3].Merge = true;
            //sheet.Cells[11, 4].Value = model.DestinationSection;

            sheet.Cells[12, 1].Value = "Yang Menerima,";
            sheet.Cells[12, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[12, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[12, 1, 12, 2].Merge = true;

            sheet.Cells[12, 4].Value = "Yang Menyerahkan,";
            sheet.Cells[12, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[12, 4].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[12, 4, 12, 5].Merge = true;

            //sheet.Cells[15, 1].Value = "( " + model.ReceiveOperator + " )";
            sheet.Cells[15, 1].Value = "(  )";
            sheet.Cells[15, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[15, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[15, 1, 15, 2].Merge = true;

            //sheet.Cells[15, 1].Value = "( " + model.SubmitOperator + " )";
            sheet.Cells[15, 4].Value = "(  )";
            sheet.Cells[15, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[15, 4].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[15, 4, 15, 5].Merge = true;

            sheet.Cells[16, 1].Value = "NO.";
            sheet.Cells[16, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[16, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[16, 1].AutoFitColumns();
            sheet.Cells[16, 1, 17, 1].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 1, 17, 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 1, 17, 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 1, 17, 1].Merge = true;

            sheet.Cells[16, 2].Value = "NAMA BARANG";
            sheet.Cells[16, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[16, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[16, 2].AutoFitColumns();
            sheet.Cells[16, 2, 17, 2].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 2, 17, 2].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 2, 17, 2].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 2, 17, 2].Merge = true;

            sheet.Cells[16, 3].Value = "SAT";
            sheet.Cells[16, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[16, 3].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[16, 3].AutoFitColumns();
            sheet.Cells[16, 3, 16, 4].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 3].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 4].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 3, 16, 4].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 3, 16, 4].Merge = true;

            sheet.Cells[17, 3].Value = "QTY";
            sheet.Cells[17, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[17, 3].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[17, 3].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[17, 3].AutoFitColumns();

            sheet.Cells[17, 4].Value = "KET";
            sheet.Cells[17, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[17, 4].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[17, 4].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[17, 4].AutoFitColumns();

            sheet.Cells[16, 5].Value = "KG";
            sheet.Cells[16, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[16, 5].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[16, 5].AutoFitColumns();
            sheet.Cells[16, 5].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 5, 17, 5].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 5, 17, 5].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[16, 5, 17, 5].Merge = true;
            #endregion

            int tableRowStart = 18;
            int tableColStart = 1;

            sheet.Cells[tableRowStart, tableColStart].LoadFromDataTable(dt, false, OfficeOpenXml.Table.TableStyles.Light8);
            sheet.Cells[tableRowStart, tableColStart].AutoFitColumns();
            //sheet.Cells[tableRowStart, tableColStart].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);

            //return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Bon Aval Area Dyeing Printing") }, true);
            return stream;
        }

        private MemoryStream GenerateExcelAdj(DyeingPrintingAreaOutputModel model)
        {
            var query = model.DyeingPrintingAreaOutputProductionOrders.OrderBy(s => s.AvalType);
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "Nama Barang", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Keluar Satuan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Keluar Berat", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No Dokumen", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Paraf", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "");
            }
            else
            {
                foreach (var item in query)
                {
                    dt.Rows.Add(item.AvalType, item.Balance.ToString("N2", CultureInfo.InvariantCulture), item.AvalQuantityKg.ToString("N2", CultureInfo.InvariantCulture),
                        item.AdjDocumentNo, "");

                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Gudang Aval") }, true);
        }

        public async Task<MemoryStream> GenerateExcel(int id)
        {
            var model = await _outputRepository.ReadByIdAsync(id);
            if (model.Type == null || model.Type == OUT)
            {
                return GenerateExcelOut(model);
            }
            else
            {
                return GenerateExcelAdj(model);
            }

        }

        public ListResult<AvailableAvalIndexViewModel> ReadAllAvailableAval(int page, int size, string filter, string order, string keyword)
        {
            var query = _inputRepository.ReadAll().Where(s =>
                                                         s.Area == GUDANGAVAL &&
                                                         s.DyeingPrintingAreaInputProductionOrders.Any(o => !o.HasOutputDocument));
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaInputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaInputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaInputModel>.Order(query, OrderDictionary);
            var data = new List<AvailableAvalIndexViewModel>();
            foreach (var avalInput in query)
            {
                foreach (var avalInputItem in avalInput.DyeingPrintingAreaInputProductionOrders)
                {
                    var avalItems = new AvailableAvalIndexViewModel()
                    {
                        AvalInputId = avalInput.Id,
                        Date = avalInput.Date,
                        Area = avalInput.Area,
                        Shift = avalInput.Shift,
                        Group = avalInput.Group,
                        BonNo = avalInput.BonNo,
                        AvalItemId = avalInputItem.Id,
                        AvalType = avalInputItem.AvalType,
                        AvalCartNo = avalInputItem.AvalCartNo,
                        AvalUomUnit = avalInputItem.UomUnit,
                        AvalQuantity = avalInputItem.Balance,
                        AvalQuantityKg = avalInputItem.AvalQuantityKg
                    };

                    data.Add(avalItems);
                }
            }

            return new ListResult<AvailableAvalIndexViewModel>(data, page, size, query.Count());
        }

        public ListResult<AvailableAvalIndexViewModel> ReadByBonAvailableAval(int bonId, int page, int size, string filter, string order, string keyword)
        {
            var query = _inputRepository.ReadAll().Where(s =>
                                                         s.Id == bonId &&
                                                         s.Area == GUDANGAVAL &&
                                                         s.DyeingPrintingAreaInputProductionOrders.Any(o => !o.HasOutputDocument));
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaInputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaInputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaInputModel>.Order(query, OrderDictionary);
            var data = new List<AvailableAvalIndexViewModel>();
            foreach (var avalInput in query)
            {
                foreach (var avalInputItem in avalInput.DyeingPrintingAreaInputProductionOrders)
                {
                    var avalItems = new AvailableAvalIndexViewModel()
                    {
                        AvalInputId = avalInput.Id,
                        Date = avalInput.Date,
                        Area = avalInput.Area,
                        Shift = avalInput.Shift,
                        Group = avalInput.Group,
                        BonNo = avalInput.BonNo,
                        AvalItemId = avalInputItem.Id,
                        AvalType = avalInputItem.AvalType,
                        AvalCartNo = avalInputItem.AvalCartNo,
                        AvalUomUnit = avalInputItem.UomUnit,
                        AvalQuantity = avalInputItem.Balance,
                        AvalQuantityKg = avalInputItem.AvalQuantityKg
                    };

                    data.Add(avalItems);
                }
            }

            return new ListResult<AvailableAvalIndexViewModel>(data, page, size, query.Count());
        }

        public ListResult<AvailableAvalIndexViewModel> ReadByTypeAvailableAval(string avalType, int page, int size, string filter, string order, string keyword)
        {
            var query = _inputRepository.ReadAll().Where(s =>
                                                         s.AvalType == avalType &&
                                                         s.Area == GUDANGAVAL &&
                                                         s.IsTransformedAval &&
                                                         s.DyeingPrintingAreaInputProductionOrders.Any(o => !o.HasOutputDocument));
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingAreaInputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingAreaInputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingAreaInputModel>.Order(query, OrderDictionary);
            var data = new List<AvailableAvalIndexViewModel>();
            var queryGroup = query.GroupBy(
                s => s.AvalType,
                s => s,
                (key, item) => new { AvalType = key, AvalItem = item }
                );
            foreach (var avalInput in queryGroup)
            {
                var avalItems = new AvailableAvalIndexViewModel()
                {

                    AvalType = avalInput.AvalType,
                    AvalUomUnit = avalInput.AvalItem.First().DyeingPrintingAreaInputProductionOrders.FirstOrDefault().UomUnit,
                    AvalQuantity = avalInput.AvalItem.Sum(s => s.TotalAvalQuantity),
                    AvalQuantityKg = avalInput.AvalItem.Sum(s => s.TotalAvalWeight),
                };

                data.Add(avalItems);
            }

            return new ListResult<AvailableAvalIndexViewModel>(data, page, size, query.Count());
        }

        public ListResult<AdjAvalItemViewModel> GetDistinctAllProductionOrder(int page, int size, string filter, string order, string keyword)
        {
            var query = _inputRepository.ReadAll()
                 .Where(s => s.Area == GUDANGAVAL && s.IsTransformedAval && s.TotalAvalQuantity != 0 && s.TotalAvalWeight != 0)
                 .Select(d => new PlainAdjAvalItem()
                 {
                     AvalType = d.AvalType,
                     AvalQuantity = d.TotalAvalQuantity,
                     AvalQuantityKg = d.TotalAvalWeight
                 })
                 .Union(_outputProductionOrderRepository.ReadAll()
                 .Where(s => s.Area == GUDANGAVAL && !s.HasNextAreaDocument)
                 .Select(d => new PlainAdjAvalItem()
                 {
                     AvalType = d.AvalType,
                     AvalQuantity = d.Balance,
                     AvalQuantityKg = d.AvalQuantityKg
                 }));
            List<string> SearchAttributes = new List<string>()
            {
                "AvalType"
            };

            query = QueryHelper<PlainAdjAvalItem>.Search(query, SearchAttributes, keyword, true);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<PlainAdjAvalItem>.Filter(query, FilterDictionary);

            var data = query.ToList()
                .GroupBy(d => d.AvalType)
                .Select(s => s.First())
                .Skip((page - 1) * size).Take(size)
                .OrderBy(s => s.AvalType)
                .Select(s => new AdjAvalItemViewModel()
                {
                    AvalType = s.AvalType,
                    AvalQuantity = s.AvalQuantity,
                    AvalQuantityKg = s.AvalQuantityKg
                });

            return new ListResult<AdjAvalItemViewModel>(data.ToList(), page, size, query.Count());
        }

        public MemoryStream GenerateExcel(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet)
        {
            var query = _outputRepository.ReadAll().Where(s => s.Area == GUDANGAVAL &&
                (((s.Type == null || s.Type == OUT) && s.DyeingPrintingAreaOutputProductionOrders.Any(d => !d.HasNextAreaDocument)) || (s.Type != null && s.Type != OUT)));

            if (dateFrom.HasValue && dateTo.HasValue)
            {
                query = query.Where(s => dateFrom.Value.Date <= s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date &&
                            s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date <= dateTo.Value.Date);
            }
            else if (!dateFrom.HasValue && dateTo.HasValue)
            {
                query = query.Where(s => s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date <= dateTo.Value.Date);
            }
            else if (dateFrom.HasValue && !dateTo.HasValue)
            {
                query = query.Where(s => dateFrom.Value.Date <= s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date);
            }


            query = query.OrderBy(s => s.Type).ThenBy(s => s.DestinationArea).ThenBy(d => d.BonNo);

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No Bon", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nama Barang", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Saldo Karung", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Saldo KG", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY Keluar Karung", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QTY Keluar KG", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis", DataType = typeof(string) });

            if (query.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "");
            }
            else
            {
                foreach (var model in query)
                {
                    if (model.Type == null || model.Type == OUT)
                    {
                        foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.Where(d => !d.HasNextAreaDocument).OrderBy(s => s.AvalType))
                        {
                            dt.Rows.Add(model.BonNo, item.AvalType, item.AvalALength.ToString("N2", CultureInfo.InvariantCulture), item.AvalBLength.ToString("N2", CultureInfo.InvariantCulture),
                               item.Balance.ToString("N2", CultureInfo.InvariantCulture), item.AvalQuantityKg.ToString("N2", CultureInfo.InvariantCulture), OUT);

                        }

                    }
                    else
                    {
                        foreach (var item in model.DyeingPrintingAreaOutputProductionOrders.OrderBy(s => s.AvalType))
                        {
                            dt.Rows.Add(model.BonNo, item.AvalType, item.AvalALength.ToString("N2", CultureInfo.InvariantCulture), item.AvalBLength.ToString("N2", CultureInfo.InvariantCulture),
                               item.Balance.ToString("N2", CultureInfo.InvariantCulture), item.AvalQuantityKg.ToString("N2", CultureInfo.InvariantCulture), ADJ);

                        }
                    }
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Inspection Material") }, true);
        }
    }
}
