using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace dashboard.Model
{
    public class Order
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("slaveId")]
        public int SlaveId { get; set; }

        [Column("orderState")]
        public int OrderState { get; set; }

        [Column("piecesPerSignal")]
        public int PiecesPerSignal { get; set; }

        [Column("targetAmount")]
        public int TargetAmount { get; set; }

        [Column("targetSetupTime")]
        public int TargetSetupTime { get; set; }

        [Column("timePerSignal")]
        public decimal TimePerSignal { get; set; }

        [Column("realSignals")]
        public decimal RealSignals { get; set; }

        [Column("reference")]
        public string Reference { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("targetBeginTime")]
        public DateTime TargetBeginTime { get; set; }

        [Column("targetEndTime")]
        public DateTime TargetEndTime { get; set; }

        [Column("realBeginTime")]
        public DateTime RealBeginTime { get; set; }

        [Column("realEndTime")]
        public DateTime RealEndTime { get; set; }

        [Column("autoStopTime")]
        public DateTime AutoStopTime { get; set; }
    }
}
