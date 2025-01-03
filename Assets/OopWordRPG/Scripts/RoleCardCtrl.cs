using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace OopWordRPG {
    public class RoleCardCtrl : MonoBehaviour {

        Role role = null;
        [SerializeField] Image roleImage;
        [SerializeField] Text roleName;
        [SerializeField] CanvasGroup cardCanvasGroup;

        [SerializeField] Image hpImage;
        [SerializeField] Image mpImage;

        [SerializeField] Image hitMask;
        Sequence onHitSequence = null;

        public void InitCard(Role p_role) {
            role = p_role;
            roleImage.sprite = p_role.RoleSprite;
            roleName.text = p_role.Name;
            hpImage.fillAmount = p_role.Hp / (float)p_role.MaxHp;
            mpImage.fillAmount = p_role.Mp / (float)p_role.MaxMp;

            p_role.OnAttack = ShowAttack;
            p_role.OnTakeDamage = ShowTakeDamage;
            p_role.OnDead = ShowDead;
            if(p_role.IsPlayer) {
                p_role.OnLevelUp = ShowLevelUp;
                p_role.OnRecover = ShowRecover;
            }
        }

        public void ShowCard() {
            cardCanvasGroup.DOFade(1, 0.2f);
        }

        private void ShowAttack() {
            Vector3 _target = Vector3.zero;
            if(role.IsPlayer) {
                _target = Vector3.right;
            } else {
                _target = Vector3.left;
            }
            transform.DOPunchPosition(_target * 5f, 0.2f);
        }
        
        private void ShowTakeDamage() {
            if(onHitSequence == null) {
                onHitSequence = DOTween.Sequence();
            }
            onHitSequence.Append(hitMask.DOFade(0.8f, 0.1f))
                .Append(DOVirtual.DelayedCall(0.1f, () => { hitMask.DOFade(0f, 0.2f); }))
                .SetAutoKill(false);

            float remainHpRate = role.Hp / (float)role.MaxHp;
            hpImage.DOFillAmount(remainHpRate, 0.3f);
        }

        private void ShowDead() {
            if(role.IsPlayer) {
                roleImage.DOColor(Color.gray, 0.1f);
            } else {
                cardCanvasGroup.DOFade(0, 0.2f);
            }
        }

        private void ShowLevelUp() {
            hpImage.DOFillAmount(1, 0.3f);
        }

        private void ShowRecover() {
            roleImage.DOColor(Color.white, 0.1f);
            hpImage.DOFillAmount(1, 0.3f);
        }
    }
}