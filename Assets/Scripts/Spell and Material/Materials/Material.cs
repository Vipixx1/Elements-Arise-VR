using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Magic.Materials
{
    public class Material : MonoBehaviour
    {
        protected bool conductivite;
        protected float mass_multiplier;
        string[] baseS = new string[0];
        
        public virtual void OnFire(ObjectData data, float[] args = null)
        {

        }
        public virtual void OnWater(ObjectData data, float[] args = null)
        {

        }
        public virtual void OnEarth(ObjectData data, float[] args = null)
        {

        }
        public virtual void OnWind(ObjectData data, float[] args = null)
        {

        }
        public virtual void OnSteam(ObjectData data , float[] args = null)
        {

        }
        public virtual void OnVolcano(ObjectData data , float[] args = null)
        {

        }
        public virtual void OnThunder(ObjectData data , float[] args = null)
        {

        }
        public virtual void OnPlant(ObjectData data , float[] args = null)
        {

        }
        public virtual void OnIce(ObjectData data , float[] args = null)
        {

        }

        public virtual void OnSand(ObjectData data, float[] args = null)
        {

        }

        public virtual void Burning(ObjectData data)
        {
            
        }

        public virtual bool Frozen(ObjectData data)
        {
            return false;
        }
        
    }
}
