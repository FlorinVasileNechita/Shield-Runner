using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[RequireComponent(typeof(Ferr2D_Path), typeof(Ferr2DT_PathTerrain))]
public class TerrainRebuild : MonoBehaviour {
	string floatListString = "5.25 4.270467 3.437455 3.167483 2.550271 1.905826 0.3581473 0.9905986 0.6209137 0.01826411 -0.6277528";
	string floatListStringTwo = "5.25 4.270467 3.437455 3.167483 2.550271 1.905826 0.3581473 0.9905986 0.6209137 0.01826411 -0.6277528";
	

		public GameObject   centerAround;
		public int          vertCount = 10;
		public float        vertSpacing = 1;
		public float        minHeight = 2;
		public float        maxHeight = 10;
		public float        heightVariance = 4;
		public float        cliffChance = 0.1f;
		
		Ferr2DT_PathTerrain terrain;
		List<float>         terrainHeights   = new List<float>();
		List<float>         terrainSecondaryHeights   = new List<float>();
		List<float>         terrainHeightsRecord = new List<float>();
		List<float>         terrainSecondaryHeightsRecord = new List<float>();
		int                 currentOffset;
		
		void Start  () {
			terrain = GetComponent<Ferr2DT_PathTerrain>();
			
//			terrainHeights          = stringToFloatList(floatListString);
//			terrainSecondaryHeights = stringToFloatList(floatListStringTwo);
//			
			for (int i = 0; i < vertCount; i++) {
				NewRight();
			}
			
			RebuildTerrain();
		}
		
		void Update () {
			UpdateTerrain();
		}
		
		void  UpdateTerrain () {
			bool updated = false;
			
			// generate points to the right if we need 'em
			while (centerAround.transform.position.x > ((currentOffset+1) * vertSpacing)) {
				currentOffset += 1;
				NewRight();
				terrainHeights         .RemoveAt(0);
				terrainSecondaryHeights.RemoveAt(0);
				updated = true;
			}
			
			// generate points to the left, if we need 'em
			while (centerAround.transform.position.x < ((currentOffset-1) * vertSpacing)) {
				currentOffset -= 1;
				NewLeft();
				terrainHeights         .RemoveAt(terrainHeights         .Count - 1);
				terrainSecondaryHeights.RemoveAt(terrainSecondaryHeights.Count - 1);
				updated = true;
			}
			
			// rebuild the terrain if we added any points
			if (updated) {
				RebuildTerrain();
			}
		}
		
		void  RebuildTerrain() {
			float startX = (currentOffset * vertSpacing) - ((vertCount / 2) * vertSpacing);
			terrain.ClearPoints();
			for (int i = 0; i < terrainHeights.Count; i++) {
				Vector2 pos = new Vector2(startX + i * vertSpacing, terrainHeights[i]);
				terrain.AddPoint(pos);
				if (terrainSecondaryHeights[i] != terrainHeights[i]) {
					pos = new Vector2(startX + i * vertSpacing, terrainSecondaryHeights[i]);
					terrain.AddPoint(pos);
				}
			}
			
			terrain.RecreatePath    (false);
			terrain.RecreateCollider();
		}
		
		void  NewRight      () {
			float right  = GetRight();
			float right2 = Random.value < cliffChance ? GetRight() : right;
			
			if (Mathf.Abs(right - right2) < 3) {
				right = right2;
			}
			
			terrainHeights         .Add(right );
			terrainSecondaryHeights.Add(right2);
			terrainHeightsRecord.Add(right );
			terrainSecondaryHeightsRecord.Add(right2);
		}
		
		void  NewLeft       () {
			float left = GetLeft();
			float left2 = Random.value < cliffChance ? GetLeft() : left;
			
			if (Mathf.Abs(left - left2) < 3) {
				left = left2;
			}
			
			terrainHeights         .Insert(0, left );
			terrainSecondaryHeights.Insert(0, left2);
		}
		
		float GetRight      () {
			if (terrainHeights.Count <= 0) return minHeight + (maxHeight - minHeight) / 2;
			return Mathf.Clamp(terrainSecondaryHeights[terrainHeights.Count - 1] + (-1 + Random.value * 2) * heightVariance, minHeight, maxHeight);
		}
		
		float GetLeft       () {
			if (terrainHeights.Count <= 0) return minHeight + (maxHeight - minHeight) / 2;
			return Mathf.Clamp(terrainSecondaryHeights[0                       ] + (-1 + Random.value * 2) * heightVariance, minHeight, maxHeight);
		}
		
		public string floatListToString(List<float> floatList) {
			StringBuilder builder = new StringBuilder();
			foreach (float f in floatList)
			{
				// Append each int to the StringBuilder overload.
				builder.Append(f).Append(" ");
			}
			string result = builder.ToString();	
			return result;
		}
		
		public List<float> stringToFloatList(string s) {
			return s.Split(' ').Select(i => float.Parse(i)).ToList();
		}	
}
