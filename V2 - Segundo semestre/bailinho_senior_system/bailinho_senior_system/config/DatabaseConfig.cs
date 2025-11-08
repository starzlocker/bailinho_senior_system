using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bailinho_senior_system.config
{
    public static class DatabaseConfig
    {
        public static string ConnectionString => @"server=127.0.0.1;uid=root;pwd=ifsp;database=bailinhoseniorsystem;ConnectionTimeout=30";
    }
}
