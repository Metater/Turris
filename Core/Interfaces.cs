using BitManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IWritable
{
    public void WriteOut(BitWriter bitWriter);
}