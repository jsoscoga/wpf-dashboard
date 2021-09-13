using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace dashboard.Model
{
    [Table("slaveData")]
    public class SlaveData
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("slaveId")]
        public int SlaveId { get; set; }

        [Column("datStart")]
        public DateTime DatStart { get; set; }

        [Column("datEnd")]
        public DateTime DatEnd { get; set; }

        [Column("duration")]
        public int Duration { get; set; }

        [Column("channel1")]
        public int Channel1 { get; set; }

        [Column("channel2")]
        public int Channel2 { get; set; }

        [Column("channel3")]
        public int Channel3 { get; set; }

        [Column("channel4")]
        public int Channel4 { get; set; }

        [Column("error")]
        public int Error { get; set; }
    }
}
