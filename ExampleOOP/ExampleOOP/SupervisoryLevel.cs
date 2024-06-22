using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleOOP
{
    public enum SupervisoryLevel
    {
        //enums are strings that represent an integer value
        //by default enum integer values start at 0 and increment by 1
        //you can assign your own values
        Entry, //0
        TeamMember, //1
        TeamLeader, //2
        Supervisor, //3
        DepartmentHead, //4
        Owner //5
    }
}
