using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace University.NetStandart.Core.Models
{
    public class Students:Entity
    {
        //[DisplayAttribute(Name = "Имя Студента")]
        public string Name { get; set; }
        
       // [DisplayAttribute(Name = "Курс")]
        public int Course { get; set; }
        
       // [DisplayAttribute(Name = "Пол")]
        public string Sex { get; set; }
        
       // [DisplayAttribute(Name = "Средний балл")]
        public int Average { get; set; }
        

    }
}
