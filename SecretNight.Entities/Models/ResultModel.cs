﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNight.Entities.Models
{
    // Definimos ResultModel como un tipo genérico
    public class ResultModel<T>
    {
        public bool isSuccess { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
    }
}
