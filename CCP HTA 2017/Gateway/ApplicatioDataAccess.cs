namespace CCP_HTA_2017.Gateway
{
    public class ApplicationDataAccess
    {
        /* Connections and commands to retrive and save data */
        public Gateway.SqliteDataSetTableAdapters.usuarioTableAdapter usuarioTableAdapter = new Gateway.SqliteDataSetTableAdapters.usuarioTableAdapter();
        public Gateway.SqliteDataSetTableAdapters.QueriesTableAdapter queriesTableAdapter = new Gateway.SqliteDataSetTableAdapters.QueriesTableAdapter();
        public Gateway.SqliteDataSetTableAdapters.pacienteTableAdapter pacienteTableAdapter = new Gateway.SqliteDataSetTableAdapters.pacienteTableAdapter();
        public Gateway.SqliteDataSetTableAdapters.registroTableAdapter registroTableAdapter = new Gateway.SqliteDataSetTableAdapters.registroTableAdapter();
        /*  Strongly type in-memory cahe of data */
        public Gateway.SqliteDataSet dataSet = new Gateway.SqliteDataSet();
    }
}
