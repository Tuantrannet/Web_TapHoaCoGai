﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Room
    {
        [Key]   
        public int Id { get; set; }

        public int Customerid { get; set; }

    }
}
