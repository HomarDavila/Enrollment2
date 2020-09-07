using System;
using System.Collections.Generic;
using System.Text;

namespace Common.HttpHelpers
{
    public interface ISyncMobile
    {
        bool NeedsSync { get; set; }
        string SynchronizedBy { get; set; }
        System.DateTime? LastSync { get; set; }
        System.DateTime? ExpireSyncOn { get; set; }
    }
}
