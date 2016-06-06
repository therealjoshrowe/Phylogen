using System.Collections.Generic;
using System;

namespace phylogen
{
    class TaxaBlock
    {
        private int dimensions;
        private List<string> taxLabels;
        public int Dimensions
        {
            get; set;
        }

        public List<string> TaxLabels
        {
            get; set;
        }

        public TaxaBlock()
        {
            Dimensions = 0;
            TaxLabels = new List<string>();
        }
    }
    class CharactersBlock
    {
        private List<string> dataMatrix;
        private int dimensions;
        private string dataType;
        private bool respectCase;
        private char gap;
        private char missing;
        private List<char> symbols;
        private bool interleave;
        private int interleaveLength;

        public List<string> DataMatrix
        {
            get; set;
        }

        public int Dimensions
        {
            get; set;
        }

        public string DataType
        {
            get; set;
        }

        public bool RespectCase
        {
            get; set;
        }

        public char Gap
        {
            get; set;
        }

        public char Missing
        {
            get; set;
        }

        public List<char> Symbols
        {
            get; set;
        }

        public bool Interleave
        {
            get; set;
        }

        public int InterleaveLength
        {
            get; set;
        }

        public CharactersBlock()
        {
            DataMatrix = new List<string>();
            Dimensions = 0;
            RespectCase = false;
            Gap = '\0';
            Missing = '\0';
            Symbols = new List<char>();
            Interleave = false;
            interleaveLength = 0;
        }
    }
    class NexusObeject
    {
        private TaxaBlock t;
        private CharactersBlock c;

        public TaxaBlock T
        {
            get; set;
        }

        public CharactersBlock C
        {
            get; set;
        }

        public NexusObeject()
        {
            T = new TaxaBlock();
            C = new CharactersBlock();
        }
    }
}