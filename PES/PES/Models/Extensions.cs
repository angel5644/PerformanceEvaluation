using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml;

namespace PES.Models
{
    public static class ExcelWorksheetExtensions
    {

        /// <summary>
        /// Function to get the column header names of an specified worksheet 
        /// </summary>
        /// <param name="sheet">Worksheet</param>
        /// <returns>List of column names</returns>
        public static IEnumerable<string> GetSheetColumnNames(this ExcelWorksheet sheet)
        {
            IEnumerable<string> columnNames;

            //Get column names from A1:AXXX [XXX=End of sheet]
            columnNames = (from cell in sheet.Cells[sheet.Dimension.Start.Row, sheet.Dimension.Start.Column, sheet.Dimension.Start.Row, sheet.Dimension.End.Column]
                           where cell.Value != null
                           select cell.Text);

            return columnNames;
        }

        /// <summary>
        /// Function to get the list of header columns of an specified worksheet 
        /// </summary>
        /// <param name="sheet">Worksheet</param>
        /// <returns>Returns a list of Columns</returns>
        public static IEnumerable<ExcelRangeBase> GetSheetColumns(this ExcelWorksheet sheet)
        {
            IEnumerable<ExcelRangeBase> columnNames;
            //Get columns from A1:AXXX [XXX=End of sheet]
            columnNames = (from cell in sheet.Cells[sheet.Dimension.Start.Row, sheet.Dimension.Start.Column, sheet.Dimension.Start.Row, sheet.Dimension.End.Column]
                           where cell.Value != null
                           select cell);

            return columnNames;
        }

        /// <summary>
        /// Function to get the rows of an specific column header
        /// </summary>
        /// <param name="headerColumn">Header column name</param>
        /// <param name="columns">List of columns of </param>
        /// <param name="sheet"></param>
        /// <returns>Returns a list of strings with the rows values</returns>
        public static IEnumerable<string> GetRowsFromHeader(this ExcelWorksheet sheet, string headerColumn, IEnumerable<ExcelRangeBase> columns)
        {
            ExcelRangeBase column = columns.Single(c => c.Text == headerColumn);
            List<string> rows = new List<string>();

            //Commented due to this method does not read empty cells
            //start in second row, start in column position, end at the end of sheet, end in column position
            //rows = from cell in sheet.Cells[column.Start.Row + 1, column.Start.Column, sheet.Dimension.End.Row, column.End.Column]
            //       where cell.Value == null
            //       select cell.Text;

            //Read rows cells of specific column
            for (int i = column.Start.Row + 1; i <= sheet.Dimension.End.Row; i++)
            {
                //Get cell
                var cell = sheet.Cells[i, column.End.Column];
                //Fill with "" string when null or empty 
                var cellValue = string.IsNullOrEmpty(cell.Text) ? "" : cell.Text;

                rows.Add(cellValue);
            }

            return rows;
        }

        /// <summary>
        /// Function to get the number of rows of the specified column name 
        /// </summary>
        /// <param name="sheet">Excel sheet</param>
        /// <param name="columnName">Column string name</param>
        /// <param name="columns">Range of columns</param>
        /// <returns>Returns the number of rows of the column</returns>
        public static int GetCountRowsFromColumn(this ExcelWorksheet sheet, string columnName, IEnumerable<ExcelRangeBase> columns)
        {
            int countRows = 0;

            //Get column rows to get the count of rows
            var rowsColumn = sheet.GetRowsFromHeader(columnName, columns);
            countRows = rowsColumn.Count();

            return countRows;
        }
    }

    public static class EnumerableExtensions
    {
        /// <summary>
        /// Do a comparision of both list and determines if source has all elements of target
        /// </summary>
        /// <typeparam name="T">List value type</typeparam>
        /// <param name="source">The list we are checking in</param>
        /// <param name="target">The list to look for in the source list</param>
        /// <returns>True if source contains all the items of target</returns>
        public static bool ContainsAll<T>(this IEnumerable<T> source, IEnumerable<T> target)
        {
            return !target.Except(source).Any();
        }
    }
}