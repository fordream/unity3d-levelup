// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using Soomla;
using Soomla.Levelup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

namespace Soomla.Store.Example
{
	public class WorldGenerator
	{
		public WorldGenerator ()
		{

		}

		//create a crazy world...
		public static World GenerateCustomWorld(){
			World mainWorld = new World("main_world");

			Score s = new Score("numberScore");
			World machineA = new World("machine_a");
			World machineB = new World("machine_b");

			// machine A
			machineA.BatchAddLevelsWithTemplates(2, null, s, null);

			// machine B

			Gate machineALevel1Complete = new WorldCompletionGate("level1_complete", machineA.GetInnerWorldAt(0).ID);

			machineB.BatchAddLevelsWithTemplates(20, machineALevel1Complete, s, null);

			Mission mission1 = new WorldCompletionMission("level2_complete", "Level 2 Completed Mission!", machineA.GetInnerWorldAt(1).ID);
			Mission mission2 = new RecordMission("level1_record_mission", "Level 1 Record Mission!", machineA.GetInnerWorldAt(0).GetSingleScore().ID, 20.0);
			Mission allMissions = new Challenge("main_challange", "MAIN CHALLENGE", new List<Mission>() { mission1, mission2 });

			machineB.GetInnerWorldAt(5).AddMission(allMissions);

			mainWorld.AddInnerWorld(machineA);
			mainWorld.AddInnerWorld(machineB);

			return mainWorld;
		}

		public static void Play(World world){
			World w1 = SoomlaLevelUp.GetWorld ("machine_a");

			SoomlaUtils.LogDebug(TAG, "Working with world: " + w1.ID);


			List<Level> levels = new List<Level> ();
			levels.Add ((Level)w1.GetInnerWorldAt (0));
			levels.Add ((Level)w1.GetInnerWorldAt (1));

			for(int i = 0 ; i < levels.Count ; i++){
				Level currentLevel = levels[i];

//				Reward currentReward = new BadgeReward("badge_bronzeMedal_" + i, "Bronze Medal_" + i);
//
//				string scoreId = "scoreId_" + i;
//				string recoreId = "record_gate_" + i;
//				RangeScore rangeScore = new RangeScore(scoreId, "RangeScore", true, new RangeScore.SRange(0, 100));
//				RecordGate recordGate = new RecordGate(recoreId, scoreId, 100);
//
//				currentLevel.AssignReward(currentReward);
//				currentLevel.Scores.Add(scoreId, rangeScore);

				currentLevel.Start();

				Thread.Sleep (UnityEngine.Random.Range(2000, 3000) * i);

				currentLevel.SetSingleScoreValue(UnityEngine.Random.Range(20, 30));

//				currentLevel.Pause();

				currentLevel.End(true);
			}
		}

		private static string TAG = "WorldGenerator";
	}

}

