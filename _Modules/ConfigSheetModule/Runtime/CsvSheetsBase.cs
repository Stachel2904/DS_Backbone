using System;
using System.Collections.Generic;
using System.Linq;
using DivineSkies.Tools.Extensions;

namespace DivineSkies.Modules.Config
{
    public abstract class CsvSheetsBase<TRow> : ConfigSheetsBase
    {
        private readonly Dictionary<string, TRow> _rows = new();
        protected Dictionary<string, TRow> Rows => _rows;
        protected string[] _header;

        internal sealed override ConfigDataType _dataType => ConfigDataType.Csv;

        protected sealed override void DeserializeFileContent(string fileId, string content)
        {
            string[] lines = content.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                if (i == 0)// Skip first row because its the header
                {
                    _header = lines[i].Split(';').Skip(1).ToArray();
                    continue;
                }

                string[] rowContent = lines[i].Split(';');
                if (rowContent.Length >= 2)
                {
                    _rows.Add(fileId, DeserializeRow(fileId, rowContent, i));
                }
            }
        }

        protected abstract TRow DeserializeRow(string fileId, string[] serializedRow, int rowIndex);

        public Dictionary<string, TRow> GetRows()
        {
            return Rows;
        }

        public TRow GetRow(Func<TRow, bool> match)
        {
            if (!TryGetRow(match, out var result))
                this.PrintError("Failed to get row in " + GetType());

            return result;
        }

        public bool TryGetRow(Func<TRow, bool> match, out TRow result)
        {
            return Rows.Values.ToArray().TryFind(match, out result);
        }
    }
}
