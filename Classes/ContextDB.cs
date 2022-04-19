using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM04
{
    internal class ContextDB
    {
        private static PM04.TheBestDBEntities _context { get; set; }

        public static PM04.TheBestDBEntities GetContext()
        {
            if (_context == null)
                _context = new PM04.TheBestDBEntities();
            return _context;
        }
    }
}
