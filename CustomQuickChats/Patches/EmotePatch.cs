using HarmonyLib;

namespace CustomQuickChats
{
    [HarmonyPatch(typeof(EmoteSystem), "EmoteConverter")]
    internal class EmotePatch
    {
        static void Postfix(ref string __result, EmoteMeanings emote)
        {
            if (!Plugin.Enabled.Value) return;

            switch (emote)
            {
                case EmoteMeanings.EMOTE_OverHere:
                    __result = Plugin.OverHereMsg.Value;
                    break;
                case EmoteMeanings.EMOTE_HurryUp:
                    __result = Plugin.HurryUpMsg.Value;
                    break;
                //case EmoteMeanings.EMOTE_No:
                //    __result = Plugin.NoMsg.Value;
                //    break;
                case EmoteMeanings.EMOTE_TooEasy:
                    __result = Plugin.TooEasyMsg.Value;
                    break;
                case EmoteMeanings.EMOTE_Thinking:
                    __result = Plugin.ThinkingMsg.Value;
                    break;
                case EmoteMeanings.EMOTE_OMG:
                    __result = Plugin.OMGMsg.Value;
                    break;
                //case EmoteMeanings.EMOTE_Yeah:
                //    __result = Plugin.YeahMsg.Value;
                //    break;
                case EmoteMeanings.EMOTE_GoodIdea:
                    __result = Plugin.GoodIdeaMsg.Value;
                    break;
                case EmoteMeanings.EMOTE_WellDone:
                    __result = Plugin.WellDoneMsg.Value;
                    break;
                case EmoteMeanings.EMOTE_SoClose:
                    __result = Plugin.SoCloseMsg.Value;
                    break;
                case EmoteMeanings.EMOTE_NotThatOne:
                    __result = Plugin.NotThatOneMsg.Value;
                    break;
                case EmoteMeanings.EMOTE_Impossible:
                    __result = Plugin.ImpossibleMsg.Value;
                    break;
                //case EmoteMeanings.EMOTE_Higher:
                //    __result = Plugin.HigherMsg.Value;
                //    break;
                //case EmoteMeanings.EMOTE_Lower:
                //    __result = Plugin.LowerMsg.Value;
                    break;
                case EmoteMeanings.EMOTE_GlueHere:
                    __result = Plugin.GlueHereMsg.Value;
                    break;
                case EmoteMeanings.EMOTE_Bomb:
                    __result = Plugin.BombMsg.Value;
                    break;
                case EmoteMeanings.EMOTE_Rematch:
                    __result = Plugin.RematchMsg.Value;
                    break;
                case EmoteMeanings.EMOTE_WaitingForAFriend:
                    __result = Plugin.WaitingForAFriendMsg.Value;
                    break;
                case EmoteMeanings.EMOTE_BeRightBack:
                    __result = Plugin.BeRightBackMsg.Value;
                    break;
                case EmoteMeanings.EMOTE_Okay:
                    __result = Plugin.OkayMsg.Value;
                    break;
                case EmoteMeanings.EMOTE_Hello:
                    __result = Plugin.HelloMsg.Value;
                    break;
                case EmoteMeanings.EMOTE_Goodbye:
                    __result = Plugin.GoodbyeMsg.Value;
                    break;
                case EmoteMeanings.EMOTE_GoodGame:
                    __result = Plugin.GoodGameMsg.Value;
                    break;
                case EmoteMeanings.EMOTE_WellPlayed:
                    __result = Plugin.WellPlayedMsg.Value;
                    break;
                case EmoteMeanings.EMOTE_NotThere:
                    __result = Plugin.NotThereMsg.Value;
                    break;
                case EmoteMeanings.EMOTE_MoreTraps:
                    __result = Plugin.MoreTrapsMsg.Value;
                    break;
                case EmoteMeanings.EMOTE_NiceOutfit:
                    __result = Plugin.NiceOutfitMsg.Value;
                    break;
                //case EmoteMeanings.EMOTE_Yes:
                //    __result = Plugin.YesMsg.Value;
                //    break;
                //case EmoteMeanings.EMOTE_UhOh:
                //    __result = Plugin.UhOhMsg.Value;
                //    break;
                case EmoteMeanings.EMOTE_Hahaha:
                    __result = Plugin.HahahaMsg.Value;
                    break;
                //case EmoteMeanings.EMOTE_Ouch:
                //    __result = Plugin.OuchMsg.Value;
                //    break;
                case EmoteMeanings.EMOTE_Nooo:
                    __result = Plugin.NooooMsg.Value;
                    break;
                //case EmoteMeanings.EMOTE_Amazing:
                //    __result = Plugin.AmazingMsg.Value;
                //    break;
                case EmoteMeanings.EMOTE_Wow:
                    __result = Plugin.WowMsg.Value;
                    break;
                //case EmoteMeanings.EMOTE_GreatRun:
                //    __result = Plugin.GreatRunMsg.Value;
                //    break;
                case EmoteMeanings.EMOTE_Thanks:
                    __result = Plugin.ThanksMsg.Value;
                    break;
                case EmoteMeanings.EMOTE_Whoops:
                    __result = Plugin.WhoopsMsg.Value;
                    break;
                case EmoteMeanings.EMOTE_Sorry:
                    __result = Plugin.SorryMsg.Value;
                    break;
                case EmoteMeanings.EMOTE_NoProblem:
                    __result = Plugin.NoProblemMsg.Value;
                    break;
                default:
                    break;
            }
        }
    }
}
