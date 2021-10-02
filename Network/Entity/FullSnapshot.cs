using BitManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FullSnapshot : IWritable
{
    public uint SequenceNumber {  get; private set; }


    // is a class

    // slowly write to each part,
    // then Assemble at end with BitWriter

    public FullSnapshot(uint sequenceNumber)
    {

    }

    public void WriteOut(BitWriter bitWriter)
    {
        
    }
}
