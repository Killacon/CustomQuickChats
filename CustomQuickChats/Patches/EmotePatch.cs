using HarmonyLib; 
using System.Collections.Generic;
using BepInEx.Configuration;

// TODO
// add controller support
// figure out UI support (only change popup, not quickchats from others)
// test more


namespace CustomQuickChats.Patches
{
    // Handles the replacement of the message
    [HarmonyPatch(typeof(EmoteSystem), "ReceiveEvent")]
    class ReceiveEventPatch
    {
        // allows use of EmoteSystem.emoteState
        static readonly AccessTools.FieldRef<EmoteSystem, emoteState> emoteStateRef =
            AccessTools.FieldRefAccess<EmoteSystem, emoteState>("emoteState");
        // alows use of EmoteSystem.possibleContent
        static readonly AccessTools.FieldRef<EmoteSystem, EmoteMeanings[]> possibleContentRef =
            AccessTools.FieldRefAccess<EmoteSystem, EmoteMeanings[]>("possibleContent");
        //static readonly AccessTools.FieldRef<EmoteSystem, bool> triggerNeedsReleasingRef =
        //    AccessTools.FieldRefAccess<EmoteSystem, bool>("TriggerNeedsReleasing");                       triggerNeedsReleasingRef(__instance) = true;

        // prefix to happen before original method
        static bool Prefix(EmoteSystem __instance, InputEvent e)
        {
            // Allows original method to run if not in message selection state
            if (emoteStateRef(__instance) != emoteState.SelectingMessage)
            {
                //Debug.Log($"[CustomQuickChats] emoteState is: {emoteStateRef(__instance)}");
                return true;
            }
            if (e.Changed && e.Valueb)
            {
                int index = -1;
                //Debug.Log($"[CustomQuickChats] Selected emote direction: {e.Key}, index: {index}");
                switch (e.Key)
                {
                    case InputEvent.InputKey.OrthoUp2:
                        index = (int)emoteDirections.UP;
                        break;
                    case InputEvent.InputKey.OrthoRight2:
                        index = (int)emoteDirections.RIGHT;
                        break;
                    case InputEvent.InputKey.OrthoDown2:
                        index = (int)emoteDirections.DOWN;
                        break;
                    case InputEvent.InputKey.OrthoLeft2:
                        index = (int)emoteDirections.LEFT;
                        break;
                    default:
                        return true;
                }

                EmoteMeanings[] possibleContent = possibleContentRef(__instance);
                if (index < 0 || index >= possibleContent.Length)
                    return true;

                EmoteMeanings requestedEmote = possibleContent[index];
                string originalMessage = EmoteSystem.EmoteConverter(requestedEmote);
                string customText = MessageMapper.GetCustomMessage(originalMessage);
                //Debug.Log($"[CustomQuickChats] Original message: \"{originalMessage}\" | Custom message: \"{customText}\"");


                // Send custom message
                //GameState.ChatSystem.NewChatMessage(customText, requestedEmote, LobbyPlayer.networkNumber);

                //if (Plugin.CustomNetID.Value == 0)
                //{
                    var netId = (int)__instance.LobbyPlayer.networkNumber;
                    __instance.animator.SetInteger("Direction", (int)index);
                    __instance.animator.SetTrigger("Confirm");
                    GameState.ChatSystem.NewChatMessage(customText, EmoteMeanings.CHAT_Text, netId);
                    emoteStateRef(__instance) = emoteState.EmoteSent;
                //}
                //else
                //{
                //    __instance.animator.SetInteger("Direction", (int)index);
                //    __instance.animator.SetTrigger("Confirm");
                //    GameState.ChatSystem.NewChatMessage(customText, EmoteMeanings.CHAT_Text, Plugin.CustomNetID.Value);
                //    emoteStateRef(__instance) = emoteState.EmoteSent;
                //}
            }
            //emoteStateRef(__instance) = emoteState.EmoteSent;
            return false; // Skip original ReceiveEvent

        }
    }
    // handles mapping of default messages to custom messages
    static class MessageMapper
    {
        private static readonly Dictionary<string, ConfigEntry<string>> messageMap = new Dictionary<string, ConfigEntry<string>>()
        {
            { "Over here!", Plugin.OverHereMsg },
            { "Too easy.", Plugin.TooEasyMsg },
            { "Thinking...", Plugin.ThinkingMsg },
            { "Impossible!", Plugin.ImpossibleMsg },
            { "Rematch?", Plugin.RematchMsg },
            { "Waiting for a friend.", Plugin.WaitingForAFriendMsg },
            { "Be right back.", Plugin.BeRightBackMsg },
            { "OMG!", Plugin.OMGMsg },
            { "Wow!", Plugin.WowMsg },
            { "Hahaha!", Plugin.HahahaMsg },
            { "Noooo!", Plugin.NooooMsg },
            { "Okay!", Plugin.OkayMsg },
            { "Not that one!", Plugin.NotThatOneMsg },
            { "Hurry up!", Plugin.HurryUpMsg },
            { "Whoops...", Plugin.WhoopsMsg },
            { "Sorry!", Plugin.SorryMsg },
            { "Hello!", Plugin.HelloMsg },
            { "Goodbye", Plugin.GoodbyeMsg },
            { "Nice Outfit!", Plugin.NiceOutfitMsg },
            { "No problem!", Plugin.NoProblemMsg },
            { "Thanks!", Plugin.ThanksMsg },
            { "Well done!", Plugin.WellDoneMsg },
            { "Good idea!", Plugin.GoodIdeaMsg },
            { "Good game!", Plugin.GoodGameMsg },
            { "Well played!", Plugin.WellPlayedMsg },
            { "So close!", Plugin.SoCloseMsg },
            { "Not there!", Plugin.NotThereMsg },
            { "More traps!", Plugin.MoreTrapsMsg },
            { "Glue here!", Plugin.GlueHereMsg },
            { "Bomb!", Plugin.BombMsg }
        };

        // handles getting the custom message
        public static string GetCustomMessage(string defaultMessage)
        {
            if (!Plugin.Enabled.Value)
                return defaultMessage;

            if (messageMap.TryGetValue(defaultMessage, out var configEntry))
            {
                return string.IsNullOrEmpty(configEntry.Value) ? defaultMessage : configEntry.Value;
            }

            return defaultMessage;
        }
    }

    // Handles the replacement of UI text
    // Issue: replaces other's quick chats with your custom chats
    // Figure out how to only replace UI?

    //[HarmonyPatch(typeof(EmoteSystem), "EmoteConverter")]
    //internal class EmotePatch
    //{
    //    static void Postfix(ref string __result, EmoteMeanings emote)
    //    {
    //        if (!Plugin.Enabled.Value) return;

    //        switch (emote)
    //        {
    //            case EmoteMeanings.EMOTE_OverHere:
    //                __result = " " + Plugin.OverHereMsg.Value;
    //                break;
    //            case EmoteMeanings.EMOTE_HurryUp:
    //                __result = " " + Plugin.HurryUpMsg.Value;
    //                break;
    //            //case EmoteMeanings.EMOTE_No:
    //            //    __result = Plugin.NoMsg.Value;
    //            //    break;
    //            case EmoteMeanings.EMOTE_TooEasy:
    //                __result = " " + Plugin.TooEasyMsg.Value;
    //                break;
    //            case EmoteMeanings.EMOTE_Thinking:
    //                __result = " " + Plugin.ThinkingMsg.Value;
    //                break;
    //            case EmoteMeanings.EMOTE_OMG:
    //                __result = " " + Plugin.OMGMsg.Value;
    //                break;
    //            //case EmoteMeanings.EMOTE_Yeah:
    //            //    __result = Plugin.YeahMsg.Value;
    //            //    break;
    //            case EmoteMeanings.EMOTE_GoodIdea:
    //                __result = " " + Plugin.GoodIdeaMsg.Value;
    //                break;
    //            case EmoteMeanings.EMOTE_WellDone:
    //                __result = " " + Plugin.WellDoneMsg.Value;
    //                break;
    //            case EmoteMeanings.EMOTE_SoClose:
    //                __result = " " + Plugin.SoCloseMsg.Value;
    //                break;
    //            case EmoteMeanings.EMOTE_NotThatOne:
    //                __result = " " + Plugin.NotThatOneMsg.Value;
    //                break;
    //            case EmoteMeanings.EMOTE_Impossible:
    //                __result = " " + Plugin.ImpossibleMsg.Value;
    //                break;
    //                //case EmoteMeanings.EMOTE_Higher:
    //                //    __result = Plugin.HigherMsg.Value;
    //                //    break;
    //                //case EmoteMeanings.EMOTE_Lower:
    //                //    __result = Plugin.LowerMsg.Value;
    //                break;
    //            case EmoteMeanings.EMOTE_GlueHere:
    //                __result = " " + Plugin.GlueHereMsg.Value;
    //                break;
    //            case EmoteMeanings.EMOTE_Bomb:
    //                __result = " " + Plugin.BombMsg.Value;
    //                break;
    //            case EmoteMeanings.EMOTE_Rematch:
    //                __result = " " + Plugin.RematchMsg.Value;
    //                break;
    //            case EmoteMeanings.EMOTE_WaitingForAFriend:
    //                __result = " " + Plugin.WaitingForAFriendMsg.Value;
    //                break;
    //            case EmoteMeanings.EMOTE_BeRightBack:
    //                __result = " " + Plugin.BeRightBackMsg.Value;
    //                break;
    //            case EmoteMeanings.EMOTE_Okay:
    //                __result = " " + Plugin.OkayMsg.Value;
    //                break;
    //            case EmoteMeanings.EMOTE_Hello:
    //                __result = " " + Plugin.HelloMsg.Value;
    //                break;
    //            case EmoteMeanings.EMOTE_Goodbye:
    //                __result = " " + Plugin.GoodbyeMsg.Value;
    //                break;
    //            case EmoteMeanings.EMOTE_GoodGame:
    //                __result = " " + Plugin.GoodGameMsg.Value;
    //                break;
    //            case EmoteMeanings.EMOTE_WellPlayed:
    //                __result = " " + Plugin.WellPlayedMsg.Value;
    //                break;
    //            case EmoteMeanings.EMOTE_NotThere:
    //                __result = " " + Plugin.NotThereMsg.Value;
    //                break;
    //            case EmoteMeanings.EMOTE_MoreTraps:
    //                __result = " " + Plugin.MoreTrapsMsg.Value;
    //                break;
    //            case EmoteMeanings.EMOTE_NiceOutfit:
    //                __result = " " + Plugin.NiceOutfitMsg.Value;
    //                break;
    //            //case EmoteMeanings.EMOTE_Yes:
    //            //    __result = Plugin.YesMsg.Value;
    //            //    break;
    //            //case EmoteMeanings.EMOTE_UhOh:
    //            //    __result = Plugin.UhOhMsg.Value;
    //            //    break;
    //            case EmoteMeanings.EMOTE_Hahaha:
    //                __result = " " + Plugin.HahahaMsg.Value;
    //                break;
    //            //case EmoteMeanings.EMOTE_Ouch:
    //            //    __result = Plugin.OuchMsg.Value;
    //            //    break;
    //            case EmoteMeanings.EMOTE_Nooo:
    //                __result = " " + Plugin.NooooMsg.Value;
    //                break;
    //            //case EmoteMeanings.EMOTE_Amazing:
    //            //    __result = Plugin.AmazingMsg.Value;
    //            //    break;
    //            case EmoteMeanings.EMOTE_Wow:
    //                __result = " " + Plugin.WowMsg.Value;
    //                break;
    //            //case EmoteMeanings.EMOTE_GreatRun:
    //            //    __result = Plugin.GreatRunMsg.Value;
    //            //    break;
    //            case EmoteMeanings.EMOTE_Thanks:
    //                __result = " " + Plugin.ThanksMsg.Value;
    //                break;
    //            case EmoteMeanings.EMOTE_Whoops:
    //                __result = " " + Plugin.WhoopsMsg.Value;
    //                break;
    //            case EmoteMeanings.EMOTE_Sorry:
    //                __result = " " + Plugin.SorryMsg.Value;
    //                break;
    //            case EmoteMeanings.EMOTE_NoProblem:
    //                __result = " " + Plugin.NoProblemMsg.Value;
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //}
}
