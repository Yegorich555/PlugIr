using System;
using System.Windows.Forms;

namespace PlugIR
{
    public class DataGridViewExt : DataGridView
    {
        public DataGridViewCell this[DataGridViewColumn column, int rowIndex]
        {
            get
            {
                return this[column.Index, rowIndex];
            }
        }

        bool cellValueChangeLock;
        protected override void OnCellValueChanged(DataGridViewCellEventArgs e)
        {
            if (!cellValueChangeLock)
                base.OnCellValueChanged(e);
        }

        protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
        {
            base.OnRowsAdded(e);
            cellValueChangeLock = true;
            Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
            cellValueChangeLock = false;
        }

        protected override void OnRowsRemoved(DataGridViewRowsRemovedEventArgs e)
        {
            base.OnRowsRemoved(e);
            cellValueChangeLock = true;
            for (int i = 0; i < RowCount; ++i)
                Rows[i].HeaderCell.Value = (i + 1).ToString();
            cellValueChangeLock = false;
        }

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            base.OnColumnAdded(e);
            if (e.Column.GetType() == typeof(DataGridViewCheckBoxColumn))
                e.Column.ReadOnly = true;
        }

        protected override void OnCellClick(DataGridViewCellEventArgs e)
        {
            base.OnCellClick(e);
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;
            if (Columns[e.ColumnIndex].GetType() == typeof(DataGridViewCheckBoxColumn))
                this[e.ColumnIndex, e.RowIndex].Value = !Convert.ToBoolean(this[e.ColumnIndex, e.RowIndex].Value);
        }

        public DataGridViewRow LastRow
        {
            get
            {
                return Rows[RowCount - 1];
            }
        }
    }
}
