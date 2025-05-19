using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace CustomQuickChats
{
    [BepInPlugin("customquickchats.killacon", "Custom Quick Chats", "0.2")]
    public class Plugin : BaseUnityPlugin
    {
        public static ConfigEntry<bool> Enabled;

        public static ConfigEntry<string> OverHereMsg;
        public static ConfigEntry<string> HurryUpMsg;
        public static ConfigEntry<string> NoMsg;
        public static ConfigEntry<string> TooEasyMsg;
        public static ConfigEntry<string> ThinkingMsg;
        public static ConfigEntry<string> OMGMsg;
        public static ConfigEntry<string> YeahMsg;
        public static ConfigEntry<string> GoodIdeaMsg;
        public static ConfigEntry<string> WellDoneMsg;
        public static ConfigEntry<string> SoCloseMsg;
        public static ConfigEntry<string> NotThatOneMsg;
        public static ConfigEntry<string> ImpossibleMsg;
        public static ConfigEntry<string> HigherMsg;
        public static ConfigEntry<string> LowerMsg;
        public static ConfigEntry<string> GlueHereMsg;
        public static ConfigEntry<string> BombMsg;
        public static ConfigEntry<string> RematchMsg;
        public static ConfigEntry<string> WaitingForAFriendMsg;
        public static ConfigEntry<string> BeRightBackMsg;
        public static ConfigEntry<string> OkayMsg;
        public static ConfigEntry<string> HelloMsg;
        public static ConfigEntry<string> GoodbyeMsg;
        public static ConfigEntry<string> GoodGameMsg;
        public static ConfigEntry<string> WellPlayedMsg;
        public static ConfigEntry<string> NotThereMsg;
        public static ConfigEntry<string> MoreTrapsMsg;
        public static ConfigEntry<string> NiceOutfitMsg;
        public static ConfigEntry<string> YesMsg;
        public static ConfigEntry<string> UhOhMsg;
        public static ConfigEntry<string> HahahaMsg;
        public static ConfigEntry<string> OuchMsg;
        public static ConfigEntry<string> NooooMsg;
        public static ConfigEntry<string> AmazingMsg;
        public static ConfigEntry<string> WowMsg;
        public static ConfigEntry<string> GreatRunMsg;
        public static ConfigEntry<string> ThanksMsg;
        public static ConfigEntry<string> WhoopsMsg;
        public static ConfigEntry<string> SorryMsg;
        public static ConfigEntry<string> NoProblemMsg;

        public static ConfigEntry<int> CustomNetID;

        internal ManualLogSource mls;
        private static Plugin Instance;

        private void Awake()
        {
            mls = BepInEx.Logging.Logger.CreateLogSource("CustomQuickChats");
            mls.LogInfo("killacon da goat frfr :speaking_head: :100:");
            
            if (Instance == null)
            {
                Instance = this;
            }
            Enabled = Config.Bind("General", "Enabled", true, "Enable or disable custom quick chats");
            //CustomNetID = Config.Bind("General", "CustomNetID", 0, "Custom net ID for quick chats. Set to 0 to use default net ID.");


            OverHereMsg = Config.Bind("QuickChats", "OverHere", "Over here!", "");
            TooEasyMsg = Config.Bind("QuickChats", "TooEasy", "Too easy.", "");
            ThinkingMsg = Config.Bind("QuickChats", "Thinking", "Thinking...", "");
            ImpossibleMsg = Config.Bind("QuickChats", "Impossible", "Impossible!", "");
            RematchMsg = Config.Bind("QuickChats", "Rematch", "Rematch?", "");
            WaitingForAFriendMsg = Config.Bind("QuickChats", "WaitingForAFriend", "Waiting for a friend.", "");
            BeRightBackMsg = Config.Bind("QuickChats", "BeRightBack", "Be right back.", "");

            OMGMsg = Config.Bind("QuickChats", "OMG", "OMG!", "");
            WowMsg = Config.Bind("QuickChats", "Wow", "Wow!", "");
            HahahaMsg = Config.Bind("QuickChats", "Hahaha", "Hahaha!", "");
            NooooMsg = Config.Bind("QuickChats", "Noooo", "Noooo!", "");
            OkayMsg = Config.Bind("QuickChats", "Okay", "Okay!", "");
            NotThatOneMsg = Config.Bind("QuickChats", "NotThatOne", "Not that one!", "");
            HurryUpMsg = Config.Bind("QuickChats", "HurryUp", "Hurry up!", ""); 

            //explitive here
            WhoopsMsg = Config.Bind("QuickChats", "Whoops", "Whoops...", ""); 
            SorryMsg = Config.Bind("QuickChats", "Sorry", "Sorry!", "");
            HelloMsg = Config.Bind("QuickChats", "Hello", "Hello!", ""); 
            GoodbyeMsg = Config.Bind("QuickChats", "Goodbye", "Goodbye", "");
            NiceOutfitMsg = Config.Bind("QuickChats", "NiceOutfit", "Nice Outfit!", "");
            NoProblemMsg = Config.Bind("QuickChats", "NoProblem", "No problem!", "");

            ThanksMsg = Config.Bind("QuickChats", "Thanks", "Thanks!", "");
            WellDoneMsg = Config.Bind("QuickChats", "WellDone", "Well done!", "");
            GoodIdeaMsg = Config.Bind("QuickChats", "GoodIdea", "Good idea!", "");
            GoodGameMsg = Config.Bind("QuickChats", "GoodGame", "Good game!", ""); 
            WellPlayedMsg = Config.Bind("QuickChats", "WellPlayed", "Well played!", ""); 
            SoCloseMsg = Config.Bind("QuickChats", "SoClose", "So close!", "");

            NotThereMsg = Config.Bind("QuickChats", "NotThere", "Not there!", "");
            MoreTrapsMsg = Config.Bind("QuickChats", "MoreTraps", "More traps!", "");
            GlueHereMsg = Config.Bind("QuickChats", "GlueHere", "Glue here!", "");
            BombMsg = Config.Bind("QuickChats", "Bomb", "Bomb!", "");

            //YeahMsg = Config.Bind("QuickChats", "Yeah", "Yeah!", "");
            //HigherMsg = Config.Bind("QuickChats", "Higher", "Higher!", "");
            //LowerMsg = Config.Bind("QuickChats", "Lower", "Lower!", "");
            //YesMsg = Config.Bind("QuickChats", "Yes", "Yes!", "");
            //UhOhMsg = Config.Bind("QuickChats", "UhOh", "Uh oh!", "");
            //OuchMsg = Config.Bind("QuickChats", "Ouch", "Ouch!", "");
            //AmazingMsg = Config.Bind("QuickChats", "Amazing", "Amazing!", "");
            //GreatRunMsg = Config.Bind("QuickChats", "GreatRun", "Great run!", "");
            //NoMsg = Config.Bind("QuickChats", "No", "Noooo!", "");

            Harmony harmony = new Harmony("customquickchats.killacon");
            harmony.PatchAll();
        }
    }
}