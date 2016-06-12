﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Prototype
{
    public class NexusFile
    {
        [XmlElement("TaxaBlock")]
        public TaxaBlock T { get; set; }
        [XmlElement("CharactersBlocks")]
        public CharactersBlock C { get; set; }
    }
}
