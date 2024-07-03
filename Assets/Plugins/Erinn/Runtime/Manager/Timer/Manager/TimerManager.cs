//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Timer Manager
    /// </summary>
    internal sealed partial class TimerManager : ModuleSingleton, ITimerManager
    {
        /// <summary>
        ///     Priority
        /// </summary>
        public override int Priority => 3;

        /// <summary>
        ///     Call upon deletion
        /// </summary>
        public override void OnDispose() => TimerLinkedList.Dispose();

        /// <summary>
        ///     UpdateWhen calling
        /// </summary>
        public static void OnUpdate()
        {
            TimerLinkedList.Poll(Timestamp);
            UnscaledTimerLinkedList.Poll(UnscaledTimestamp);
        }
    }
}