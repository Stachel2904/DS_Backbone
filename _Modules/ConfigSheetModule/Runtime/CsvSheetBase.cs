using System;
using System.Collections.Generic;
using System.Linq;
using DivineSkies.Tools.Extensions;

namespace DivineSkies.Modules.Config
{
    public abstract class CsvSheetBase<TRow> : ConfigSheetBase
    {
        private readonly List<TRow> _rows = new();
        protected List<TRow> Rows => _rows;
        protected string[] _header;

        internal sealed override ConfigDataType _dataType => ConfigDataType.Csv;

        protected sealed override void DeserializeFileContent(string content)
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
                    _rows.Add(DeserializeRow(rowContent, i));
                }
            }
        }

        protected abstract TRow DeserializeRow(string[] serializedRow, int rowIndex);

        public List<TRow> GetRows()
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
            return Rows.TryFind(match, out result);
        }
    }
}
