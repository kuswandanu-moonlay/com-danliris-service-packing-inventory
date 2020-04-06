﻿using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.FabricQualityControl
{
    public class FabricQualityControlModel : StandardEntity
    {
        public FabricQualityControlModel()
        {

        }

        public FabricQualityControlModel(string code, DateTimeOffset dateIm, string group, bool isUsed, int dyeingPrintingAreaMovementId, string dyeingPrintingAreaMovementBonNo,
            string productionOrderNo, string machineNoIm, string operatorIm, double pointLimit, double pointSystem, ICollection<FabricGradeTestModel> fabricGradeTests)
        {
            Code = code;
            DateIm = dateIm;
            Group = group;
            IsUsed = isUsed;
            DyeingPrintingAreaMovementId = dyeingPrintingAreaMovementId;
            DyeingPrintingAreaMovementBonNo = dyeingPrintingAreaMovementBonNo;
            ProductionOrderNo = productionOrderNo;
            MachineNoIm = machineNoIm;
            OperatorIm = operatorIm;
            PointLimit = pointLimit;
            PointSystem = pointSystem;
            FabricGradeTests = fabricGradeTests;
        }

        public string UId { get; private set; }
        public string Code { get; private set; }
        public DateTimeOffset DateIm { get; private set; }
        public ICollection<FabricGradeTestModel> FabricGradeTests { get; private set; }
        public string Group { get; private set; }
        public bool IsUsed { get; private set; }
        public int DyeingPrintingAreaMovementId { get; private set; }
        public string DyeingPrintingAreaMovementBonNo { get; private set; }
        public string ProductionOrderNo { get; private set; }
        public string MachineNoIm { get; private set; }
        public string OperatorIm { get; private set; }
        public double PointLimit { get; private set; }
        public double PointSystem { get; private set; }

        public void SetDateIm(DateTimeOffset newDateIm, string user, string agent)
        {
            if(newDateIm != DateIm)
            {
                DateIm = newDateIm;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetGroup(string newGroup, string user, string agent)
        {
            if(newGroup != Group)
            {
                Group = newGroup;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetIsUsed(bool newIsUsed, string user, string agent)
        {
            if (newIsUsed != IsUsed)
            {
                IsUsed = newIsUsed;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetDyeingPrintingAreaMovement(int newDyeingPrintingAreaMovementId, string newDyeingPrintingAreaMovementBonNo, 
            string newProductionOrderNo, string user, string agent)
        {
            if(newDyeingPrintingAreaMovementId != DyeingPrintingAreaMovementId)
            {
                DyeingPrintingAreaMovementId = newDyeingPrintingAreaMovementId;
                this.FlagForUpdate(user, agent);
            }

            if(newDyeingPrintingAreaMovementBonNo!= DyeingPrintingAreaMovementBonNo)
            {
                DyeingPrintingAreaMovementBonNo = newDyeingPrintingAreaMovementBonNo;
                this.FlagForUpdate(user, agent);
            }

            if (newProductionOrderNo != ProductionOrderNo)
            {
                ProductionOrderNo = newProductionOrderNo;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetMachineNoIm(string newMachineNoIm, string user, string agent)
        {
            if(newMachineNoIm != MachineNoIm)
            {
                MachineNoIm = newMachineNoIm;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetOperatorIm(string newOperatorIm, string user, string agent)
        {
            if (newOperatorIm != OperatorIm)
            {
                OperatorIm = newOperatorIm;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetPointLimit(double newPointLimit, string user, string agent)
        {
            if (newPointLimit != PointLimit)
            {
                PointLimit = newPointLimit;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetPointSystem(double newPointSystem, string user, string agent)
        {
            if (newPointSystem != PointSystem)
            {
                PointSystem = newPointSystem;
                this.FlagForUpdate(user, agent);
            }
        }
    }
}
