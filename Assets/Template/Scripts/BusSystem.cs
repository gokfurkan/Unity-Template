using System;

namespace Template.Scripts
{
    public static class BusSystem
    {
        //Economy
        public static Action OnSetMoneys;
        public static void CallSetMoneys() { OnSetMoneys?.Invoke(); }
        
        //Game Manager
        public static Action OnLevelStart;
        public static void CallLevelStart() { OnLevelStart?.Invoke(); }
     
        public static Action <bool> OnLevelEnd;
        public static void CallLevelEnd(bool win) { OnLevelEnd?.Invoke(win); }
     
        public static Action OnLevelLoad;
        public static void CallLevelLoad() { OnLevelLoad?.Invoke(); }
        
        //Input
        public static Action OnMouseClickDown;
        public static void CallMouseClickDown() { OnMouseClickDown?.Invoke(); }
        
        public static Action OnMouseClicking;
        public static void CallMouseClicking() { OnMouseClicking?.Invoke(); }
        
        public static Action OnMouseClickUp;
        public static void CallMouseClickUp() { OnMouseClickUp?.Invoke(); }
        
        //Shop
        public static Action OnChangeShopPanelPage;
        public static void CallChangeShopPanelPage() { OnChangeShopPanelPage?.Invoke(); }

        public static Action OnSetPlayerSkin;
        public static void CallSetPlayerSkin() { OnSetPlayerSkin?.Invoke(); }
    }
}