﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KWFCI.Models
{
    public class StaffProfile : Person
    {
        public StaffUser User { get; set; }
        public string Role { get; set; }
        public int StaffProfileID { get; set; }
        public List<Interaction> Interactions { get; set; }
        public List<KWTask> Tasks { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public StaffProfile()
        {
            Interactions = new List<Interaction>();
            Tasks = new List<KWTask>();
        }
    }
}
