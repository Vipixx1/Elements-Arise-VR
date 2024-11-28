using UnityEngine;

namespace Magic.Materials
{
    public class Material : MonoBehaviour
    {
        protected bool conductivite;
        protected float mass_multiplier;

        public virtual void OnFire(ObjectData data)
        {

        }
        public virtual void OnWater(ObjectData data)
        {

        }
        public virtual void OnEarth(ObjectData data)
        {

        }
        public virtual void OnWind(ObjectData data)
        {

        }
        public virtual void OnSteam(ObjectData data)
        {

        }
        public virtual void OnVolcano(ObjectData data)
        {

        }
        public virtual void OnThunder(ObjectData data)
        {

        }
        public virtual void OnPlant(ObjectData data)
        {

        }
        public virtual void OnIce(ObjectData data)
        {

        }

        public virtual void OnSand(ObjectData data)
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
