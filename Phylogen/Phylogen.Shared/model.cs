using System.Collections.Generic;
using System;

namespace phylogen
{
    public class TaxaBlock
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
    public class CharactersBlock
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
        private List<String> equates;

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
            get;
        }

        public bool Interleave
        {
            get; set;
        }

        public int InterleaveLength
        {
            get; set;
        }

        public List<string> Equates
        {
            get;
        }

        public CharactersBlock()
        {
            DataType = "";
            DataMatrix = new List<string>();
            Dimensions = 0;
            RespectCase = false;
            Gap = '\0';
            Missing = '\0';
            Symbols = new List<char>();
            Interleave = false;
            interleaveLength = 0;
            Equates = new List<string>();
        }
    }
    public class NexusObject
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

        public NexusObject()
        {
            T = new TaxaBlock();
            C = new CharactersBlock();
        }
    }
}