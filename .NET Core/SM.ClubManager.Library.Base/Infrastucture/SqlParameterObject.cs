using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.Library.Base.Infrastructure
{
    public class SqlParameterObject
    {
        string _name;
        object _value;
        int? _size;
        ParameterDirection _direction;
        DbType _dbType;
        SqlDbType _SqlDbType;

        public string Name { get { return _name; } set { _name = value; } }

        public DbType DbType { get { return _dbType; } set { _dbType = value; } }

        public SqlDbType SqlDbType { get { return _SqlDbType; } set { _SqlDbType = value; } }

        public object Value { get { return _value; } set { _value = value; } }

        public int? Size { get { return _size; } set { _size = value; } }

        public ParameterDirection Direction { get { return _direction; } set { _direction = value; } }

        public SqlParameterObject(string Name, DbType DbType, SqlDbType sqlDbType, object Value = null, int? Size = null, ParameterDirection Direction = ParameterDirection.InputOutput)
        {
            _name = Name;
            _value = Value;
            _size = Size;
            _direction = Direction;
            _dbType = DbType;
            _SqlDbType = sqlDbType;
        }
    }
}
