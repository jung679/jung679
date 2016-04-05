﻿using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

namespace Mario_s_Lib
{
    public static class EntitiesManager
    {
        public static Obj_AI_Base GetJungleMinion(this Spell.SpellBase spell)
        {
            return
                EntityManager.MinionsAndMonsters.GetJungleMonsters()
                    .OrderByDescending(m => m.Health)
                    .FirstOrDefault(m => m.IsValidTarget(spell.Range));
        }

        public static int CountEnemyLaneMinions(this Obj_AI_Base target, float range = 100)
        {
            return EntityManager.MinionsAndMonsters.GetLaneMinions().Count(m => m.Distance(target) <= range);
        }

        public static int CountEnemyJungleMinions(this Obj_AI_Base target, float range = 100)
        {
            return EntityManager.MinionsAndMonsters.GetJungleMonsters().Count(m => m.Distance(target) <= range);
        }
    }
}