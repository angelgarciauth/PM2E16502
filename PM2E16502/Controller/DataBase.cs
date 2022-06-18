using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PM2E16502.Models;
using SQLite;

namespace PM022PP0122.Controller
{
    public class DataBase
    {
        readonly SQLiteAsyncConnection dbase;
        /*Constructor de clase*/
        
        public DataBase(string dbpath) {
            dbase = new SQLiteAsyncConnection(dbpath);
            //creacion de las tablas de la base de datos
            dbase.CreateTableAsync<Sitios>(); //creando tabla de empleados

        }

        #region Operaciones

        //CRUD -CREATE  -READ  -UPDATE  -DELETE


        //Create
        public Task<int> SiteSave(Sitios site)
        {
            if(site.id != 0)
            {
                return dbase.UpdateAsync(site);
            }
            else
            {
                return dbase.InsertAsync(site);
            }
        }

        //Read
        public Task<List<Sitios>> getListSite()
        {
            return dbase.Table<Sitios>().ToListAsync();
        }

        //Read v2
        public Task<Sitios> getSite(int pid)
        {
            return dbase.Table<Sitios>()
                .Where(i => i.id == pid)
                .FirstOrDefaultAsync();
        }

        //Delete

        public Task<int> DeleteSite(Sitios site)
        {
            return dbase.DeleteAsync(site);
        }

        #endregion Operaciones

      
    }
}
