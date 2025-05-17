using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Exceptions
{
    public class DataAccessException: Exception
    {
        public DataAccessException(Exception ex,string custemMessage ,ILogger logger)
        {
            logger.LogError($"main exception {ex.Message} devloper custem exception " +
                $"{custemMessage}");           
        }
    }
}
