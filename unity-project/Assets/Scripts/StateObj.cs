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
using System.Collections;

	public class StateObj
	{
		
		private int score;
		private int collected;
		private int watercount;
		private int methanecount;
		private int beamenergy;

		private ArrayList collected_collectables;


		public StateObj ()
		{
			collected_collectables = new ArrayList ();
		}

		public void SaveState(ArrayList collectables, int score, int collected, int watercount, int methanecount, int beamEnergy){
		    
			//Adds newly collected molecules
			for (int i=0; i<collectables.Count; i++) {
				this.collected_collectables.Add(collectables[i]);
			}

			this.score = score;
			this.collected = collected;
			this.watercount = watercount;
			this.methanecount = methanecount;
			this.beamenergy = beamEnergy;
		}

		public ArrayList getCollectables(){
			return this.collected_collectables;
		}
		public int getCollected(){
			return this.collected;
		}
		public int getScore(){
			return this.score;
		}
		public int getWatercount(){
			return this.watercount;
		}
		public int getMethanecount(){
			return this.methanecount;
		}
		public int getBeamenergy(){
			return this.beamenergy;
		}

	}


