using Photon.Pun;
using UnityEngine;
using UnityEngine.Playables;

public class ZaidAnimPlay : MonoBehaviourPun
{
    [SerializeField] PlayableDirector playableDirector;

    public void PlayZaidAnimation() {
        photonView.RPC("RPC_PlayZaidAnimation", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RPC_PlayZaidAnimation()
    {
        gameObject.SetActive(true);
        playableDirector.Play();
    }
}
