using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TestSolition.Entity
{
    internal class Model : DbContext
    {
        public DbSet<sample01> sample01s { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(GetConnectionString());
        }

        private string GetConnectionString()
        {
            String DB_SERVER = "localhost";
            String DB_PORT = "5432";
            String DB_CATAROG = "postgres";
            String DB_USER = "postgres";
            String DB_PASSWORD = "ac-12457";
            var sb = new StringBuilder();
            sb.Append($"Server={DB_SERVER};")
            .Append($"Port={DB_PORT};")
            .Append($"Database={DB_CATAROG};")
            .Append($"User Id={DB_USER};")
            .Append($"Password={DB_PASSWORD};ApplicationName=TAS-CL;Enlist=True;");
            return sb.ToString();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<sample01>().HasKey(r => new { r.name });
        }
    }
    public class IQueryableToDataTable
    {
        //
        // IQueryable で取得したデータ から DataTable に変換するクラス.
        //
        private DataTable table = null;

        public IQueryableToDataTable(string table_name = null)
        {
            // コンストラクタ.
            table = new DataTable(table_name);
        }

        /// <summary>
        /// クエリデータをテーブルに変換する.
        /// </summary>
        public void setDataRowAll(IQueryable<object> tgt)
        {
            // 取得したデータを全て展開する.
            foreach (var row in tgt)
            {
                setDataRow(row);
            }
        }

        /// <summary>
        /// テーブルの取得.
        /// </summary>
        public DataTable getDataTable()
        {
            return table;
        }

        /// <summary>
        /// DataRowのデータ取得.
        /// </summary>
        public DataRow getDataRow(int index)
        {
            int ret_idx = index;
            // インデックスがテーブルの要素数より,
            // 多ければ要素数末尾を設定.
            if (ret_idx >= table.Rows.Count)
            {
                ret_idx = table.Rows.Count - 1;
            }
            return table.Rows[ret_idx];
        }

        /// <summary>
        /// DataRowのデータ追加.
        /// </summary>
        public void setDataRow(object tgt)
        {
            // データの追加処理.
            if (table.Columns.Count == 0)
            {
                // カラム設定されていない場合の処理.
                setColumns(tgt);
            }

            DataRow row = table.NewRow();
            foreach (PropertyInfo prop in tgt.GetType().GetProperties())
            {
                // インスタンスのプロパティをチェック.
                var property = tgt.GetType().GetProperty(prop.Name);
                var value = property?.GetValue(tgt);

                // カラムにデータを登録.
                row[prop.Name] = value ?? DBNull.Value;
            }
            // データ追加.
            table.Rows.Add(row);
        }

        /// <summary>
        /// DataRowのデータ削除.
        /// </summary>
        public void delDataRow(int index)
        {
            int ret_idx = index;
            // インデックスがテーブルの要素数より,
            // 多ければ要素数末尾を設定.
            if (ret_idx >= table.Rows.Count)
            {
                ret_idx = table.Rows.Count - 1;
            }
            // 行を削除する（DataRow）
            DataRow dr = table.Rows[ret_idx];
            table.Rows.Remove(dr);
        }

        /// <summary>
        /// テーブルのデータクリア.
        /// </summary>
        public void clearDataTable()
        {
            // DataTableのデータをクリア.
            table.Clear();
        }

        /// <summary>
        /// インスタンスの破棄.
        /// </summary>
        public void Dispose()
        {
            if (table != null)
            {
                // DataTableのデータをクリア.
                clearDataTable();
                // テーブルのインスタンスを破棄.
                table.Dispose();
            }
        }

        /// <summary>
        /// テーブルのカラム追加.
        /// </summary>
        private void setColumns(object tgt)
        {
            foreach (PropertyInfo prop in tgt.GetType().GetProperties())
            {
                // プロパティの型を設定.
                Type type = prop.PropertyType;
                if (Nullable.GetUnderlyingType(prop.PropertyType) != null)
                    type = Nullable.GetUnderlyingType(prop.PropertyType);

                // カラム追加.
                table.Columns.Add(prop.Name, type);
            }
        }
    }
}
