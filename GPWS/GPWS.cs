using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GPWS
{
    class GPWS : MonoBehaviour
    {
        
        private void Start()
        {
            this.flightInfo = VTOLAPI.GetPlayersVehicleGameObject().GetComponentInChildren<FlightInfo>(true);
            this.gearAnimator = VTOLAPI.GetPlayersVehicleGameObject().GetComponentInChildren<GearAnimator>(true);
            this.flightWarnings = VTOLAPI.GetPlayersVehicleGameObject().GetComponentInChildren<FlightWarnings>(true);
        }
        private void Update()
        {

    

            if (gearAnimator.state == GearAnimator.GearStates.Extended)
            {

                checkHeight();
            }

        }

        //Checks the radar alt and plays voice lines everytime you pass a height idx
        private void checkHeight()
        {

           
            if (this.flightInfo.sweptRadarAltitude * 3.28084f <= heights[heightIdx] && heightIdx < heights.Length - 1)
            {
                Debug.Log("Going to increment");

                this.flightWarnings.audioSource.volume = 1;
                if (this.flightWarnings.audioSource.isPlaying)
                {
                    this.flightWarnings.audioSource.Stop();
                }

                this.flightWarnings.audioSource.PlayOneShot(Main.GetClip(heights[heightIdx]), 1);
               
                

                FlightLogger.Log($"GPWS increasing index({heightIdx + 1}): " + heights[heightIdx].ToString());
                heightIdx++;
                // audioSource.play(blah);
                heightIdx = Mathf.Clamp(heightIdx, 0, heights.Length - 1);
                Debug.Log($"Current height idx {heightIdx}");
            }
            else if (heightIdx > 0 && this.flightInfo.sweptRadarAltitude * 3.28084f > heights[heightIdx - 1]){

                Debug.Log("Going to decrement");
                FlightLogger.Log($"GPWS reducing index({heightIdx - 1}): " + heights[heightIdx].ToString());
                heightIdx--;
                heightIdx = Mathf.Clamp(heightIdx, 0, heights.Length - 1); ;
            }
        }

        private void playAudio(int height)
        {
            Main.GetClip(height);
        }

        public static int[] GetHeights()
        {
            return heights;
        }


   
        
        public static int[] heights = new int[] { 2500, 1000, 500, 400, 300, 200, 100, 70, 60, 40, 30, 20, 10 };

        private int heightIdx = 0;
        private FlightInfo flightInfo;
        private GearAnimator gearAnimator;
        private FlightWarnings flightWarnings;

    }
}
