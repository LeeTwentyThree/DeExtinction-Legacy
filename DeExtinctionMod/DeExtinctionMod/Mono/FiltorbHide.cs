using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.Mono
{
    public class FiltorbHide : ReactToPredatorAction, IOnTakeDamage, ISecondaryTooltip
    {
        private Animator animator;
        private Pickupable pickupable;
        private DamageModifier closedDamageModifier;
        private VFXSurface surface;

        private bool closed;
        public bool Closed
        {
            set
            {
                if(closed == value)
                {
                    return;
                }
                closed = value;
                if (closed)
                {
                    closedDamageModifier.enabled = true;
                    pickupable.isPickupable = false;
                    animator.SetBool("open", false);
                    surface.surfaceType = VFXSurfaceTypes.metal;
                }
                else
                {
                    closedDamageModifier.enabled = false;
                    pickupable.isPickupable = true;
                    animator.SetBool("open", true);
                    surface.surfaceType = VFXSurfaceTypes.organic;
                }
            }
        }

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
            pickupable = GetComponent<Pickupable>();
            closedDamageModifier = gameObject.EnsureComponent<DamageModifier>();
            closedDamageModifier.multiplier = 0.25f;
            closedDamageModifier.damageType = DamageType.Normal;
            surface = GetComponent<VFXSurface>();
        }
        public override void StartPerform(Creature creature)
        {
            Closed = true;
        }

        public override void StopPerform(Creature creature)
        {
            Closed = false;
        }

        private void OnKill()
        {
            Closed = false;
        }

        public void OnTakeDamage(DamageInfo damageInfo)
        {
            performingAction = true;
            timeStopAction = Time.time + 2f;
        }

        public string GetSecondaryTooltip()
        {
            if (closed)
            {
                return "Closed";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
