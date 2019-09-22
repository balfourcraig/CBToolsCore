﻿using System;

namespace CBTools_Core
{
    [Flags]
    enum Days : byte
    {
        None = 0,
        Monday = 0b_0000_0001,
        Tuesday = 0b_0000_0010,
        Wednesday = 0b_0000_0100,
        Thursday = 0b_0000_1000,
        Friday = 0b_0001_0000,
        Saturday = 0b_0010_0000,
        Sunday = 0b_0100_0000,
        Weekend = Saturday | Sunday,
        Weekdays = Monday | Tuesday | Wednesday | Thursday | Friday,
        Week = Weekdays | Weekend,
    }
}
