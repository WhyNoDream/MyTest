using System;
using System.Collections.Generic;
using System.Text;

namespace ConsulUnit.Entitys
{
    public class ConsulConfigurationEnitiy
    {
        public int LockIndex { get; set; }
        public string Key { get; set; }
        public int Flags { get; set; }
        public string Value { get; set; }
        public long CreateIndex { get; set; }
        public long ModifyIndex { get; set; }
    }
}
