﻿using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class DyeingPrintingAreaOutputDataUtil : BaseDataUtil<DyeingPrintingAreaOutputRepository, DyeingPrintingAreaOutputModel>
    {
        public DyeingPrintingAreaOutputDataUtil(DyeingPrintingAreaOutputRepository repository) : base(repository)
        {
        }

        public override DyeingPrintingAreaOutputModel GetModel()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, "IM", "pa", "1", false, "TRANSIT", "A", new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                new DyeingPrintingAreaOutputProductionOrderModel("IM","TRANSIT",false,1,"a","e",1,"rr","1","as","test","unit","color","motif","mtr", "rem","a","a",2, 1,1),
                new DyeingPrintingAreaOutputProductionOrderModel("IM","TRANSIT",false,1,"a","e",1,"rr","1","as","test","unit","color","motif","mtr", "rem","a","a",2,1,1, "type"),
                new DyeingPrintingAreaOutputProductionOrderModel("IM","TRANSIT",false,1,"a",1,"a","a0,",1,"s","s","s","s","d","d","e","note",1,1, "unit","type",1,1, false,"s","s",1),
                new DyeingPrintingAreaOutputProductionOrderModel("IM","TRANSIT",false,1,"a","e","rr","1","as","test","unit","color","motif","mtr", "rem",10,"a","test",1,"PACK",10,"Pack",1),
                new DyeingPrintingAreaOutputProductionOrderModel("SAMBUNGAN","5-11","KRG", 15, 10)

            });
        }

        public DyeingPrintingAreaOutputModel GetModelShippingPenjualan()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, "SHIPPING", "pa", "1", false, "PENJUALAN", "A",1,"no",false, new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                 new DyeingPrintingAreaOutputProductionOrderModel("SHIPPING","PENJUALAN",false,1,"a",1,"a","a0,",1,"s","s","s","s","d","d","e",null,1,1, "unit","type",1,1, false,"s","s",1)

            });
        }

        public DyeingPrintingAreaOutputModel GetModelShippingPenjualanAfter()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, "SHIPPING", "pa", "1", false, "PENJUALAN", "A", 1, "no", false, new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                 new DyeingPrintingAreaOutputProductionOrderModel("SHIPPING","PENJUALAN",false,1,"a",1,"a","a0,",1,"s","s","s","s","d","d","e","note",1,1, "unit","type",1,1, false,"s","s",1)

            });
        }

        public DyeingPrintingAreaOutputModel GetModelShippingBuyer()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, "SHIPPING", "pa", "1", false, "BUYER", "A", 1, "no", false, new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                 new DyeingPrintingAreaOutputProductionOrderModel("SHIPPING","PENJUALAN",false,1,"a",1,"a","a0,",1,"s","s","s","s","d","d","e",null,1,1, "unit","type",1,1, false,"s","s",1)

            });
        }

        public DyeingPrintingAreaOutputModel GetModelShippingBuyerAfter()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, "SHIPPING", "pa", "1", false, "BUYER", "A", 1, "no", false, new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                 new DyeingPrintingAreaOutputProductionOrderModel("SHIPPING","PENJUALAN",false,1,"a",1,"a","a0,",1,"s","s","s","s","d","d","e","note",1,1, "unit","type",1,1, false,"s","s",1)

            });
        }

        public DyeingPrintingAreaOutputModel GetModelForUpdateAfter()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, "IM", "pa", "1", false, "TRANSIT", "A", new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                new DyeingPrintingAreaOutputProductionOrderModel("IM","TRANSIT",false,1,"a","e",1,"rr","1","as","test","unit","color","motif","mtr", "rem","a","a",2,1,1, "type"),
                
            });
        }

        public DyeingPrintingAreaOutputModel GetModelForUpdateBefore()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, "IM", "pa", "1", false, "TRANSIT", "A", new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                new DyeingPrintingAreaOutputProductionOrderModel("IM","TRANSIT",false,1,"a","e",1,"rr","1","as","test","unit","color","motif","mtr", "rem","a","a",2,1,1, "type"),

            });
        }

        public DyeingPrintingAreaOutputModel GetModelForUpdateAfter2()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, "IM", "pa", "1", false, "TRANSIT", "A", new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                new DyeingPrintingAreaOutputProductionOrderModel("IM","TRANSIT",false,1,"a","e",1,"rr","1","as","test","unit","color","motif","mtr", "rem","a","a",2,1,1, "type"),

            });
        }

        public DyeingPrintingAreaOutputModel GetEmptyModelBefore()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, null, null, null, false, null, null, new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                new DyeingPrintingAreaOutputProductionOrderModel(null,null,false,1,"a","e",1,"rr","1","as","test","unit","color","motif","mtr", "rem","a","a",2,1,0, null)
            });
        }

        public override DyeingPrintingAreaOutputModel GetEmptyModel()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, null, null, null, true, null, null, new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                new DyeingPrintingAreaOutputProductionOrderModel(null,null,true,0,null,null,0,null,null,null,null,null,null,null,null,null,null,null,1,1,0),
                new DyeingPrintingAreaOutputProductionOrderModel(null,null,true,1,"a","e",1,"rr","1","as","test","unit","color","motif","mtr", "rem","a","a",2,1,0, null),
                new DyeingPrintingAreaOutputProductionOrderModel(null,null,true,0,null,0,null,null,1,null,null,null,null,null,null,null,null,0,1, null, null, 0,0, true,null,null,0),
                new DyeingPrintingAreaOutputProductionOrderModel(null,null,true,0,null,null,null,null,null,null,null,null,null,null,null,1,null,null,1,null,1,null,0),
                new DyeingPrintingAreaOutputProductionOrderModel(null,null,null, 0, 0)

            });
        }

        public DyeingPrintingAreaOutputModel GetWithDOModel()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, "IM", "pa", "1", false, "TRANSIT", "A", 1, "Np", false, new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                new DyeingPrintingAreaOutputProductionOrderModel("IM","TRANSIT",false,1,"a","e",1,"rr","1","as","test","unit","color","motif","mtr", "rem","a","a",2, 1,1),
                new DyeingPrintingAreaOutputProductionOrderModel("IM","TRANSIT",false,1,"a","e",1,"rr","1","as","test","unit","color","motif","mtr", "rem","a","a",2,1,1, "type"),
                new DyeingPrintingAreaOutputProductionOrderModel("IM","TRANSIT",false,1,"a",1,"a","a0,",1,"s","s","s","s","d","d","e","note",1,1, "unit","type,",1,1, false,"s","s",1),
                new DyeingPrintingAreaOutputProductionOrderModel("IM","TRANSIT",false,1,"a","e","rr","1","as","test","unit","color","motif","mtr", "rem",10,"a","test",1,"PACK",10,"Pack",1),
                new DyeingPrintingAreaOutputProductionOrderModel("SAMBUNGAN","5-11","KRG", 15, 10)

            });
        }

        public DyeingPrintingAreaOutputModel GetEmptyWithDOModel()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, null, null, null, true, null, null, 0, null, true, new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                new DyeingPrintingAreaOutputProductionOrderModel(null,null,true,0,null,null,0,null,null,null,null,null,null,null,null,null,null,null,1,1,0),
                new DyeingPrintingAreaOutputProductionOrderModel(null,null,true,1,"a","e",1,"rr","1","as","test","unit","color","motif","mtr", "rem","a","a",2,1,0, null),
                new DyeingPrintingAreaOutputProductionOrderModel(null,null,true,0,null,0,null,null,1,null,null,null,null,null,null,null,null,0,1,null,null,0,0, true,null,null,0),
                new DyeingPrintingAreaOutputProductionOrderModel(null,null,true,0,null,null,null,null,null,null,null,null,null,null,null,1,null,null,1,null,1,null,0),
                new DyeingPrintingAreaOutputProductionOrderModel(null,null,null, 0, 0)

            });
        }
    }
}
