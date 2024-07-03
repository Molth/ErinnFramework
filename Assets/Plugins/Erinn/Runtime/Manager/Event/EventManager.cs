//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    internal sealed partial class EventManager : ModuleSingleton, IEventManager
    {
        public override int Priority => 0;

        public override void OnDispose()
        {
            ClearListeners();
            UnregisterAll();
            Shutdown();
        }
    }
}