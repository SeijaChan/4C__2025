using System.Collections;
using UnityEngine;

namespace Sound
{
    public class BellController : MonoBehaviour
    {
        public float swingAngle = 30f;
        public float swingDuration = 0.5f;

        public void Swing()
        {
            if()
        }

        private IEnumerator DoSwing()
        {
            Quaternion start = transform.rotation;
            Quaternion end = Quaternion.Euler(0,0,swingAngle);
            float t = 0;
            while (t < swingDuration)
            {
                transform.rotation = Quaternion.Slerp(start, end, t / swingDuration);
                t += Time.deltaTime;
                yield return null;
                while (t > 0)
                {
                    
                }
            }
        }
    }
}