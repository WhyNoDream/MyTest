using System;
using System.Collections.Generic;
using System.Text;

namespace ProductDomain.ValueObjects
{
    public class Specification
    {
        /// <summary>
        /// 属性名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        public string Value { get; set; }
    }
}
