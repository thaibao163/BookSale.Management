﻿using BookSale.Management.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Application.DTOs
{
    public class ResponseModel
    {
        public bool Status { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public ActionType Action { get; set; } = ActionType.Get;

        public int? RemainingSeconds { get; set; }
    }
}
