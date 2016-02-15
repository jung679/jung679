﻿using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;

namespace Mario_sGangplank
{
    internal class EventsManager
    {
        public static bool CanPreAttack { get; private set; }
        public static bool CanPostAttack { get; private set; }

        public static void InitEventManagers()
        {
            Orbwalker.OnPreAttack += Orbwalker_OnPreAttack;
            Orbwalker.OnPostAttack += Orbwalker_OnPostAttack;
            Drawing.OnDraw += Drawing_OnDraw;
            Obj_AI_Base.OnBuffGain += Obj_AI_Base_OnBuffGain;
        }

        private static void Obj_AI_Base_OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            if (!sender.IsMe || !Spells.W.IsReady())return;


            if (args.Buff.IsStunOrSuppressed && Helpers.GetCheckBoxValue(Helpers.MenuTypes.Settings, "wBuffStun"))
            {
                Spells.W.Cast();
            }

            if (args.Buff.IsSlow && Helpers.GetCheckBoxValue(Helpers.MenuTypes.Settings, "wBuffSlow"))
            {
                Spells.W.Cast();
            }

            if (args.Buff.IsBlind && Helpers.GetCheckBoxValue(Helpers.MenuTypes.Settings, "wBuffBlind"))
            {
                Spells.W.Cast();
            }

            if (args.Buff.IsSuppression && Helpers.GetCheckBoxValue(Helpers.MenuTypes.Settings, "wBuffSupression"))
            {
                Spells.W.Cast();
            }

            if (args.Buff.IsRoot && Helpers.GetCheckBoxValue(Helpers.MenuTypes.Settings, "wBuffSnare"))
            {
                Spells.W.Cast();
            }

        }

        //Dont need to change
        private static void Orbwalker_OnPreAttack(AttackableUnit target, Orbwalker.PreAttackArgs args)
        {
            CanPostAttack = false;
            CanPreAttack = true;
            Core.DelayAction(() => CanPreAttack = false, 60);
        }

        private static void Orbwalker_OnPostAttack(AttackableUnit target, System.EventArgs args)
        {
            CanPreAttack = false;
            CanPostAttack = true;
            Core.DelayAction(() => CanPostAttack = false, 60);
        }

        private static void Drawing_OnDraw(System.EventArgs args)
        {
            foreach (var barrel in Barrrels.GetBarrels())
            {
                Circle.Draw(barrel.Health <= 1 ? SharpDX.Color.DarkRed : SharpDX.Color.YellowGreen, 350, 3f, barrel);
            }

            var ready = Helpers.GetCheckBoxValue(Helpers.MenuTypes.Drawings, "readyDraw");

            if (Helpers.GetCheckBoxValue(Helpers.MenuTypes.Drawings, "qDraw") && (ready ? Spells.Q.IsReady() : Spells.Q.IsLearned))
            {
                Circle.Draw(SharpDX.Color.Red, Spells.Q.Range, 1f, Player.Instance);
            }

            if (Helpers.GetCheckBoxValue(Helpers.MenuTypes.Drawings, "wDraw") && (ready ? Spells.W.IsReady() : Spells.W.IsLearned))
            {
                Circle.Draw(SharpDX.Color.Blue, Spells.W.Range, 1f, Player.Instance);
            }

            if (Helpers.GetCheckBoxValue(Helpers.MenuTypes.Drawings, "eDraw") && (ready ? Spells.E.IsReady() : Spells.E.IsLearned))
            {
                Circle.Draw(SharpDX.Color.Purple, Spells.E.Range, 1f, Player.Instance);
            }
        }
    }
}
