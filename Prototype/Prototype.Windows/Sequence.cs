﻿using System.Collections.Generic;
using System.Xml.Serialization;

namespace Prototype
{
    public class Sequence
    {
        [XmlAttribute("Name")]
        public string name;
        [XmlAttribute("Characters")]
        public string characters;
        public List<string> charsString;

        public Sequence(string n, string c)
        {
            name = n;
            characters = c;
        }
    }
}