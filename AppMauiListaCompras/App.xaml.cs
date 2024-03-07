using AppMauiListaCompras.Helpers;

namespace AppMauiListaCompras

{
    public partial class App : Application
    {
        static SQliteDatabaseHelper _db;
       
        public static SQliteDatabaseHelper Db
        {
            get
            {
                if (_db == null)
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData
                        ),"banco_sqlite_compras.db3"
                        );

                    _db = new SQliteDatabaseHelper(path);
                }
                return _db;
            }
        }
        
    }

}
